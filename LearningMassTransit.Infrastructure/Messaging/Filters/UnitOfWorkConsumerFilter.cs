using System;
using System.Threading.Tasks;
using LearningMassTransit.Infrastructure.Database;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace LearningMassTransit.Infrastructure.Messaging.Filters;

public class UnitOfWorkConsumerFilter<T> : IFilter<ConsumeContext<T>> where T : class
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UnitOfWorkConsumerFilter<T>> _logger;

    public UnitOfWorkConsumerFilter(IUnitOfWork unitOfWork, ILogger<UnitOfWorkConsumerFilter<T>> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        using (_unitOfWork.BeginTransaction())
        {
            try
            {
                await next.Send(context);
                await _unitOfWork.Commit(context.CancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception consuming message with UnitOfWork filter");
                await _unitOfWork.Rollback(context.CancellationToken);
                throw;
            }
        }
    }

    public void Probe(ProbeContext context)
    {
        context.CreateFilterScope("unitofwork");
    }
}