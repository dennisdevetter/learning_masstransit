using System;
using LearningMassTransit.Domain.Lara;
using LearningMassTransit.Messaging.Lara;
using MassTransit;

namespace LearningMassTransit.Application.Sagas;

public class AtomaireActieStateMachine : MassTransitStateMachine<AtomaireActieState>
{
    public AtomaireActieStateMachine()
    {
        InstanceState(x => x.CurrentState);

        DefineEvents();

        DefineBehaviour();
    }

    private void DefineEvents()
    {
        // init
        Event(() => AtomaireActieInitializedEvent, x =>
        {
            x.CorrelateById(c => c.WorkflowId, i => i.Message.WorkflowId)
                .SelectId(context => context.CorrelationId ?? context.Message.WorkflowId);

            x.InsertOnInitial = true;

            x.SetSagaFactory(context => new AtomaireActieState
            {
                WorkflowId = context.Message.WorkflowId,
                CreationDate = DateTime.UtcNow,
                CorrelationId = context.CorrelationId ?? context.Message.WorkflowId,
                Actie = context.Message.Actie,
            });
        });

        Event(() => ProposeStreetNameTicketCompletedEvent, x => x.CorrelateById(i => i.WorkflowId, c => c.Message.CorrelationId));
        Event(() => AdresStatusTicketCompletedEvent, x => x.CorrelateById(i => i.WorkflowId, c => c.Message.CorrelationId));
    }

    private void DefineBehaviour()
    {
        Initially(
            When(AtomaireActieInitializedEvent)
                .TransitionTo(Initialized));
        
        During(Initialized, 
            Ignore(AtomaireActieInitializedEvent), 
            When(ProposeStreetNameTicketCompletedEvent)
                .TransitionTo(Complete));

        During(Initialized, 
            Ignore(AtomaireActieInitializedEvent), 
            When(AdresStatusTicketCompletedEvent)
                .TransitionTo(Complete));
    }

    public State Initialized { get; private set; }
    public State Complete { get; private set; }

    public Event<AtomaireActieInitializedEvent> AtomaireActieInitializedEvent { get; private set; }
    
    // add all the atomaire events here
    public Event<ProposeStreetNameTicketCompletedEvent> ProposeStreetNameTicketCompletedEvent { get; private set; }
    public Event<AdresStatusTicketCompletedEvent> AdresStatusTicketCompletedEvent { get; private set; }
    

}