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
    /// Repository of the AccountTransaction entity
    /// </summary>
    public class AccountTransactionRepository : GenericRepository<AccountTransaction>, IAccountTransactionRepository
    {
        /// <summary>
        /// Get list of AccountTransaction
        /// </summary>
        /// <returns></returns>
        public IQueryable<AccountTransaction> GetAccountTransactions()
        {
            return this.Context.AccountTransactions.AsQueryable();
        }

        /// <summary>
        /// Get list of AccountTransaction by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<AccountTransaction> GetAccountTransactionsByName(string name)
        {
            var accountTransactions = (from r in this.Context.AccountTransactions
                                       select r).AsQueryable();

            return accountTransactions;
        }

        /// <summary>
        /// Get AccountTransaction by AccountTransactionId
        /// </summary>
        /// <param name="accountTransactionId"></param>
        /// <returns></returns>
        public AccountTransaction GetAccountTransactionById(Guid accountTransactionId)
        {
            return this.Context.AccountTransactions.FirstOrDefault(r => r.AccountTransactionId == accountTransactionId);
        }

        /// <summary>
        /// Get AccountTransaction by AccountTransactionId using async
        /// </summary>
        /// <param name="accountTransactionId"></param>
        /// <returns></returns>
        public async Task<AccountTransaction> GetAccountTransactionByIdAsync(Guid accountTransactionId)
        {
            var accountTransaction = await (from r in this.Context.AccountTransactions
                                            where r.AccountTransactionId == accountTransactionId
                                            select r).FirstOrDefaultAsync();

            return accountTransaction;
        }

        /// <summary>
        /// Insert/Update AccountTransaction entity
        /// </summary>
        /// <param name="accountTransaction"></param>
        /// <returns></returns>
        public bool SaveOrUpdateAccountTransactionDetails(AccountTransaction accountTransaction)
        {
            if (RequestType.New == accountTransaction.RequestType)
                Context.AccountTransactions.Add(accountTransaction);
            else
            {
                Context.Entry<AccountTransaction>(accountTransaction).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by AccountTransactionId
        /// </summary>
        /// <param name="accountTransactionId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid accountTransactionId)
        {
            AccountTransaction accountTransaction = this.Context.AccountTransactions.FirstOrDefault(r => r.AccountTransactionId == accountTransactionId);

            if (accountTransaction != null)
            {
                Context.Entry<AccountTransaction>(accountTransaction).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate AccountTransaction by Name
        /// </summary>
        /// <param name="accountTransactionModel"></param>
        /// <returns></returns>
        public bool CheckAccountTransactionExistByName(AccountTransactionModel accountTransactionModel)
        {
            AccountTransaction accountTransaction = this.Context.AccountTransactions.FirstOrDefault(r => r.AccountId == accountTransactionModel.AccountId && r.TransactionId == accountTransactionModel.TransactionId);

            return accountTransaction != null;
        }

        /// <summary>}
        /// List of AccountTransaction with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<AccountTransactionViewModel> GetAccountTransactionsByCriteria(SearchAccountTransactionModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "accountId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.UserTypeId.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[4] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.AccountTransactionViewModels.FromSql<AccountTransactionViewModel>("CALL GetAccountTransactionsByCriteria (@accountId, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }

        /// <summary>
        /// Get RoleId by AccountTransactionId
        /// </summary>
        /// <param name="accountTransactionId"></param>
        /// <returns></returns>
        public Guid GetAccountRoleIdByUser(Guid accountTransactionId)
        {
            Guid roleId = (from ut in this.Context.AccountTransactions
                           join ar in this.Context.AccountRoles on ut.AccountId equals ar.AccountId
                           where ut.AccountTransactionId == accountTransactionId
                           select ar.RoleId).FirstOrDefault();

            return roleId;
        }
    }
}