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

namespace LearningMassTransit.Application.Handlers;

public class CreateAdresVoorstelRequestHandler : IRequestHandler<CreateAdresVoorstelRequest, CreateAdresVoorstelResponse>
{
    private readonly IApplicationBus _applicationBus;
    private readonly ILaraUnitOfWork _laraUnitOfWork;

    public CreateAdresVoorstelRequestHandler(IApplicationBus applicationBus, ILaraUnitOfWork laraUnitOfWork)
    {
        _applicationBus = applicationBus;
        _laraUnitOfWork = laraUnitOfWork;
    }

    public async Task<CreateAdresVoorstelResponse> Handle(CreateAdresVoorstelRequest request, CancellationToken cancellationToken)
    {
        // TODO add backend validation ?

        var workflow = await CreateWorkflow(request, cancellationToken);

        await StartSaga(workflow, cancellationToken);

        return new CreateAdresVoorstelResponse();
    }

    private async Task<Workflow> CreateWorkflow(CreateAdresVoorstelRequest request, CancellationToken cancellationToken)
    {
        var userId = "7D35AFD6933D4049BD17A4560BA30674";

        var workflow = new Workflow
        {
            WorkflowId = Guid.NewGuid(),
            UserId = userId,
            Data = JsonConvert.SerializeObject(request.Adres),
            CreationDate = DateTime.UtcNow,
            WorkflowType = WorkflowTypeEnum.NieuwAdresMetStatusWijziging,
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