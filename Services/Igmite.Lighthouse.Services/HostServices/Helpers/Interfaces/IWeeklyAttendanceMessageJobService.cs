using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.HostServices.Helpers
{
    public interface IWeeklyAttendanceMessageJobService
    {
        Task PerformWeeklyAttendanceMessageJobService(string schedule);
    }
}