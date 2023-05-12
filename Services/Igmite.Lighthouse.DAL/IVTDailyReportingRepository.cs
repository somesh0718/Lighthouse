using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VTDailyReporting entity
    /// </summary>
    public interface IVTDailyReportingRepository : IGenericRepository<VTDailyReporting>
    {
        /// <summary>
        /// Get list of VTDailyReporting
        /// </summary>
        /// <returns></returns>
        IQueryable<VTDailyReporting> GetVTDailyReportings();

        /// <summary>
        /// Get list of VTDailyReporting by vtDailyReportingName
        /// </summary>
        /// <param name="vtDailyReportingName"></param>
        /// <returns></returns>
        IQueryable<VTDailyReporting> GetVTDailyReportingsByName(string vtDailyReportingName);

        /// <summary>
        /// Get VTDailyReporting by VTDailyReportingId
        /// </summary>
        /// <param name="vtDailyReportingId"></param>
        /// <returns></returns>
        VTDailyReporting GetVTDailyReportingById(Guid vtDailyReportingId);

        /// <summary>
        /// Get VTDailyReporting by VT Id & Reporting Date
        /// </summary>
        /// <param name="vtId"></param>
        /// <param name="reportingDate"></param>
        /// <returns></returns>
        VTDailyReporting GetVTDailyReportingById(Guid vtId, DateTime reportingDate);

        /// <summary>
        /// Get VTDailyReporting by VTDailyReportingId using async
        /// </summary>
        /// <param name="vtDailyReportingId"></param>
        /// <returns></returns>
        Task<VTDailyReporting> GetVTDailyReportingByIdAsync(Guid vtDailyReportingId);

        /// <summary>
        /// Insert/Update VTDailyReporting entity
        /// </summary>
        /// <param name="vtDailyReporting"></param>
        /// <returns></returns>
        bool SaveOrUpdateVTDailyReportingDetails(VTDailyReporting vtDailyReporting, VTDailyReportingModel dailyReportingModel);

        /// <summary>
        /// Delete a record by VTDailyReportingId
        /// </summary>
        /// <param name="vtDailyReportingId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtDailyReportingId);

        /// <summary>
        /// Check duplicate Daily Reporting by Type
        /// </summary>
        /// <param name="dailyReportingModel"></param>
        /// <returns>List of error messages for daily reporting submitted by VT</returns>
        List<string> CheckVTDailyReportingExistByName(VTDailyReportingModel dailyReportingModel);

        /// <summary>
        /// List of VTDailyReporting with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTDailyReportingViewModel> GetVTDailyReportingsByCriteria(SearchVTDailyReportingModel searchModel);

        /// <summary>
        /// Get list of VTWorkingTypes by dailyReportingId
        /// </summary>
        /// <param name="dailyReportingId"></param>
        /// <returns></returns>
        IList<string> GetWorkingTypesById(Guid dailyReportingId);

        VTDailyReportingModel GetVTDailyReportingDetailsById(Guid dailyReportingId);

        IList<VTRTeachingVocationalEducationModel> GetTeachingVocationalEducationsById(Guid dailyReportingId);

        VTRTrainingOfTeacherModel GetTrainingOfTeacherById(Guid dailyReportingId);

        VTROnJobTrainingCoordinationModel GetOnJobTrainingCoordinationById(Guid dailyReportingId);

        VTRAssessorInOtherSchoolForExamModel GetAssessorInOtherSchoolForExamById(Guid dailyReportingId);

        VTRParentTeachersMeetingModel GetParentTeachersMeetingById(Guid dailyReportingId);

        VTRCommunityHomeVisitModel GetCommunityHomeVisitById(Guid dailyReportingId);

        IList<VTRVisitToIndustryModel> GetVisitToIndustriesById(Guid dailyReportingId);

        IList<VTRVisitToEducationalInstitutionModel> GetVisitToEducationalInstitutionsById(Guid dailyReportingId);

        VTRAssignmentFromVocationalDepartmentModel GetAssignmentFromVocationalDepartmentById(Guid dailyReportingId);

        VTRTeachingNonVocationalSubjectModel GetTeachingNonVocationalSubjectById(Guid dailyReportingId);

        LeaveModel GetLeaveById(Guid dailyReportingId);

        HolidayModel GetHolidayById(Guid dailyReportingId);

        VTRObservationDayModel GetObservationDayById(Guid dailyReportingId);
    }
}