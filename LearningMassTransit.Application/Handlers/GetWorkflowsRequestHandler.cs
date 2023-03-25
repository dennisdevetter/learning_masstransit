using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Contracts.Requests;
using LearningMassTransit.Contracts.Responses;
using LearningMassTransit.Domain;
using LearningMassTransit.Domain.Lara;
using MediatR;

namespace LearningMassTransit.Application.Handlers;

public class GetWorkflowsRequestHandler : IRequestHandler<GetWorkflowsRequest, GetWorkflowsResponse>
{
    private readonly ILaraUnitOfWork _laraUnitOfWork;

    public GetWorkflowsRequestHandler(ILaraUnitOfWork laraUnitOfWork)
    {
        _laraUnitOfWork = laraUnitOfWork;
    }

    public async Task<GetWorkflowsResponse> Handle(GetWorkflowsRequest request, CancellationToken cancellationToken)
    {
        var workflows = (await _laraUnitOfWork.Workflows.Find(x => true, cancellationToken)).OrderByDescending(x => x.CreationDate).ToList();

        var dtos = workflows.Select(x => MapToDto(x)).ToList();

        return new GetWorkflowsResponse(dtos);
    }

    private WorkflowDto MapToDto(Workflow workflow)
    {
        return new WorkflowDto
        {
            CreationDate = workflow.CreationDate,
            UserId = workflow.UserId,
            WorkflowId = workflow.WorkflowId,
            WorkflowType = workflow.WorkflowType.ToString(),
            Status = MapStatus(workflow)
        };
    }

    private string? MapStatus(Workflow workflow)
    {
        switch (workflow.WorkflowType)
        {
            case Domain.Lara.WorkflowTypeEnum.NieuwAdresMetStatusWijziging: return workflow.VoorstellenAdresState?.CurrentState;
            default: return null;
        }
    }
}