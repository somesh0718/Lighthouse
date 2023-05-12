using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VCDailyReporting entity
    /// </summary>
    public interface IVCDailyReportingManager : IGenericManager<VCDailyReportingModel>
    {
        /// <summary>
        /// Get list of VCDailyReportings
        /// </summary>
        /// <returns></returns>
        IQueryable<VCDailyReportingModel> GetVCDailyReportings();

        /// <summary>
        /// Get list of VCDailyReportings by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VCDailyReportingModel> GetVCDailyReportingsByName(string vcDailyReportingName);

        /// <summary>
        /// Get VCDailyReporting by VCDailyReportingId
        /// </summary>
        /// <param name="vcDailyReportingId"></param>
        /// <returns></returns>
        VCDailyReportingModel GetVCDailyReportingById(Guid vcDailyReportingId);

        /// <summary>
        /// Get VCDailyReporting by VCDailyReportingId using async
        /// </summary>
        /// <param name="vcDailyReportingId"></param>
        /// <returns></returns>
        Task<VCDailyReportingModel> GetVCDailyReportingByIdAsync(Guid vcDailyReportingId);

        /// <summary>
        /// Insert/Update VCDailyReporting entity
        /// </summary>
        /// <param name="vcDailyReportingModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVCDailyReportingDetails(VCDailyReportingModel vcDailyReportingModel);

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
    }
}
