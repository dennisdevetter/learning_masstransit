
using LearningMassTransit.Consumers;
using MassTransit;
using NSwag;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument(cfg => cfg.PostProcess = d =>
{
    d.Info.Title = "Api";
    d.Info.Contact = new OpenApiContact
    {
        Name = "Sample",
    };
});

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

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

await app.RunAsync();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
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
