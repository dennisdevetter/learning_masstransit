
using System.Reflection;
using LearningMassTransit.Consumers;
using LearningMassTransit.DataAccess;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using NSwag;
using MediatR;
using Correlate.DependencyInjection;
using LearningMassTransit.Application.Handlers;
using LearningMassTransit.Infrastructure.Options;
using LearningMassTransit.Infrastructure.Security;

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

    });

    InitializeDatabase();
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

    ConfigureCorrelation(services);

    ConfigureDatabase(services, configuration);

    ConfigureMassTransit(services, configuration);

    ConfigureMediatR(services);

    ConfigureApplicationContext(services);
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

void ConfigureMediatR(IServiceCollection services)
{
    services.AddMediatR(typeof(CreateAdresVoorstelRequestHandler).GetTypeInfo().Assembly);
}

void ConfigureApplicationContext(IServiceCollection services)
{
    // temporary. this is not used now
    services.AddTransient<IApplicationContext, ApplicationContext>();
}

void InitializeDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<LaraDbContext>();
        context.Database.Migrate();
    }
}