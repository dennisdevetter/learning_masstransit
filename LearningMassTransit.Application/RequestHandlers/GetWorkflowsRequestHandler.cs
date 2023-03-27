using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Contracts.Requests;
using LearningMassTransit.Contracts.Responses;
using LearningMassTransit.Domain;
using LearningMassTransit.Domain.Lara;
using MediatR;

namespace LearningMassTransit.Application.RequestHandlers;

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

        var dtos = workflows.Select(MapToDto).ToList();

        return new GetWorkflowsResponse(dtos);
    }

    private WorkflowDto MapToDto(Workflow workflow)
    {
        return new WorkflowDto
        {
            CreationDate = workflow.CreationDate,
            UserId = workflow.UserId,
            WorkflowId = workflow.WorkflowId,
            WorkflowAction = MapAction(workflow),
            WorkflowType = (Contracts.Enums.WorkflowTypeEnum)workflow.WorkflowType,
            Status = MapStatus(workflow)
        };
    }

    private string? MapAction(Workflow workflow)
    {
        if (workflow.WorkflowType == WorkflowTypeEnum.Complex)
        {
            return workflow.WorkflowAction.ToString();
        }
        return workflow.AtomaireActieState?.Actie;
    }

    private string? MapStatus(Workflow workflow)
    {
        switch (workflow.WorkflowAction)
        {
            case WorkflowActionEnum.NieuwAdresMetStatusWijziging: return workflow.VoorstellenAdresState?.CurrentState;
            case WorkflowActionEnum.AtomaireActie: return workflow.AtomaireActieState?.CurrentState;
            default: return null;
        }
    }
}