using System;
using LearningMassTransit.Contracts.Enums;

namespace LearningMassTransit.Contracts.Dtos;

public class AtomaireActieOutputDto
{
    public WorkflowActieEnum Actie { get; set; }
    public string TicketId { get; set; }
    public Guid WorkflowId { get; set; }
}