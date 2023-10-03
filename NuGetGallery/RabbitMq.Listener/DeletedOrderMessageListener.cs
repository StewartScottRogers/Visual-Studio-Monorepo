﻿using Microsoft.Extensions.Options;
using ObjectModels;
using ObjectModels.Models;
using RabbitMq.Listener.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Listener;

/// <summary>
/// Listener that executes when a customer is deleted.
/// </summary>
public class DeletedOrderMessageListener : AbstractMessageListener<Buyer>
{
    public DeletedOrderMessageListener(IRabbitMqConfiguration iRabbitMqConfiguration) : base(iRabbitMqConfiguration)
    {
        SetSubject("Deleted", "Order");
    }

    protected override void HandleMessage(Buyer model)
    {

    }
}