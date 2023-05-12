using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the HMIssueReporting entity
    /// </summary>
    public interface IHMIssueReportingManager : IGenericManager<HMIssueReportingModel>
    {
        /// <summary>
        /// Get list of HMIssueReportings
        /// </summary>
        /// <returns></returns>
        IQueryable<HMIssueReportingModel> GetHMIssueReportings();

        /// <summary>
        /// Get list of HMIssueReportings by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<HMIssueReportingModel> GetHMIssueReportingsByName(string hmIssueReportingName);

        /// <summary>
        /// Get HMIssueReporting by HMIssueReportingId
        /// </summary>
        /// <param name="hmIssueReportingId"></param>
        /// <returns></returns>
        HMIssueReportingModel GetHMIssueReportingById(Guid hmIssueReportingId);

        /// <summary>
        /// Get HMIssueReporting by HMIssueReportingId using async
        /// </summary>
        /// <param name="hmIssueReportingId"></param>
        /// <returns></returns>
        Task<HMIssueReportingModel> GetHMIssueReportingByIdAsync(Guid hmIssueReportingId);

        /// <summary>
        /// Insert/Update HMIssueReporting entity
        /// </summary>
        /// <param name="hmIssueReportingModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateHMIssueReportingDetails(HMIssueReportingModel hmIssueReportingModel);

        /// <summary>
        /// Delete a record by HMIssueReportingId
        /// </summary>
        /// <param name="hmIssueReportingId"></param>
        /// <returns></returns>
        bool DeleteById(Guid hmIssueReportingId);

        /// <summary>
        /// Check duplicate HMIssueReporting by Name
        /// </summary>
        /// <param name="hmIssueReportingModel"></param>
        /// <returns></returns>
        List<string> CheckHMIssueReportingExistByName(HMIssueReportingModel hmIssueReportingModel);

        /// <summary>
        /// List of HMIssueReporting with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<HMIssueReportingViewModel> GetHMIssueReportingsByCriteria(SearchHMIssueReportingModel searchModel);

        /// <summary>
        /// Approved HM Issue Reporting
        /// </summary>
        /// <param name="hmApprovalRequest"></param>
        /// <returns></returns>
        SingularResponse<string> ApprovedHMIssueReporting(HMIssueReportingApprovalRequest hmApprovalRequest);
    }
}