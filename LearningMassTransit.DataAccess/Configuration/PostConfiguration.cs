using LearningMassTransit.DataAccess.Constants;
using LearningMassTransit.Domain.Blogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningMassTransit.DataAccess.Configuration;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            .ToTable("Posts", DatabaseSchemas.Lara)
            .HasKey(c => c.PostId);

        builder.Property(b => b.PostId)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Title).HasMaxLength(255);
        builder.Property(b => b.Content).HasMaxLength(512);
    }
}