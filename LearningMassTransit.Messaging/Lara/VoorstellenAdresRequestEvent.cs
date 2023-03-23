using System;
using LearningMassTransit.Contracts.Dtos;

namespace LearningMassTransit.Messaging.Lara;

public class VoorstellenAdresRequestEvent
{
    public CreateAdresVoorstelDto Data { get; set; }

    public Guid Id { get; }

    public VoorstellenAdresRequestEvent(CreateAdresVoorstelDto data)
    {
        Id = Guid.NewGuid();
        Data = data;
    }
}