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
                UserId = context.Message.UserId,
                CorrelationId = context.CorrelationId ?? context.Message.WorkflowId,
                Data = SerializeData(context.Message.Data)
            });
        });

        Event(() => AdresVoorstelCreatedEvent, x => x.CorrelateById(i => i.WorkflowId, c => c.Message.CorrelationId));
        Event(() => ProposeStreetNameTicketCompletedEvent, x => x.CorrelateById(i => i.WorkflowId, c => c.Message.CorrelationId));
        Event(() => AdresStatusChangeCreatedEvent, x => x.CorrelateById(i => i.WorkflowId, c => c.Message.CorrelationId));
        Event(() => AdresStatusTicketCompletedEvent, x => x.CorrelateById(i => i.WorkflowId, c => c.Message.CorrelationId));
    }

    private void DefineBehaviour()
    {
        Initially(
            When(VoorstellenAdresRequest)
                .TransitionTo(AdresVoorstelCreating)
                .Then(async context =>
                {
                    await context.Send<CreateAdresVoorstelCommand>(new
                    {
                        Adres = context.Data.Data,
                        CorrelationId = context.Data.WorkflowId,
                        __correlationId = context.Data.WorkflowId
                    });
                }));

        During(AdresVoorstelCreating,
            Ignore(VoorstellenAdresRequest),
            When(AdresVoorstelCreatedEvent)
                .TransitionTo(AdresVoorstelCreated));
        
        During(AdresVoorstelCreated,
            Ignore(VoorstellenAdresRequest),
            Ignore(AdresVoorstelCreatedEvent),
            When(ProposeStreetNameTicketCompletedEvent)
                .TransitionTo(AdresStatusChanging)
                .Then(async context =>
                {
                    await context.Send<ChangeAdresStatusCommand>(new
                    {
                        context.Data.ObjectId,
                        context.Data.CorrelationId,
                        Approved = !string.IsNullOrWhiteSpace(context.Data.ObjectId),
                        __correlationId = context.Data.CorrelationId
                    });
                }));

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

    public State AdresVoorstelCreating { get; private set; }
    public State AdresVoorstelCreated { get; private set; }
    public State AdresStatusChanging { get; private set; }
    public State AdresStatusChanged { get; private set; }
    public State Complete { get; private set; }

    public Event<VoorstellenAdresRequestEvent> VoorstellenAdresRequest { get; private set; }
    public Event<AdresVoorstelCreatedEvent> AdresVoorstelCreatedEvent { get; private set; }
    public Event<ProposeStreetNameTicketCompletedEvent> ProposeStreetNameTicketCompletedEvent { get; private set; }
    public Event<AdresStatusChangeCreatedEvent> AdresStatusChangeCreatedEvent { get; private set; }
    public Event<AdresStatusTicketCompletedEvent> AdresStatusTicketCompletedEvent { get; private set; }

    private static string SerializeData(CreateAdresVoorstelDto data)
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(data);
    }

    private static CreateAdresVoorstelDto? DeserializeData(string data)
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAdresVoorstelDto>(data);
    }
}