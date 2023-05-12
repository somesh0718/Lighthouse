using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the Transaction entity
    /// </summary>
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        /// <summary>
        /// Get list of Transaction
        /// </summary>
        /// <returns></returns>
        public IQueryable<Transaction> GetTransactions()
        {
            return this.Context.Transactions.AsQueryable();
        }

        /// <summary>
        /// Get list of Transaction by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<Transaction> GetTransactionsByName(string name)
        {
            var transactions = (from t in this.Context.Transactions
                                where t.Name.Contains(name)
                                select t).AsQueryable();

            return transactions;
        }

        /// <summary>
        /// Get Transaction by TransactionId
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public Transaction GetTransactionById(Guid transactionId)
        {
            return this.Context.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
        }

        /// <summary>
        /// Get Transaction by Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Transaction GetTransactionByCode(string code)
        {
            return this.Context.Transactions.FirstOrDefault(t => t.Code == code);
        }

        /// <summary>
        /// Get Transaction by TransactionId using async
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public async Task<Transaction> GetTransactionByIdAsync(Guid transactionId)
        {
            var transaction = await (from t in this.Context.Transactions
                                     where t.TransactionId == transactionId
                                     select t).FirstOrDefaultAsync();

            return transaction;
        }

        /// <summary>
        /// Insert/Update Transaction entity
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool SaveOrUpdateTransactionDetails(Transaction transaction)
        {
            if (RequestType.New == transaction.RequestType)
                Context.Transactions.Add(transaction);
            else
            {
                Context.Entry<Transaction>(transaction).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by TransactionId
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid transactionId)
        {
            Transaction transaction = this.Context.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);

            if (transaction != null)
            {
                Context.Entry<Transaction>(transaction).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate Transaction by Name
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        public bool CheckTransactionExistByName(TransactionModel transactionModel)
        {
            Transaction transaction = this.Context.Transactions.FirstOrDefault(t => (t.Code == transactionModel.Code));

            return transaction != null;
        }

        /// <summary>}
        /// List of Transaction with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<TransactionViewModel> GetTransactionsByCriteria(SearchTransactionModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.TransactionViewModels.FromSql<TransactionViewModel>("CALL GetTransactionsByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}