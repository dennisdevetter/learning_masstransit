using LearningMassTransit.Contracts.Responses;
using MediatR;

namespace LearningMassTransit.Contracts.Requests;

public class GetTicketsRequest : IRequest<GetTicketsResponse>
{
    public GetTicketsRequest()
    {
    }
}