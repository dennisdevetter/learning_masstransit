using LearningMassTransit.Domain;
using LearningMassTransit.Infrastructure.EntityFramework;
using LearningMassTransit.Infrastructure.EntityFramework.Hints;
using LearningMassTransit.Infrastructure.Extensions;
using LearningMassTransit.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace LearningMassTransit.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, DatabaseOptions databaseOptions)
    {
        services.AddScoped<IInterceptor, HintsInterceptor>();

        services.AddDbContext<LaraDbContext>((provider, options) =>
        {
            options.UseNpgsql(databaseOptions.Connection);
            options.AddInterceptors(provider.GetServices<IInterceptor>());
        });

        services.AddRepositories(typeof(ServiceCollectionExtensions).Assembly);

        services.AddScopedAsImplementedInterfaces<LaraUnitOfWork>();

        return services;
    }
}