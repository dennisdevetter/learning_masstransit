using System;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Commands;
using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Domain.Lara;
using LearningMassTransit.Messaging.Lara;
using MassTransit;

namespace LearningMassTransit.Application.Sagas;

public class VoorstellenAdresStateMachine : MassTransitStateMachine<VoorstellenAdresState>
{
    public VoorstellenAdresStateMachine()
    {
        InstanceState(x => x.CurrentState);

        DefineEvents();

        DefineBehaviour();
    }

    private void DefineEvents()
    {
        // init
        Event(() => VoorstellenAdresRequest, x =>
        {
            x.CorrelateById(c => c.WorkflowId, i => i.Message.WorkflowId)
                .SelectId(context => context.CorrelationId ?? context.Message.WorkflowId);

            x.InsertOnInitial = true;

            x.SetSagaFactory(context => new VoorstellenAdresState
            {
                WorkflowId = context.Message.WorkflowId,
                CreationDate = DateTime.UtcNow,
                CorrelationId = context.CorrelationId ?? context.Message.WorkflowId,
            });
        });

        Event(() => AdresVoorstelInitializedEvent, x => x.CorrelateById(i => i.WorkflowId, c => c.Message.CorrelationId));
        Event(() => AdresVoorstelCreatedEvent, x => x.CorrelateById(i => i.WorkflowId, c => c.Message.CorrelationId));
        Event(() => ProposeStreetNameTicketCompletedEvent, x => x.CorrelateById(i => i.WorkflowId, c => c.Message.CorrelationId));
        Event(() => AdresStatusChangeCreatedEvent, x => x.CorrelateById(i => i.WorkflowId, c => c.Message.CorrelationId));
        Event(() => AdresStatusTicketCompletedEvent, x => x.CorrelateById(i => i.WorkflowId, c => c.Message.CorrelationId));
    }

    private void DefineBehaviour()
    {
        Initially(
            When(VoorstellenAdresRequest)
                .TransitionTo(AdresVoorstelInitializing)
                .Then(async context => await context.Publish(new AdresVoorstelInitializedEvent{ CorrelationId = context.Data.WorkflowId})));

        During(AdresVoorstelInitializing,
            Ignore(VoorstellenAdresRequest),
            When(AdresVoorstelInitializedEvent)
                .TransitionTo(AdresVoorstelCreating)
                .Then(async context => await SendCreateAdresVoorstelCommand(context)));

        During(AdresVoorstelCreating,
            Ignore(AdresVoorstelInitializedEvent),
            Ignore(VoorstellenAdresRequest),
            When(AdresVoorstelCreatedEvent)
                .TransitionTo(AdresVoorstelCreated));

        During(AdresVoorstelCreated,
            Ignore(VoorstellenAdresRequest),
            Ignore(AdresVoorstelCreatedEvent),
            When(ProposeStreetNameTicketCompletedEvent)
                .TransitionTo(AdresStatusChanging)
                .Then(async context => await SendChangeAdresStatusCommand(context)));

        During(AdresStatusChanging,
            Ignore(VoorstellenAdresRequest),
            Ignore(AdresVoorstelCreatedEvent),
            Ignore(ProposeStreetNameTicketCompletedEvent),
            When(AdresStatusChangeCreatedEvent)
                .TransitionTo(AdresStatusChanged));

        During(AdresStatusChanged,
            Ignore(VoorstellenAdresRequest),
            Ignore(AdresVoorstelCreatedEvent),
            Ignore(ProposeStreetNameTicketCompletedEvent),
            Ignore(AdresStatusChangeCreatedEvent),
            When(AdresStatusTicketCompletedEvent)
                .TransitionTo(Complete));
    }

    public State AdresVoorstelInitializing { get; private set; }
    public State AdresVoorstelCreating { get; private set; }
    public State AdresVoorstelCreated { get; private set; }
    public State AdresStatusChanging { get; private set; }
    public State AdresStatusChanged { get; private set; }
    public State Complete { get; private set; }

    public Event<AdresVoorstelInitializedEvent> AdresVoorstelInitializedEvent { get; private set; }
    public Event<VoorstellenAdresRequestEvent> VoorstellenAdresRequest { get; private set; }
    public Event<AdresVoorstelCreatedEvent> AdresVoorstelCreatedEvent { get; private set; }
    public Event<ProposeStreetNameTicketCompletedEvent> ProposeStreetNameTicketCompletedEvent { get; private set; }
    public Event<AdresStatusChangeCreatedEvent> AdresStatusChangeCreatedEvent { get; private set; }
    public Event<AdresStatusTicketCompletedEvent> AdresStatusTicketCompletedEvent { get; private set; }

    private static CreateAdresVoorstelDto? DeserializeData(string data)
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAdresVoorstelDto>(data);
    }

    private static async Task SendCreateAdresVoorstelCommand(BehaviorContext<VoorstellenAdresState> context)
    {
        var correlationId = context.Instance.WorkflowId;
        var adres = DeserializeData(context.Instance.Workflow.Data);
        
        await context.Send<CreateAdresVoorstelCommand>(new
        {
            Adres = adres,
            CorrelationId = correlationId,
            __correlationId = correlationId
        });
    }

    private static async Task SendChangeAdresStatusCommand(BehaviorContext<VoorstellenAdresState, ProposeStreetNameTicketCompletedEvent> context)
    {
        await context.Send<ChangeAdresStatusCommand>(new
        {
            context.Data.ObjectId,
            context.Data.CorrelationId,
            Approved = !string.IsNullOrWhiteSpace(context.Data.ObjectId),
            __correlationId = context.Data.CorrelationId
        });
    }
}