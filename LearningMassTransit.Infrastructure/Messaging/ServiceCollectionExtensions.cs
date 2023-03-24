using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using System;

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

public static class ReceiveEndpointConfiguratorExtensions
{
    public static void ConfigureConsumers(this IReceiveEndpointConfigurator configurator, IServiceProvider serviceProvider, params Assembly[] assemblies)
    {
        var consumerMethod = typeof(DependencyInjectionReceiveEndpointExtensions).GetMethods().First(method => method.Name == nameof(DependencyInjectionReceiveEndpointExtensions.Consumer) && method.GetParameters()[0].ParameterType == typeof(IReceiveEndpointConfigurator));

        foreach (var consumer in assemblies.SelectMany(assembly => assembly.GetTypes().Where(p => typeof(IConsumer).IsAssignableFrom(p))))
        {
            var generic = consumerMethod.MakeGenericMethod(consumer);
            generic.Invoke(null, new object?[] { configurator, serviceProvider, null });
        }
    }
}