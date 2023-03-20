public interface IAuditEntity
{
    string? UserId { get; }
    DateTime? AuditDate { get; }
    Guid? CorrelationId { get; }
    void SetAuditInfo(string? userId, DateTime? auditDate, Guid? correlationId);
}