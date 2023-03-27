using System.Reflection;
using LearningMassTransit.Application.CommandHandlers;
using LearningMassTransit.Application.RequestHandlers;
using LearningMassTransit.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LearningMassTransit.Application;

public static class ServiceCollectionExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        ConfigureMediatR(services);

        ConfigureServices(services);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<ICreateAdresVoorstelService, CreateAdresVoorstelService>();
        services.AddTransient<IChangeAdresStatusService, ChangeAdresStatusService>();
        services.AddTransient<IAtomaireActieService, AtomaireActieService>();
        services.AddTransient<ITicketService, TicketService>();
    }

    private static void ConfigureMediatR(IServiceCollection services)
    {
        services.AddMediatR(typeof(CreateAdresVoorstelCommandHandler).GetTypeInfo().Assembly);
    }
}