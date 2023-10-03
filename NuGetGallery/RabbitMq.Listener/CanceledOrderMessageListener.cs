using Microsoft.Extensions.Options;
using ObjectModels;
using ObjectModels.Models;
using RabbitMq.Listener.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Listener;

public class CanceledOrderMessageListener : AbstractMessageListener<Buyer>
{
    public CanceledOrderMessageListener(IRabbitMqConfiguration iRabbitMqConfiguration) : base(iRabbitMqConfiguration)
    {
        SetSubject("Canceled", "Order");
    }

    protected override void HandleMessage(Buyer model)
    {

    }
}