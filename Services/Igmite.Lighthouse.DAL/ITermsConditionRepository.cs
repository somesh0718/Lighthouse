using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the TermsCondition entity
    /// </summary>
    public interface ITermsConditionRepository : IGenericRepository<TermsCondition>
    {
        /// <summary>
        /// Get list of TermsCondition
        /// </summary>
        /// <returns></returns>
        IQueryable<TermsCondition> GetTermsConditions();

        /// <summary>
        /// Get list of TermsCondition by termsConditionName
        /// </summary>
        /// <param name="termsConditionName"></param>
        /// <returns></returns>
        IQueryable<TermsCondition> GetTermsConditionsByName(string termsConditionName);

        /// <summary>
        /// Get TermsCondition by TermsConditionId
        /// </summary>
        /// <param name="termsConditionId"></param>
        /// <returns></returns>
        TermsCondition GetTermsConditionById(Guid termsConditionId);

        /// <summary>
        /// Get TermsCondition by TermsConditionId using async
        /// </summary>
        /// <param name="termsConditionId"></param>
        /// <returns></returns>
        Task<TermsCondition> GetTermsConditionByIdAsync(Guid termsConditionId);

        /// <summary>
        /// Insert/Update TermsCondition entity
        /// </summary>
        /// <param name="termsCondition"></param>
        /// <returns></returns>
        bool SaveOrUpdateTermsConditionDetails(TermsCondition termsCondition);

        /// <summary>
        /// Delete a record by TermsConditionId
        /// </summary>
        /// <param name="termsConditionId"></param>
        /// <returns></returns>
        bool DeleteById(Guid termsConditionId);

        /// <summary>
        /// Check duplicate TermsCondition by Name
        /// </summary>
        /// <param name="termsConditionModel"></param>
        /// <returns></returns>
        bool CheckTermsConditionExistByName(TermsConditionModel termsConditionModel);

        /// <summary>
        /// List of TermsCondition with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<TermsConditionViewModel> GetTermsConditionsByCriteria(SearchTermsConditionModel searchModel);
    }
}
