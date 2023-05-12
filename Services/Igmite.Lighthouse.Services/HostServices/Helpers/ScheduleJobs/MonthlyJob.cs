using Quartz;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.HostServices.Helpers
{
    public class MonthlyJob : IMonthlyJob
    {
        public IReportJobService reportJobService;

        public MonthlyJob(IReportJobService _reportJobService)
        {
            this.reportJobService = _reportJobService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await reportJobService.PerformReportJobService(JobScheduleType.Monthly);
        }
    }
}