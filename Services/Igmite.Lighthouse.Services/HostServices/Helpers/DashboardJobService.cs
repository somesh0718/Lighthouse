using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.Models;
using System;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.HostServices.Helpers
{
    public class DashboardJobService : IDashboardJobService
    {
        private readonly IDashboardJobManager dashboardJobManager;

        public DashboardJobService(IDashboardJobManager _dashboardJobManager)
        {
            this.dashboardJobManager = _dashboardJobManager;
        }

        public async Task PerformDashboardJobService(string schedule)
        {
            try
            {
                await Task.Run(() =>
                {
                    this.dashboardJobManager.GenerateDashboardInformationAsync();

                    return true;
                });
            }
            catch (Exception ex)
            {
                QuartzLogger.Instance.WriteErrorLogsInFile(string.Format("{0}: Exception is occured at GenerateDashboardInformationAsync(): {1}", Constants.GetCurrentDateTime, ex.Message));

                throw new QuartzConfigurationException(ex.Message);
            }
        }
    }
}