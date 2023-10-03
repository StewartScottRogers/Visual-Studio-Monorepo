using System;
using System.Collections;

namespace RabbitMq.SharedProject.Messaging
{
    public class RabbitMqConfiguration : IRabbitMqConfiguration
    {

        public string Hostname { get => GetEnvironmentVariable($"{nameof(Hostname)}"); }

        public string UserName { get => GetEnvironmentVariable($"{nameof(UserName)}"); }

        public string Password { get => GetEnvironmentVariable($"{nameof(Password)}"); }

        public string Port { get => GetEnvironmentVariable($"{nameof(Port)}"); }

        public string ContentType { get => GetEnvironmentVariable($"{nameof(ContentType)}"); }

        public string Exchange { get => GetEnvironmentVariable($"{nameof(Exchange)}"); }

        private static string GetEnvironmentVariable(string value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            string variable
                = $"RabbitMq{value.Trim()}";

            string result
                = Environment.GetEnvironmentVariable(variable);

            if (result is null)
            {
                IDictionary environmentVariables 
                    = Environment.GetEnvironmentVariables();

                throw new NullReferenceException(nameof(result));
            }

            return result;
        }

    }
}