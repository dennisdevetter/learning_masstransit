using LearningMassTransit.DataAccess.Constants;
using LearningMassTransit.Domain.Lara;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningMassTransit.DataAccess.Configuration;

public class WizardStepConfiguration : IEntityTypeConfiguration<WizardStep>
{
    public void Configure(EntityTypeBuilder<WizardStep> builder)
    {
        builder
            .ToTable("WizardSteps", DatabaseSchemas.Lara)
            .HasKey(c => c.WizardStepId);

        builder.Property(b => b.WizardStepId)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(b => b.StepNr).IsRequired();
        builder.Property(b => b.StepData).IsRequired();
        builder.Property(b => b.StepType).IsRequired();
        builder.Property(b => b.CreationDate).IsRequired();
        builder.Property(b => b.TicketId);
    }
}