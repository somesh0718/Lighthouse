using Igmite.Lighthouse.Models;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Repository of the Dashboard Job Works
    /// </summary>
    public interface IDashboardJobManager : IGenericManager<RoleModel>
    {
        /// <summary>
        /// Generate Information for Summary Dashboard
        /// </summary>
        Task GenerateDashboardInformationAsync();

        /// <summary>
        /// Generate Not Submitted Reporting data for all Vocational Trainers
        /// </summary>
        Task GenerateNotSubmittedReportByVTAsync();

        /// <summary>
        /// Send Weekly VC/VT Attendance Message To User
        /// </summary>
        Task SendWeeklyAttendanceMessageToUserAsync();
    }
}