using System.Threading.Tasks;
using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Contracts.Requests;
using LearningMassTransit.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningMassTransit.Api.Controllers;

[ApiController]
[Route("voorstellen")]
public class BeheerAdressenController : AppController<BeheerAdressenController>
{
    public BeheerAdressenController(IMediator mediator, ILogger<BeheerAdressenController> logger) : base(mediator, logger)
    {
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateAdresVoorstel([FromBody] CreateAdresVoorstelDto createAdresVoorstelDto)
    {
        var request = new CreateAdresVoorstelRequest(createAdresVoorstelDto);

        return await ExecuteRequest<CreateAdresVoorstelRequest, CreateAdresVoorstelResponse>(request, response => new AcceptedResult(string.Empty, response));
    }
}