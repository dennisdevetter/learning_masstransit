using System;
using MediatR;

namespace LearningMassTransit.Messaging.Lara;

public class AtomaireActieTicketCompletedEvent : INotification
{
    public string TicketId { get; set; }
    public Guid CorrelationId { get; set; }
    public string ObjectId { get; set; }
}