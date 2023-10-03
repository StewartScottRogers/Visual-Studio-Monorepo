﻿using Microsoft.Extensions.Options;
using ObjectModels;
using ObjectModels.Models;
using RabbitMq.Publisher.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Publisher;

public class FullfilledOrderMessagePublisher : AbstractMessagePublisher<FullFilledMessageEnvelope<Buyer>>
{
    protected override string Subject => "OrderCreated";

    public FullfilledOrderMessagePublisher(IOptions<RabbitMqConfiguration> options) : base(options)
    {
    }
}