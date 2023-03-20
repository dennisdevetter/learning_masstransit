using System.Reflection.Metadata;

namespace LearningMassTransit.Contracts.Dtos;

public class BlogDto
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<PostDto> Posts { get; } = new();
}