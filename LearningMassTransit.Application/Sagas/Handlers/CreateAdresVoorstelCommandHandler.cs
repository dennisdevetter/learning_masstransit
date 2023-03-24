﻿using System;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Commands;
using LearningMassTransit.Domain;
using LearningMassTransit.Domain.Lara;
using LearningMassTransit.Infrastructure.Messaging;
using LearningMassTransit.Messaging.Lara;
using MediatR;

namespace LearningMassTransit.Application.Sagas.Handlers;

public class CreateAdresVoorstelCommandHandler : IRequestHandler<CreateAdresVoorstelCommand>
{
    private readonly IApplicationBus _applicationBus;
    private readonly ILaraUnitOfWork _unitOfWork;

    public CreateAdresVoorstelCommandHandler(IApplicationBus applicationBus, ILaraUnitOfWork unitOfWork)
    {
        _applicationBus = applicationBus;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateAdresVoorstelCommand command, CancellationToken cancellationToken)
    {
        var adres = command.Adres;

        // todo
        // do call to basisregisters
        // get ticket id and store in database

        var ticketId = Guid.NewGuid().ToString();

        var ticket = new Ticket
        {
            CorrelationId = command.CorrelationId,
            Actie = ActieEnum.ProposeStreetName,
            CreationDate = DateTime.UtcNow,
            Status = TicketStatusEnum.Waiting,
            TicketId = ticketId,
        };

        await _unitOfWork.Tickets.Add(ticket, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _applicationBus.Publish(new AdresVoorstelCreatedEvent
        {
            TicketId = ticketId,
            CorrelationId = command.CorrelationId

        }, cancellationToken);

        return Unit.Value;
    }
}