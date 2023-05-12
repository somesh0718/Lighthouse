using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the DRPDailyReporting entity
    /// </summary>
    public interface IDRPDailyReportingManager : IGenericManager<DRPDailyReportingModel>
    {
        /// <summary>
        /// Get list of DRPDailyReportings
        /// </summary>
        /// <returns></returns>
        IQueryable<DRPDailyReportingModel> GetDRPDailyReportings();

        /// <summary>
        /// Get list of DRPDailyReportings by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<DRPDailyReportingModel> GetDRPDailyReportingsByName(string dailyReportingName);

        /// <summary>
        /// Get DRPDailyReporting by DRPDailyReportingId
        /// </summary>
        /// <param name="dailyReportingId"></param>
        /// <returns></returns>
        DRPDailyReportingModel GetDRPDailyReportingById(Guid dailyReportingId);

        /// <summary>
        /// Get DRPDailyReporting by DRPDailyReportingId using async
        /// </summary>
        /// <param name="dailyReportingId"></param>
        /// <returns></returns>
        Task<DRPDailyReportingModel> GetDRPDailyReportingByIdAsync(Guid dailyReportingId);

        /// <summary>
        /// Insert/Update DRPDailyReporting entity
        /// </summary>
        /// <param name="dailyReportingModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateDRPDailyReportingDetails(DRPDailyReportingModel dailyReportingModel);

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
    }
}