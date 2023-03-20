using LearningMassTransit.Infrastructure.Database;

namespace LearningMassTransit.DataAccess.Blogging;

public class Post : Entity<int>
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}