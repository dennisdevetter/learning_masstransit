using System.Collections.Generic;
using System.Reflection.Metadata;

namespace LearningMassTransit.Contracts.Dtos;

public class BlogDto
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public virtual List<PostDto> Posts { get; } = new();
}