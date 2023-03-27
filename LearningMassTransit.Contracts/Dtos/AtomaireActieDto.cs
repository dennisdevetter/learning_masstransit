using System;
using LearningMassTransit.Contracts.Enums;

namespace LearningMassTransit.Contracts.Dtos;

public class AtomaireActieDto
{
    public ActieEnum Actie { get; set; }
    public Guid WorkflowId { get; set; }
    public string TicketId { get; set;}
}