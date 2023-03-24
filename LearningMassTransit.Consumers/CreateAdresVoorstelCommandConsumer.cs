using System.Threading.Tasks;
using LearningMassTransit.Contracts.Commands;
using MassTransit;
using MediatR;

namespace LearningMassTransit.Processor.Api.Consumers;

public class CreateAdresVoorstelCommandConsumer : IConsumer<CreateAdresVoorstelCommand>
{
    private readonly IMediator _mediator;

    public CreateAdresVoorstelCommandConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CreateAdresVoorstelCommand> context)
    {
        await _mediator.Send(context.Message, context.CancellationToken);
    }
}