using System;
using MediatR;

namespace LearningMassTransit.Messaging.Lara;

public class AdresStatusTicketCompletedEvent : INotification
{
    public string TicketId { get; set; }
    public Guid CorrelationId { get; set; }
}