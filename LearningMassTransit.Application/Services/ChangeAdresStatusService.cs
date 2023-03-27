using System;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Contracts.Enums;
using LearningMassTransit.Domain;
using LearningMassTransit.Domain.Lara;
using LearningMassTransit.Infrastructure.Messaging;
using LearningMassTransit.Messaging.Lara;
using ActieEnum = LearningMassTransit.Contracts.Enums.ActieEnum;

namespace LearningMassTransit.Application.Services;

public class ChangeAdresStatusService : IChangeAdresStatusService
{
    private readonly IApplicationBus _applicationBus;
    private readonly ITicketService _ticketService;

    public ChangeAdresStatusService(IApplicationBus applicationBus, ITicketService ticketService)
    {
        _applicationBus = applicationBus;
        _ticketService = ticketService;
    }

    public async Task<TicketDto> ChangeAdresStatus(ChangeAdresStatusDto changeAdresStatusDto, Guid correlationId, CancellationToken cancellationToken)
    {
        // todo
        // do call to basisregisters

        var ticketId = Guid.NewGuid().ToString();

        var ticket = await _ticketService.CreateTicket(
            ticketId,
            MapToActie(changeAdresStatusDto),
            correlationId,
            cancellationToken
        );

        await _applicationBus.Publish(new AdresStatusChangeCreatedEvent
        {
            TicketId = ticketId,
            CorrelationId = correlationId

        }, cancellationToken);

        return ticket;
    }

    private ActieEnum MapToActie(ChangeAdresStatusDto changeAdresStatusDto)
    {
        if (changeAdresStatusDto.Status == AdresStatusEnum.InGebruik)
        {
            return ActieEnum.ApproveAddress;
        }

        return ActieEnum.RejectAddress;
    }
}