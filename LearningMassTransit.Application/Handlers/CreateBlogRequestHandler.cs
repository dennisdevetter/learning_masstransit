using LearningMassTransit.Contracts.Requests;
using LearningMassTransit.DataAccess;
using LearningMassTransit.DataAccess.Blogging;
using MediatR;

namespace LearningMassTransit.Application.Handlers;

public class CreateBlogRequestHandler : IRequestHandler<CreateBlogRequest, string>
{
    private readonly LaraDbContext _db;

    public CreateBlogRequestHandler(LaraDbContext db)
    {
        _db = db;
    }

    public Task<string> Handle(CreateBlogRequest request, CancellationToken cancellationToken)
    {
        _db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
        _db.SaveChanges();

        return Task.FromResult("created");
    }
}