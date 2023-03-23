using Correlate.DependencyInjection;
using LearningMassTransit.Application.BackgroundServices;
using LearningMassTransit.Consumers;
using LearningMassTransit.DataAccess;
using LearningMassTransit.Infrastructure;
using LearningMassTransit.Infrastructure.Messaging;
using LearningMassTransit.Infrastructure.Options;
using LearningMassTransit.Infrastructure.Security;
using LearningMassTransit.Processor.Api.Jobs;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using Quartz;
using System;

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

        x.AddConsumers(typeof(HelloMessageConsumer).Assembly);

        //x.AddSagaStateMachines(entryAssembly);
        //x.AddSagas(entryAssembly);
        //x.AddActivities(entryAssembly);

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

    // processors
    //services.AddHostedService<HelloMessagePublisher>();

    services.AddQuartz(q =>
    {
        // as of 3.3.2 this also injects scoped services (like EF DbContext) without problems
        q.UseMicrosoftDependencyInjectionJobFactory();

        // quickest way to create a job with single trigger is to use ScheduleJob
        // (requires version 3.2)
        q.ScheduleJob<TicketPollingJob>(trigger => trigger
            .WithIdentity("Combined Configuration Trigger")
            .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(7)))
            .WithDailyTimeIntervalSchedule(x => x.WithInterval(10, IntervalUnit.Second))
            .WithDescription("my awesome trigger configured for a job with single call")
        );
    });

    services.AddTransient<TicketPollingJob>();

    // Quartz.Extensions.Hosting allows you to fire background service that handles scheduler lifecycle
    services.AddQuartzHostedService(options =>
    {
        // when shutting down we want jobs to complete gracefully
        options.WaitForJobsToComplete = true;
    });
}


void ConfigureApplicationContext(IServiceCollection services)
{
    // temporary. this is not used now
    services.AddTransient<IApplicationContext, ApplicationContext>();
}
