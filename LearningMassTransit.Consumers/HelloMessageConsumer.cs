using System.Threading.Tasks;
using LearningMassTransit.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace LearningMassTransit.Consumers;

public class HelloMessageConsumer : IConsumer<HelloMessage>
{
    readonly ILogger<HelloMessageConsumer> _logger;

    public HelloMessageConsumer(ILogger<HelloMessageConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<HelloMessage> context)
    {
        _logger.LogInformation($"Hello {context.Message.Name}");
        return Task.CompletedTask;
    }
}