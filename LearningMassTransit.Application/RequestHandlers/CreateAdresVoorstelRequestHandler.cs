using System;
using System.Threading;
using System.Threading.Tasks;
using LearningMassTransit.Application.Services;
using LearningMassTransit.Contracts.Dtos;
using LearningMassTransit.Contracts.Requests;
using LearningMassTransit.Contracts.Responses;
using MediatR;
using Newtonsoft.Json;

namespace LearningMassTransit.Application.RequestHandlers;

public class CreateAdresVoorstelRequestHandler : IRequestHandler<CreateAdresVoorstelRequest, CreateAdresVoorstelResponse>
{
    private readonly IAtomaireActieService _atomaireActieService;
    private readonly ICreateAdresVoorstelService _createAdresVoorstelService;

    public CreateAdresVoorstelRequestHandler(IAtomaireActieService atomaireActieService, ICreateAdresVoorstelService createAdresVoorstelService)
    {
        _atomaireActieService = atomaireActieService;
        _createAdresVoorstelService = createAdresVoorstelService;
    }

    public async Task<CreateAdresVoorstelResponse> Handle(CreateAdresVoorstelRequest request, CancellationToken cancellationToken)
    {
        var data = JsonConvert.SerializeObject(request.Adres);

        var output = await _atomaireActieService.Execute(data, (correlationId) => DoAction(request, correlationId, cancellationToken), cancellationToken);

        return new CreateAdresVoorstelResponse(output.WorkflowId);
    }

    private async Task<TicketDto> DoAction(CreateAdresVoorstelRequest request, Guid correlationId, CancellationToken cancellationToken)
    {
        // do call to basisregisters

        var ticket = await _createAdresVoorstelService.CreateAdresVoorstel(request.Adres, correlationId, cancellationToken);

        return ticket;
    }
}