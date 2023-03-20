using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearningMassTransit.Api.Controllers;

/// <summary>
/// Base controller class for all  controllers.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class AppController<T> : ControllerBase
{
    /// <summary>
    /// The <see cref="IMediator"/> instance.
    /// </summary>
    public IMediator Mediator { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="AppController{T}"/>.
    /// </summary>
    /// <param name="mediator">The <see cref="IMediator"/> instance.</param>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> instance.</param>
    protected AppController(IMediator mediator, ILogger<T> logger)
    {
        Mediator = mediator;
    }

    /// <summary>
    /// Executes a request using MediatR.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    /// <param name="request">The request to be handled by the mediator.</param>
    /// /// <param name="actionResultCreator">change the action result</param>
    /// <returns>The response of the mediator handler.</returns>
    protected async Task<IActionResult> ExecuteRequest<TRequest, TResponse>(TRequest request, Func<TResponse, IActionResult> actionResultCreator = null) where TRequest : IRequest<TResponse>
    {
        var response = await Mediator.Send(request);

        if (response != null)
        {
            if (actionResultCreator != null)
            {
                return actionResultCreator.Invoke(response);
            }

            return new OkObjectResult(response);
        }

        return NotFound();
    }
}