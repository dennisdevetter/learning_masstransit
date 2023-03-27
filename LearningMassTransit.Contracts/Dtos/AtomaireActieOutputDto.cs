using System;
using LearningMassTransit.Contracts.Enums;

namespace LearningMassTransit.Contracts.Dtos;

public class AtomaireActieOutputDto
{
    public ActieEnum Actie { get; set; }
    public string TicketId { get; set; }
    public Guid WorkflowId { get; set; }
}