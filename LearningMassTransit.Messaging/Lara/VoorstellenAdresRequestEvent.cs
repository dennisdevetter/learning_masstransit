using System;
using MediatR;

namespace LearningMassTransit.Messaging.Lara;

public class VoorstellenAdresRequestEvent : IRequest
{
    public Guid WorkflowId { get; }

    public VoorstellenAdresRequestEvent(Guid workflowId)
    {
        WorkflowId = workflowId;
    }
}