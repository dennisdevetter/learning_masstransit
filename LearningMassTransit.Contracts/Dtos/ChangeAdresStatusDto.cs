using LearningMassTransit.Contracts.Enums;

namespace LearningMassTransit.Contracts.Dtos;

public class ChangeAdresStatusDto
{
    public string ObjectId { get; set; }
    public AdresStatusEnum Status { get; set; }
}
