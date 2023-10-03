﻿using Microsoft.Extensions.Options;
using ObjectModels;
using ObjectModels.Models;
using RabbitMq.Publisher.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Publisher;

public class CanceledOrderMessagePublisher : AbstractMessagePublisher<CanceledMessageEnvelope<Buyer>>
{
    protected override string Subject => "OrderCreated";

    public CanceledOrderMessagePublisher(IOptions<RabbitMqConfiguration> options) : base(options)
    {
    }
}