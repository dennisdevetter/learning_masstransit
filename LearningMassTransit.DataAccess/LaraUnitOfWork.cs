using System;
using Correlate;
using LearningMassTransit.Domain;
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
        IRepository<Ticket, string> tickets,
        IApplicationContext applicationContext) : base(context, correlationContextAccessor, applicationContext)
    {
        Tickets = tickets;
    }

    public IRepository<Ticket, string> Tickets { get; }
}