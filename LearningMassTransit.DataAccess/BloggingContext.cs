using LearningMassTransit.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningMassTransit.DataAccess;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    public BloggingContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=lara;User Id=postgres;Password=postgres;");
}