using LearningMassTransit.DataAccess.Blogging;
using LearningMassTransit.DataAccess.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningMassTransit.DataAccess.Configuration;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder
            .ToTable("Blogs", DatabaseSchemas.Lara)
            .HasKey(c => c.BlogId);

        builder.Property(b => b.BlogId)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Url);

        builder.HasMany(x => x.Posts).WithOne(x => x.Blog).HasForeignKey(x => x.BlogId).OnDelete(DeleteBehavior.Cascade);
    }
}