using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the Transaction entity
    /// </summary>
    public interface ITransactionManager : IGenericManager<TransactionModel>
    {
        /// <summary>
        /// Get list of Transactions
        /// </summary>
        /// <returns></returns>
        IQueryable<TransactionModel> GetTransactions();

        /// <summary>
        /// Get list of Transactions by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<TransactionModel> GetTransactionsByName(string transactionName);

        /// <summary>
        /// Get Transaction by TransactionId
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        TransactionModel GetTransactionById(Guid transactionId);

        /// <summary>
        /// Get Transaction by Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        TransactionModel GetTransactionByCode(string code);

        /// <summary>
        /// Get Transaction by TransactionId using async
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        Task<TransactionModel> GetTransactionByIdAsync(Guid transactionId);

        /// <summary>
        /// Insert/Update Transaction entity
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateTransactionDetails(TransactionModel transactionModel);

        /// <summary>
        /// Delete a record by TransactionId
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        bool DeleteById(Guid transactionId);

        /// <summary>
        /// Check duplicate Transaction by Name
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        bool CheckTransactionExistByName(TransactionModel transactionModel);

        /// <summary>
        /// List of Transaction with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<TransactionViewModel> GetTransactionsByCriteria(SearchTransactionModel searchModel);
    }
}
