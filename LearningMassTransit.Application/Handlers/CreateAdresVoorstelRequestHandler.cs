using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Contracts.Requests;
using LearningMassTransit.Contracts.Responses;
using LearningMassTransit.Infrastructure.Messaging;
using LearningMassTransit.Messaging.Lara;
using MediatR;

namespace LearningMassTransit.Application.Handlers;

public class CreateAdresVoorstelRequestHandler : IRequestHandler<CreateAdresVoorstelRequest, CreateAdresVoorstelResponse>
{
    private readonly IApplicationBus _applicationBus;

    public CreateAdresVoorstelRequestHandler(IApplicationBus applicationBus)
    {
        _applicationBus = applicationBus;
    }

    public async Task<CreateAdresVoorstelResponse> Handle(CreateAdresVoorstelRequest request, CancellationToken cancellationToken)
    {
        // TODO add backend validation ?

        var userId = "7D35AFD6933D4049BD17A4560BA30674";

        var voorstellenAdresRequestEvent = new VoorstellenAdresRequestEvent(request.Adres, userId);

        await _applicationBus.Publish(voorstellenAdresRequestEvent, cancellationToken);

        return  new CreateAdresVoorstelResponse();
    }
}