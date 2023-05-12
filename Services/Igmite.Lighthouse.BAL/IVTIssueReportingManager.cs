using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VTIssueReporting entity
    /// </summary>
    public interface IVTIssueReportingManager : IGenericManager<VTIssueReportingModel>
    {
        /// <summary>
        /// Get list of VTIssueReportings
        /// </summary>
        /// <returns></returns>
        IQueryable<VTIssueReportingModel> GetVTIssueReportings();

        /// <summary>
        /// Get list of VTIssueReportings by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VTIssueReportingModel> GetVTIssueReportingsByName(string vtIssueReportingName);

        /// <summary>
        /// Get VTIssueReporting by VTIssueReportingId
        /// </summary>
        /// <param name="vtIssueReportingId"></param>
        /// <returns></returns>
        VTIssueReportingModel GetVTIssueReportingById(Guid vtIssueReportingId);

        /// <summary>
        /// Get VTIssueReporting by VTIssueReportingId using async
        /// </summary>
        /// <param name="vtIssueReportingId"></param>
        /// <returns></returns>
        Task<VTIssueReportingModel> GetVTIssueReportingByIdAsync(Guid vtIssueReportingId);

        /// <summary>
        /// Insert/Update VTIssueReporting entity
        /// </summary>
        /// <param name="vtIssueReportingModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVTIssueReportingDetails(VTIssueReportingModel vtIssueReportingModel);

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

        /// <summary>
        /// Approved VT Issue Reporting
        /// </summary>
        /// <param name="vtApprovalRequest"></param>
        /// <returns></returns>
        SingularResponse<string> ApprovedVTIssueReporting(VTIssueReportingApprovalRequest vtApprovalRequest);
    }
}