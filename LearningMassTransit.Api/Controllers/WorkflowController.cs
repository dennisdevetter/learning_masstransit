using System.Threading.Tasks;
using LearningMassTransit.Contracts.Requests;
using LearningMassTransit.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningMassTransit.Api.Controllers;

[ApiController]
[Route("workflows")]
public class WorkflowController : AppController<WorkflowController>
{
    private readonly IMediator _mediator;

    public WorkflowController(IMediator mediator, ILogger<WorkflowController> logger) : base(mediator, logger)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetWorkflows()
    {
        var request = new GetWorkflowsRequest();

        return await ExecuteRequest<GetWorkflowsRequest, GetWorkflowsResponse>(request);
    }
}