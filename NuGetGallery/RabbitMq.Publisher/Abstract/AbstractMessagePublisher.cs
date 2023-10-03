﻿using Microsoft.Extensions.Options;
using RabbitMq.SharedProject.Messaging;
using RabbitMq.SharedProject.Messaging.Extensions;
using RabbitMQ.Client;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabbitMq.Publisher.Abstract
{
    public abstract class AbstractMessagePublisher<TModel> where TModel : class
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

        protected AbstractMessagePublisher(IOptions<RabbitMqConfiguration> options)
        {
            rabbitMqConfiguration = options.Value;

            Channel = Connection.CreateModel();
            Channel.ExchangeDeclare(rabbitMqConfiguration.Exchange, ExchangeType.Fanout);
        }

        public async Task Send(TModel obj)
        {
            IBasicProperties message = Channel.CreateBasicProperties();

            message.ContentType = rabbitMqConfiguration.ContentType;
            message.SetSubject(Subject);

            byte[] body = JsonSerializer.SerializeToUtf8Bytes(obj, typeof(TModel));

            await DoSend(message, body);
        }

        private async Task DoSend(IBasicProperties message, byte[] body)
        {
            await Task.Run(() =>
            {
                Channel.BasicPublish(rabbitMqConfiguration.Exchange, string.Empty, message, body);
            });
        }
    }
}