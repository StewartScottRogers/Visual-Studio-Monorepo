using Microsoft.Extensions.Options;
using ObjectModels;
using ObjectModels.Models;
using RabbitMq.Listener.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Listener;

/// <summary>
/// Listener that executes when a customer is deleted.
/// </summary>
public class FullfilledOrderMessageListener : AbstractMessageListener<FullFilledMessageEnvelope<Buyer>>
{
    public FullfilledOrderMessageListener(IOptions<RabbitMqConfiguration> options) : base(options)
    {
        SetSubject("Fullfilled", "Order");
    }

    protected override void HandleMessage(FullFilledMessageEnvelope<Buyer> model)
    {

    }
}