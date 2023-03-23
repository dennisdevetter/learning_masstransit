using MassTransit;
using System.Threading.Tasks;

namespace LearningMassTransit.Infrastructure.Messaging.Filters;

public class ApplicationBusConsumerFilter<T> : IFilter<ConsumeContext<T>> where T : class
{
    private readonly IMassTransitApplicationBus _massTransitApplicationBus;

    public ApplicationBusConsumerFilter(IMassTransitApplicationBus massTransitApplicationBus)
    {
        _massTransitApplicationBus = massTransitApplicationBus;
    }

    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        _massTransitApplicationBus.DefineEndpoints(context, context);
        await next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
        context.CreateFilterScope($"application-bus-consumer");

    }
}