using LearningMassTransit.DataAccess.Blogging;
using LearningMassTransit.Infrastructure.Database;

namespace LearningMassTransit.Domain;

public interface ILaraUnitOfWork : IUnitOfWork
{
    IRepository<Blog, int> Blogs { get; }
    IRepository<Post, int> Posts { get; }
}