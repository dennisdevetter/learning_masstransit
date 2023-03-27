using System.Threading.Tasks;
using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Contracts.Requests;
using LearningMassTransit.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningMassTransit.Api.Controllers;

[ApiController]
[Route("api/v1/beheer/adressen")]
public class BeheerAdressenController : AppController<BeheerAdressenController>
{
    public BeheerAdressenController(IMediator mediator, ILogger<BeheerAdressenController> logger) : base(mediator, logger)
    {
    }

    [HttpPost]
    [Route("nieuwadresmetstatus")]
    public async Task<IActionResult> NieuwAdresMetStatus([FromBody] CreateAdresVoorstelMetStatusDto createAdresVoorstelMetStatusDto)
    {
        var request = new CreateAdresVoorstelMetStatusRequest(createAdresVoorstelMetStatusDto);

        return await ExecuteRequest<CreateAdresVoorstelMetStatusRequest, CreateAdresVoorstelMetStatusResponse>(request, response => new AcceptedResult(string.Empty, response));
    }

    [HttpPost]
    [Route("nieuwadres")]
    public async Task<IActionResult> NieuwAdres([FromBody] CreateAdresVoorstelDto createAdresVoorstelDto)
    {
        var request = new CreateAdresVoorstelRequest(createAdresVoorstelDto);

        return await ExecuteRequest<CreateAdresVoorstelRequest, CreateAdresVoorstelResponse>(request);
    }
}