using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the IssueMapping entity
    /// </summary>
    public interface IIssueMappingRepository : IGenericRepository<IssueMapping>
    {
        /// <summary>
        /// Get list of IssueMapping
        /// </summary>
        /// <returns></returns>
        IQueryable<IssueMapping> GetIssueMapping();

        /// <summary>
        /// Get list of IssueMapping by IssueMappingName
        /// </summary>
        /// <param name="IssueMappingName"></param>
        /// <returns></returns>
        IQueryable<IssueMapping> GetIssueMappingByIssueId(string MainIssueId, string SubIssueId);

        /// <summary>
        /// Get IssueMapping by IssueMappingId
        /// </summary>
        /// <param name="IssueMappingId"></param>
        /// <returns></returns>
        IssueMapping GetIssueMappingById(Guid IssueMappingId);

        /// <summary>
        /// Get GetIssueMappingById by mainIssueId & subIssueId
        /// </summary>
        /// <param name="IssueMappingId"></param>
        /// <returns></returns>
        SubIssue GetIssueMappingById(Guid mainIssueId, Guid subIssueId);

        /// <summary>
        /// Get IssueMapping by IssueMappingId using async
        /// </summary>
        /// <param name="IssueMappingId"></param>
        /// <returns></returns>
        Task<IssueMapping> GetIssueMappingByIdAsync(Guid IssueMappingId);

        /// <summary>
        /// Insert/Update IssueMapping entity
        /// </summary>
        /// <param name="IssueMapping"></param>
        /// <returns></returns>
        bool SaveOrUpdateIssueMappingDetails(IssueMapping IssueMapping);

        /// <summary>
        /// Delete a record by IssueMappingId
        /// </summary>
        /// <param name="IssueMappingId"></param>
        /// <returns></returns>
        bool DeleteById(Guid IssueMappingId);

        /// <summary>
        /// Check duplicate IssueMapping by Name
        /// </summary>
        /// <param name="IssueMappingModel"></param>
        /// <returns></returns>
        string CheckIssueMappingExistByName(IssueMappingModel IssueMappingModel);

        /// <summary>
        /// List of IssueMapping with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<IssueMappingViewModel> GetIssueMappingByCriteria(SearchIssueMappingModel searchModel);

        /// <summary>
        /// List of Issue by userId with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<IssueViewModel> GetIssueByCriteria(SearchIssueModel searchModel);
    }
}