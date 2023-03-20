using MassTransit;

namespace LearningMassTransit.Consumers.Lara;

public class WizardCreatedConsumerDefinition : ConsumerDefinition<WizardCreatedConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<WizardCreatedConsumer> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
    }
}