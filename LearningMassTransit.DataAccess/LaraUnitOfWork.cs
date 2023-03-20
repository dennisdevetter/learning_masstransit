using System;
using Correlate;
using LearningMassTransit.Domain;
using LearningMassTransit.Domain.Blogging;
using LearningMassTransit.Domain.Lara;
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
        IRepository<Wizard, Guid> wizards,
        IApplicationContext applicationContext) : base(context, correlationContextAccessor, applicationContext)
    {
        Blogs = blogs;
        Posts = posts;
        Wizards = wizards;
    }

    public IRepository<Blog, int> Blogs { get; }
    public IRepository<Post, int> Posts { get; }
    public IRepository<Wizard, Guid> Wizards { get; }
}