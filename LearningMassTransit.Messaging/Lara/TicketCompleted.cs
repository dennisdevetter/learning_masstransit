using System;

namespace LearningMassTransit.Messaging.Lara;

public class TicketCompleted
{
    public string TicketId { get; set; }
    public Guid CorrelationId { get; set; }
}