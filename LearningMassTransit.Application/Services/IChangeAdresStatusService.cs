using System;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Dtos;

namespace LearningMassTransit.Application.Services;

public interface IChangeAdresStatusService
{
    Task<TicketDto> ChangeAdresStatus(ChangeAdresStatusDto changeAdresStatusDto, Guid correlationId, CancellationToken cancellationToken);
}