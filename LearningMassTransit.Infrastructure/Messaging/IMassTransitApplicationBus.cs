using MassTransit;

namespace LearningMassTransit.Infrastructure.Messaging;

public interface IMassTransitApplicationBus : IApplicationBus
{
    void DefineEndpoints(ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint);
}