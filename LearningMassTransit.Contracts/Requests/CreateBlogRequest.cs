using MediatR;

namespace LearningMassTransit.Contracts.Requests;

public class CreateBlogRequest : IRequest<string>
{
    public string Url { get; }

    public CreateBlogRequest(string url)
    {
        Url = url;
    }
}