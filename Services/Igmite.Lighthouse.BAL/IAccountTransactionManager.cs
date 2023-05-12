using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the AccountTransaction entity
    /// </summary>
    public interface IAccountTransactionManager : IGenericManager<AccountTransactionModel>
    {
        /// <summary>
        /// Get list of AccountTransactions
        /// </summary>
        /// <returns></returns>
        IQueryable<AccountTransactionModel> GetAccountTransactions();

        /// <summary>
        /// Get list of AccountTransactions by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<AccountTransactionModel> GetAccountTransactionsByName(string accountTransactionName);

        /// <summary>
        /// Get AccountTransaction by AccountTransactionId
        /// </summary>
        /// <param name="accountTransactionId"></param>
        /// <returns></returns>
        AccountTransactionModel GetAccountTransactionById(Guid accountTransactionId);

        /// <summary>
        /// Get AccountTransaction by AccountTransactionId using async
        /// </summary>
        /// <param name="accountTransactionId"></param>
        /// <returns></returns>
        Task<AccountTransactionModel> GetAccountTransactionByIdAsync(Guid accountTransactionId);

        /// <summary>
        /// Insert/Update AccountTransaction entity
        /// </summary>
        /// <param name="accountTransactionModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateAccountTransactionDetails(AccountTransactionModel accountTransactionModel);

        /// <summary>
        /// Delete a record by AccountTransactionId
        /// </summary>
        /// <param name="accountTransactionId"></param>
        /// <returns></returns>
        bool DeleteById(Guid accountTransactionId);

        /// <summary>
        /// Check duplicate AccountTransaction by Name
        /// </summary>
        /// <param name="accountTransactionModel"></param>
        /// <returns></returns>
        bool CheckAccountTransactionExistByName(AccountTransactionModel accountTransactionModel);

        /// <summary>
        /// List of AccountTransaction with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<AccountTransactionViewModel> GetAccountTransactionsByCriteria(SearchAccountTransactionModel searchModel);
    }
}