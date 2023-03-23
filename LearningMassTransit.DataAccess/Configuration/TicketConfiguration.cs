using LearningMassTransit.DataAccess.Constants;
using LearningMassTransit.Domain.Lara;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningMassTransit.DataAccess.Configuration;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder
            .ToTable("Ticket", DatabaseSchemas.Lara)
            .HasKey(c => c.TicketId);

        builder.Property(b => b.TicketId)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(b => b.Actie).IsRequired();
        builder.Property(b => b.CreationDate).IsRequired();
        builder.Property(b => b.ModifiedDate);
        builder.Property(b => b.CorrelationId).IsRequired();
        builder.Property(b => b.Status).IsRequired();
        builder.Property(b => b.Result);
        builder.Property(b => b.TicketId);
    }
}