using Microsoft.Extensions.Options;
using ObjectModels.Models;
using RabbitMq.Publisher.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Publisher;

public class DeleteOrderMessagePublisher : AbstractMessagePublisher<Buyer>
{
    public DeleteOrderMessagePublisher(IOptions<RabbitMqConfiguration> options) : base(options)
    {
        SetSubject("Delete", "Order");
    }
}