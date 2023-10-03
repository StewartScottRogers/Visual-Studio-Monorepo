using Microsoft.Extensions.Options;
using ObjectModels.Models;
using RabbitMq.Publisher.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Publisher;

public class FullfilledOrderMessagePublisher : AbstractMessagePublisher<Buyer>
{
    public FullfilledOrderMessagePublisher(IOptions<RabbitMqConfiguration> options) : base(options)
    {
        SetSubject("Fullfilled", "Order");
    }
}