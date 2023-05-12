using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the HMIssueReporting entity
    /// </summary>
    public interface IHMIssueReportingRepository : IGenericRepository<HMIssueReporting>
    {
        /// <summary>
        /// Get list of HMIssueReporting
        /// </summary>
        /// <returns></returns>
        IQueryable<HMIssueReporting> GetHMIssueReportings();

        /// <summary>
        /// Get list of HMIssueReporting by hmIssueReportingName
        /// </summary>
        /// <param name="hmIssueReportingName"></param>
        /// <returns></returns>
        IQueryable<HMIssueReporting> GetHMIssueReportingsByName(string hmIssueReportingName);

        /// <summary>
        /// Get HMIssueReporting by HMIssueReportingId
        /// </summary>
        /// <param name="hmIssueReportingId"></param>
        /// <returns></returns>
        HMIssueReporting GetHMIssueReportingById(Guid hmIssueReportingId);

        /// <summary>
        /// Get HMIssueReporting by HMIssueReportingId using async
        /// </summary>
        /// <param name="hmIssueReportingId"></param>
        /// <returns></returns>
        Task<HMIssueReporting> GetHMIssueReportingByIdAsync(Guid hmIssueReportingId);

        /// <summary>
        /// Insert/Update HMIssueReporting entity
        /// </summary>
        /// <param name="hmIssueReporting"></param>
        /// <param name="issueApprovalHistory"></param>
        /// <returns></returns>
        bool SaveOrUpdateHMIssueReportingDetails(HMIssueReporting hmIssueReporting, IssueApprovalHistory issueApprovalHistory = null);

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
    }
}