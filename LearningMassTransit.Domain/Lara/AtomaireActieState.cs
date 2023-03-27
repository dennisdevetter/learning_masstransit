using System;
using MassTransit;

namespace LearningMassTransit.Domain.Lara;

public class AtomaireActieState : SagaStateMachineInstance
{
    public Guid WorkflowId { get; set; }
    public string Actie { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public byte[] RowVersion { get; set; }
    public virtual Workflow Workflow { get; set; }
}