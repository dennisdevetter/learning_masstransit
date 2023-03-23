using System.Linq;
using System.Threading.Tasks;
using LearningMassTransit.Domain;
using LearningMassTransit.Domain.Lara;
using LearningMassTransit.Messaging.Lara;
using MassTransit;
using Quartz;

namespace LearningMassTransit.Processor.Api.Jobs;

public class TicketPollingJob : IJob
{
    private readonly IBus _bus;
    private readonly ILaraUnitOfWork _laraUnitOfWork;

    public TicketPollingJob(ILaraUnitOfWork laraUnitOfWork, IBus bus)
    {
        _bus = bus;
        _laraUnitOfWork = laraUnitOfWork;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var tickets = await _laraUnitOfWork.Tickets.All(context.CancellationToken);

        // polling logic to async api

        var completedTickets = tickets.Where(x => x.Status == TicketStatusEnum.Completed).ToList();

        if (completedTickets.Any())
        {
            foreach (var ticket in completedTickets)
            {
                // notify saga
                await _bus.Publish(new TicketCompleted
                {
                    TicketId = ticket.TicketId,
                    CorrelationId = ticket.CorrelationId
                }, context.CancellationToken);
            }
        }
    }
}