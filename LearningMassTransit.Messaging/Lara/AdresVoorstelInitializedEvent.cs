using System;
using MediatR;

namespace LearningMassTransit.Messaging.Lara;

public class AdresVoorstelInitializedEvent : INotification
{
    public Guid CorrelationId { get; set; }
}