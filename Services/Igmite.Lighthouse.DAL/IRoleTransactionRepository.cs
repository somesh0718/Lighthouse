using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the RoleTransaction entity
    /// </summary>
    public interface IRoleTransactionRepository : IGenericRepository<RoleTransaction>
    {
        /// <summary>
        /// Get list of RoleTransaction
        /// </summary>
        /// <returns></returns>
        IQueryable<RoleTransaction> GetRoleTransactions();

        /// <summary>
        /// Get list of RoleTransaction by roleTransactionName
        /// </summary>
        /// <param name="roleTransactionName"></param>
        /// <returns></returns>
        IQueryable<RoleTransaction> GetRoleTransactionsByName(string roleTransactionName);

        /// <summary>
        /// Get RoleTransaction by RoleTransactionId
        /// </summary>
        /// <param name="roleTransactionId"></param>
        /// <returns></returns>
        RoleTransaction GetRoleTransactionById(Guid roleTransactionId);

        /// <summary>
        /// Get RoleTransaction by RoleTransactionId using async
        /// </summary>
        /// <param name="roleTransactionId"></param>
        /// <returns></returns>
        Task<RoleTransaction> GetRoleTransactionByIdAsync(Guid roleTransactionId);

        /// <summary>
        /// Insert/Update RoleTransaction entity
        /// </summary>
        /// <param name="roleTransaction"></param>
        /// <returns></returns>
        bool SaveOrUpdateRoleTransactionDetails(RoleTransaction roleTransaction);

        /// <summary>
        /// Delete a record by RoleTransactionId
        /// </summary>
        /// <param name="roleTransactionId"></param>
        /// <returns></returns>
        bool DeleteById(Guid roleTransactionId);

        /// <summary>
        /// Check duplicate RoleTransaction by Name
        /// </summary>
        /// <param name="roleTransactionModel"></param>
        /// <returns></returns>
        bool CheckRoleTransactionExistByName(RoleTransactionModel roleTransactionModel);

        /// <summary>
        /// List of RoleTransaction with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<RoleTransactionViewModel> GetRoleTransactionsByCriteria(SearchRoleTransactionModel searchModel);
    }
}
