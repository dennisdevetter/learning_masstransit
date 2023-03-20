using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Contracts.Requests;
using LearningMassTransit.DataAccess;
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

        var res=  blogs.Select(x => new BlogDto
        {
            BlogId = x.BlogId,
            Url = x.Url
        }).ToList();

        return res;
    }
}