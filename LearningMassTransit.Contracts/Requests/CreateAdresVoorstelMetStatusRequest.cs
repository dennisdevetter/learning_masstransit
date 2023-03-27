using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Contracts.Responses;
using MediatR;

namespace LearningMassTransit.Contracts.Requests;

public class CreateAdresVoorstelMetStatusRequest : IRequest<CreateAdresVoorstelMetStatusResponse>
{
    public CreateAdresVoorstelMetStatusDto Adres { get; }

    public CreateAdresVoorstelMetStatusRequest(CreateAdresVoorstelMetStatusDto adres)
    {
        Adres = adres;
    }
}