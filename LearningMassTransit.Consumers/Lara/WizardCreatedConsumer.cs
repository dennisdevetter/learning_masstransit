using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Domain;
using LearningMassTransit.Domain.Lara;
using LearningMassTransit.Messaging.Lara;
using MassTransit;
using Newtonsoft.Json;

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

        switch (wizard?.Kind)
        {
            case "AdresVoorstel":
                HandleAdresVoorstelWizard(wizard);
                break;
        }
    }

    private void HandleAdresVoorstelWizard(Wizard wizard)
    {
        foreach (var step in wizard.Steps)
        {
            if (step.StepType == typeof(CreateAdresVoorstelDto).AssemblyQualifiedName)
            {
                HandleCreateAdresVoorstel(step);
            }
        }
    }

    private void HandleCreateAdresVoorstel(WizardStep step)
    {
        var data = JsonConvert.DeserializeObject<CreateAdresVoorstelDto>(step.StepData);

        if (data != null)
        {

        }

    }
}