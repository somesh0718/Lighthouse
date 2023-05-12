using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the AccountTransaction entity
    /// </summary>
    public interface IAccountTransactionRepository : IGenericRepository<AccountTransaction>
    {
        /// <summary>
        /// Get list of AccountTransaction
        /// </summary>
        /// <returns></returns>
        IQueryable<AccountTransaction> GetAccountTransactions();

        /// <summary>
        /// Get list of AccountTransaction by accountTransactionName
        /// </summary>
        /// <param name="accountTransactionName"></param>
        /// <returns></returns>
        IQueryable<AccountTransaction> GetAccountTransactionsByName(string accountTransactionName);

        /// <summary>
        /// Get AccountTransaction by AccountTransactionId
        /// </summary>
        /// <param name="accountTransactionId"></param>
        /// <returns></returns>
        AccountTransaction GetAccountTransactionById(Guid accountTransactionId);

        /// <summary>
        /// Get AccountTransaction by AccountTransactionId using async
        /// </summary>
        /// <param name="accountTransactionId"></param>
        /// <returns></returns>
        Task<AccountTransaction> GetAccountTransactionByIdAsync(Guid accountTransactionId);

        /// <summary>
        /// Insert/Update AccountTransaction entity
        /// </summary>
        /// <param name="accountTransaction"></param>
        /// <returns></returns>
        bool SaveOrUpdateAccountTransactionDetails(AccountTransaction accountTransaction);

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

        /// <summary>
        /// Get RoleId by AccountTransactionId
        /// </summary>
        /// <param name="accountTransactionId"></param>
        /// <returns></returns>
        Guid GetAccountRoleIdByUser(Guid accountTransactionId);
    }
}