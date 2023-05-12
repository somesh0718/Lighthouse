using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the ForgotPasswordHistory entity
    /// </summary>
    public interface IForgotPasswordHistoryManager : IGenericManager<ForgotPasswordHistoryModel>
    {
        /// <summary>
        /// Get list of ForgotPasswordHistories
        /// </summary>
        /// <returns></returns>
        IQueryable<ForgotPasswordHistoryModel> GetForgotPasswordHistories();

        /// <summary>
        /// Get list of ForgotPasswordHistories by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<ForgotPasswordHistoryModel> GetForgotPasswordHistoriesByName(string forgotPasswordHistoryName);

        /// <summary>
        /// Get ForgotPasswordHistory by ForgotPasswordId
        /// </summary>
        /// <param name="forgotPasswordId"></param>
        /// <returns></returns>
        ForgotPasswordHistoryModel GetForgotPasswordHistoryById(Guid forgotPasswordId);

        /// <summary>
        /// Get ForgotPasswordHistory by ForgotPasswordId using async
        /// </summary>
        /// <param name="forgotPasswordId"></param>
        /// <returns></returns>
        Task<ForgotPasswordHistoryModel> GetForgotPasswordHistoryByIdAsync(Guid forgotPasswordId);

        /// <summary>
        /// Insert/Update ForgotPasswordHistory entity
        /// </summary>
        /// <param name="forgotPasswordHistoryModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateForgotPasswordHistoryDetails(ForgotPasswordHistoryModel forgotPasswordHistoryModel);

        /// <summary>
        /// Delete a record by ForgotPasswordId
        /// </summary>
        /// <param name="forgotPasswordId"></param>
        /// <returns></returns>
        bool DeleteById(Guid forgotPasswordId);

        /// <summary>
        /// Check duplicate ForgotPasswordHistory by Name
        /// </summary>
        /// <param name="forgotPasswordHistoryModel"></param>
        /// <returns></returns>
        bool CheckForgotPasswordHistoryExistByName(ForgotPasswordHistoryModel forgotPasswordHistoryModel);

        /// <summary>
        /// List of ForgotPasswordHistory with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<ForgotPasswordHistoryViewModel> GetForgotPasswordHistoriesByCriteria(SearchForgotPasswordHistoryModel searchModel);
    }
}
