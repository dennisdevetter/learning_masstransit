using System;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Requests;
using LearningMassTransit.Contracts.Responses;
using LearningMassTransit.Domain;
using LearningMassTransit.Domain.Lara;
using LearningMassTransit.Infrastructure.Messaging;
using LearningMassTransit.Messaging.Lara;
using MediatR;
using Newtonsoft.Json;

namespace LearningMassTransit.Application.RequestHandlers;

public class CreateAdresVoorstelMetStatusRequestHandler : IRequestHandler<CreateAdresVoorstelMetStatusRequest, CreateAdresVoorstelMetStatusResponse>
{
    private readonly IApplicationBus _applicationBus;
    private readonly ILaraUnitOfWork _laraUnitOfWork;

    public CreateAdresVoorstelMetStatusRequestHandler(IApplicationBus applicationBus, ILaraUnitOfWork laraUnitOfWork)
    {
        _applicationBus = applicationBus;
        _laraUnitOfWork = laraUnitOfWork;
    }

    public async Task<CreateAdresVoorstelMetStatusResponse> Handle(CreateAdresVoorstelMetStatusRequest metStatusRequest, CancellationToken cancellationToken)
    {
        // TODO add backend validation ?

        var workflow = await CreateWorkflow(metStatusRequest, cancellationToken);

        await StartSaga(workflow, cancellationToken);

        return new CreateAdresVoorstelMetStatusResponse();
    }

    private async Task<Workflow> CreateWorkflow(CreateAdresVoorstelMetStatusRequest metStatusRequest, CancellationToken cancellationToken)
    {
        var userId = "7D35AFD6933D4049BD17A4560BA30674";

        var workflow = new Workflow
        {
            WorkflowId = Guid.NewGuid(),
            UserId = userId,
            Data = JsonConvert.SerializeObject(metStatusRequest.Adres),
            CreationDate = DateTime.UtcNow,
            WorkflowActie = WorkflowActieEnum.NieuwAdresMetStatusWijziging,
            WorkflowType = WorkflowTypeEnum.Complex
        };

        await _laraUnitOfWork.Workflows.Add(workflow, cancellationToken);
        await _laraUnitOfWork.SaveChangesAsync(cancellationToken);

        return workflow;
    }

    private async Task StartSaga(Workflow workflow, CancellationToken cancellationToken)
    {
        var voorstellenAdresRequestEvent = new VoorstellenAdresRequestEvent(workflow.WorkflowId);

        await _applicationBus.Publish(voorstellenAdresRequestEvent, cancellationToken);
    }
}