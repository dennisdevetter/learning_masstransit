using System;
using MediatR;

namespace LearningMassTransit.Contracts.Commands;

public class ChangeAdresStatusCommand : IRequest
{
    public string ObjectId { get; set; }
    public bool Approved { get; set; }
    public Guid CorrelationId { get; set; }
}