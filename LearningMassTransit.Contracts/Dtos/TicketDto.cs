using System;
using LearningMassTransit.Contracts.Enums;

namespace LearningMassTransit.Contracts.Dtos;

public class TicketDto
{
    public string TicketId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public Guid CorrelationId { get; set; }
    public ActieEnum Actie { get; set; }
    public string Result { get; set; }
    public TicketStatusEnum Status { get; set; }
}