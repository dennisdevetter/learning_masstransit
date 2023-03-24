using MassTransit;
using System.Threading.Tasks;

namespace LearningMassTransit.Infrastructure.Messaging.Filters;

public class ExceptionsFilter : IFilter<ExceptionReceiveContext>
{
    public Task Send(ExceptionReceiveContext context, IPipe<ExceptionReceiveContext> next)
    {
        var exception = context.Exception;

        return Task.CompletedTask;
    }

    public void Probe(ProbeContext context)
    {
        context.CreateFilterScope(nameof(ExceptionsFilter));
    }
}