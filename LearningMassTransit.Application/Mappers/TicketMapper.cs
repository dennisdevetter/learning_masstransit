using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Domain.Lara;
using ActieEnum = LearningMassTransit.Contracts.Enums.ActieEnum;
using TicketStatusEnum = LearningMassTransit.Contracts.Enums.TicketStatusEnum;

namespace LearningMassTransit.Application.Mappers;

public static class TicketMapper
{
    public static TicketDto MapToTicketDto(Ticket ticket)
    {
        return new TicketDto
        {
            Status = (TicketStatusEnum)ticket.Status,
            CreationDate = ticket.CreationDate,
            Actie = (ActieEnum)ticket.Actie,
            CorrelationId = ticket.CorrelationId,
            ModifiedDate = ticket.ModifiedDate,
            Result = ticket.Result,
            TicketId = ticket.TicketId
        };
    }
}