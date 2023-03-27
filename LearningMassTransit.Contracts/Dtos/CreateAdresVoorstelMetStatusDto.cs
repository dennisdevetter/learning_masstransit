using LearningMassTransit.Contracts.Enums;

namespace LearningMassTransit.Contracts.Dtos;

public class CreateAdresVoorstelMetStatusDto : CreateAdresVoorstelDto
{
    public AdresStatusEnum Status { get; set; }
}