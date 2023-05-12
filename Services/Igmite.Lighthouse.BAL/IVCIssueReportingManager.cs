using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VCIssueReporting entity
    /// </summary>
    public interface IVCIssueReportingManager : IGenericManager<VCIssueReportingModel>
    {
        /// <summary>
        /// Get list of VCIssueReportings
        /// </summary>
        /// <returns></returns>
        IQueryable<VCIssueReportingModel> GetVCIssueReportings();

        /// <summary>
        /// Get list of VCIssueReportings by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VCIssueReportingModel> GetVCIssueReportingsByName(string vcIssueReportingName);

        /// <summary>
        /// Get VCIssueReporting by VCIssueReportingId
        /// </summary>
        /// <param name="vcIssueReportingId"></param>
        /// <returns></returns>
        VCIssueReportingModel GetVCIssueReportingById(Guid vcIssueReportingId);

        /// <summary>
        /// Get VCIssueReporting by VCIssueReportingId using async
        /// </summary>
        /// <param name="vcIssueReportingId"></param>
        /// <returns></returns>
        Task<VCIssueReportingModel> GetVCIssueReportingByIdAsync(Guid vcIssueReportingId);

        /// <summary>
        /// Insert/Update VCIssueReporting entity
        /// </summary>
        /// <param name="vcIssueReportingModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVCIssueReportingDetails(VCIssueReportingModel vcIssueReportingModel);

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

        /// <summary>
        /// Approved VC Issue Reporting
        /// </summary>
        /// <param name="vcApprovalRequest"></param>
        /// <returns></returns>
        SingularResponse<string> ApprovedVCIssueReporting(VCIssueReportingApprovalRequest vcApprovalRequest);
    }
}