using Microsoft.Extensions.Options;
using RabbitMq.SharedProject.Messaging;
using RabbitMq.SharedProject.Messaging.Extensions;
using RabbitMQ.Client;
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

        protected AbstractMessagePublisher(IRabbitMqConfiguration iRabbitMqConfiguration)
        {
            RabbitMqConfiguration = iRabbitMqConfiguration;

            Channel = Connection.CreateModel();
            Channel.ExchangeDeclare(RabbitMqConfiguration.Exchange, ExchangeType.Fanout);
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