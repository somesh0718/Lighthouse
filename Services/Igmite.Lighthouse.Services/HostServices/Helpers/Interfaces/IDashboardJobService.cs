using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.HostServices.Helpers
{
    public interface IDashboardJobService
    {
        Task PerformDashboardJobService(string schedule);
    }
}
