using System;
using LearningMassTransit.Infrastructure.Database;

namespace LearningMassTransit.Domain.Lara;

public class Ticket: Entity<string>
{
    public string TicketId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public Guid CorrelationId { get; set; }
    public WorkflowActieEnum Actie { get; set; }
    public string Result { get; set; }
    public TicketStatusEnum Status { get; set; }

    public void SetComplete(string result)
    {
        Status = TicketStatusEnum.Completed;
        Result = result;
        ModifiedDate = DateTime.UtcNow;
    }
}