using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VCDailyReporting entity
    /// </summary>
    public interface IVCDailyReportingRepository : IGenericRepository<VCDailyReporting>
    {
        /// <summary>
        /// Get list of VCDailyReporting
        /// </summary>
        /// <returns></returns>
        IQueryable<VCDailyReporting> GetVCDailyReportings();

        /// <summary>
        /// Get list of VCDailyReporting by vcDailyReportingName
        /// </summary>
        /// <param name="vcDailyReportingName"></param>
        /// <returns></returns>
        IQueryable<VCDailyReporting> GetVCDailyReportingsByName(string vcDailyReportingName);

        /// <summary>
        /// Get VCDailyReporting by VCDailyReportingId
        /// </summary>
        /// <param name="vcDailyReportingId"></param>
        /// <returns></returns>
        VCDailyReporting GetVCDailyReportingById(Guid vcDailyReportingId);

        /// <summary>
        /// Get VCDailyReporting by VC Id & Reporting Date
        /// </summary>
        /// <param name="vcId"></param>
        /// <param name="reportingDate"></param>
        /// <returns></returns>
        VCDailyReporting GetVCDailyReportingById(Guid vcId, DateTime reportingDate);

        /// <summary>
        /// Get VCDailyReporting by VCDailyReportingId using async
        /// </summary>
        /// <param name="vcDailyReportingId"></param>
        /// <returns></returns>
        Task<VCDailyReporting> GetVCDailyReportingByIdAsync(Guid vcDailyReportingId);

        /// <summary>
        /// Insert/Update VCDailyReporting entity
        /// </summary>
        /// <param name="vcDailyReporting"></param>
        /// <returns></returns>
        bool SaveOrUpdateVCDailyReportingDetails(VCDailyReporting vcDailyReporting, VCDailyReportingModel dailyReportingModel);

        /// <summary>
        /// Delete a record by VCDailyReportingId
        /// </summary>
        /// <param name="vcDailyReportingId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vcDailyReportingId);

        /// <summary>
        /// Check duplicate VCDailyReporting by Name
        /// </summary>
        /// <param name="vcDailyReportingModel"></param>
        /// <returns></returns>
        List<string> CheckVCDailyReportingExistByName(VCDailyReportingModel vcDailyReportingModel);

        /// <summary>
        /// List of VCDailyReporting with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VCDailyReportingViewModel> GetVCDailyReportingsByCriteria(SearchVCDailyReportingModel searchModel);

        IList<string> GetWorkTypesByDailyReportingId(Guid dailyReportingId);

        VCRIndustryExposureVisitModel GetIndustryExposureVisitByDailyReportingId(Guid dailyReportingId);

        LeaveModel GetLeaveByDailyReportingId(Guid dailyReportingId);

        HolidayModel GetHolidayByDailyReportingId(Guid dailyReportingId);

        VCRPraticalModel GetPraticalByDailyReportingId(Guid dailyReportingId);
    }
}