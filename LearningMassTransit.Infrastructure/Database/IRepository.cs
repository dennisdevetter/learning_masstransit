using System.Linq.Expressions;
using LearningMassTransit.Infrastructure.Database.Hints;

namespace LearningMassTransit.Infrastructure.Database;

public interface IRepository<TEntity, TId> where TEntity : class
{
    Task<IEnumerable<TEntity>> All(CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> All<TOrderBy>(Expression<Func<TEntity, TOrderBy>> order, OrderDirection direction, CancellationToken cancellationToken);
    Task<TEntity> GetById(TId id, CancellationToken cancellationToken);
    Task<bool> Add(TEntity entity, CancellationToken cancellationToken);
    Task<bool> Update(TEntity entity);
    Task<bool> Delete(TId id, CancellationToken cancellationToken);
    Task<bool> Delete(TEntity entity, CancellationToken cancellationToken);
    Task<bool> DeleteRange(IEnumerable<TEntity> entities);
    Task<IQueryable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    IRepository<TEntity, TId> WithHint(HintBase hint);
}