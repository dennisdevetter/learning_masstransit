using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearningMassTransit.Api.Controllers;

[ApiController]
[Route("blogging")]
public class BloggingController : AppController<BloggingController>
{
    public BloggingController(IMediator mediator, ILogger<BloggingController> logger) : base(mediator, logger)
    {
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var request = new GetBlogsRequest();
        return await ExecuteRequest<GetBlogsRequest, IList<BlogDto>>(request);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create()
    {
        var request = new CreateBlogRequest("http://blogs.msdn.com/adonet");
        return await ExecuteRequest<CreateBlogRequest, string>(request);
    }
}