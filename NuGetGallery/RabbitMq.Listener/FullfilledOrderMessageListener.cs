using ObjectModels.Models;
using RabbitMq.Listener.Abstract;
using RabbitMq.SharedProject.Messaging;
using System;

namespace RabbitMq.Listener;

public class FullfilledOrderMessageListener : AbstractMessageListener<Buyer>
{
    private readonly Action<Buyer> Action;

    public FullfilledOrderMessageListener(IRabbitMqConfiguration iRabbitMqConfiguration, Action<Buyer> action) : base(iRabbitMqConfiguration)
    {
        Action = action;
        SetSubject("Fullfilled", "Order");
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