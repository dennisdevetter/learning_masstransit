using Microsoft.AspNetCore.Mvc;

namespace LearningMassTransit.Processor.Api.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("processor running");
    }
}