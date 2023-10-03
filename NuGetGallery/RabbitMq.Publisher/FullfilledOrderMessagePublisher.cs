using Microsoft.Extensions.Options;
using ObjectModels.Models;
using RabbitMq.Publisher.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Publisher;

public class FullfilledOrderMessagePublisher : AbstractMessagePublisher<Buyer>
{
    public FullfilledOrderMessagePublisher(IRabbitMqConfiguration iRabbitMqConfiguration) : base(iRabbitMqConfiguration)
    {
        SetSubject("Fullfilled", "Order");
    }
}