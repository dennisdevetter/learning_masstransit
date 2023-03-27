using System;
using MediatR;

namespace LearningMassTransit.Messaging.Lara;

public class AtomaireActieInitializedEvent : INotification
{
    public string TicketId { get; }
    public string Actie { get; }
    public Guid WorkflowId { get; }

    public AtomaireActieInitializedEvent(Guid workflowId, string ticketId, string actie)
    {
        WorkflowId = workflowId;
        TicketId = ticketId;
        Actie = actie;
    }
}