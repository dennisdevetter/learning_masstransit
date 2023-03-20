using System;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Messaging.Lara;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace LearningMassTransit.Application.BackgroundServices;

public class WizardCreatedTestPublisher : BackgroundService
{
    private readonly IBus _bus;

    public WizardCreatedTestPublisher(IBus bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _bus.Publish(new WizardCreated()
        {
            WizardId = Guid.Parse("597548e0-0aa1-499e-b894-83aadf3bc8aa")
        }, stoppingToken);
    }
}