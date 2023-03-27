using System;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Domain;
using LearningMassTransit.Domain.Lara;
using LearningMassTransit.Infrastructure.Messaging;
using LearningMassTransit.Messaging.Lara;

namespace LearningMassTransit.Application.Services;

public class AtomaireActieService : IAtomaireActieService
{
    private readonly IApplicationBus _applicationBus;
    private readonly ILaraUnitOfWork _laraUnitOfWork;

    public AtomaireActieService(IApplicationBus applicationBus, ILaraUnitOfWork laraUnitOfWork)
    {
        _applicationBus = applicationBus;
        _laraUnitOfWork = laraUnitOfWork;
    }

    public async Task<AtomaireActieOutputDto> Execute(string data, Func<Guid, Task<TicketDto>> executor, CancellationToken cancellationToken)
    {
        var workflow = await CreateWorkflow(data, cancellationToken);

        var ticket = await executor(workflow.WorkflowId);

        await StartSaga(workflow, ticket, cancellationToken);

        return new AtomaireActieOutputDto
        {
            Actie = ticket.Actie,
            TicketId = ticket.TicketId,
            WorkflowId = workflow.WorkflowId
        };
    }

    private async Task<Workflow> CreateWorkflow(string data, CancellationToken cancellationToken)
    {
        var userId = "7D35AFD6933D4049BD17A4560BA30674";

        var workflow = new Workflow
        {
            WorkflowId = Guid.NewGuid(),
            UserId = userId,
            Data = data,
            CreationDate = DateTime.UtcNow,
            WorkflowAction = WorkflowActionEnum.AtomaireActie,
            WorkflowType = WorkflowTypeEnum.Atomair
        };

        await _laraUnitOfWork.Workflows.Add(workflow, cancellationToken);
        await _laraUnitOfWork.SaveChangesAsync(cancellationToken);
        return workflow;
    }

    private async Task StartSaga(Workflow workflow, TicketDto ticket, CancellationToken cancellationToken)
    {
        var atomaireActieInitializedEvent = new AtomaireActieInitializedEvent(workflow.WorkflowId, ticket.TicketId, ticket.Actie.ToString());

        await _applicationBus.Publish(atomaireActieInitializedEvent, cancellationToken);
    }
}