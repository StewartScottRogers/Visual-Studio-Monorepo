using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace RabbitMq.SharedProject.Messaging.Extensions
{
    public static class BasicDeliverEventArgsExtensions
    {
        public static T GetModel<T>(this BasicDeliverEventArgs args)
        {
            string content = Encoding.UTF8.GetString(args.Body.ToArray());

            return JsonSerializer.Deserialize<T>(content);
        }

        public static string GetSubject(this BasicDeliverEventArgs args)
        {
            if (args is null)
                throw new ArgumentNullException(nameof(args));

            return args.BasicProperties.Headers?
                .Where(c => c.Key == "Subject")
                .Select(t => ParseHeaderString(t.Value))
                .FirstOrDefault();
        }

        private static string ParseHeaderString(object obj)
        {
            if (obj is byte[] bytes)
            {
                return Encoding.UTF8.GetString(bytes);
            }

            return null;
        }
    }
}