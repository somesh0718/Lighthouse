using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the RoleTransaction entity
    /// </summary>
    public class RoleTransactionRepository : GenericRepository<RoleTransaction>, IRoleTransactionRepository
    {
        /// <summary>
        /// Get list of RoleTransaction
        /// </summary>
        /// <returns></returns>
        public IQueryable<RoleTransaction> GetRoleTransactions()
        {
            return this.Context.RoleTransactions.AsQueryable();
        }

        /// <summary>
        /// Get list of RoleTransaction by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<RoleTransaction> GetRoleTransactionsByName(string name)
        {
            var roleTransactions = (from r in this.Context.RoleTransactions
                         select r).AsQueryable();

            return roleTransactions;
        }

        /// <summary>
        /// Get RoleTransaction by RoleTransactionId
        /// </summary>
        /// <param name="roleTransactionId"></param>
        /// <returns></returns>
        public RoleTransaction GetRoleTransactionById(Guid roleTransactionId)
        {
            return this.Context.RoleTransactions.FirstOrDefault(r => r.RoleTransactionId == roleTransactionId);
        }

        /// <summary>
        /// Get RoleTransaction by RoleTransactionId using async
        /// </summary>
        /// <param name="roleTransactionId"></param>
        /// <returns></returns>
        public async Task<RoleTransaction> GetRoleTransactionByIdAsync(Guid roleTransactionId)
        {
            var roleTransaction = await (from r in this.Context.RoleTransactions
                              where r.RoleTransactionId == roleTransactionId
                              select r).FirstOrDefaultAsync();

            return roleTransaction;
        }

        /// <summary>
        /// Insert/Update RoleTransaction entity
        /// </summary>
        /// <param name="roleTransaction"></param>
        /// <returns></returns>
        public bool SaveOrUpdateRoleTransactionDetails(RoleTransaction roleTransaction)
        {
            if (RequestType.New == roleTransaction.RequestType)
                Context.RoleTransactions.Add(roleTransaction);
            else
            {
                Context.Entry<RoleTransaction>(roleTransaction).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by RoleTransactionId
        /// </summary>
        /// <param name="roleTransactionId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid roleTransactionId)
        {
            RoleTransaction roleTransaction = this.Context.RoleTransactions.FirstOrDefault(r => r.RoleTransactionId == roleTransactionId);

            if (roleTransaction != null)
            {
                Context.Entry<RoleTransaction>(roleTransaction).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate RoleTransaction by Name
        /// </summary>
        /// <param name="roleTransactionModel"></param>
        /// <returns></returns>
        public bool CheckRoleTransactionExistByName(RoleTransactionModel roleTransactionModel)
        {
            RoleTransaction roleTransaction = this.Context.RoleTransactions.FirstOrDefault(r => r.RoleId == roleTransactionModel.RoleId && r.TransactionId == roleTransactionModel.TransactionId);

            return roleTransaction != null;
        }

        /// <summary>}
        /// List of RoleTransaction with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<RoleTransactionViewModel> GetRoleTransactionsByCriteria(SearchRoleTransactionModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.RoleTransactionViewModels.FromSql<RoleTransactionViewModel>("CALL GetRoleTransactionsByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
