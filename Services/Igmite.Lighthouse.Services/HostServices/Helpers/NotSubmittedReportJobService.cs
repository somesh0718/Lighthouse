using Igmite.Lighthouse.BAL;
using System;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.HostServices.Helpers
{
    public class NotSubmittedReportJobService : INotSubmittedReportJobService
    {
        private readonly IDashboardJobManager dashboardJobManager;

        public NotSubmittedReportJobService(IDashboardJobManager _dashboardJobManager)
        {
            this.dashboardJobManager = _dashboardJobManager;
        }

        public async Task PerformNotSubmittedReportJobService(string schedule)
        {
            try
            {
                await Task.Run(() =>
                {
                    this.dashboardJobManager.GenerateNotSubmittedReportByVTAsync();

                    return true;
                });
            }
            catch (Exception ex)
            {
                QuartzLogger.Instance.WriteErrorLogsInFile(string.Format("Exception is occured at PerformService(): {0}", ex.Message));

                throw new QuartzConfigurationException(ex.Message);
            }
        }
    }
}