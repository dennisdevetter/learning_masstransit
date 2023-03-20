using LearningMassTransit.Contracts.Dtos;

namespace LearningMassTransit.Contracts.Responses;

public class CreateAdresVoorstelResponse : ResponseOf<AdresVoorstelCreatingDto>
{
    public CreateAdresVoorstelResponse(AdresVoorstelCreatingDto result) : base(result)
    {
    }
}