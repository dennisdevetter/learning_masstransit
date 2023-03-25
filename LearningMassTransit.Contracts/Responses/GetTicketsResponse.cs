using System.Collections.Generic;
using LearningMassTransit.Contracts.Dtos;

namespace LearningMassTransit.Contracts.Responses;

public class GetTicketsResponse : ResponseOf<IList<TicketDto>>
{
    public GetTicketsResponse(IList<TicketDto> tickets) : base(tickets)
    {
    }
}