using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.HostServices.Helpers
{
    public interface INotSubmittedReportJobService
    {
        Task PerformNotSubmittedReportJobService(string schedule);
    }
}
