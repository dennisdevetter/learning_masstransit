using System;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Contracts.Enums;

namespace LearningMassTransit.Application.Services;

public interface IAtomaireActieService
{
    Task<AtomaireActieOutputDto> Execute(WorkflowActieEnum actie, string data, Func<Guid, Task<TicketDto>> executor, CancellationToken cancellationToken);
}