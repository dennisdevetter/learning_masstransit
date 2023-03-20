
using LearningMassTransit.Consumers;
using LearningMassTransit.DataAccess;
using LearningMassTransit.DataAccess.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using NSwag;

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

    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<BloggingContext>();
        context.Database.Migrate();
    }
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

    ConfigureDatabase(services, configuration);

    ConfigureMassTransit(services, configuration);
}

void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
{
    var connectionstring = configuration.GetValue<string>("PostgressDatabase:Connectionstring");

    services.AddDbContext<BloggingContext>(options => options.UseNpgsql(connectionstring));
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
