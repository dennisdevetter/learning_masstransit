using System;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Dtos;
using ActieEnum = LearningMassTransit.Contracts.Enums.ActieEnum;

namespace LearningMassTransit.Application.Services;

public interface ITicketService
{
    Task<TicketDto> CreateTicket(string ticketId, ActieEnum actie, Guid correlationId,  CancellationToken cancellationToken);
}