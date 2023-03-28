using System;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Contracts.Enums;
using LearningMassTransit.Infrastructure.Messaging;
using LearningMassTransit.Messaging.Lara;
using WorkflowActieEnum = LearningMassTransit.Contracts.Enums.WorkflowActieEnum;

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

    private WorkflowActieEnum MapToActie(ChangeAdresStatusDto changeAdresStatusDto)
    {
        if (changeAdresStatusDto.Status == AdresStatusEnum.InGebruik)
        {
            return WorkflowActieEnum.ApproveAddress;
        }

        return WorkflowActieEnum.RejectAddress;
    }
}