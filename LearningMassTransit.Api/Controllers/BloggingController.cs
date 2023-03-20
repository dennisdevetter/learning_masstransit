using LearningMassTransit.DataAccess;
using LearningMassTransit.DataAccess.Blogging;
using Microsoft.AspNetCore.Mvc;

namespace LearningMassTransit.Api.Controllers;

[ApiController]
[Route("blogging")]
public class BloggingController : ControllerBase
{
    private readonly ILogger<BloggingController> _logger;
    private readonly LaraDbContext _db;

    public BloggingController(ILogger<BloggingController> logger, LaraDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    [HttpGet]
    public IActionResult Get()
    {

        var blogs = _db.Blogs.ToList();

        return Ok(blogs);
    }

    [HttpPost]
    [Route("create")]
    public IActionResult Create()
    {
        _db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
        _db.SaveChanges();

        return Ok("created");
    }
}