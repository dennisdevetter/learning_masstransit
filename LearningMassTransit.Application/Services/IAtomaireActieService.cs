using System;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Dtos;

namespace LearningMassTransit.Application.Services;

public interface IAtomaireActieService
{
    Task<AtomaireActieOutputDto> Execute(string data, Func<Guid, Task<TicketDto>> executor, CancellationToken cancellationToken);
}