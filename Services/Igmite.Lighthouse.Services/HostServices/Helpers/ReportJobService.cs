using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.HostServices.Helpers
{
    public class ReportJobService : IReportJobService
    {
        private readonly IRoleManager roleManager;

        public ReportJobService(IRoleManager _roleManager)
        {
            this.roleManager = _roleManager;
        }

        public async Task PerformReportJobService(string schedule)
        {
            try
            {
                QuartzLogger.Instance.WriteErrorLogsInFile(string.Format("{0}: The PerformService() is called with {1} schedule", Constants.GetCurrentDateTime, schedule));

                IQueryable<RoleModel> roles = await Task.Run(() =>
                {
                    return this.roleManager.GetRoles();
                });

                QuartzLogger.Instance.WriteErrorLogsInFile(string.Format("{0}: The PerformService() is finished with {1} schedule. Total no of report is {2}", Constants.GetCurrentDateTime, schedule, roles.Count()));
            }
            catch (Exception ex)
            {
                QuartzLogger.Instance.WriteErrorLogsInFile(string.Format("{0}: Exception is occured at PerformService(): {1}", Constants.GetCurrentDateTime, ex.Message));

                throw new QuartzConfigurationException(ex.Message);
            }
        }
    }
}