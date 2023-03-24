using System.Threading.Tasks;
using LearningMassTransit.Contracts.Commands;
using MassTransit;
using MediatR;

namespace LearningMassTransit.Consumers;

public class ChangeAdresStatusCommandConsumer : IConsumer<ChangeAdresStatusCommand>
{
    private readonly IMediator _mediator;

    public ChangeAdresStatusCommandConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ChangeAdresStatusCommand> context)
    {
        await _mediator.Send(context.Message, context.CancellationToken);
    }
}