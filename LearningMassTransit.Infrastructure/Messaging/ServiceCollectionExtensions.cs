using Microsoft.Extensions.DependencyInjection;

namespace LearningMassTransit.Infrastructure.Messaging;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationBus(this IServiceCollection services)
    {
        services.AddScoped<IMassTransitApplicationBus, ApplicationBus>();
        services.AddScoped<IApplicationBus>(c => c.GetService<IMassTransitApplicationBus>());

        return services;
    }
}