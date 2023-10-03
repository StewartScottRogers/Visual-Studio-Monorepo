using Microsoft.Extensions.Options;
using ObjectModels;
using ObjectModels.Models;
using RabbitMq.Listener.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Listener;

public class CanceledOrderMessageListener : AbstractMessageListener<CanceledMessageEnvelope<Buyer>>
{
    protected override string Subject => "CanceledOrderMessageBuyer";


    public CanceledOrderMessageListener(IOptions<RabbitMqConfiguration> options) : base(options)
    {

    }

    protected override void HandleMessage(CanceledMessageEnvelope<Buyer> model)
    {

    }
}