using Microsoft.Extensions.Options;
using ObjectModels;
using ObjectModels.Models;
using RabbitMq.Publisher.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Publisher;

public class DeleteOrderMessagePublisher : AbstractMessagePublisher<DeleteMessageEnvelope<Buyer>>
{
    protected override string Subject => "OrderCreated";

    public DeleteOrderMessagePublisher(IOptions<RabbitMqConfiguration> options) : base(options)
    {
    }
}