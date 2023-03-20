using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Infrastructure.Database;
using LearningMassTransit.Infrastructure.Database.Hints;
using LearningMassTransit.Infrastructure.EntityFramework.Hints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LearningMassTransit.Infrastructure.EntityFramework
{
    public class Repository<TContext, TEntity, TId> : IRepository<TEntity, TId> where TContext : DbContext where TEntity : Entity<TId>
    {
        internal DbSet<TEntity> DbSet;
        private readonly IEnumerable<IInterceptor> _interceptors;

        public Repository(TContext dbContext, IEnumerable<IInterceptor> interceptors = null)
        {
            DbSet = dbContext.Set<TEntity>();
            _interceptors = interceptors;
        }

        public async Task<IEnumerable<TEntity>> All<TOrderBy>(Expression<Func<TEntity, TOrderBy>> order, OrderDirection direction, CancellationToken cancellationToken)
        {
            return direction == OrderDirection.Descending
                ? await DbSet.OrderByDescending(order).ToListAsync(cancellationToken)
                : await DbSet.OrderBy(order).ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity> GetById(TId id, CancellationToken cancellationToken)
        {
            return await DbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual async Task<bool> Add(TEntity entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            return true;
        }

        public virtual Task<bool> Update(TEntity entity)
        {
            DbSet.Update(entity);
            return Task.FromResult(true);
        }

        public virtual async Task<bool> Delete(TId id, CancellationToken cancellationToken)
        {
            var entity = await GetById(id, cancellationToken);

            if (entity == null) return false;

            DbSet.Remove(entity);

            return true;
        }

        public virtual Task<bool> Delete(TEntity entity, CancellationToken cancellationToken)
        {
            if (entity == null) return Task.FromResult(false);

            DbSet.Remove(entity);

            return Task.FromResult(true);
        }

        public virtual Task<bool> DeleteRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);

            return Task.FromResult(true);
        }

        public virtual async Task<IEnumerable<TEntity>> All(CancellationToken cancellationToken)
        {
            return await DbSet.ToListAsync(cancellationToken);
        }

        public Task<IQueryable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return Task.FromResult(DbSet.Where(predicate));
        }

        public IRepository<TEntity, TId> WithHint(HintBase hint)
        {
            if (_interceptors != null)
            {
                var hintsInterceptor = _interceptors.FirstOrDefault(i => i.GetType() == typeof(HintsInterceptor));
                
                if (hintsInterceptor == null)
                {
                    throw new ApplicationException("No hints interceptor registered");
                }

                ((HintsInterceptor)hintsInterceptor).Add(hint);
            }
            
            return this;
        }
    }
}
