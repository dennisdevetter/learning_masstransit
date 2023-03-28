using System;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Dtos;
using WorkflowActieEnum = LearningMassTransit.Contracts.Enums.WorkflowActieEnum;

namespace LearningMassTransit.Application.Services;

public interface ITicketService
{
    Task<TicketDto> CreateTicket(string ticketId, WorkflowActieEnum actie, Guid correlationId,  CancellationToken cancellationToken);
}