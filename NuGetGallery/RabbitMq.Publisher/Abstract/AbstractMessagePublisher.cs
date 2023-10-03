using Microsoft.Extensions.Options;
using RabbitMq.SharedProject.Messaging;
using RabbitMq.SharedProject.Messaging.Extensions;
using RabbitMQ.Client;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabbitMq.Publisher.Abstract
{
    public abstract class AbstractMessagePublisher<TModel> where TModel : class
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

        protected AbstractMessagePublisher(IRabbitMqConfiguration iRabbitMqConfiguration)
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
                Channel.ExchangeDeclare(RabbitMqConfiguration.Exchange, ExchangeType.Fanout);
            }
            catch (Exception exception)
            {
                throw new Exception($"failed {nameof(Channel.ExchangeDeclare)}.", exception);
            }
        }

        public async Task Send(TModel tModel)
        {
            IBasicProperties message = Channel.CreateBasicProperties();

            message.ContentType = RabbitMqConfiguration.ContentType;
            message.SetSubject(subject);

            byte[] body = JsonSerializer.SerializeToUtf8Bytes(tModel, typeof(TModel));

            await Task.Run(() =>
            {
                Channel.BasicPublish(RabbitMqConfiguration.Exchange, string.Empty, message, body);
            });
        }
    }
}