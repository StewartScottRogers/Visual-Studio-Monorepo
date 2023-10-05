using Microsoft.Extensions.Hosting;
using RabbitMq.SharedProject.Messaging;
using RabbitMq.SharedProject.Messaging.Extensions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMq.Listener.Abstract
{
    public abstract class AbstractMessageListener<TModel> : BackgroundService where TModel : class
    {
        private readonly IRabbitMqConfiguration RabbitMqConfiguration;

        protected void SetSubject(string verb, string noun)
        {
            if (verb is null)
                throw new ArgumentNullException(nameof(verb));

            if (noun is null)
                throw new ArgumentNullException(nameof(noun));

            subject = $"{verb.Trim().ToLower()}-{noun.Trim().ToLower()}";
        }
        private string subject;

        private ConnectionFactory connectionFactory;
        private ConnectionFactory ConnectionFactory
        {
            get
            {
                if (connectionFactory is null)
                    try
                    {
                        connectionFactory = new()
                        {
                            HostName = RabbitMqConfiguration.Hostname,
                            Port = int.Parse(RabbitMqConfiguration.Port),
                            UserName = RabbitMqConfiguration.UserName,
                            Password = RabbitMqConfiguration.Password
                        };
                    }
                    catch (Exception exception)
                    {
                        throw new Exception($"failed new of {nameof(connectionFactory)}.", exception);
                    }

                return connectionFactory;
            }
        }

        private IConnection Connection
        {
            get
            {
                try
                {
                    return ConnectionFactory.CreateConnection();
                }
                catch (Exception exception)
                {
                    throw new Exception($"failed {nameof(ConnectionFactory.CreateConnection)}.", exception);
                }
            }
        }

        private IModel Channel { get; }

        protected AbstractMessageListener(IRabbitMqConfiguration iRabbitMqConfiguration)
        {
            RabbitMqConfiguration = iRabbitMqConfiguration ?? throw new ArgumentNullException(nameof(iRabbitMqConfiguration));

            try
            {
                Channel = Connection.CreateModel();
            }
            catch (Exception exception)
            {
                throw new Exception($"failed {nameof(Connection.CreateModel)}.", exception);
            }

            try
            {
                Channel
                    .ExchangeDeclare(
                        exchange: RabbitMqConfiguration.Exchange,
                        type: ExchangeType.Topic,
                        durable: true,
                        autoDelete: false,
                        arguments: new Dictionary<string, object>()
                        {
                        }
                    );
            }
            catch (Exception exception)
            {
                throw new Exception($"failed {nameof(Channel.ExchangeDeclare)}.", exception);
            }

            try
            {
                QueueDeclareOk queueDeclareOk = Channel.QueueDeclare(string.Empty, exclusive: true);
                Channel.QueueBind(queueDeclareOk.QueueName, RabbitMqConfiguration.Exchange, string.Empty);
            }
            catch (Exception exception)
            {
                throw new Exception($"failed {nameof(Channel.QueueDeclare)} or {nameof(Channel.QueueBind)}.", exception);
            }
        }

        protected abstract void HandleMessage(TModel model);

        protected sealed override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                EventingBasicConsumer consumer = new(Channel);

                consumer.Received += HandleMessage;

                Channel.BasicConsume(string.Empty, false, consumer);

                return Task.CompletedTask;
            }
            catch (Exception exception)
            {
                throw new Exception($"failed {nameof(Channel.BasicConsume)} .", exception);
            }
        }

        private void HandleMessage(object sender, BasicDeliverEventArgs args)
        {
            if (ShouldHandleMessage(args))
            {
                try
                {
                    TModel model = args.GetModel<TModel>();

                    HandleMessage(model);

                    Channel.BasicAck(args.DeliveryTag, false);
                }
                catch (Exception exception)
                {
                    throw new Exception($"failed {nameof(Channel.BasicAck)}.", exception);
                }
            }
        }

        private bool ShouldHandleMessage(BasicDeliverEventArgs args)
        {
            try
            {
                return args.GetSubject()?.Equals(subject, StringComparison.OrdinalIgnoreCase) ?? false;
            }
            catch (Exception exception)
            {
                throw new Exception($"failed GetSubject.", exception);
            }
        }
    }
}