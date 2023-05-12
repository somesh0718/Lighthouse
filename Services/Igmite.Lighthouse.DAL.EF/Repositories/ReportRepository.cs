using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the Report entity
    /// </summary>
    public class ReportRepository : GenericRepository<DataType>, IReportRepository
    {
        /// <summary>
        /// Get Lighthouse report data with filter criteria
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IList<T> GetLighthouseReports<T>(ReportFilterModel searchModel)
        {
            #region "Report Parameters"

            MySqlParameter[] sqlParams = new MySqlParameter[10];
            sqlParams[0] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.UserId.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
            sqlParams[2] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.DivisionId };
            sqlParams[3] = new MySqlParameter { ParameterName = "DistrictId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.DistrictId };
            sqlParams[4] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
            sqlParams[5] = new MySqlParameter { ParameterName = "JobRoleId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.JobRoleId };
            sqlParams[6] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
            sqlParams[7] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.ClassId };
            sqlParams[8] = new MySqlParameter { ParameterName = "MonthId", MySqlDbType = MySqlDbType.String, Value = searchModel.MonthId };
            sqlParams[9] = new MySqlParameter { ParameterName = "SchoolManagementId", MySqlDbType = MySqlDbType.String, Value = searchModel.SchoolManagementId };

            #endregion "Report Parameters"

            try
            {
                if (typeof(T) == typeof(SchoolInfoReport))
                {
                    sqlParams = this.GetReportParamsForHM(searchModel);
                    IList<SchoolInfoReport> schoolInfoReports = Context.SchoolInfoReports.FromSql<SchoolInfoReport>("CALL GetSchoolInformationReportV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId, @HMId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < schoolInfoReports.Count; rowIndex++)
                    {
                        schoolInfoReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return schoolInfoReports as IList<T>;
                }
                else if (typeof(T) == typeof(CourseMaterialStatusReport))
                {
                    sqlParams = this.GetReportParamsForHM(searchModel);
                    IList<CourseMaterialStatusReport> courseMaterialStatusReports = Context.CourseMaterialStatusReports.FromSql<CourseMaterialStatusReport>("CALL GetCourseMaterialStatusReportV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId, @HMId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < courseMaterialStatusReports.Count; rowIndex++)
                    {
                        courseMaterialStatusReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return courseMaterialStatusReports as IList<T>;
                }
                else if (typeof(T) == typeof(ToolsAndEquipmentStatusReport))
                {
                    sqlParams = this.GetReportParamsForHM(searchModel);
                    IList<ToolsAndEquipmentStatusReport> toolsAndEquipmentStatusReports = Context.ToolsAndEquipmentStatusReports.FromSql<ToolsAndEquipmentStatusReport>("CALL GetToolsAndEquipmentStatusReportV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId, @HMId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < toolsAndEquipmentStatusReports.Count; rowIndex++)
                    {
                        toolsAndEquipmentStatusReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return toolsAndEquipmentStatusReports as IList<T>;
                }
                else if (typeof(T) == typeof(GuestLectureConductedReport))
                {
                    sqlParams = this.GetReportParamsForHM(searchModel);
                    IList<GuestLectureConductedReport> guestLectureConductedReports = Context.GuestLectureConductedReports.FromSql<GuestLectureConductedReport>("CALL GetGuestLectureConductedReportsByCriteriaV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId, @HMId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < guestLectureConductedReports.Count; rowIndex++)
                    {
                        guestLectureConductedReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return guestLectureConductedReports as IList<T>;
                }
                else if (typeof(T) == typeof(FieldIndustryVisitConductedReport))
                {
                    sqlParams = this.GetReportParamsForHM(searchModel);
                    IList<FieldIndustryVisitConductedReport> fieldIndustryVisitConductedReports = Context.FieldIndustryVisitConductedReports.FromSql<FieldIndustryVisitConductedReport>("CALL GetFieldVisitIndustryVisitConductedReportV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId, @HMId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < fieldIndustryVisitConductedReports.Count; rowIndex++)
                    {
                        fieldIndustryVisitConductedReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return fieldIndustryVisitConductedReports as IList<T>;
                }
                else if (typeof(T) == typeof(StudentEnrollmentReport))
                {
                    sqlParams = this.GetReportParamsForHM(searchModel);
                    IList<StudentEnrollmentReport> studentEnrollmentReports = Context.StudentEnrollmentReports.FromSql<StudentEnrollmentReport>("CALL GetStudentEnrollmentReportV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId, @HMId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < studentEnrollmentReports.Count; rowIndex++)
                    {
                        studentEnrollmentReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return studentEnrollmentReports as IList<T>;
                }
                else if (typeof(T) == typeof(GuestLectureStatusReport))
                {
                    sqlParams = this.GetReportParamsForHM(searchModel);
                    IList<GuestLectureStatusReport> guestLectureStatusReports = Context.GuestLectureStatusReports.FromSql<GuestLectureStatusReport>("CALL GetGuestLectureStatusReportV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId, @HMId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < guestLectureStatusReports.Count; rowIndex++)
                    {
                        guestLectureStatusReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return guestLectureStatusReports as IList<T>;
                }
                else if (typeof(T) == typeof(FieldAndIndustryVisitStatusReport))
                {
                    sqlParams = this.GetReportParamsForHM(searchModel);
                    IList<FieldAndIndustryVisitStatusReport> fieldAndIndustryVisitStatusReports = Context.FieldAndIndustryVisitStatusReports.FromSql<FieldAndIndustryVisitStatusReport>("CALL GetFieldAndIndustryVisitStatusReportV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId, @HMId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < fieldAndIndustryVisitStatusReports.Count; rowIndex++)
                    {
                        fieldAndIndustryVisitStatusReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return fieldAndIndustryVisitStatusReports as IList<T>;
                }
                else if (typeof(T) == typeof(VTReportingAttendanceReport))
                {
                    sqlParams = this.GetReportParamsForHM(searchModel);
                    IList<VTReportingAttendanceReport> vtReportingAttendanceReports = Context.VTReportingAttendanceReports.FromSql<VTReportingAttendanceReport>("CALL GetVTReportingAttendanceReportsByCriteriaV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId, @HMId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < vtReportingAttendanceReports.Count; rowIndex++)
                    {
                        vtReportingAttendanceReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return vtReportingAttendanceReports as IList<T>;
                }
                else if (typeof(T) == typeof(VCReportingAttendanceReport))
                {
                    IList<VCReportingAttendanceReport> vcReportingAttendanceReports = Context.VCReportingAttendanceReports.FromSql<VCReportingAttendanceReport>("CALL GetVCReportingAttendanceReportsByCriteriaV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < vcReportingAttendanceReports.Count; rowIndex++)
                    {
                        vcReportingAttendanceReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return vcReportingAttendanceReports as IList<T>;
                }
                else if (typeof(T) == typeof(StudentAttendanceReportingReport))
                {
                    IList<StudentAttendanceReportingReport> studentAttendanceReportingReports = Context.StudentAttendanceReportingReports.FromSql<StudentAttendanceReportingReport>("CALL GetStudentAttendanceReportingReportV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < studentAttendanceReportingReports.Count; rowIndex++)
                    {
                        studentAttendanceReportingReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return studentAttendanceReportingReports as IList<T>;
                }
                else if (typeof(T) == typeof(StudentDetailsReport))
                {
                    sqlParams = new MySqlParameter[14];
                    sqlParams[0] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.UserId };
                    sqlParams[1] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
                    sqlParams[2] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.DivisionId };
                    sqlParams[3] = new MySqlParameter { ParameterName = "DistrictId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.DistrictId };
                    sqlParams[4] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
                    sqlParams[5] = new MySqlParameter { ParameterName = "JobRoleId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.JobRoleId };
                    sqlParams[6] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
                    sqlParams[7] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.ClassId };
                    sqlParams[8] = new MySqlParameter { ParameterName = "MonthId", MySqlDbType = MySqlDbType.String, Value = searchModel.MonthId };
                    sqlParams[9] = new MySqlParameter { ParameterName = "SchoolManagementId", MySqlDbType = MySqlDbType.String, Value = searchModel.SchoolManagementId };
                    sqlParams[10] = new MySqlParameter { ParameterName = "HMId", MySqlDbType = MySqlDbType.String, Value = searchModel.HMId };
                    sqlParams[11] = new MySqlParameter { ParameterName = "Name", MySqlDbType = MySqlDbType.String, Value = searchModel.Name };
                    sqlParams[12] = new MySqlParameter { ParameterName = "PageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex };
                    sqlParams[13] = new MySqlParameter { ParameterName = "PageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize };

                    IList<StudentDetailsReport> studentDetailsReports = Context.StudentDetailsReports.FromSql<StudentDetailsReport>("CALL GetStudentDetailsReportV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId, @HMId, @Name, @PageIndex, @PageSize)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < studentDetailsReports.Count; rowIndex++)
                    {
                        studentDetailsReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return studentDetailsReports as IList<T>;
                }
                else if (typeof(T) == typeof(VCSchoolVisitSummaryReport))
                {
                    IList<VCSchoolVisitSummaryReport> vcSchoolVisitSummaryReports = Context.VCSchoolVisitSummaryReports.FromSql<VCSchoolVisitSummaryReport>("CALL GetVCSchoolVisitSummaryReportV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < vcSchoolVisitSummaryReports.Count; rowIndex++)
                    {
                        vcSchoolVisitSummaryReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return vcSchoolVisitSummaryReports as IList<T>;
                }
                else if (typeof(T) == typeof(VocationalTrainerAttendanceReport))
                {
                    IList<VocationalTrainerAttendanceReport> vocationalTrainerAttendanceReports = Context.VocationalTrainerAttendanceReports.FromSql<VocationalTrainerAttendanceReport>("CALL GetVocationalTrainerAttendanceReportV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < vocationalTrainerAttendanceReports.Count; rowIndex++)
                    {
                        vocationalTrainerAttendanceReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return vocationalTrainerAttendanceReports as IList<T>;
                }
                else if (typeof(T) == typeof(VTPBillSubmissionStatusReport))
                {
                    IList<VTPBillSubmissionStatusReport> vtpBillSubmissionStatusReports = Context.VTPBillSubmissionStatusReports.FromSql<VTPBillSubmissionStatusReport>("CALL GetVTPBillSubmissionStatusReportV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < vtpBillSubmissionStatusReports.Count; rowIndex++)
                    {
                        vtpBillSubmissionStatusReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return vtpBillSubmissionStatusReports as IList<T>;
                }
                else if (typeof(T) == typeof(SchoolVTPSectorReport))
                {
                    IList<SchoolVTPSectorReport> schoolVTPSectorReports = Context.SchoolVTPSectorReports.FromSql<SchoolVTPSectorReport>("CALL GetSchoolVTPSectorReportsByCriteriaV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < schoolVTPSectorReports.Count; rowIndex++)
                    {
                        schoolVTPSectorReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return schoolVTPSectorReports as IList<T>;
                }
                else if (typeof(T) == typeof(VTSchoolSectorReport))
                {
                    IList<VTSchoolSectorReport> vtSchoolSectorReports = Context.VTSchoolSectorReports.FromSql<VTSchoolSectorReport>("CALL GetVTSchoolSectorReportsByCriteriaV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < vtSchoolSectorReports.Count; rowIndex++)
                    {
                        vtSchoolSectorReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return vtSchoolSectorReports as IList<T>;
                }
                else if (typeof(T) == typeof(VCSchoolSectorReport))
                {
                    IList<VCSchoolSectorReport> vcSchoolSectorReports = Context.VCSchoolSectorReports.FromSql<VCSchoolSectorReport>("CALL GetVCSchoolSectorReportsByCriteriaV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < vcSchoolSectorReports.Count; rowIndex++)
                    {
                        vcSchoolSectorReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return vcSchoolSectorReports as IList<T>;
                }
                else if (typeof(T) == typeof(VCIssueReport))
                {
                    IList<VCIssueReport> vcIssueReports = Context.VCIssueReports.FromSql<VCIssueReport>("CALL GetVCIssueReportsByCriteriaV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < vcIssueReports.Count; rowIndex++)
                    {
                        vcIssueReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return vcIssueReports as IList<T>;
                }
                else if (typeof(T) == typeof(VTIssueReport))
                {
                    IList<VTIssueReport> vtIssueReports = Context.VTIssueReports.FromSql<VTIssueReport>("CALL GetVTIssueReportsByCriteriaV2 (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < vtIssueReports.Count; rowIndex++)
                    {
                        vtIssueReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return vtIssueReports as IList<T>;
                }
                else if (typeof(T) == typeof(VTDailyAttendanceTrackingReport))
                {
                    //sqlParams = this.GetReportParamsForDC(searchModel);
                    sqlParams = new MySqlParameter[15];
                    sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
                    sqlParams[1] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
                    sqlParams[2] = new MySqlParameter { ParameterName = "VCId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VCId };
                    sqlParams[3] = new MySqlParameter { ParameterName = "VTId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTId };
                    sqlParams[4] = new MySqlParameter { ParameterName = "SchoolId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SchoolId };
                    sqlParams[5] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
                    sqlParams[6] = new MySqlParameter { ParameterName = "JobRoleId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.JobRoleId };
                    sqlParams[7] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.DivisionId };
                    sqlParams[8] = new MySqlParameter { ParameterName = "DistrictId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.DistrictId };
                    sqlParams[9] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.ClassId };
                    sqlParams[10] = new MySqlParameter { ParameterName = "SchoolManagementId", MySqlDbType = MySqlDbType.String, Value = searchModel.SchoolManagementId };
                    sqlParams[11] = new MySqlParameter { ParameterName = "FromDate", MySqlDbType = MySqlDbType.DateTime, Value = searchModel.FromDate };
                    sqlParams[12] = new MySqlParameter { ParameterName = "ToDate", MySqlDbType = MySqlDbType.DateTime, Value = searchModel.ToDate };
                    sqlParams[13] = new MySqlParameter { ParameterName = "PageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex };
                    sqlParams[14] = new MySqlParameter { ParameterName = "PageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize };

                    IList<VTDailyAttendanceTrackingReport> vtReportingAttendanceReports = Context.VTDailyAttendanceTrackingReports.FromSql<VTDailyAttendanceTrackingReport>("CALL GetVTDailyAttendanceTrackingByCriteriaV2 (@AcademicYearId, @VTPId, @VCId, @VTId, @SchoolId, @SectorId, @JobRoleId, @DivisionId, @DistrictId, @ClassId, @SchoolManagementId, @FromDate, @ToDate, @PageIndex, @PageSize)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < vtReportingAttendanceReports.Count; rowIndex++)
                    {
                        vtReportingAttendanceReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return vtReportingAttendanceReports as IList<T>;
                }
                else if (typeof(T) == typeof(VTDailyReportingNotSubmittedModel))
                {
                    sqlParams = new MySqlParameter[3];
                    sqlParams[0] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.UserId.StringVal() };
                    sqlParams[1] = new MySqlParameter { ParameterName = "FromDate", MySqlDbType = MySqlDbType.DateTime, Value = searchModel.FromDate };
                    sqlParams[2] = new MySqlParameter { ParameterName = "ToDate", MySqlDbType = MySqlDbType.DateTime, Value = searchModel.ToDate };

                    IList<VTDailyReportingNotSubmittedModel> vtReportingAttendanceReports = Context.VTDailyReportingNotSubmittedModels.FromSql<VTDailyReportingNotSubmittedModel>("CALL GetVTDailyReportNotSubmittedTrackingByCriteriaV2 (@UserId, @FromDate, @ToDate)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < vtReportingAttendanceReports.Count; rowIndex++)
                    {
                        vtReportingAttendanceReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return vtReportingAttendanceReports as IList<T>;
                }
                else if (typeof(T) == typeof(VCDailyAttendanceTrackingReport))
                {
                    sqlParams = this.GetReportParamsForDC(searchModel);
                    IList<VCDailyAttendanceTrackingReport> vtReportingAttendanceReports = Context.VCDailyAttendanceTrackingReports.FromSql<VCDailyAttendanceTrackingReport>("CALL GetVCDailyAttendanceTrackingByCriteriaV2(@AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @SchoolManagementId, @UserId, @FromDate, @ToDate)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < vtReportingAttendanceReports.Count; rowIndex++)
                    {
                        vtReportingAttendanceReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return vtReportingAttendanceReports as IList<T>;
                }
                else if (typeof(T) == typeof(VTStudentTrackingReport))
                {
                    sqlParams = this.GetReportParamsForDC(searchModel);

                    IList<VTStudentTrackingReport> vtReportingAttendanceReports = Context.VTStudentTrackingReports.FromSql<VTStudentTrackingReport>("CALL GetVTStudentTrackingByCriteriaV2 (@AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @SchoolManagementId, @UserId, @FromDate, @ToDate)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < vtReportingAttendanceReports.Count; rowIndex++)
                    {
                        vtReportingAttendanceReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return vtReportingAttendanceReports as IList<T>;
                }
                else if (typeof(T) == typeof(VTCourseModuleDailyTrackingReport))
                {
                    sqlParams = new MySqlParameter[7];
                    sqlParams[0] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.UserId.StringVal() };
                    sqlParams[1] = new MySqlParameter { ParameterName = "FromDate", MySqlDbType = MySqlDbType.DateTime, Value = searchModel.FromDate };
                    sqlParams[2] = new MySqlParameter { ParameterName = "ToDate", MySqlDbType = MySqlDbType.DateTime, Value = searchModel.ToDate };
                    sqlParams[3] = new MySqlParameter { ParameterName = "VTId", MySqlDbType = MySqlDbType.DateTime, Value = searchModel.VTId };
                    sqlParams[4] = new MySqlParameter { ParameterName = "SchoolId", MySqlDbType = MySqlDbType.DateTime, Value = searchModel.SchoolId };
                    sqlParams[5] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.DateTime, Value = searchModel.ClassId };
                    sqlParams[6] = new MySqlParameter { ParameterName = "SectionId", MySqlDbType = MySqlDbType.DateTime, Value = searchModel.SectionId };

                    IList<VTCourseModuleDailyTrackingReport> vtReportingAttendanceReports = Context.VTCourseModuleDailyTrackingReports.FromSql<VTCourseModuleDailyTrackingReport>("CALL GetVTCourseModuleDailyTrackingByCriteriaV2 (@UserId, @FromDate, @ToDate, @VTId, @SchoolId, @ClassId, @SectionId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < vtReportingAttendanceReports.Count; rowIndex++)
                    {
                        vtReportingAttendanceReports[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return vtReportingAttendanceReports as IList<T>;
                }
                else if (typeof(T) == typeof(VTDailyMonthlyModel))
                {
                    sqlParams = new MySqlParameter[10];
                    sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
                    sqlParams[1] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
                    sqlParams[2] = new MySqlParameter { ParameterName = "VCId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VCId };
                    sqlParams[3] = new MySqlParameter { ParameterName = "HMId", MySqlDbType = MySqlDbType.String, Value = searchModel.HMId };
                    sqlParams[4] = new MySqlParameter { ParameterName = "VTId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.VTId };
                    sqlParams[5] = new MySqlParameter { ParameterName = "SchoolId", MySqlDbType = MySqlDbType.DateTime, Value = searchModel.SchoolId };
                    sqlParams[6] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.DivisionId };
                    sqlParams[7] = new MySqlParameter { ParameterName = "DistrictId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.DistrictId };
                    sqlParams[8] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
                    sqlParams[9] = new MySqlParameter { ParameterName = "ReportDate", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.ReportDate };

                    IList<VTDailyMonthlyModel> vtDailyMonthlyModels = Context.VTDailyMonthlyModels.FromSql<VTDailyMonthlyModel>("CALL GetVTDailyMonthlyTrackingByCriteria (@AcademicYearId, @VTPId, @VCId, @HMId, @VTId, @SchoolId, @DivisionId, @DistrictId, @SectorId, @ReportDate)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < vtDailyMonthlyModels.Count; rowIndex++)
                    {
                        vtDailyMonthlyModels[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return vtDailyMonthlyModels as IList<T>;
                }
                else if (typeof(T) == typeof(VCDailyMonthlyModel))
                {
                    sqlParams = new MySqlParameter[9];
                    sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
                    sqlParams[1] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
                    sqlParams[2] = new MySqlParameter { ParameterName = "VCId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VCId };
                    sqlParams[3] = new MySqlParameter { ParameterName = "HMId", MySqlDbType = MySqlDbType.String, Value = searchModel.HMId };
                    sqlParams[4] = new MySqlParameter { ParameterName = "SchoolId", MySqlDbType = MySqlDbType.DateTime, Value = searchModel.SchoolId };
                    sqlParams[5] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.DivisionId };
                    sqlParams[6] = new MySqlParameter { ParameterName = "DistrictId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.DistrictId };
                    sqlParams[7] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
                    sqlParams[8] = new MySqlParameter { ParameterName = "ReportDate", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.ReportDate };

                    IList<VCDailyMonthlyModel> vcDailyMonthlyModels = Context.VCDailyMonthlyModels.FromSql<VCDailyMonthlyModel>("CALL GetVCDailyMonthlyTrackingByCriteria (@AcademicYearId, @VTPId, @VCId, @HMId, @SchoolId, @DivisionId, @DistrictId, @SectorId, @ReportDate)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < vcDailyMonthlyModels.Count; rowIndex++)
                    {
                        vcDailyMonthlyModels[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return vcDailyMonthlyModels as IList<T>;
                }
                else if (typeof(T) == typeof(VTPMonthlyModel))
                {
                    sqlParams = new MySqlParameter[11];
                    sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
                    sqlParams[1] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
                    sqlParams[2] = new MySqlParameter { ParameterName = "VCId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VCId };
                    sqlParams[3] = new MySqlParameter { ParameterName = "HMId", MySqlDbType = MySqlDbType.String, Value = searchModel.HMId };
                    sqlParams[4] = new MySqlParameter { ParameterName = "VTId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.VTId };
                    sqlParams[5] = new MySqlParameter { ParameterName = "SchoolId", MySqlDbType = MySqlDbType.DateTime, Value = searchModel.SchoolId };
                    sqlParams[6] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.DivisionId };
                    sqlParams[7] = new MySqlParameter { ParameterName = "DistrictId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.DistrictId };
                    sqlParams[8] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
                    sqlParams[9] = new MySqlParameter { ParameterName = "PageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex };
                    sqlParams[10] = new MySqlParameter { ParameterName = "PageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize };

                    IList<VTPMonthlyModel> vtpMonthlyModels = Context.VTPMonthlyModels.FromSql<VTPMonthlyModel>("CALL GetVTPMonthlyTrackingByCriteria (@AcademicYearId, @VTPId, @VCId, @HMId, @VTId, @SchoolId, @DivisionId, @DistrictId, @SectorId, @PageIndex, @PageSize)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < vtpMonthlyModels.Count; rowIndex++)
                    {
                        vtpMonthlyModels[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return vtpMonthlyModels as IList<T>;
                }
                else if (typeof(T) == typeof(VEAModel))
                {
                    sqlParams = new MySqlParameter[16];
                    sqlParams[0] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.UserId };
                    sqlParams[1] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
                    sqlParams[2] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.DivisionId };
                    sqlParams[3] = new MySqlParameter { ParameterName = "DistrictId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.DistrictId };
                    sqlParams[4] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
                    sqlParams[5] = new MySqlParameter { ParameterName = "JobRoleId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.JobRoleId };
                    sqlParams[6] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
                    sqlParams[7] = new MySqlParameter { ParameterName = "VCId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VCId };
                    sqlParams[8] = new MySqlParameter { ParameterName = "SchoolId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SchoolId };
                    sqlParams[9] = new MySqlParameter { ParameterName = "VTId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTId };
                    sqlParams[10] = new MySqlParameter { ParameterName = "HMId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.HMId };
                    sqlParams[11] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.ClassId };
                    sqlParams[12] = new MySqlParameter { ParameterName = "GenderId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.GenderId };
                    sqlParams[13] = new MySqlParameter { ParameterName = "Name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name };
                    sqlParams[14] = new MySqlParameter { ParameterName = "PageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex };
                    sqlParams[15] = new MySqlParameter { ParameterName = "PageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize };

                    IList<VEAModel> vEAModels = Context.VEAModels.FromSql<VEAModel>("CALL GetStudentAssesmentReport (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @VCId, @SchoolId, @VTId, @HMId, @ClassId, @GenderId, @Name, @PageIndex, @PageSize)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < vEAModels.Count; rowIndex++)
                    {
                        vEAModels[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return vEAModels as IList<T>;
                }
                else if (typeof(T) == typeof(LabConditionModel))
                {
                    sqlParams = this.GetReportParamsForHM(searchModel);

                    IList<LabConditionModel> labConditionModels = Context.LabConditionModels.FromSql<LabConditionModel>("CALL GetLabConditionReport (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId, @HMId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < labConditionModels.Count; rowIndex++)
                    {
                        labConditionModels[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return labConditionModels as IList<T>;
                }
                else if (typeof(T) == typeof(ToolListModel))
                {
                    sqlParams = this.GetReportParamsForHM(searchModel);

                    IList<ToolListModel> toolListModels = Context.ToolListModels.FromSql<ToolListModel>("CALL GetToolListReport (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId, @HMId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < toolListModels.Count; rowIndex++)
                    {
                        toolListModels[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return toolListModels as IList<T>;
                }
                else if (typeof(T) == typeof(MaterialListModel))
                {
                    sqlParams = this.GetReportParamsForHM(searchModel);

                    IList<MaterialListModel> materialListModels = Context.MaterialListModels.FromSql<MaterialListModel>("CALL GetMaterialListReport (@UserId, @AcademicYearId, @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId, @HMId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < materialListModels.Count; rowIndex++)
                    {
                        materialListModels[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return materialListModels as IList<T>;
                }
                else if (typeof(T) == typeof(PraticalAssesmentModel))
                {
                    sqlParams = new MySqlParameter[10];
                    sqlParams[0] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.UserId };
                    sqlParams[1] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
                    sqlParams[2] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.DivisionId };
                    sqlParams[3] = new MySqlParameter { ParameterName = "DistrictId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.DistrictId };
                    sqlParams[4] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
                    sqlParams[5] = new MySqlParameter { ParameterName = "JobRoleId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.JobRoleId };
                    sqlParams[6] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
                    sqlParams[7] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.ClassId };
                    sqlParams[8] = new MySqlParameter { ParameterName = "MonthId", MySqlDbType = MySqlDbType.String, Value = searchModel.MonthId };
                    sqlParams[9] = new MySqlParameter { ParameterName = "SchoolManagementId", MySqlDbType = MySqlDbType.String, Value = searchModel.SchoolManagementId };

                    IList<PraticalAssesmentModel> praticalAssesmentModels = Context.PraticalAssesmentModels.FromSql<PraticalAssesmentModel>("CALL GetPracticalAssessmentReport  (@UserId, @AcademicYearId , @DivisionId, @DistrictId, @SectorId, @JobRoleId, @VTPId, @ClassId, @MonthId, @SchoolManagementId)", sqlParams).ToList();

                    for (int rowIndex = 0; rowIndex < praticalAssesmentModels.Count; rowIndex++)
                    {
                        praticalAssesmentModels[rowIndex].SrNo = (searchModel.PageSize * searchModel.PageIndex) + rowIndex + 1;
                    }

                    return praticalAssesmentModels as IList<T>;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new List<T>();
        }

        private MySqlParameter[] GetReportParamsForDC(ReportFilterModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[11];
            sqlParams[0] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.DivisionId };
            sqlParams[2] = new MySqlParameter { ParameterName = "DistrictId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.DistrictId };
            sqlParams[3] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
            sqlParams[4] = new MySqlParameter { ParameterName = "JobRoleId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.JobRoleId };
            sqlParams[5] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
            sqlParams[6] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.ClassId };
            sqlParams[7] = new MySqlParameter { ParameterName = "SchoolManagementId", MySqlDbType = MySqlDbType.String, Value = searchModel.SchoolManagementId };
            sqlParams[8] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.UserId };
            sqlParams[9] = new MySqlParameter { ParameterName = "FromDate", MySqlDbType = MySqlDbType.DateTime, Value = searchModel.FromDate };
            sqlParams[10] = new MySqlParameter { ParameterName = "ToDate", MySqlDbType = MySqlDbType.DateTime, Value = searchModel.ToDate };

            return sqlParams;
        }

        private MySqlParameter[] GetReportParamsForHM(ReportFilterModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[11];
            sqlParams[0] = new MySqlParameter { ParameterName = "UserId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.UserId };
            sqlParams[1] = new MySqlParameter { ParameterName = "AcademicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
            sqlParams[2] = new MySqlParameter { ParameterName = "DivisionId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.DivisionId };
            sqlParams[3] = new MySqlParameter { ParameterName = "DistrictId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.DistrictId };
            sqlParams[4] = new MySqlParameter { ParameterName = "SectorId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SectorId };
            sqlParams[5] = new MySqlParameter { ParameterName = "JobRoleId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.JobRoleId };
            sqlParams[6] = new MySqlParameter { ParameterName = "VTPId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
            sqlParams[7] = new MySqlParameter { ParameterName = "ClassId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.ClassId };
            sqlParams[8] = new MySqlParameter { ParameterName = "MonthId", MySqlDbType = MySqlDbType.String, Value = searchModel.MonthId };
            sqlParams[9] = new MySqlParameter { ParameterName = "SchoolManagementId", MySqlDbType = MySqlDbType.String, Value = searchModel.SchoolManagementId };
            sqlParams[10] = new MySqlParameter { ParameterName = "HMId", MySqlDbType = MySqlDbType.String, Value = searchModel.HMId };

            return sqlParams;
        }

        /// <summary>
        /// Get VT Attendance Header For a Vocational Trainner
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vtId"></param>
        /// <param name="reportDate"></param>
        /// <returns></returns>
        public VTAttendanceHeaderModel GetVTAttendanceHeaderForPDF(string userId, Guid vtId, DateTime reportDate)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.VarChar, Value = userId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtId", MySqlDbType = MySqlDbType.Guid, Value = vtId };
            sqlParams[2] = new MySqlParameter { ParameterName = "reportDate", MySqlDbType = MySqlDbType.Date, Value = reportDate };

            return Context.VTAttendanceHeaderModels.FromSql<VTAttendanceHeaderModel>("CALL GetVTAttendanceHeaderForPDF (@userId, @vtId, @reportDate)", sqlParams).FirstOrDefault();
        }

        /// <summary>
        /// Get VT Attendance Details For a Vocational Trainner
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vtId"></param>
        /// <param name="reportDate"></param>
        /// <returns></returns>
        public IList<VTAttendanceDetailModel> GetVTAttendanceDetailForPDF(string userId, Guid vtId, DateTime reportDate)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.VarChar, Value = userId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtId", MySqlDbType = MySqlDbType.Guid, Value = vtId };
            sqlParams[2] = new MySqlParameter { ParameterName = "reportDate", MySqlDbType = MySqlDbType.Date, Value = reportDate };

            return Context.VTAttendanceDetailModels.FromSql<VTAttendanceDetailModel>("CALL GetVTAttendanceDetailForPDF (@userId, @vtId, @reportDate)", sqlParams).ToList();
        }

        /// <summary>
        /// Get VC Attendance Header For a Vocational Trainner
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vcId"></param>
        /// <param name="reportDate"></param>
        /// <returns></returns>
        public VCAttendanceHeaderModel GetVCAttendanceHeaderForPDF(string userId, Guid vcId, DateTime reportDate)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.VarChar, Value = userId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vcId", MySqlDbType = MySqlDbType.Guid, Value = vcId };
            sqlParams[2] = new MySqlParameter { ParameterName = "reportDate", MySqlDbType = MySqlDbType.Date, Value = reportDate };

            return Context.VCAttendanceHeaderModels.FromSql<VCAttendanceHeaderModel>("CALL GetVCAttendanceHeaderForPDF (@userId, @vcId, @reportDate)", sqlParams).FirstOrDefault();
        }

        /// <summary>
        /// Get VC Attendance Details For a Vocational Trainner
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vcId"></param>
        /// <param name="reportDate"></param>
        /// <returns></returns>
        public IList<VCAttendanceDetailModel> GetVCAttendanceDetailForPDF(string userId, Guid vcId, DateTime reportDate)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.VarChar, Value = userId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vcId", MySqlDbType = MySqlDbType.Guid, Value = vcId };
            sqlParams[2] = new MySqlParameter { ParameterName = "reportDate", MySqlDbType = MySqlDbType.Date, Value = reportDate };

            return Context.VCAttendanceDetailModels.FromSql<VCAttendanceDetailModel>("CALL GetVCAttendanceDetailForPDF (@userId, @vcId, @reportDate)", sqlParams).ToList();
        }

        /// <summary>
        /// Get VocationalEducationAssessment For a Student
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public VocationalEducationAssessmentReport GetVocationalEducationAssessmentForPDF(Guid academicYearId, Guid vtId, Guid schoolId)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[3];
            sqlParams[0] = new MySqlParameter { ParameterName = "academicYearId", MySqlDbType = MySqlDbType.Guid, Value = academicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtId", MySqlDbType = MySqlDbType.Guid, Value = vtId };
            sqlParams[2] = new MySqlParameter { ParameterName = "schoolId", MySqlDbType = MySqlDbType.Guid, Value = schoolId };
            VocationalEducationAssessmentReport vocationalEducationAssessmentReport = new VocationalEducationAssessmentReport();

            vocationalEducationAssessmentReport.VEAHeaderModels = Context.VEAHeaderModels.FromSql<VEAHeaderModel>("CALL GetVEAHeaderBySchoolAndVTId (@academicYearId,@vtId, @schoolId)", sqlParams).FirstOrDefault();

            vocationalEducationAssessmentReport.VEADetailsModels = Context.VEADetailsModels.FromSql<VEADetailsModel>("CALL GetVocationalEducationAssessmentBySchoolAndVTId (@academicYearId,@vtId, @schoolId)", sqlParams).ToList();
            return vocationalEducationAssessmentReport;
        }
    }
}