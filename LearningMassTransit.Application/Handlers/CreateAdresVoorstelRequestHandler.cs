using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Contracts.Requests;
using LearningMassTransit.Contracts.Responses;
using MediatR;

namespace LearningMassTransit.Application.Handlers;

public class CreateAdresVoorstelRequestHandler : IRequestHandler<CreateAdresVoorstelRequest, CreateAdresVoorstelResponse>
{
    public Task<CreateAdresVoorstelResponse> Handle(CreateAdresVoorstelRequest request, CancellationToken cancellationToken)
    {
        var ticketId = Guid.NewGuid().ToString();

        var res = new CreateAdresVoorstelResponse(new AdresVoorstelCreatingDto(ticketId));

        return Task.FromResult(res);
    }
}