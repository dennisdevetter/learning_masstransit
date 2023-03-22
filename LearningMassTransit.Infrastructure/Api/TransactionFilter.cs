using System.Threading.Tasks;
using LearningMassTransit.Infrastructure.Database;
using LearningMassTransit.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearningMassTransit.Infrastructure.Api;

public class TransactionFilter : IAsyncActionFilter
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IApplicationBus _applicationBus;

    public TransactionFilter(IUnitOfWork unitOfWork, IApplicationBus applicationBus = null)
    {
        _unitOfWork = unitOfWork;
        _applicationBus = applicationBus;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.HttpContext.Request.Method != "GET")
        {
            using (_unitOfWork.BeginTransaction())
            {
                var executed = await next();
                if (executed.Exception == null)
                {
                    await _unitOfWork.Commit(context.HttpContext.RequestAborted);
                    if (_applicationBus != null)
                    {
                        await _applicationBus.ReleaseTransaction();
                    }
                }
                else
                {
                    await _unitOfWork.Rollback(context.HttpContext.RequestAborted);
                }
            }
        }
        else
        {
            var executed = await next();
            if (executed.Exception == null && _applicationBus != null)
            {
                await _applicationBus.ReleaseTransaction();
            }
        }
    }
}