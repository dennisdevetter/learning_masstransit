using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Application.Services;
using LearningMassTransit.Contracts.Commands;
using LearningMassTransit.Contracts.Dtos;
using MediatR;

namespace LearningMassTransit.Application.CommandHandlers;

public class ChangeAdresStatusCommandHandler : IRequestHandler<ChangeAdresStatusCommand>
{
    private readonly IChangeAdresStatusService _changeAdresStatusService;

    public ChangeAdresStatusCommandHandler(IChangeAdresStatusService changeAdresStatusService)
    {
        _changeAdresStatusService = changeAdresStatusService;
    }

    public async Task<Unit> Handle(ChangeAdresStatusCommand command, CancellationToken cancellationToken)
    {
        var dto = new ChangeAdresStatusDto
        {
            ObjectId = command.ObjectId,
            Status = command.Status 
        };

        await _changeAdresStatusService.ChangeAdresStatus(dto, command.CorrelationId, cancellationToken);

        return Unit.Value;
    }
}