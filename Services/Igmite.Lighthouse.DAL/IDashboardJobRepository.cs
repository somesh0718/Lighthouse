using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the Dashboard Job Works
    /// </summary>
    public interface IDashboardJobRepository : IGenericRepository<ExecuteScriptQuery>
    {
        /// <summary>
        /// Is Executed Generating Dashboard Data Scripts
        /// </summary>
        /// <param name="reportDate"></param>
        /// <returns></returns>
        Task<KeyValuePair<Guid, IList<string>>> IsExecutedGeneratingDashboardDataScripts(DateTime reportDate);

        /// <summary>
        /// Generate School Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        Task GenerateDashboardSchoolInfoAsync(Guid academicYearId, DateTime reportDate);

        /// <summary>
        /// Generate Trainer Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        Task GenerateDashboardTrainerInfoAsync(Guid academicYearId, DateTime reportDate);

        /// <summary>
        /// Generate Class Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        Task GenerateDashboardClassInfoAsync(Guid academicYearId, DateTime reportDate);

        /// <summary>
        /// Generate Student Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        Task GenerateDashboardStudentInfoAsync(Guid academicYearId, DateTime reportDate);

        /// <summary>
        /// Generate Class of School Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        Task GenerateDashboardSchoolClassesInfoAsync(Guid academicYearId, DateTime reportDate);

        /// <summary>
        /// Generate Course Material Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        Task GenerateDashboardCourseMaterialInfoAsync(Guid academicYearId, DateTime reportDate);

        /// <summary>
        /// Generate Sector JobRole Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        Task GenerateDashboardSectorJobRoleInfoAsync(Guid academicYearId, DateTime reportDate);

        /// <summary>
        /// Generate Tools & Equipments Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        Task GenerateDashboardToolsAndEquipmentsInfoAsync(Guid academicYearId, DateTime reportDate);

        /// <summary>
        /// Generate Field Visit Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        Task GenerateDashboardFieldVisitInfoAsync(Guid academicYearId, DateTime reportDate);

        /// <summary>
        /// Generate Guest Lecture Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        Task GenerateDashboardGuestLectureInfoAsync(Guid academicYearId, DateTime reportDate);

        /// <summary>
        /// Generate Trainer Attendance Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        Task GenerateDashboardTrainerAttendanceInfoAsync(Guid academicYearId, DateTime reportDate);

        /// <summary>
        /// Generate Coordinator Attendance Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        Task GenerateDashboardCoordinatorAttendanceInfoAsync(Guid academicYearId, DateTime reportDate);

        /// <summary>
        /// Generate Student Attendance Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        Task GenerateDashboardStudentAttendanceInfoAsync(Guid academicYearId, DateTime reportDate);

        /// <summary>
        /// Generate Not Submitted Reporting data for all Vocational Trainers
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        Task GenerateNotSubmittedReportByVTAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// List of VT Not Reported Daily Attendances with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTNotReportedDailyAttendanceViewModel> GetVTNotReportedDailyAttendances(DateTime startDate, DateTime endDate);
    }
}