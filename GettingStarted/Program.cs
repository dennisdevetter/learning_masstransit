using System.Threading.Tasks;
using LearningMassTransit.Infrastructure;
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

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) => { ConfigureApplication(services); });

        private static void ConfigureApplication(IServiceCollection services)
        {
            services.ConfigureMassTransit();
        }
    }
}
