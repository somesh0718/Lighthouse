using Quartz;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.HostServices.Helpers
{
    public class WeeklyJob : IWeeklyJob
    {
        public IReportJobService reportJobService;

        public WeeklyJob(IReportJobService _reportJobService)
        {
            this.reportJobService = _reportJobService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await reportJobService.PerformReportJobService(JobScheduleType.Monthly);
        }
    }
}