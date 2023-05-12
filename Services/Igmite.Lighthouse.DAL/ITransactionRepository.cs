using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the Transaction entity
    /// </summary>
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        /// <summary>
        /// Get list of Transaction
        /// </summary>
        /// <returns></returns>
        IQueryable<Transaction> GetTransactions();

        /// <summary>
        /// Get list of Transaction by transactionName
        /// </summary>
        /// <param name="transactionName"></param>
        /// <returns></returns>
        IQueryable<Transaction> GetTransactionsByName(string transactionName);

        /// <summary>
        /// Get Transaction by TransactionId
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        Transaction GetTransactionById(Guid transactionId);

        /// <summary>
        /// Get Transaction by Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Transaction GetTransactionByCode(string code);

        /// <summary>
        /// Get Transaction by TransactionId using async
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        Task<Transaction> GetTransactionByIdAsync(Guid transactionId);

        /// <summary>
        /// Insert/Update Transaction entity
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        bool SaveOrUpdateTransactionDetails(Transaction transaction);

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
