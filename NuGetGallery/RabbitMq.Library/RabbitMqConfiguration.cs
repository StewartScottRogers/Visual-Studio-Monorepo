using System;
using System.Collections;
using System.Diagnostics;

namespace RabbitMq.SharedProject.Messaging
{
    public class RabbitMqConfiguration : IRabbitMqConfiguration
    {

        [Conditional("DEBUG")]
        public static void DumpEnvironmentVariables()
        {
            IDictionary environmentVariables = Environment.GetEnvironmentVariables();
            foreach (object variable in environmentVariables)
                Console.WriteLine(variable.ToString());
        }

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
                throw new NullReferenceException(nameof(result), new ArgumentException($"Not Found '{variable}'."));

            return result;
        }

    }
}