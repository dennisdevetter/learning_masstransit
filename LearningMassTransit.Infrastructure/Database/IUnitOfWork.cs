namespace LearningMassTransit.Infrastructure.Database;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync(CancellationToken cancellationToken);

    public IUnitOfWork BeginTransaction();
    public Task Rollback(CancellationToken cancellationToken);
    public Task Commit(CancellationToken cancellationToken);
}