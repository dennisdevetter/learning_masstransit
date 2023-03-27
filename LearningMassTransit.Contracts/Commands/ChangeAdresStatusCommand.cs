using System;
using LearningMassTransit.Contracts.Enums;
using MediatR;

namespace LearningMassTransit.Contracts.Commands;

public class ChangeAdresStatusCommand : IRequest
{
    public string ObjectId { get; set; }
    public AdresStatusEnum Status { get; set; }
    public Guid CorrelationId { get; set; }
}