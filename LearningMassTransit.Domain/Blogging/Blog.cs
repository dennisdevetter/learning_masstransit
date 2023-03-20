using LearningMassTransit.Infrastructure.Database;

namespace LearningMassTransit.DataAccess.Blogging;

public class Blog : Entity<int>
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; } = new();
}