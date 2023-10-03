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
        private readonly RabbitMqConfiguration rabbitMqConfiguration;

        protected abstract string Subject { get; }

        private ConnectionFactory connectionFactory;
        private ConnectionFactory ConnectionFactory
        {
            get
            {
                if (connectionFactory == null)
                {
                    connectionFactory = new()
                    {
                        HostName = rabbitMqConfiguration.Hostname,
                        Port = rabbitMqConfiguration.Port,
                        UserName = rabbitMqConfiguration.UserName,
                        Password = rabbitMqConfiguration.Password
                    };
                }

                return connectionFactory;
            }
        }

        private IConnection Connection => ConnectionFactory.CreateConnection();

        private IModel Channel { get; }

        protected AbstractMessageListener(IOptions<RabbitMqConfiguration> options)
        {
            rabbitMqConfiguration = options.Value;

            Channel = Connection.CreateModel();
            Channel.ExchangeDeclare(rabbitMqConfiguration.Exchange, ExchangeType.Fanout);

            QueueDeclareOk result = Channel.QueueDeclare(string.Empty, exclusive: true);

            Channel.QueueBind(result.QueueName, rabbitMqConfiguration.Exchange, string.Empty);
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
            return args.GetSubject()?.Equals(Subject, StringComparison.OrdinalIgnoreCase)
                ?? false;
        }
    }
}