using System.Threading.Tasks;
using LearningMassTransit.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GettingStarted
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) => { ConfigureApplication(services, hostContext); });

        private static void ConfigureApplication(IServiceCollection services, HostBuilderContext hostBuilderContext)
        {
            var configuration = hostBuilderContext.Configuration;

            ConfigureMassTransit(services, configuration);
        }

        private static void ConfigureMassTransit(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                // By default, sagas are in-memory, but should be changed to a durable
                // saga repository.
                x.SetInMemorySagaRepositoryProvider();

                x.AddConsumers(typeof(GettingStartedConsumer).Assembly);

                var useRabbitMq = configuration.GetValue<bool>("Masstransit:UseRabbitMq");

                if (useRabbitMq)
                {
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host("localhost", "/", h => {
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
    }
}
