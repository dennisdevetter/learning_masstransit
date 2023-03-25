using LearningMassTransit.DataAccess.Constants;
using LearningMassTransit.Domain.Lara;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningMassTransit.DataAccess.Configuration;

public class WorkflowConfiguration : IEntityTypeConfiguration<Workflow>
{
    public void Configure(EntityTypeBuilder<Workflow> builder)
    {
        builder
            .ToTable("Workflow", DatabaseSchemas.Lara)
            .HasKey(c => c.WorkflowId);

        builder.Property(b => b.WorkflowId)
            .IsRequired()
            .ValueGeneratedNever();
        
        builder.Property(b => b.UserId).IsRequired();
        builder.Property(b => b.CreationDate).IsRequired();
        builder.Property(b => b.WorkflowType).IsRequired();
        builder.Property(b => b.Data).IsRequired();

        builder.HasOne(b => b.VoorstellenAdresState).WithOne(b => b.Workflow).OnDelete(DeleteBehavior.Cascade);
        
        builder.Navigation(b => b.VoorstellenAdresState)
            .UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}