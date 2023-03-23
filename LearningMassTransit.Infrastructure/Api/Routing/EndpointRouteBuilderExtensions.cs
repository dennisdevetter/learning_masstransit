using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace LearningMassTransit.Infrastructure.Api.Routing;

public static class EndpointRouteBuilderExtensions
{
    public static void MapAlwaysOnEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/",
            async context => { await HttpResponseWritingExtensions.WriteAsync(context.Response, "Always on"); });
    }
}