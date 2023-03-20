using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Contracts.Requests;
using LearningMassTransit.DataAccess;
using LearningMassTransit.Domain.Blogging;
using MediatR;

namespace LearningMassTransit.Application.Handlers;

public class GetBlogsRequestHandler : IRequestHandler<GetBlogsRequest, IList<BlogDto>>
{
    private readonly LaraDbContext _db;

    public GetBlogsRequestHandler(LaraDbContext db)
    {
        _db = db;
    }

    public async Task<IList<BlogDto>> Handle(GetBlogsRequest request, CancellationToken cancellationToken)
    {
        var blogs = _db.Blogs.ToList();

        var dtos = blogs.Select(MapToBlogDto).ToList();

        return dtos;
    }

    private static BlogDto MapToBlogDto(Blog blog)
    {
        var blogDto = new BlogDto
        {
            BlogId = blog.BlogId,
            Url = blog.Url
        };

        var postDtos = blog.Posts?.Select(MapToPostDto).ToList();

        if (postDtos != null)
        {
            blogDto.Posts.AddRange(postDtos);
        }

        return blogDto;
    }

    private static PostDto MapToPostDto(Post post)
    {
        return new PostDto
        {
            BlogId = post.BlogId,
            Content = post.Content,
            Title = post.Title,
            PostId = post.PostId
        };
    }
}