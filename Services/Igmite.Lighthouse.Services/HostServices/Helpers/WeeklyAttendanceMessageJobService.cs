using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.Models;
using System;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.HostServices.Helpers
{
    public class WeeklyAttendanceMessageJobService : IWeeklyAttendanceMessageJobService
    {
        private readonly IDashboardJobManager dashboardJobManager;

        public WeeklyAttendanceMessageJobService(IDashboardJobManager _dashboardJobManager)
        {
            this.dashboardJobManager = _dashboardJobManager;
        }

        public async Task PerformWeeklyAttendanceMessageJobService(string schedule)
        {
            try
            {
                await Task.Run(() =>
                {
                    this.dashboardJobManager.SendWeeklyAttendanceMessageToUserAsync();

                    return true;
                });
            }
            catch (Exception ex)
            {
                QuartzLogger.Instance.WriteErrorLogsInFile(string.Format("{0}: Exception is occured at SendWeeklyAttendanceMessageToUserAsync(): {1}", Constants.GetCurrentDateTime, ex.Message));

                throw new QuartzConfigurationException(ex.Message);
            }
        }
    }
}