using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.HostServices.Helpers
{
    public interface IReportJobService
    {
        Task PerformReportJobService(string schedule);
    }
}