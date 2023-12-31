﻿using Microsoft.Extensions.Options;
using ObjectModels;
using ObjectModels.Models;
using RabbitMq.Publisher.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Publisher;

public class CanceledOrderMessagePublisher : AbstractMessagePublisher<Buyer>
{
    public CanceledOrderMessagePublisher(IRabbitMqConfiguration options) : base(options)
    {
        SetSubject("Canceled", "Order");
    }
}