using LearningMassTransit.DataAccess;
using MassTransit;
using NSwag;
using Correlate.DependencyInjection;
using LearningMassTransit.Application;
using LearningMassTransit.Infrastructure.Options;
using LearningMassTransit.Infrastructure.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LearningMassTransit.Infrastructure;
using LearningMassTransit.Infrastructure.Messaging;
using LearningMassTransit.Infrastructure.Api.Routing;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();
ConfigureApp();

await app.RunAsync();

void ConfigureApp()
{

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }
    else
    {
        app.UseDeveloperExceptionPage();
        app.UseMigrationsEndPoint();
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
        endpoints.MapAlwaysOnEndpoint();
        endpoints.MapControllers();
    });
}

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddEndpointsApiExplorer();

    services.AddMicroserviceControllers(); // custom

    services.AddApplicationBus(); // custom

    services.AddOpenApiDocument(cfg => cfg.PostProcess = d =>
    {
        d.Info.Title = "Api";
        d.Info.Contact = new OpenApiContact
        {
            Name = "Sample",
        };
    });

    ConfigureCorrelation(services);

    ConfigureDatabase(services, configuration);

    ConfigureMassTransit(services, configuration);

    ConfigureApplicationContext(services);

    ConfigureApplication(services);
}

void ConfigureApplication(IServiceCollection services)
{
    services.ConfigureApplication();
}

void ConfigureCorrelation(IServiceCollection services)
{
    services.AddCorrelate(options => options.RequestHeaders = new[] { "X-Correlation-ID" });

    services.AddHeaderPropagation(options =>
    {
        options.Headers.Add("x-correlation-id");
    });
}

void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
{
    var connection = configuration.GetValue<string>("PostgressDatabase:Connectionstring");

    services.AddDataAccess(new DatabaseOptions
    {
        Connection = connection
    });
}

void ConfigureMassTransit(IServiceCollection services, IConfiguration configuration)
{
    services.AddMassTransit(x =>
    {
        x.SetKebabCaseEndpointNameFormatter();

        x.AddTransactionalBus();

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

void ConfigureApplicationContext(IServiceCollection services)
{
    // temporary. this is not used now
    services.AddTransient<IApplicationContext, ApplicationContext>();
}