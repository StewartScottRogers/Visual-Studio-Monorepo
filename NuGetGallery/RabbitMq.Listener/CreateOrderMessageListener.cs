using ObjectModels.Models;
using RabbitMq.Listener.Abstract;
using RabbitMq.SharedProject.Messaging;
using System;

namespace RabbitMq.Listener;

public class CreateOrderMessageListener : AbstractMessageListener<Buyer>
{
    private readonly Action<Buyer> Action;

    public CreateOrderMessageListener(IRabbitMqConfiguration iRabbitMqConfiguration, Action<Buyer> action) : base(iRabbitMqConfiguration)
    {
        Action = action;
        SetSubject("Create", "Order");
    }

    protected override void HandleMessage(Buyer model)
    {
        try
        {
            Action(model);
        }
        catch (Exception exception)
        {
            throw new Exception($"failed {nameof(Action)}.", exception);
        }
    }
}