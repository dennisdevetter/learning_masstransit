using System;
using LearningMassTransit.Infrastructure.Database;

namespace LearningMassTransit.Domain.Lara;

public class WizardStep : Entity<Guid>
{
    public Guid WizardStepId { get; set; }
    public int StepNr { get; set; }
    public string StepData { get; set; }
    public string StepType { get; set; }
    public Guid WizardId { get; set; }
    public Wizard Wizard { get; set; }
    public string TicketId { get; set; }
    public string TicketData { get; set; }
    public DateTime CreationDate { get; set; }
}