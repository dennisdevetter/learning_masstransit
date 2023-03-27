using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Application.Services;
using LearningMassTransit.Contracts.Commands;
using MediatR;

namespace LearningMassTransit.Application.CommandHandlers;

public class CreateAdresVoorstelCommandHandler : IRequestHandler<CreateAdresVoorstelCommand>
{
    private readonly ICreateAdresVoorstelService _createAdresVoorstelService;

    public CreateAdresVoorstelCommandHandler(ICreateAdresVoorstelService createAdresVoorstelService)
    {
        _createAdresVoorstelService = createAdresVoorstelService;
    }

    public async Task<Unit> Handle(CreateAdresVoorstelCommand command, CancellationToken cancellationToken)
    {
        await _createAdresVoorstelService.CreateAdresVoorstel(command.Adres, command.CorrelationId, cancellationToken);

        return Unit.Value;
    }
}
