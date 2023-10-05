using ObjectModels.Models;
using RabbitMq.Listener.Abstract;
using RabbitMq.SharedProject.Messaging;
using System;

namespace RabbitMq.Listener;

public class DeletedOrderMessageListener : AbstractMessageListener<Buyer>
{
    private readonly Action<Buyer> Action;

    public DeletedOrderMessageListener(IRabbitMqConfiguration iRabbitMqConfiguration, Action<Buyer> action) : base(iRabbitMqConfiguration)
    {
        Action = action;
        SetSubject("Deleted", "Order");
    }

    protected override void HandleMessage(Buyer model)
    {
        Action(model);
    }
}