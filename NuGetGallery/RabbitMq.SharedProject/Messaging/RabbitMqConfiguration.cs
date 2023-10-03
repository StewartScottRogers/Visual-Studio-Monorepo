using System;

namespace RabbitMq.SharedProject.Messaging
{
    public class RabbitMqConfiguration
    {
        public string Hostname { get => Environment.GetEnvironmentVariable($"RabbitMq{nameof(Hostname)}"); }

        public string UserName { get => Environment.GetEnvironmentVariable($"RabbitMq{nameof(UserName)}"); }

        public string Password { get => Environment.GetEnvironmentVariable($"RabbitMq{nameof(Password)}"); }

        public string Port { get => Environment.GetEnvironmentVariable($"RabbitMq{nameof(Port)}"); }

        public string ContentType { get => Environment.GetEnvironmentVariable($"RabbitMq{nameof(ContentType)}"); }

        public string Exchange { get => Environment.GetEnvironmentVariable($"RabbitMq{nameof(Exchange)}"); }

    }
}