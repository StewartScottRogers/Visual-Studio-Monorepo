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
    protected override string Subject => "DeletedOrderMessageBuyer";


    public DeletedOrderMessageListener(IOptions<RabbitMqConfiguration> options) : base(options)
    {

    }

    protected override void HandleMessage(DeleteMessageEnvelope<Buyer> model)
    {

    }
}