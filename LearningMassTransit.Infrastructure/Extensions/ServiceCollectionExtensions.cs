using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LearningMassTransit.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add scoped as implemented interfaces.
    /// </summary>
    /// <typeparam name="TImplementation"></typeparam>
    /// <param name="services">The services.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection AddScopedAsImplementedInterfaces<TImplementation>(this IServiceCollection services)
        where TImplementation : class
    {
        services.AddScopedAsImplementedInterfaces(typeof(TImplementation));
        return services;
    }

    /// <summary>
    /// Add scoped as implemented interfaces.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="typeImplementation">The type implementation.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection AddScopedAsImplementedInterfaces(this IServiceCollection services, Type typeImplementation)
    {
        foreach (var implementedInterface in typeImplementation.GetInterfaces()?.Where(i => i != typeof(IDisposable)))
        {
            services.AddScoped(implementedInterface, typeImplementation);
        }

        return services;
    }

    /// <summary>
    /// Add scoped as implemented interfaces.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="assemblies">The assemblies.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection AddScopedAsImplementedInterfaces(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var client in assemblies.SelectMany(a => a.GetTypes().Where(t => t.IsClass)))
        {
            services.AddScopedAsImplementedInterfaces(client);
        }

        return services;
    }
}