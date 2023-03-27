using System;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Infrastructure.Messaging;
using LearningMassTransit.Messaging.Lara;
using ActieEnum = LearningMassTransit.Contracts.Enums.ActieEnum;

namespace LearningMassTransit.Application.Services;

public class CreateAdresVoorstelService : ICreateAdresVoorstelService
{
    private readonly IApplicationBus _applicationBus;
    private readonly ITicketService _ticketService;

    public CreateAdresVoorstelService(IApplicationBus applicationBus, ITicketService ticketService)
    {
        _applicationBus = applicationBus;
        _ticketService = ticketService;
    }

    public async Task<TicketDto> CreateAdresVoorstel(CreateAdresVoorstelDto createAdresVoorstelDto, Guid correlationId, CancellationToken cancellationToken)
    {
        // todo
        // do call to basisregisters

        var ticketId = Guid.NewGuid().ToString();

        var ticket = await _ticketService.CreateTicket(
            ticketId, 
            ActieEnum.ProposeStreetName, 
            correlationId, 
            cancellationToken
        );

        await _applicationBus.Publish(new AdresVoorstelCreatedEvent
        {
            TicketId = ticketId,
            CorrelationId = correlationId

        }, cancellationToken);

        return ticket;
    }
}