using LearningMassTransit.Contracts.Dtos;
using MediatR;

namespace LearningMassTransit.Contracts.Requests;

public class GetBlogsRequest : IRequest<IList<BlogDto>>
{
}