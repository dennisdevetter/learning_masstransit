using System.Threading;
using System.Threading.Tasks;

namespace LearningMassTransit.Infrastructure.Messaging;

public interface IApplicationBus
{
    Task Publish<T>(T message, CancellationToken cancellationToken) where T : class;
    Task Send<T>(T message, CancellationToken cancellationToken) where T : class;
    Task ReleaseTransaction();
}