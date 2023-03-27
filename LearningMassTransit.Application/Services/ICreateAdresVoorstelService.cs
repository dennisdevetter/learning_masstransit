using System;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Dtos;

namespace LearningMassTransit.Application.Services;

public interface ICreateAdresVoorstelService
{
    Task<TicketDto> CreateAdresVoorstel(CreateAdresVoorstelDto createAdresVoorstelDto, Guid correlationId, CancellationToken cancellationToken);
}