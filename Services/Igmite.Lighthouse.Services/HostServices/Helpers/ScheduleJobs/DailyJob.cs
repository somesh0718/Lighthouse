using Quartz;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.HostServices.Helpers
{
    public class DailyJob : IDailyJob
    {
        public IReportJobService reportJobService;

        public DailyJob(IReportJobService _reportJobService)
        {
            this.reportJobService = _reportJobService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await reportJobService.PerformReportJobService(JobScheduleType.Daily);
        }
    }
}