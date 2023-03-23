using System;
using LearningMassTransit.Contracts.Dtos;
using MediatR;

namespace LearningMassTransit.Contracts.Commands;

public class CreateAdresVoorstelCommand : IRequest
{
    public CreateAdresVoorstelDto Adres { get; set; }
    public Guid CorrelationId { get; set; }
}