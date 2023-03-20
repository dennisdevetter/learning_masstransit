using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Contracts.Responses;
using MediatR;

namespace LearningMassTransit.Contracts.Requests;

public class CreateAdresVoorstelRequest : IRequest<CreateAdresVoorstelResponse>
{
    public CreateAdresVoorstelDto Adres { get; }

    public CreateAdresVoorstelRequest(CreateAdresVoorstelDto adres)
    {
        Adres = adres;
    }
}