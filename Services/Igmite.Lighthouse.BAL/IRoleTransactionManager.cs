using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the RoleTransaction entity
    /// </summary>
    public interface IRoleTransactionManager : IGenericManager<RoleTransactionModel>
    {
        /// <summary>
        /// Get list of RoleTransactions
        /// </summary>
        /// <returns></returns>
        IQueryable<RoleTransactionModel> GetRoleTransactions();

        /// <summary>
        /// Get list of RoleTransactions by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<RoleTransactionModel> GetRoleTransactionsByName(string roleTransactionName);

        /// <summary>
        /// Get RoleTransaction by RoleTransactionId
        /// </summary>
        /// <param name="roleTransactionId"></param>
        /// <returns></returns>
        RoleTransactionModel GetRoleTransactionById(Guid roleTransactionId);

        /// <summary>
        /// Get RoleTransaction by RoleTransactionId using async
        /// </summary>
        /// <param name="roleTransactionId"></param>
        /// <returns></returns>
        Task<RoleTransactionModel> GetRoleTransactionByIdAsync(Guid roleTransactionId);

        /// <summary>
        /// Insert/Update RoleTransaction entity
        /// </summary>
        /// <param name="roleTransactionModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateRoleTransactionDetails(RoleTransactionModel roleTransactionModel);

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
