using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the Role entity
    /// </summary>
    public interface IRoleManager : IGenericManager<RoleModel>
    {
        /// <summary>
        /// Get list of Roles
        /// </summary>
        /// <returns></returns>
        IQueryable<RoleModel> GetRoles();

        /// <summary>
        /// Get list of Roles by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<RoleModel> GetRolesByName(string roleName);

        /// <summary>
        /// Get Role by RoleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        RoleModel GetRoleById(Guid roleId);

        /// <summary>
        /// Get Role by Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        RoleModel GetRoleByCode(string code);

        /// <summary>
        /// Get Role by RoleId using async
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<RoleModel> GetRoleByIdAsync(Guid roleId);

        /// <summary>
        /// Insert/Update Role entity
        /// </summary>
        /// <param name="roleModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateRoleDetails(RoleModel roleModel);

        /// <summary>
        /// Delete a record by RoleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        bool DeleteById(Guid roleId);

        /// <summary>
        /// Check duplicate Role by Name
        /// </summary>
        /// <param name="roleModel"></param>
        /// <returns></returns>
        bool CheckRoleExistByName(RoleModel roleModel);

        /// <summary>
        /// List of Role with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<RoleViewModel> GetRolesByCriteria(SearchRoleModel searchModel);
    }
}
