using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the Role entity
    /// </summary>
    public interface IRoleRepository : IGenericRepository<Role>
    {
        /// <summary>
        /// Get list of Role
        /// </summary>
        /// <returns></returns>
        IQueryable<Role> GetRoles();

        /// <summary>
        /// Get list of Role by roleName
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        IQueryable<Role> GetRolesByName(string roleName);

        /// <summary>
        /// Get Role by RoleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Role GetRoleById(Guid roleId);

        /// <summary>
        /// Get Role by Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Role GetRoleByCode(string code);

        /// <summary>
        /// Get Role by RoleId using async
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<Role> GetRoleByIdAsync(Guid roleId);

        /// <summary>
        /// Insert/Update Role entity
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        bool SaveOrUpdateRoleDetails(Role role);

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
