using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Requests;
using LearningMassTransit.Domain;
using LearningMassTransit.Domain.Blogging;
using MediatR;

namespace LearningMassTransit.Application.Handlers;

public class CreateBlogRequestHandler : IRequestHandler<CreateBlogRequest, string>
{
    private readonly ILaraUnitOfWork _laraUnitOfWork;

    public CreateBlogRequestHandler(ILaraUnitOfWork laraUnitOfWork)
    {
        _laraUnitOfWork = laraUnitOfWork;
    }

    public async Task<string> Handle(CreateBlogRequest request, CancellationToken cancellationToken)
    {
        using (var transaction = _laraUnitOfWork.BeginTransaction())
        {
            var blog = new Blog { Url = "http://blogs.msdn.com/adonet" };

            await _laraUnitOfWork.Blogs.Add(blog, cancellationToken);

            await _laraUnitOfWork.SaveChangesAsync(cancellationToken);

            await transaction.Commit(cancellationToken);
        }

        return "created";
    }
}