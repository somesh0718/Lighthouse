using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the TermsCondition entity
    /// </summary>
    public interface ITermsConditionManager : IGenericManager<TermsConditionModel>
    {
        /// <summary>
        /// Get list of TermsConditions
        /// </summary>
        /// <returns></returns>
        IQueryable<TermsConditionModel> GetTermsConditions();

        /// <summary>
        /// Get list of TermsConditions by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<TermsConditionModel> GetTermsConditionsByName(string termsConditionName);

        /// <summary>
        /// Get TermsCondition by TermsConditionId
        /// </summary>
        /// <param name="termsConditionId"></param>
        /// <returns></returns>
        TermsConditionModel GetTermsConditionById(Guid termsConditionId);

        /// <summary>
        /// Get TermsCondition by TermsConditionId using async
        /// </summary>
        /// <param name="termsConditionId"></param>
        /// <returns></returns>
        Task<TermsConditionModel> GetTermsConditionByIdAsync(Guid termsConditionId);

        /// <summary>
        /// Insert/Update TermsCondition entity
        /// </summary>
        /// <param name="termsConditionModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateTermsConditionDetails(TermsConditionModel termsConditionModel);

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
