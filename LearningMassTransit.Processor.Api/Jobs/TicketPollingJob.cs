using System;
using System.Linq;
using System.Threading.Tasks;
using LearningMassTransit.Domain;
using LearningMassTransit.Domain.Lara;
using LearningMassTransit.Infrastructure.Messaging;
using LearningMassTransit.Messaging.Lara;
using Quartz;

namespace LearningMassTransit.Processor.Api.Jobs;

public class TicketPollingJob : IJob
{
    private readonly ILaraUnitOfWork _laraUnitOfWork;
    private readonly IApplicationBus _applicationBus;

    public TicketPollingJob(ILaraUnitOfWork laraUnitOfWork, IApplicationBus applicationBus)
    {
        _laraUnitOfWork = laraUnitOfWork;
        _applicationBus = applicationBus;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await InnerExecute(context);
    }

    private async Task InnerExecute(IJobExecutionContext context)
    {
        var ticketsToProcess = await _laraUnitOfWork.Tickets.Find(x => x.Status == TicketStatusEnum.Waiting, context.CancellationToken);

        if (ticketsToProcess.Any())
        {
            foreach (var ticket in ticketsToProcess)
            {
                await ProcessTicket(context, ticket);
            }

            await _laraUnitOfWork.SaveChangesAsync(context.CancellationToken);
        }
    }

    private async Task ProcessTicket(IJobExecutionContext context, Ticket ticket)
    {
        switch (ticket.Actie)
        {
            case ActieEnum.None:
                break;
            case ActieEnum.ProposeStreetName:
                await ProcessProposeStreetName(context, ticket);
                break;
            case ActieEnum.ApproveAddress:
            case ActieEnum.RejectAddress:
                await ProcessChangeAddressStatus(context, ticket);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private async Task ProcessProposeStreetName(IJobExecutionContext context, Ticket ticket)
    {
        // TODO get status of ticket from basisregisters

        var ticketResult = new
        {
            result = "12345",
            status = "Completed"
        };

        if (ticketResult.status != "Completed")
        {
            return;
        }

        // update ticket
        ticket.SetComplete(ticketResult.result);

        // notify saga
        await _applicationBus.Publish(new ProposeStreetNameTicketCompletedEvent
        {
            ObjectId = ticketResult.result,
            TicketId = ticket.TicketId,
            CorrelationId = ticket.CorrelationId
        }, context.CancellationToken);
    }

    private async Task ProcessChangeAddressStatus(IJobExecutionContext context, Ticket ticket)
    {
        // TODO get status of ticket from basisregisters

        var ticketResult = new
        {
            result = "Changed",
            status = "Completed"
        };

        if (ticketResult.status != "Completed")
        {
            return;
        }

        // update ticket
        ticket.SetComplete(ticketResult.result);

        // notify saga
        await _applicationBus.Publish(new AdresStatusTicketCompletedEvent
        {
            TicketId = ticket.TicketId,
            CorrelationId = ticket.CorrelationId
        }, context.CancellationToken);
    }
}