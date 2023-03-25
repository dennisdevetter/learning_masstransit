﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Contracts.Requests;
using LearningMassTransit.Contracts.Responses;
using LearningMassTransit.Domain;
using LearningMassTransit.Domain.Lara;
using MediatR;

namespace LearningMassTransit.Application.Handlers;

public class GetTicketsRequestHandler : IRequestHandler<GetTicketsRequest, GetTicketsResponse>
{
    private readonly ILaraUnitOfWork _laraUnitOfWork;

    public GetTicketsRequestHandler(ILaraUnitOfWork laraUnitOfWork)
    {
        _laraUnitOfWork = laraUnitOfWork;
    }

    public async Task<GetTicketsResponse> Handle(GetTicketsRequest request, CancellationToken cancellationToken)
    {
        var tickets = (await _laraUnitOfWork.Tickets.Find(x => true, cancellationToken)).OrderByDescending(x => x.CreationDate).ToList();
        
        var dtos = tickets.Select(x => MapToDto(x)).ToList();

        return new GetTicketsResponse(dtos);
    }

    private TicketDto MapToDto(Ticket ticket)
    {
        return new TicketDto
        {
            CreationDate = ticket.CreationDate,
            CorrelationId = ticket.CorrelationId,
            Status = ticket.Status.ToString(),
            TicketId = ticket.TicketId,
            ModifiedDate = ticket.ModifiedDate,
            Actie = ticket.Actie.ToString(),
            Result = ticket.Result
        };
    }
}