using Microsoft.Extensions.Options;
using ObjectModels;
using ObjectModels.Models;
using RabbitMq.Publisher.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Publisher;

public class CreateOrderMessagePublisher : AbstractMessagePublisher<CreateMessageEnvelope<Buyer>>
{
    protected override string Subject => "OrderCreated";

    public CreateOrderMessagePublisher(IOptions<RabbitMqConfiguration> options) : base(options)
    {
    }
}