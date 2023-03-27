using System;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Application.Mappers;
using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Domain;
using LearningMassTransit.Domain.Lara;
using ActieEnum = LearningMassTransit.Contracts.Enums.ActieEnum;

namespace LearningMassTransit.Application.Services;

public class TicketService : ITicketService
{
    private readonly ILaraUnitOfWork _unitOfWork;

    public TicketService(ILaraUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TicketDto> CreateTicket(string ticketId, ActieEnum actie, Guid correlationId, CancellationToken cancellationToken)
    {
        var ticket = new Ticket
        {
            CorrelationId = correlationId,
            Actie = (Domain.Lara.ActieEnum)actie,
            CreationDate = DateTime.UtcNow,
            Status = TicketStatusEnum.Waiting,
            TicketId = ticketId,
        };

        await _unitOfWork.Tickets.Add(ticket, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var res = TicketMapper.MapToTicketDto(ticket);

        return res;
    }
}