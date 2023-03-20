using System;
using LearningMassTransit.Domain.Blogging;
using LearningMassTransit.Domain.Lara;
using LearningMassTransit.Infrastructure.Database;

namespace LearningMassTransit.Domain;

public interface ILaraUnitOfWork : IUnitOfWork
{
    IRepository<Blog, int> Blogs { get; }
    IRepository<Post, int> Posts { get; }
    IRepository<Wizard, Guid> Wizards { get; }
}