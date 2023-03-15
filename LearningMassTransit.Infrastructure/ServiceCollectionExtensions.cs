using System.Reflection;
using GettingStarted;
using LearningMassTransit.Consumers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace LearningMassTransit.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                // By default, sagas are in-memory, but should be changed to a durable
                // saga repository.
                x.SetInMemorySagaRepositoryProvider();

                //x.AddConsumers(typeof(GettingStartedConsumer).Assembly);

                // var entryAssembly = Assembly.GetEntryAssembly();
                //x.AddSagaStateMachines(entryAssembly);
                //x.AddSagas(entryAssembly);
                //x.AddActivities(entryAssembly);

                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            });

            // processors
            services.AddHostedService<Worker>();
        }
    }
}