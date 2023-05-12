using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the ForgotPasswordHistory entity
    /// </summary>
    public interface IForgotPasswordHistoryRepository : IGenericRepository<ForgotPasswordHistory>
    {
        /// <summary>
        /// Get list of ForgotPasswordHistory
        /// </summary>
        /// <returns></returns>
        IQueryable<ForgotPasswordHistory> GetForgotPasswordHistories();

        /// <summary>
        /// Get list of ForgotPasswordHistory by forgotPasswordHistoryName
        /// </summary>
        /// <param name="forgotPasswordHistoryName"></param>
        /// <returns></returns>
        IQueryable<ForgotPasswordHistory> GetForgotPasswordHistoriesByName(string forgotPasswordHistoryName);

        /// <summary>
        /// Get ForgotPasswordHistory by ForgotPasswordId
        /// </summary>
        /// <param name="forgotPasswordId"></param>
        /// <returns></returns>
        ForgotPasswordHistory GetForgotPasswordHistoryById(Guid forgotPasswordId);

        /// <summary>
        /// Get ForgotPasswordHistory by ForgotPasswordId using async
        /// </summary>
        /// <param name="forgotPasswordId"></param>
        /// <returns></returns>
        Task<ForgotPasswordHistory> GetForgotPasswordHistoryByIdAsync(Guid forgotPasswordId);

        /// <summary>
        /// Insert/Update ForgotPasswordHistory entity
        /// </summary>
        /// <param name="forgotPasswordHistory"></param>
        /// <returns></returns>
        bool SaveOrUpdateForgotPasswordHistoryDetails(ForgotPasswordHistory forgotPasswordHistory);

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
