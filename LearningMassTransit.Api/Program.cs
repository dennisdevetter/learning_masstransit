
using LearningMassTransit.Consumers;
using LearningMassTransit.DataAccess;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NSwag;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();
ConfigureApp();

await app.RunAsync();

void ConfigureApp()
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    // Configure the HTTP request pipeline.

    app.UseHttpsRedirection();

    app.UseOpenApi();
    app.UseSwaggerUi3();

    app.UseRouting();
    app.UseAuthorization();

    app.MapControllers();

    app.UseEndpoints(endpoints =>
    {

    });
}

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddEndpointsApiExplorer();
    services.AddControllers();
    services.AddOpenApiDocument(cfg => cfg.PostProcess = d =>
    {
        d.Info.Title = "Api";
        d.Info.Contact = new OpenApiContact
        {
            Name = "Sample",
        };
    });

    services.AddDbContext<BloggingContext>(options => options.UseNpgsql("Server=127.0.0.1;Port=5432;Database=lara;User Id=postgres;Password=postgres;"));

    ConfigureMassTransit(services, configuration);
}

void ConfigureMassTransit(IServiceCollection services, IConfiguration configuration)
{
    services.AddMassTransit(x =>
    {
        x.SetKebabCaseEndpointNameFormatter();

        x.AddConsumers(typeof(GettingStartedConsumer).Assembly);

        var useRabbitMq = configuration.GetValue<bool>("Masstransit:UseRabbitMq");

        if (useRabbitMq)
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");

                });
                cfg.ConfigureEndpoints(context);
            });
        }

        else
        {
            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        }
    });
}
