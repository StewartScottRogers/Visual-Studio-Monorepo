using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMq.SharedProject.Messaging;
using RabbitMq.SharedProject.Messaging.Extensions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMq.Listener.Abstract
{
    public abstract class AbstractMessageListener<TModel> : BackgroundService where TModel : class
    {
        private readonly IRabbitMqConfiguration RabbitMqConfiguration;

        protected void SetSubject(string verb, string noune)
        {
            subject = $"{verb.Trim().ToLower()}-{noune.Trim().ToLower()}";
        }
        private string subject;

        private ConnectionFactory connectionFactory;
        private ConnectionFactory ConnectionFactory
        {
            get
            {
                if (connectionFactory == null)
                {
                    connectionFactory = new()
                    {
                        HostName = RabbitMqConfiguration.Hostname,
                        Port = int.Parse(RabbitMqConfiguration.Port),
                        UserName = RabbitMqConfiguration.UserName,
                        Password = RabbitMqConfiguration.Password
                    };
                }

                return connectionFactory;
            }
        }

        private IConnection Connection => ConnectionFactory.CreateConnection();

        private IModel Channel { get; }

        protected AbstractMessageListener(IRabbitMqConfiguration iRabbitMqConfiguration)
        {
            RabbitMqConfiguration = iRabbitMqConfiguration;

            Channel = Connection.CreateModel();
            Channel.ExchangeDeclare(RabbitMqConfiguration.Exchange, ExchangeType.Fanout);

            QueueDeclareOk result = Channel.QueueDeclare(string.Empty, exclusive: true);

            Channel.QueueBind(result.QueueName, RabbitMqConfiguration.Exchange, string.Empty);
        }

        protected abstract void HandleMessage(TModel model);

        protected sealed override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            EventingBasicConsumer consumer = new(Channel);

            consumer.Received += HandleMessage;

            Channel.BasicConsume(string.Empty, false, consumer);

            return Task.CompletedTask;
        }

        private void HandleMessage(object sender, BasicDeliverEventArgs args)
        {
            if (ShouldHandleMessage(args))
            {
                TModel model = args.GetModel<TModel>();

                HandleMessage(model);

                Channel.BasicAck(args.DeliveryTag, false);
            }
        }

        private bool ShouldHandleMessage(BasicDeliverEventArgs args)
        {
            return args.GetSubject()?.Equals(subject, StringComparison.OrdinalIgnoreCase)
                ?? false;
        }
    }
}