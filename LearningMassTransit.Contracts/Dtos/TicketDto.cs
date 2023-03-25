using System;

namespace LearningMassTransit.Contracts.Dtos;

public class TicketDto
{
    public string TicketId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public Guid CorrelationId { get; set; }
    public string Actie { get; set; }
    public string Result { get; set; }
    public string Status { get; set; }
}