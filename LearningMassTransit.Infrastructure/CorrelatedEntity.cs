using System;
using LearningMassTransit.Infrastructure.Database;

namespace LearningMassTransit.Infrastructure;

public class AuditEntity<T> : Entity<T>, IAuditEntity
{
    public virtual Guid? CorrelationId { get; internal set; }
    public string UserId { get; internal set; }
    public DateTime? AuditDate { get; internal set; }

    public void SetAuditInfo(string? userId, DateTime? auditDate, Guid? correlationId)
    {
        UserId = userId;
        AuditDate = auditDate;
        CorrelationId = correlationId;
    }
}