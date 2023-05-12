using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the DRPDailyReporting entity
    /// </summary>
    public interface IDRPDailyReportingRepository : IGenericRepository<DRPDailyReporting>
    {
        /// <summary>
        /// Get list of DRPDailyReporting
        /// </summary>
        /// <returns></returns>
        IQueryable<DRPDailyReporting> GetDRPDailyReportings();

        /// <summary>
        /// Get list of DRPDailyReporting by dailyReportingName
        /// </summary>
        /// <param name="dailyReportingName"></param>
        /// <returns></returns>
        IQueryable<DRPDailyReporting> GetDRPDailyReportingsByName(string dailyReportingName);

        /// <summary>
        /// Get DRPDailyReporting by DRPDailyReportingId
        /// </summary>
        /// <param name="dailyReportingId"></param>
        /// <returns></returns>
        DRPDailyReporting GetDRPDailyReportingById(Guid dailyReportingId);

        /// <summary>
        /// Get DRPDailyReporting by DRPDailyReportingId using async
        /// </summary>
        /// <param name="dailyReportingId"></param>
        /// <returns></returns>
        Task<DRPDailyReporting> GetDRPDailyReportingByIdAsync(Guid dailyReportingId);

        /// <summary>
        /// Insert/Update DRPDailyReporting entity
        /// </summary>
        /// <param name="dailyReporting"></param>
        /// <returns></returns>
        bool SaveOrUpdateDRPDailyReportingDetails(DRPDailyReporting dailyReporting, DRPDailyReportingModel dailyReportingModel);

        /// <summary>
        /// Delete a record by DRPDailyReportingId
        /// </summary>
        /// <param name="dailyReportingId"></param>
        /// <returns></returns>
        bool DeleteById(Guid dailyReportingId);

        /// <summary>
        /// Check duplicate DRPDailyReporting by Name
        /// </summary>
        /// <param name="dailyReportingModel"></param>
        /// <returns></returns>
        List<string> CheckDRPDailyReportingExistByName(DRPDailyReportingModel dailyReportingModel);

        /// <summary>
        /// List of DRPDailyReporting with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<DRPDailyReportingViewModel> GetDRPDailyReportingsByCriteria(SearchDRPDailyReportingModel searchModel);

        IList<string> GetWorkTypesByDailyReportingId(Guid dailyReportingId);

        DRPRIndustryExposureVisitModel GetIndustryExposureVisitByDailyReportingId(Guid dailyReportingId);

        LeaveModel GetLeaveByDailyReportingId(Guid dailyReportingId);

        HolidayModel GetHolidayByDailyReportingId(Guid dailyReportingId);
    }
}