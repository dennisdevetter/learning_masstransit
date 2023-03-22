using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Contracts.Requests;
using LearningMassTransit.Contracts.Responses;
using LearningMassTransit.Domain;
using LearningMassTransit.Domain.Lara;
using LearningMassTransit.Messaging;
using LearningMassTransit.Messaging.Lara;
using MassTransit;
using MediatR;
using Newtonsoft.Json;

namespace LearningMassTransit.Application.Handlers;

public class CreateAdresVoorstelRequestHandler : IRequestHandler<CreateAdresVoorstelRequest, CreateAdresVoorstelResponse>
{
    private readonly ILaraUnitOfWork _laraUnitOfWork;
    private readonly IBus _bus;

    public CreateAdresVoorstelRequestHandler(ILaraUnitOfWork laraUnitOfWork, IBus bus)
    {
        _laraUnitOfWork = laraUnitOfWork;
        _bus = bus;
    }

    public async Task<CreateAdresVoorstelResponse> Handle(CreateAdresVoorstelRequest request, CancellationToken cancellationToken)
    {
        var ticketId = Guid.NewGuid().ToString();

        // post to basisregisters goes here and returns ticket id.

        var wizard = CreateWizard(request, ticketId);

        await _laraUnitOfWork.Wizards.Add(wizard, cancellationToken);

        await _laraUnitOfWork.SaveChangesAsync(cancellationToken);

        await _bus.Publish(new WizardCreated { WizardId = wizard.WizardId }, cancellationToken);

        var res = new CreateAdresVoorstelResponse(new AdresVoorstelCreatingDto(ticketId));

        return res;
    }

    private static Wizard CreateWizard(CreateAdresVoorstelRequest request, string ticketId)
    {
        var steps = new List<WizardStep>();

        steps.Add(new WizardStep
        {
            WizardStepId = Guid.NewGuid(),
            CreationDate = DateTime.Now.ToUniversalTime(),
            StepNr = 0,
            StepType = request.Adres.GetType().AssemblyQualifiedName,
            StepData = JsonConvert.SerializeObject(request.Adres),
            TicketId = ticketId,
        });

        var wizard = new Wizard
        {
            WizardId = Guid.NewGuid(),
            CreationDate = DateTime.Now.ToUniversalTime(),
            Kind = "AdresVoorstel",
            Status = WizardStatusEnum.Started,
            UserId = "sub",
            Steps = steps
        };
        return wizard;
    }
}