using System;
using LearningMassTransit.Infrastructure.Database;

namespace LearningMassTransit.Domain.Lara;

public class Workflow: Entity<Guid>
{
    public Guid WorkflowId { get; set; }
    public string UserId { get; set; }
    public DateTime CreationDate { get; set; }
    public WorkflowActionEnum WorkflowAction { get; set; }
    public WorkflowTypeEnum WorkflowType { get; set; }
    public string Data { get; set; }

    public virtual AtomaireActieState AtomaireActieState { get; set; }
    public virtual VoorstellenAdresState VoorstellenAdresState { get; set; }
}