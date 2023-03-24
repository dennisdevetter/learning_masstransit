using LearningMassTransit.Domain.Lara;
using LearningMassTransit.Infrastructure.Database;

namespace LearningMassTransit.Domain;

public interface ILaraUnitOfWork : IUnitOfWork
{
    IRepository<Ticket, string> Tickets { get; }
}