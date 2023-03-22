using LearningMassTransit.Infrastructure.Api;
using Microsoft.Extensions.DependencyInjection;

namespace LearningMassTransit.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IMvcBuilder AddMicroserviceControllers(this IServiceCollection services)
    {
        services.AddScoped<TransactionFilter>();

        return services
            .AddControllers(config => {
                config.Filters.AddService<TransactionFilter>();
            });
    }
}