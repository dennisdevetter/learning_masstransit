using System.Threading.Tasks;
using LearningMassTransit.Domain;
using Quartz;

namespace LearningMassTransit.Processor.Api.Jobs;

public class WizzardPollingJob : IJob
{
    private readonly ILaraUnitOfWork _laraUnitOfWork;

    public WizzardPollingJob(ILaraUnitOfWork laraUnitOfWork)
    {
        _laraUnitOfWork = laraUnitOfWork;
    }


    public async Task Execute(IJobExecutionContext context)
    {
        var wizards = await _laraUnitOfWork.Wizards.All(context.CancellationToken);


    }
}