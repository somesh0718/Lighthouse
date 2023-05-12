using Quartz;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.HostServices.Helpers
{
    public class WeeklyAttendanceMessageJob : IWeeklyAttendanceMessageJob
    {
        public IWeeklyAttendanceMessageJobService weeklyAttendanceMessageJobService;

        public WeeklyAttendanceMessageJob(IWeeklyAttendanceMessageJobService _weeklyAttendanceMessageJobService)
        {
            this.weeklyAttendanceMessageJobService = _weeklyAttendanceMessageJobService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await weeklyAttendanceMessageJobService.PerformWeeklyAttendanceMessageJobService(JobScheduleType.Weekly);
        }
    }
}