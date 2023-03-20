using Correlate;
using LearningMassTransit.DataAccess.Blogging;
using LearningMassTransit.Domain;
using LearningMassTransit.Infrastructure.Database;
using LearningMassTransit.Infrastructure.EntityFramework;
using LearningMassTransit.Infrastructure.Security;

namespace LearningMassTransit.DataAccess;

public class LaraUnitOfWork : UnitOfWork, ILaraUnitOfWork
{
    public LaraUnitOfWork(
        LaraDbContext context, 
        ICorrelationContextAccessor correlationContextAccessor,
        IRepository<Blog, int> blogs,
        IRepository<Post, int> posts,
        IApplicationContext applicationContext) : base(context, correlationContextAccessor, applicationContext)
    {
        Blogs = blogs;
        Posts = posts;
    }

    public IRepository<Blog, int> Blogs { get; }
    public IRepository<Post, int> Posts { get; }
}