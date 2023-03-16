using LearningMassTransit.DataAccess;
using LearningMassTransit.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningMassTransit.Api.Controllers;

[ApiController]
[Route("blogging")]
public class BloggingController : ControllerBase
{
    private readonly ILogger<BloggingController> _logger;

    public BloggingController(ILogger<BloggingController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var db = new BloggingContext();

        var blogs = db.Blogs.ToList();

        return Ok(blogs);
    }

    [HttpPost]
    [Route("create")]
    public IActionResult Create()
    {
        var db = new BloggingContext();

        db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
        db.SaveChanges();

        return Ok("created");
    }
}