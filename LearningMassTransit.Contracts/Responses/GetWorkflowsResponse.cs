using LearningMassTransit.Contracts.Dtos;
using System.Collections.Generic;

namespace LearningMassTransit.Contracts.Responses;

public class GetWorkflowsResponse : ResponseOf<IList<WorkflowDto>>
{
    public GetWorkflowsResponse(IList<WorkflowDto> workflows) : base(workflows)
    {
    }
}