using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the IssueMapping entity
    /// </summary>
    public interface IIssueMappingManager : IGenericManager<IssueMappingModel>
    {
        /// <summary>
        /// Get list of IssueMappings
        /// </summary>
        /// <returns></returns>
        IQueryable<IssueMappingModel> GetIssueMapping();

        /// <summary>
        /// Get list of IssueMappings by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<IssueMappingModel> GetIssueMappingByIssueId(string MainIssueId, string SubIssueId);

        /// <summary>
        /// Get IssueMapping by IssueMappingId
        /// </summary>
        /// <param name="IssueMappingId"></param>
        /// <returns></returns>
        IssueMappingModel GetIssueMappingById(Guid IssueMappingId);

        /// <summary>
        /// Get IssueMapping by IssueMappingId using async
        /// </summary>
        /// <param name="IssueMappingId"></param>
        /// <returns></returns>
        Task<IssueMappingModel> GetIssueMappingByIdAsync(Guid IssueMappingId);

        /// <summary>
        /// Insert/Update IssueMapping entity
        /// </summary>
        /// <param name="IssueMappingModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateIssueMappingDetails(IssueMappingModel IssueMappingModel);

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
