﻿using System;
using LearningMassTransit.Contracts.Commands;
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
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(context.Message.Data)
            });
        });

        Event(() => AdresVoorstelCreatedEvent, x => x.CorrelateById(i => i.WorkflowId, c => c.Message.CorrelationId));

        Event(() => ProposeStreetNameTicketCompletedEvent, x => 
                x
                .CorrelateById(i => i.WorkflowId, c => GetCorrelationIdForTicketEvent(c.Message))
                .SelectId(c => GetCorrelationIdForTicketEvent(c.Message))
        );
    }

    private Guid GetCorrelationIdForTicketEvent(ProposeStreetNameTicketCompletedEvent evt)
    {
        return evt.CorrelationId;
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
                .TransitionTo(AdresVoorstelCompleted)
                .Then(context =>
                {
                    var foo = context.Data;
                }));
    }

    public State AdresVoorstelCreating { get; private set; }
    public State AdresVoorstelCreated { get; private set; }
    public State AdresVoorstelCompleted { get; private set; }


    public Event<VoorstellenAdresRequestEvent> VoorstellenAdresRequest { get; private set; }
    public Event<AdresVoorstelCreatedEvent> AdresVoorstelCreatedEvent { get; private set; }
    public Event<ProposeStreetNameTicketCompletedEvent> ProposeStreetNameTicketCompletedEvent { get; private set; }
}