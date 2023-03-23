using LearningMassTransit.DataAccess.Constants;
using LearningMassTransit.Domain.Lara;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningMassTransit.DataAccess.Configuration;

public class VoorstellenAdresStateConfiguration : IEntityTypeConfiguration<VoorstellenAdresState>
{
    public void Configure(EntityTypeBuilder<VoorstellenAdresState> builder)
    {
        builder
            .ToTable("VoorstellenAdresState", DatabaseSchemas.Lara)
            .HasKey(c => c.WorkflowId);

        builder.Property(b => b.WorkflowId)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(b => b.CurrentState).IsRequired();
        builder.Property(b => b.CreationDate).IsRequired();
        builder.Property(b => b.CorrelationId).IsRequired();
        builder.Property(b => b.UserId).IsRequired();
        builder.Property(b => b.Data);
        builder.Property(x => x.RowVersion).IsRowVersion();
    }
}