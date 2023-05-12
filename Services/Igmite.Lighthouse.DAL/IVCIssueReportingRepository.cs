using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VCIssueReporting entity
    /// </summary>
    public interface IVCIssueReportingRepository : IGenericRepository<VCIssueReporting>
    {
        /// <summary>
        /// Get list of VCIssueReporting
        /// </summary>
        /// <returns></returns>
        IQueryable<VCIssueReporting> GetVCIssueReportings();

        /// <summary>
        /// Get list of VCIssueReporting by vcIssueReportingName
        /// </summary>
        /// <param name="vcIssueReportingName"></param>
        /// <returns></returns>
        IQueryable<VCIssueReporting> GetVCIssueReportingsByName(string vcIssueReportingName);

        /// <summary>
        /// Get VCIssueReporting by VCIssueReportingId
        /// </summary>
        /// <param name="vcIssueReportingId"></param>
        /// <returns></returns>
        VCIssueReporting GetVCIssueReportingById(Guid vcIssueReportingId);

        /// <summary>
        /// Get VCIssueReporting by VCIssueReportingId using async
        /// </summary>
        /// <param name="vcIssueReportingId"></param>
        /// <returns></returns>
        Task<VCIssueReporting> GetVCIssueReportingByIdAsync(Guid vcIssueReportingId);

        /// <summary>
        /// Insert/Update VCIssueReporting entity
        /// </summary>
        /// <param name="vcIssueReporting"></param>
        /// <param name="issueApprovalHistory"></param>
        /// <returns></returns>
        bool SaveOrUpdateVCIssueReportingDetails(VCIssueReporting vcIssueReporting, IssueApprovalHistory issueApprovalHistory = null);

        /// <summary>
        /// Delete a record by VCIssueReportingId
        /// </summary>
        /// <param name="vcIssueReportingId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vcIssueReportingId);

        /// <summary>
        /// Check duplicate VCIssueReporting by Name
        /// </summary>
        /// <param name="vcIssueReportingModel"></param>
        /// <returns></returns>
        List<string> CheckVCIssueReportingExistByName(VCIssueReportingModel vcIssueReportingModel);

        /// <summary>
        /// List of VCIssueReporting with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VCIssueReportingViewModel> GetVCIssueReportingsByCriteria(SearchVCIssueReportingModel searchModel);
    }
}