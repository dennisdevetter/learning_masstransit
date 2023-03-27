using LearningMassTransit.DataAccess.Constants;
using LearningMassTransit.Domain.Lara;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningMassTransit.DataAccess.Configuration;

public class AtomaireActieStateConfiguration : IEntityTypeConfiguration<AtomaireActieState>
{
    public void Configure(EntityTypeBuilder<AtomaireActieState> builder)
    {
        builder
            .ToTable("AtomaireActieState", DatabaseSchemas.Lara)
            .HasKey(c => c.WorkflowId);

        builder.Property(b => b.WorkflowId)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(b => b.CurrentState).IsRequired();
        builder.Property(b => b.CreationDate).IsRequired();
        builder.Property(b => b.CorrelationId).IsRequired();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.HasOne(b => b.Workflow).WithOne(b => b.AtomaireActieState);
        
        builder.Navigation(b => b.Workflow)
            .UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}