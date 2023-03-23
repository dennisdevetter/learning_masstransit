using System;
using LearningMassTransit.Contracts.Dtos;
using MediatR;

namespace LearningMassTransit.Messaging.Lara;

public class VoorstellenAdresRequestEvent : IRequest
{
    public CreateAdresVoorstelDto Data { get; }

    public Guid WorkflowId { get; }

    public string UserId { get;  }

    public VoorstellenAdresRequestEvent(CreateAdresVoorstelDto data, string userId)
    {
        WorkflowId = Guid.NewGuid();
        Data = data;
        UserId = userId;
    }
}