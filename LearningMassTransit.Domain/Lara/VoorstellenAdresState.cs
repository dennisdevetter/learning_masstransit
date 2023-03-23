using System;
using MassTransit;

namespace LearningMassTransit.Domain.Lara;

public class VoorstellenAdresState : SagaStateMachineInstance
{
    public Guid WorkflowId { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid CorrelationId { get; set; }
    public string UserId { get; set; }
    public string CurrentState { get; set; }
    public string Data { get; set; }
    public byte[] RowVersion { get; set; }
}