using System;

namespace LearningMassTransit.Contracts.Responses;

public class CreateAdresVoorstelResponse
{
    public Guid WorkflowId { get; }

    public CreateAdresVoorstelResponse(Guid workflowId)
    {
        WorkflowId = workflowId;
    }
}