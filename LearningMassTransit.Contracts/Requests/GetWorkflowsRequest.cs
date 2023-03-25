using LearningMassTransit.Contracts.Responses;
using MediatR;

namespace LearningMassTransit.Contracts.Requests;

public class GetWorkflowsRequest : IRequest<GetWorkflowsResponse>
{
    public GetWorkflowsRequest()
    {
    }
}