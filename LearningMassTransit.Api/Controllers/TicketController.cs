using System.Threading.Tasks;
using LearningMassTransit.Contracts.Requests;
using LearningMassTransit.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningMassTransit.Api.Controllers;

[ApiController]
[Route("tickets")]
public class TicketController : AppController<TicketController>
{
    private readonly IMediator _mediator;

    public TicketController(IMediator mediator, ILogger<TicketController> logger) : base(mediator, logger)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetTickets()
    {
        var request = new GetTicketsRequest();

        return await ExecuteRequest<GetTicketsRequest, GetTicketsResponse>(request);
    }
}