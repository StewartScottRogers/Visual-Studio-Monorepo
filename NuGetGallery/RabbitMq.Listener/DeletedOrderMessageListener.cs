using Microsoft.Extensions.Options;
using ObjectModels;
using ObjectModels.Models;
using RabbitMq.Listener.Abstract;
using RabbitMq.SharedProject.Messaging;

namespace RabbitMq.Listener;

/// <summary>
/// Listener that executes when a customer is deleted.
/// </summary>
public class DeletedOrderMessageListener : AbstractMessageListener<DeleteMessageEnvelope<Buyer>>
{
    public DeletedOrderMessageListener(IOptions<RabbitMqConfiguration> options) : base(options)
    {
        SetSubject("Deleted", "Order");
    }

    protected override void HandleMessage(DeleteMessageEnvelope<Buyer> model)
    {

    }
}