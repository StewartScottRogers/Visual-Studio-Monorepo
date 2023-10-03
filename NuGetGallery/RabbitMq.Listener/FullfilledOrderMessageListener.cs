using Microsoft.Extensions.Options;
using ObjectModels;
using ObjectModels.Models;
using RabbitMq.Listener.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Listener;

/// <summary>
/// Listener that executes when a customer is deleted.
/// </summary>
public class FullFilledOrderMessageListener : AbstractMessageListener<FullFilledMessageEnvelope<Buyer>>
{
    protected override string Subject => "FullFilledOrderMessage";


    public FullFilledOrderMessageListener(IOptions<RabbitMqConfiguration> options) : base(options)
    {

    }

    protected override void HandleMessage(FullFilledMessageEnvelope<Buyer> model)
    {

    }
}