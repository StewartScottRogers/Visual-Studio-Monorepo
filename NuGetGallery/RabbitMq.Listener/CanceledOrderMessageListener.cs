using Microsoft.Extensions.Options;
using ObjectModels;
using ObjectModels.Models;
using RabbitMq.Listener.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Listener;

public class CanceledOrderMessageListener : AbstractMessageListener<CanceledMessageEnvelope<Buyer>>
{  
    public CanceledOrderMessageListener(IOptions<RabbitMqConfiguration> options) : base(options)
    {
        SetSubject("Canceled", "Order");
    }

    protected override void HandleMessage(CanceledMessageEnvelope<Buyer> model)
    {

    }
}