using System.Linq;
using System.Threading.Tasks;
using LearningMassTransit.Domain;
using LearningMassTransit.Domain.Lara;
using LearningMassTransit.Messaging.Lara;
using MassTransit;
using Quartz;

namespace LearningMassTransit.Processor.Api.Jobs;

public class WizzardPollingJob : IJob
{
    private readonly IBus _bus;
    private readonly ILaraUnitOfWork _laraUnitOfWork;

    public WizzardPollingJob(ILaraUnitOfWork laraUnitOfWork, IBus bus)
    {
        _bus = bus;
        _laraUnitOfWork = laraUnitOfWork;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var wizards = await _laraUnitOfWork.Wizards.All(context.CancellationToken);

        // polling logic to async api

        var completedWizards = wizards.Where(x => x.Status == WizardStatusEnum.Completed).ToList();

        if (completedWizards.Any())
        {
            foreach (var completedWizard in completedWizards)
            {
                // notify saga
                await _bus.Publish(new WizardCompleted { WizardId = completedWizard.WizardId }, context.CancellationToken);
            }
        }
    }
}