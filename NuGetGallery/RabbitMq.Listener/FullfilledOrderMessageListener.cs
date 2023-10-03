﻿using Microsoft.Extensions.Options;
using ObjectModels;
using ObjectModels.Models;
using RabbitMq.Listener.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Listener;

/// <summary>
/// Listener that executes when a customer is deleted.
/// </summary>
public class FullfilledOrderMessageListener : AbstractMessageListener<Buyer>
{
    public FullfilledOrderMessageListener(IRabbitMqConfiguration iRabbitMqConfiguration) : base(iRabbitMqConfiguration)
    {
        SetSubject("Fullfilled", "Order");
    }

    protected override void HandleMessage(Buyer model)
    {

    }
}