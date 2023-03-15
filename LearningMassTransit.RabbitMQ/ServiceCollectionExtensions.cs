using MassTransit;

namespace LearningMassTransit.RabbitMQ
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureRabbitMq(this IBusRegistrationConfigurator configurator)
        {
            configurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h => {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(context);
            });
        }
    }
}