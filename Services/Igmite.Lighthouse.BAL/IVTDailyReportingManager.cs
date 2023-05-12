using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VTDailyReporting entity
    /// </summary>
    public interface IVTDailyReportingManager : IGenericManager<VTDailyReportingModel>
    {
        /// <summary>
        /// Get list of VTDailyReportings
        /// </summary>
        /// <returns></returns>
        IQueryable<VTDailyReportingModel> GetVTDailyReportings();

        /// <summary>
        /// Get list of VTDailyReportings by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VTDailyReportingModel> GetVTDailyReportingsByName(string vtDailyReportingName);

        /// <summary>
        /// Get VTDailyReporting by VTDailyReportingId
        /// </summary>
        /// <param name="vtDailyReportingId"></param>
        /// <returns></returns>
        VTDailyReportingModel GetVTDailyReportingById(Guid vtDailyReportingId);

        /// <summary>
        /// Get VTDailyReporting by VTDailyReportingId using async
        /// </summary>
        /// <param name="vtDailyReportingId"></param>
        /// <returns></returns>
        Task<VTDailyReportingModel> GetVTDailyReportingByIdAsync(Guid vtDailyReportingId);

        /// <summary>
        /// Insert/Update VTDailyReporting entity
        /// </summary>
        /// <param name="vtDailyReportingModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVTDailyReportingDetails(VTDailyReportingModel vtDailyReportingModel);

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
        /// Approved VT Daily Reporting
        /// </summary>
        /// <param name="vtApprovalRequest"></param>
        /// <returns></returns>
        SingularResponse<string> ApprovedVTDailyReporting(VTDailyReportingApprovalRequest vtApprovalRequest);
    }
}