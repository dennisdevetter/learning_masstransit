using System.Threading.Tasks;
using LearningMassTransit.Domain;
using LearningMassTransit.Messaging.Lara;
using MassTransit;

namespace LearningMassTransit.Consumers.Lara;

public class WizardCreatedConsumer : IConsumer<WizardCreated>
{
    private readonly ILaraUnitOfWork _laraUnitOfWork;

    public WizardCreatedConsumer(ILaraUnitOfWork laraUnitOfWork)
    {
        _laraUnitOfWork = laraUnitOfWork;
    }

    public async Task Consume(ConsumeContext<WizardCreated> context)
    {
        var wizardid = context.Message.WizardId;

        var wizard = await _laraUnitOfWork.Wizards.GetById(wizardid, context.CancellationToken);

        if (wizard != null)
        {
            var steps = wizard.Steps;
            foreach (var step in steps)
            {

            }
        }
    }
}