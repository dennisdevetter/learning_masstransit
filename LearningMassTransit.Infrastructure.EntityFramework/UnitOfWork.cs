using Correlate;
using LearningMassTransit.Infrastructure.Database;
using LearningMassTransit.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LearningMassTransit.Infrastructure.EntityFramework;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private readonly ICorrelationContextAccessor _correlationContextAccessor;
    private readonly IApplicationContext _applicationContext;
    private IDbContextTransaction _transaction;
    private bool _disposed;
    private bool _externalTransaction;

    public UnitOfWork(DbContext context, ICorrelationContextAccessor correlationContextAccessor, IApplicationContext applicationContext)
    {
        _context = context;
        _correlationContextAccessor = correlationContextAccessor;
        _applicationContext = applicationContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        Guid? correlationId = Guid.TryParse(_correlationContextAccessor.CorrelationContext?.CorrelationId, out var temp) ? temp != Guid.Empty ? temp : null : null;

        foreach (var entityEntry in _context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
        {
            TryAssignAuditData(entityEntry.Entity, correlationId);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    private void TryAssignAuditData(object entity, Guid? correlationId)
    {
        //if (entity is IAuditEntity auditEntity)
        //{
        //    var userid = (_applicationContext.KlipUserId != null && _applicationContext.KlipUserId != Guid.Empty) ? _applicationContext.KlipUserId.ToString() : _applicationContext.UserId.ToString();
        //    auditEntity.SetAuditInfo(userid, DateTime.Now, correlationId);
        //}
    }

    public IUnitOfWork BeginTransaction()
    {
        _externalTransaction = _context.Database.CurrentTransaction != null;
        _transaction = _context.Database.CurrentTransaction ?? _context.Database.BeginTransaction();
        return this;
    }

    public async Task Rollback(CancellationToken cancellationToken)
    {
        if (_transaction != null && !_externalTransaction)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
        }
            
        _context.ChangeTracker.Clear();
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        if (_transaction != null && !_externalTransaction)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
        }
    }

    public void Dispose()
    {
        if (!_disposed && !_externalTransaction)
        {
            _transaction?.Dispose();
            _disposed = true;
        }
    }
}