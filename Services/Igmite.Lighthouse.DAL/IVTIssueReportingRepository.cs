using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VTIssueReporting entity
    /// </summary>
    public interface IVTIssueReportingRepository : IGenericRepository<VTIssueReporting>
    {
        /// <summary>
        /// Get list of VTIssueReporting
        /// </summary>
        /// <returns></returns>
        IQueryable<VTIssueReporting> GetVTIssueReportings();

        /// <summary>
        /// Get list of VTIssueReporting by vtIssueReportingName
        /// </summary>
        /// <param name="vtIssueReportingName"></param>
        /// <returns></returns>
        IQueryable<VTIssueReporting> GetVTIssueReportingsByName(string vtIssueReportingName);

        /// <summary>
        /// Get VTIssueReporting by VTIssueReportingId
        /// </summary>
        /// <param name="vtIssueReportingId"></param>
        /// <returns></returns>
        VTIssueReporting GetVTIssueReportingById(Guid vtIssueReportingId);

        /// <summary>
        /// Get VTIssueReporting by VTIssueReportingId using async
        /// </summary>
        /// <param name="vtIssueReportingId"></param>
        /// <returns></returns>
        Task<VTIssueReporting> GetVTIssueReportingByIdAsync(Guid vtIssueReportingId);

        /// <summary>
        /// Insert/Update VTIssueReporting entity
        /// </summary>
        /// <param name="vtIssueReporting"></param>
        /// <param name="issueApprovalHistory"></param>
        /// <returns></returns>
        bool SaveOrUpdateVTIssueReportingDetails(VTIssueReporting vtIssueReporting, IssueApprovalHistory issueApprovalHistory = null);

        /// <summary>
        /// Delete a record by VTIssueReportingId
        /// </summary>
        /// <param name="vtIssueReportingId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtIssueReportingId);

        /// <summary>
        /// Check duplicate VTIssueReporting by Name
        /// </summary>
        /// <param name="vtIssueReportingModel"></param>
        /// <returns></returns>
        List<string> CheckVTIssueReportingExistByName(VTIssueReportingModel vtIssueReportingModel);

        /// <summary>
        /// List of VTIssueReporting with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTIssueReportingViewModel> GetVTIssueReportingsByCriteria(SearchVTIssueReportingModel searchModel);
    }
}