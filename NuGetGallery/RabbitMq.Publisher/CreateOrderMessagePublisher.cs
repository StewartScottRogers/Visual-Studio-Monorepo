﻿using Microsoft.Extensions.Options;
using ObjectModels.Models;
using RabbitMq.Publisher.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Publisher;

public class CreateOrderMessagePublisher : AbstractMessagePublisher<Buyer>
{
    public CreateOrderMessagePublisher(IRabbitMqConfiguration iRabbitMqConfiguration) : base(iRabbitMqConfiguration)
    {
        SetSubject("Create", "Order");
    }
}