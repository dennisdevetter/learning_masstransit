using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Contracts.Requests;
using LearningMassTransit.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearningMassTransit.Api.Controllers;

[ApiController]
public class BeheerAdressenController : AppController<BeheerAdressenController>
{
    private readonly IMediator _mediator;

    public BeheerAdressenController(IMediator mediator, ILogger<BeheerAdressenController> logger) : base(mediator, logger)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("voorstellen")]
    public async Task<IActionResult> CreateAdresVoorstel([FromBody] CreateAdresVoorstelDto createAdresVoorstelDto)
    {
        var request = new CreateAdresVoorstelRequest(createAdresVoorstelDto);

        return await ExecuteRequest<CreateAdresVoorstelRequest, CreateAdresVoorstelResponse>(request, response => new AcceptedResult(string.Empty, response));
    }
}