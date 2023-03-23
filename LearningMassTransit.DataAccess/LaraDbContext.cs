using LearningMassTransit.DataAccess.Constants;
using LearningMassTransit.Domain.Blogging;
using LearningMassTransit.Domain.Lara;
using Microsoft.EntityFrameworkCore;

namespace LearningMassTransit.DataAccess;

public class LaraDbContext : DbContext
{
    public LaraDbContext()
    {
        
    }

    public LaraDbContext(DbContextOptions<LaraDbContext> options) : base(options)
    {
    
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<VoorstellenAdresState> VoorstellenAdresStates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseSchemas.Lara);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}