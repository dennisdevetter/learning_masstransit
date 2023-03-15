using LearningMassTransit.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace LearningMassTransit.Consumers;

public class GettingStartedConsumer : IConsumer<HelloMessage>
{
    readonly ILogger<GettingStartedConsumer> _logger;

    public GettingStartedConsumer(ILogger<GettingStartedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<HelloMessage> context)
    {
        _logger.LogInformation($"Hello {context.Message.Name}");
        return Task.CompletedTask;
    }
}