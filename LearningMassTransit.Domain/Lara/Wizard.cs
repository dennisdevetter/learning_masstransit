using System;
using System.Collections.Generic;
using LearningMassTransit.Infrastructure.Database;

namespace LearningMassTransit.Domain.Lara;

public class Wizard : Entity<Guid>
{
    public Guid WizardId { get; set; }
    public string Kind { get; set; }
    public string UserId { get; set; }
    public virtual IList<WizardStep> Steps { get; set; }
    public DateTime CreationDate { get; set; }
    public WizardStatusEnum Status { get; set; }
}