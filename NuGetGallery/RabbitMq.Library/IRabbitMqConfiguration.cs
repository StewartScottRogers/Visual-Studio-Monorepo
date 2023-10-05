namespace RabbitMq.SharedProject.Messaging
{
    public interface IRabbitMqConfiguration
    {
        string ContentType { get; }
        string Exchange { get; }
        string Hostname { get; }
        string Password { get; }
        string Port { get; }
        string UserName { get; }
    }
}