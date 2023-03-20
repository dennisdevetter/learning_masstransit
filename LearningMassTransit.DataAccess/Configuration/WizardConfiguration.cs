using LearningMassTransit.DataAccess.Constants;
using LearningMassTransit.Domain.Lara;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningMassTransit.DataAccess.Configuration;

public class WizardConfiguration : IEntityTypeConfiguration<Wizard>
{
    public void Configure(EntityTypeBuilder<Wizard> builder)
    {
        builder
            .ToTable("Wizards", DatabaseSchemas.Lara)
            .HasKey(c => c.WizardId);

        builder.Property(b => b.WizardId)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(b => b.Kind).IsRequired();
        builder.Property(b => b.CreationDate).IsRequired();
        builder.Property(b => b.Status).IsRequired();
        builder.Property(b => b.UserId).IsRequired();

        builder.HasMany(x => x.Steps).WithOne(x => x.Wizard).HasForeignKey(x => x.WizardId).OnDelete(DeleteBehavior.Cascade);
    }
}