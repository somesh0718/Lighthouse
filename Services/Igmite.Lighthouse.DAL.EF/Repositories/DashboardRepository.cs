using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Models.Common;
using Igmite.Lighthouse.Platform;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Lighthouse Dashboard services
    /// </summary>
    public class DashboardRepository : GenericRepository<Account>, IDashboardRepository
    {
        /// <summary>
        /// Get Lighthouse dashboard data with filter criteria
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dashboardRequest"></param>
        /// <returns></returns>
        public IList<T> GetLighthouseDashboards<T>(DashboardDataRequest dashboardRequest)
        {
            #region "Report Parameters"

            MySqlParameter[] sqlParams = new MySqlParameter[11];
            sqlParams[0] = new MySqlParameter { ParameterName = "DataType", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DataType.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.UserId.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = dashboardRequest.AcademicYearId };
            sqlParams[3] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.Guid, Value = dashboardRequest.DivisionId };
            sqlParams[4] = new MySqlParameter { ParameterName = "DistrictCode", MySqlDbType = MySqlDbType.VarChar, Value = dashboardRequest.DistrictCode };
            sqlParams[5] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.Guid, Value = dashboardRequest.SectorId };
            sqlParams[6] = new MySqlParameter { ParameterName = "JobRoleId", MySqlDbType = MySqlDbType.Guid, Value = dashboardRequest.JobRoleId };
            sqlParams[7] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.Guid, Value = dashboardRequest.VTPId };
            sqlParams[8] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.Guid, Value = dashboardRequest.ClassId };
            sqlParams[9] = new MySqlParameter { ParameterName = "MonthId", MySqlDbType = MySqlDbType.String, Value = dashboardRequest.MonthId };
            sqlParams[10] = new MySqlParameter { ParameterName = "SchoolManagementId", MySqlDbType = MySqlDbType.String, Value = dashboardRequest.SchoolManagementId };

            #endregion "Report Parameters"

            try
            {
                #region "Compare Dashboards"

                if (typeof(T) == typeof(CompareSchoolModel))
                {
                    IList<CompareSchoolModel> schoolModels = Context.CompareSchoolModels.FromSql<CompareSchoolModel>("CALL GetCompareSchoolsDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return schoolModels as IList<T>;
                }
                else if (typeof(T) == typeof(CompareCourseMaterialModel))
                {
                    IList<CompareCourseMaterialModel> courseMaterialModels = Context.CompareCourseMaterialModels.FromSql<CompareCourseMaterialModel>("CALL GetCompareCourseMaterialsDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return courseMaterialModels as IList<T>;
                }
                else if (typeof(T) == typeof(CompareToolsAndEquipmentModel))
                {
                    IList<CompareToolsAndEquipmentModel> toolsAndEquipmentModels = Context.CompareToolsAndEquipmentModels.FromSql<CompareToolsAndEquipmentModel>("CALL GetCompareToolsAndEquipmentsDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return toolsAndEquipmentModels as IList<T>;
                }
                else if (typeof(T) == typeof(CompareStudentModel))
                {
                    IList<CompareStudentModel> studentModels = Context.CompareStudentModels.FromSql<CompareStudentModel>("CALL GetCompareStudentsDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return studentModels as IList<T>;
                }
                else if (typeof(T) == typeof(CompareNewEnrolmentAndDropoutStudentModel))
                {
                    IList<CompareNewEnrolmentAndDropoutStudentModel> newEnrolmentAndDropoutStudentModels = Context.CompareNewEnrolmentAndDropoutStudentModels.FromSql<CompareNewEnrolmentAndDropoutStudentModel>("CALL GetCompareNewEnrolmentAndDropoutStudentsDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return newEnrolmentAndDropoutStudentModels as IList<T>;
                }
                else if (typeof(T) == typeof(CompareGuestLectureModel))
                {
                    IList<CompareGuestLectureModel> guestLectureModels = Context.CompareGuestLectureModels.FromSql<CompareGuestLectureModel>("CALL GetCompareGuestLecturesDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return guestLectureModels as IList<T>;
                }
                else if (typeof(T) == typeof(CompareFieldVisitModel))
                {
                    IList<CompareFieldVisitModel> fieldVisitModels = Context.CompareFieldVisitModels.FromSql<CompareFieldVisitModel>("CALL GetCompareFieldVisitsDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return fieldVisitModels as IList<T>;
                }
                else if (typeof(T) == typeof(CompareTrainerModel))
                {
                    IList<CompareTrainerModel> trainerModels = Context.CompareTrainerModels.FromSql<CompareTrainerModel>("CALL GetCompareTrainersDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return trainerModels as IList<T>;
                }
                else if (typeof(T) == typeof(CompareCoordinatorModel))
                {
                    IList<CompareCoordinatorModel> coordinatorModels = Context.CompareCoordinatorModels.FromSql<CompareCoordinatorModel>("CALL GetCompareCoordinatorsDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return coordinatorModels as IList<T>;
                }
                else if (typeof(T) == typeof(CompareVTVCReportingModel))
                {
                    IList<CompareVTVCReportingModel> vtVCReportingModels = Context.CompareVTVCReportingModels.FromSql<CompareVTVCReportingModel>("CALL GetCompareVTVCReportingDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return vtVCReportingModels as IList<T>;
                }

                #endregion "Compare Dashboards"

                #region "Summary Dashboards"

                if (typeof(T) == typeof(DashboardSchoolModel))
                {
                    IList<DashboardSchoolModel> schoolModels = Context.DashboardSchoolModels.FromSql<DashboardSchoolModel>("CALL GetDashboardSchoolChartDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return schoolModels as IList<T>;
                }
                else if (typeof(T) == typeof(VocationalTrainersCardModel))
                {
                    IList<VocationalTrainersCardModel> trainerModels = Context.VocationalTrainersCardModels.FromSql<VocationalTrainersCardModel>("CALL GetDashboardVocationalTrainersCardDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return trainerModels as IList<T>;
                }
                else if (typeof(T) == typeof(ClassCardModel))
                {
                    IList<ClassCardModel> classModels = Context.ClassCardModels.FromSql<ClassCardModel>("CALL GetDashboardClassesCardDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return classModels as IList<T>;
                }
                else if (typeof(T) == typeof(StudentCardModel))
                {
                    IList<StudentCardModel> studentModels = Context.StudentCardModels.FromSql<StudentCardModel>("CALL GetDashboardStudentsCardDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return studentModels as IList<T>;
                }
                else if (typeof(T) == typeof(DashboardCourseMaterialModel))
                {
                    IList<DashboardCourseMaterialModel> courseMaterialModels = Context.DashboardCourseMaterialModels.FromSql<DashboardCourseMaterialModel>("CALL GetDashboardCourseMaterialChartDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return courseMaterialModels as IList<T>;
                }
                else if (typeof(T) == typeof(DashboardToolEquipmentModel))
                {
                    IList<DashboardToolEquipmentModel> toolEquipmentModels = Context.DashboardToolEquipmentModels.FromSql<DashboardToolEquipmentModel>("CALL GetDashboardToolsAndEquipmentChartDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return toolEquipmentModels as IList<T>;
                }
                else if (typeof(T) == typeof(DashboardGuestLectureModel))
                {
                    IList<DashboardGuestLectureModel> guestLectureModels = Context.DashboardGuestLectureModels.FromSql<DashboardGuestLectureModel>("CALL GetDashboardGuestLectureChartDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return guestLectureModels as IList<T>;
                }
                else if (typeof(T) == typeof(DashboardFieldVisitModel))
                {
                    IList<DashboardFieldVisitModel> fieldVisitModels = Context.DashboardFieldVisitModels.FromSql<DashboardFieldVisitModel>("CALL GetDashboardFieldVisitChartDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return fieldVisitModels as IList<T>;
                }
                else if (typeof(T) == typeof(DashboardModel))
                {
                    IList<DashboardModel> vtAttendanceModels = Context.DashboardModels.FromSql<DashboardModel>("CALL GetDashboardVTAttendanceChartDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return vtAttendanceModels as IList<T>;
                }
                else if (typeof(T) == typeof(DashboardStudentAttendanceModel))
                {
                    IList<DashboardStudentAttendanceModel> studentAttendanceModels = Context.DashboardStudentAttendanceModels.FromSql<DashboardStudentAttendanceModel>("CALL GetDashboardStudentAttendanceChartDataV2 (@DataType, @UserId, @AcademicYearId, @DivisionId, @DistrictCode, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    return studentAttendanceModels as IList<T>;
                }
                #endregion "Summary Dashboards"
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new List<T>();
        }
    }
}