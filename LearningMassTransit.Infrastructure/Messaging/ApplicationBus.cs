using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Transactions;

namespace LearningMassTransit.Infrastructure.Messaging;

public class ApplicationBus : IMassTransitApplicationBus
{
    private readonly IBus _bus;
    private readonly ITransactionalBus _transactionalBus;
    private IPublishEndpoint _publishEndpoint;
    private ISendEndpointProvider _sendEndpointProvider;

    public ApplicationBus(IBus bus, ITransactionalBus transactionalBus = null)
    {
        _bus = bus;
        _transactionalBus = transactionalBus;
        _sendEndpointProvider = transactionalBus;
        _publishEndpoint = transactionalBus;
    }

    public async Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class
    {
        if (_publishEndpoint != null)
        {
            await _publishEndpoint.Publish(message, cancellationToken);
        }
        else
        {
            await _bus.Publish(message, cancellationToken);
        }
    }

    public async Task Send<T>(T message, CancellationToken cancellationToken = default) where T : class
    {
        if (_sendEndpointProvider != null)
        {
            await _sendEndpointProvider.Send(message, cancellationToken);
        }
        else
        {
            await _bus.Send(message, cancellationToken);
        }
    }

    public async Task ReleaseTransaction()
    {
        if (_transactionalBus != null)
        {
            await _transactionalBus.Release();
        }
    }

    public void DefineEndpoints(ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint)
    {
        _sendEndpointProvider = sendEndpointProvider;
        _publishEndpoint = publishEndpoint;
    }
}