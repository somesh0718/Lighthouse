using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the Dashboard Job Works
    /// </summary>
    public class DashboardJobRepository : GenericRepository<ExecuteScriptQuery>, IDashboardJobRepository
    {
        /// <summary>
        /// Is Executed Generating Dashboard Data Scripts
        /// </summary>
        /// <param name="reportDate"></param>
        /// <returns></returns>
        public async Task<KeyValuePair<Guid, IList<string>>> IsExecutedGeneratingDashboardDataScripts(DateTime reportDate)
        {
            return await Task<KeyValuePair<Guid, IList<string>>>.Run(() =>
             {
                 AcademicYear academicYear = Context.AcademicYears.FirstOrDefault(y => y.IsCurrentAcademicYear == true);

                 IList<string> dataManagements = Context.DsDataManagement.Where(v => v.AcademicYearId == academicYear.AcademicYearId && v.ReportDate.Date == reportDate.Date).Select(r => r.DataType).ToList();

                 return new KeyValuePair<Guid, IList<string>>(academicYear.AcademicYearId, dataManagements);
             });
        }

        /// <summary>
        /// Generate School Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        public async Task GenerateDashboardSchoolInfoAsync(Guid academicYearId, DateTime reportDate)
        {
            await Task.Run(() =>
            {
                MySqlParameter[] sqlParams = new MySqlParameter[2];
                sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = academicYearId };
                sqlParams[1] = new MySqlParameter { ParameterName = "ReportDate", MySqlDbType = MySqlDbType.VarChar, Value = reportDate };

                Context.Database.ExecuteSqlCommand("CALL GenSchoolsDataV2 (@AcademicYearId, @ReportDate)", sqlParams);
                //Context.Query<ExecuteScriptQuery>().FromSql("CALL GenSchoolsData (@AcademicYearId, @ReportDate)", sqlParams).FirstOrDefaultAsync();
                //Context.ExecuteScriptQuery.FromSql<ExecuteScriptQuery>("CALL GenSchoolsData (@AcademicYearId, @ReportDate)", sqlParams);
            });
        }

        /// <summary>
        /// Generate Trainer Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        public async Task GenerateDashboardTrainerInfoAsync(Guid academicYearId, DateTime reportDate)
        {
            await Task.Run(() =>
            {
                MySqlParameter[] sqlParams = new MySqlParameter[2];
                sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = academicYearId };
                sqlParams[1] = new MySqlParameter { ParameterName = "ReportDate", MySqlDbType = MySqlDbType.VarChar, Value = reportDate };

                Context.Database.ExecuteSqlCommand("CALL GenTrainersDataV2 (@AcademicYearId, @ReportDate)", sqlParams);
            });
        }

        /// <summary>
        /// Generate Class Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        public async Task GenerateDashboardClassInfoAsync(Guid academicYearId, DateTime reportDate)
        {
            await Task.Run(() =>
            {
                MySqlParameter[] sqlParams = new MySqlParameter[2];
                sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = academicYearId };
                sqlParams[1] = new MySqlParameter { ParameterName = "ReportDate", MySqlDbType = MySqlDbType.VarChar, Value = reportDate };

                Context.Database.ExecuteSqlCommand("CALL GenClassesDataV2 (@AcademicYearId, @ReportDate)", sqlParams);
            });
        }

        /// <summary>
        /// Generate Student Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        public async Task GenerateDashboardStudentInfoAsync(Guid academicYearId, DateTime reportDate)
        {
            await Task.Run(() =>
            {
                MySqlParameter[] sqlParams = new MySqlParameter[2];
                sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = academicYearId };
                sqlParams[1] = new MySqlParameter { ParameterName = "ReportDate", MySqlDbType = MySqlDbType.VarChar, Value = reportDate };

                Context.Database.ExecuteSqlCommand("CALL GenStudentsDataV2 (@AcademicYearId, @ReportDate)", sqlParams);
            });
        }

        /// <summary>
        /// Generate Class of School Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        public async Task GenerateDashboardSchoolClassesInfoAsync(Guid academicYearId, DateTime reportDate)
        {
            await Task.Run(() =>
            {
                MySqlParameter[] sqlParams = new MySqlParameter[2];
                sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = academicYearId };
                sqlParams[1] = new MySqlParameter { ParameterName = "ReportDate", MySqlDbType = MySqlDbType.VarChar, Value = reportDate };

                Context.Database.ExecuteSqlCommand("CALL GenSchoolClassesDataV2 (@AcademicYearId, @ReportDate)", sqlParams);
            });
        }

        /// <summary>
        /// Generate Course Material Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        public async Task GenerateDashboardCourseMaterialInfoAsync(Guid academicYearId, DateTime reportDate)
        {
            await Task.Run(() =>
            {
                MySqlParameter[] sqlParams = new MySqlParameter[2];
                sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = academicYearId };
                sqlParams[1] = new MySqlParameter { ParameterName = "ReportDate", MySqlDbType = MySqlDbType.VarChar, Value = reportDate };

                Context.Database.ExecuteSqlCommand("CALL GenCourseMaterialDataV2 (@AcademicYearId, @ReportDate)", sqlParams);
            });
        }

        /// <summary>
        /// Generate Sector JobRole Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        public async Task GenerateDashboardSectorJobRoleInfoAsync(Guid academicYearId, DateTime reportDate)
        {
            await Task.Run(() =>
            {
                MySqlParameter[] sqlParams = new MySqlParameter[2];
                sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = academicYearId };
                sqlParams[1] = new MySqlParameter { ParameterName = "ReportDate", MySqlDbType = MySqlDbType.VarChar, Value = reportDate };

                Context.Database.ExecuteSqlCommand("CALL GenSectorJobRoleDataV2 (@AcademicYearId, @ReportDate)", sqlParams);
            });
        }

        /// <summary>
        /// Generate Tools & Equipments Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        public async Task GenerateDashboardToolsAndEquipmentsInfoAsync(Guid academicYearId, DateTime reportDate)
        {
            await Task.Run(() =>
            {
                MySqlParameter[] sqlParams = new MySqlParameter[2];
                sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = academicYearId };
                sqlParams[1] = new MySqlParameter { ParameterName = "ReportDate", MySqlDbType = MySqlDbType.VarChar, Value = reportDate };

                Context.Database.ExecuteSqlCommand("CALL GenToolsAndEquipmentDataV2 (@AcademicYearId, @ReportDate)", sqlParams);
            });
        }

        /// <summary>
        /// Generate Field Visit Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        public async Task GenerateDashboardFieldVisitInfoAsync(Guid academicYearId, DateTime reportDate)
        {
            await Task.Run(() =>
            {
                MySqlParameter[] sqlParams = new MySqlParameter[2];
                sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = academicYearId };
                sqlParams[1] = new MySqlParameter { ParameterName = "ReportDate", MySqlDbType = MySqlDbType.VarChar, Value = reportDate };

                Context.Database.ExecuteSqlCommand("CALL GenFieldVisitDataV2 (@AcademicYearId, @ReportDate)", sqlParams);
            });
        }

        /// <summary>
        /// Generate Guest Lecture Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        public async Task GenerateDashboardGuestLectureInfoAsync(Guid academicYearId, DateTime reportDate)
        {
            await Task.Run(() =>
            {
                MySqlParameter[] sqlParams = new MySqlParameter[2];
                sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = academicYearId };
                sqlParams[1] = new MySqlParameter { ParameterName = "ReportDate", MySqlDbType = MySqlDbType.VarChar, Value = reportDate };

                Context.Database.ExecuteSqlCommand("CALL GenGuestLectureDataV2 (@AcademicYearId, @ReportDate)", sqlParams);
            });
        }

        /// <summary>
        /// Generate Trainer Attendance Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        public async Task GenerateDashboardTrainerAttendanceInfoAsync(Guid academicYearId, DateTime reportDate)
        {
            await Task.Run(() =>
            {
                MySqlParameter[] sqlParams = new MySqlParameter[2];
                sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = academicYearId };
                sqlParams[1] = new MySqlParameter { ParameterName = "ReportDate", MySqlDbType = MySqlDbType.VarChar, Value = reportDate };

                Context.Database.ExecuteSqlCommand("CALL GenTrainerAttendanceDataV2 (@AcademicYearId, @ReportDate)", sqlParams);
            });
        }

        /// <summary>
        /// Generate Coordinator Attendance Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        public async Task GenerateDashboardCoordinatorAttendanceInfoAsync(Guid academicYearId, DateTime reportDate)
        {
            await Task.Run(() =>
            {
                MySqlParameter[] sqlParams = new MySqlParameter[2];
                sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = academicYearId };
                sqlParams[1] = new MySqlParameter { ParameterName = "ReportDate", MySqlDbType = MySqlDbType.VarChar, Value = reportDate };

                Context.Database.ExecuteSqlCommand("CALL GenCoordinatorAttendanceDataV2 (@AcademicYearId, @ReportDate)", sqlParams);
            });
        }

        /// <summary>
        /// Generate Student Attendance Information for Summary Dashboard
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="reportDate"></param>
        public async Task GenerateDashboardStudentAttendanceInfoAsync(Guid academicYearId, DateTime reportDate)
        {
            await Task.Run(() =>
            {
                MySqlParameter[] sqlParams = new MySqlParameter[2];
                sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = academicYearId };
                sqlParams[1] = new MySqlParameter { ParameterName = "ReportDate", MySqlDbType = MySqlDbType.VarChar, Value = reportDate };

                Context.Database.ExecuteSqlCommand("CALL GenStudentAttendanceDataV2 (@AcademicYearId, @ReportDate)", sqlParams);
            });
        }

        /// <summary>
        /// Generate Not Submitted Reporting data for all Vocational Trainers
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public async Task GenerateNotSubmittedReportByVTAsync(DateTime startDate, DateTime endDate)
        {
            await Task.Run(() =>
            {
                MySqlParameter[] sqlParams = new MySqlParameter[2];
                sqlParams[0] = new MySqlParameter { ParameterName = "startDate", MySqlDbType = MySqlDbType.Date, Value = startDate.Date };
                sqlParams[1] = new MySqlParameter { ParameterName = "endDate", MySqlDbType = MySqlDbType.Date, Value = endDate.Date };

                Context.Database.ExecuteSqlCommand("CALL GenerateVTNotSubmittedDailyReportingDataV2 (@startDate, @endDate)", sqlParams);
            });
        }

        /// <summary>
        /// List of VT Not Reported Daily Attendances with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IList<VTNotReportedDailyAttendanceViewModel> GetVTNotReportedDailyAttendances(DateTime startDate, DateTime endDate)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[2];
            sqlParams[0] = new MySqlParameter { ParameterName = "startDate", MySqlDbType = MySqlDbType.DateTime, Value = startDate };
            sqlParams[1] = new MySqlParameter { ParameterName = "endDate", MySqlDbType = MySqlDbType.DateTime, Value = endDate };

            return Context.VTNotReportedDailyAttendanceViewModels.FromSql<VTNotReportedDailyAttendanceViewModel>("CALL GetVTNotReportedDailyAttendancesV2 (@startDate, @endDate)", sqlParams).ToList();
        }
    }
}