using Microsoft.Extensions.Options;
using ObjectModels.Models;
using RabbitMq.Publisher.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Publisher;

public class CreateOrderMessagePublisher : AbstractMessagePublisher<Buyer>
{
    public CreateOrderMessagePublisher(IOptions<RabbitMqConfiguration> options) : base(options)
    {
        SetSubject("Create", "Order");
    }
}