using Microsoft.Extensions.Options;
using ObjectModels;
using ObjectModels.Models;
using RabbitMq.Listener.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Listener;

/// <summary>
/// Listener that executes when a customer is deleted.
/// </summary>
public class CreateOrderMessageListener : AbstractMessageListener<Buyer>
{
    public CreateOrderMessageListener(IRabbitMqConfiguration iRabbitMqConfiguration) : base(iRabbitMqConfiguration)
    {
        SetSubject("Create", "Order");
    }

    protected override void HandleMessage(Buyer model)
    {

    }
}