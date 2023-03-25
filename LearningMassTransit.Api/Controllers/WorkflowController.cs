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
    public WorkflowController(IMediator mediator, ILogger<WorkflowController> logger) : base(mediator, logger)
    {
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetWorkflows()
    {
        var request = new GetWorkflowsRequest();

        return await ExecuteRequest<GetWorkflowsRequest, GetWorkflowsResponse>(request);
    }
}