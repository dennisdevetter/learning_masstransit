using System;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Commands;
using LearningMassTransit.Domain;
using LearningMassTransit.Domain.Lara;
using LearningMassTransit.Infrastructure.Messaging;
using LearningMassTransit.Messaging.Lara;
using MediatR;

namespace LearningMassTransit.Application.Sagas.Handlers;

public class ChangeAdresStatusCommandHandler : IRequestHandler<ChangeAdresStatusCommand>
{
    private readonly IApplicationBus _applicationBus;
    private readonly ILaraUnitOfWork _unitOfWork;

    public ChangeAdresStatusCommandHandler(IApplicationBus applicationBus, ILaraUnitOfWork unitOfWork)
    {
        _applicationBus = applicationBus;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ChangeAdresStatusCommand command, CancellationToken cancellationToken)
    {
        // todo
        // do call to basisregisters
        // get ticket id and store in database

        var ticketId = Guid.NewGuid().ToString();

        var ticket = new Ticket
        {
            CorrelationId = command.CorrelationId,
            Actie = command.Approved ? ActieEnum.ApproveAddress : ActieEnum.RejectAddress,
            CreationDate = DateTime.UtcNow,
            Status = TicketStatusEnum.Waiting,
            TicketId = ticketId,
        };

        await _unitOfWork.Tickets.Add(ticket, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _applicationBus.Publish(new AdresStatusChangeCreatedEvent
        {
            TicketId = ticketId,
            CorrelationId = command.CorrelationId

        }, cancellationToken);

        return Unit.Value;
    }
}