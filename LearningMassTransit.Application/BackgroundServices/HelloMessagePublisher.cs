using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Messaging;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace LearningMassTransit.Application.BackgroundServices
{
    public class HelloMessagePublisher : BackgroundService
    {
        private readonly IBus _bus;

        public HelloMessagePublisher(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(new HelloMessage
                {
                    Name = "World"
                }, stoppingToken);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}