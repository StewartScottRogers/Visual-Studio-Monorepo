using Microsoft.Extensions.Options;
using ObjectModels;
using ObjectModels.Models;
using RabbitMq.Publisher.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Publisher;

public class DeleteOrderMessagePublisher : AbstractMessagePublisher<DeleteMessageEnvelope<Buyer>>
{
    public DeleteOrderMessagePublisher(IOptions<RabbitMqConfiguration> options) : base(options)
    {
        SetSubject("Delete", "Order");
    }
}