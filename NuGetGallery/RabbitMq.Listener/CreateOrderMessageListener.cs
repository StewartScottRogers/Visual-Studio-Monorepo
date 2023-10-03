﻿using Microsoft.Extensions.Options;
using ObjectModels;
using ObjectModels.Models;
using RabbitMq.Listener.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Listener;

/// <summary>
/// Listener that executes when a customer is deleted.
/// </summary>
public class CreateOrderMessageListener : AbstractMessageListener<CreateMessageEnvelope<Buyer>>
{
    protected override string Subject => " CreateOrderMessageBuyer";


    public CreateOrderMessageListener(IOptions<RabbitMqConfiguration> options) : base(options)
    {

    }

    protected override void HandleMessage(CreateMessageEnvelope<Buyer> model)
    {

    }
}