using Quartz;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.HostServices.Helpers
{
    public class DailyDashboardJob : IDailyDashboardJob
    {
        public IDashboardJobService dashboardJobService;

        public DailyDashboardJob(IDashboardJobService _dashboardJobService)
        {
            this.dashboardJobService = _dashboardJobService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await dashboardJobService.PerformDashboardJobService(JobScheduleType.Daily);
        }
    }
}
