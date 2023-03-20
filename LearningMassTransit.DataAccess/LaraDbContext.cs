using LearningMassTransit.DataAccess.Constants;
using LearningMassTransit.Domain.Blogging;
using LearningMassTransit.Domain.Lara;
using Microsoft.EntityFrameworkCore;

namespace LearningMassTransit.DataAccess;

public class LaraDbContext : DbContext
{
    private readonly string _connectionString;

    public LaraDbContext()
    {
        // only used to create database schemas via commandline
        // dotnet ef migrations add InitialCreate
        // dotnet ef database update
        _connectionString = "Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;Password=postgres;";
    }

    public LaraDbContext(DbContextOptions<LaraDbContext> options) : base(options)
    {
        _connectionString = null;
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Wizard> Wizards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseSchemas.Lara);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!string.IsNullOrWhiteSpace(_connectionString))
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}