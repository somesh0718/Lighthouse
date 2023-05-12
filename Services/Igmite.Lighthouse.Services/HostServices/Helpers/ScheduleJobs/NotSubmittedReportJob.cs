using Quartz;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.HostServices.Helpers
{
    public class NotSubmittedReportJob : INotSubmittedReportJob
    {
        public INotSubmittedReportJobService notSubmittedReportJobService;

        public NotSubmittedReportJob(INotSubmittedReportJobService _notSubmittedReportJobService)
        {
            this.notSubmittedReportJobService = _notSubmittedReportJobService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await notSubmittedReportJobService.PerformNotSubmittedReportJobService(JobScheduleType.Daily);
        }
    }
}