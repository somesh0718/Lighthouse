using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VTPSectorJobRole entity
    /// </summary>
    public interface IVTPSectorJobRoleRepository : IGenericRepository<VTPSectorJobRole>
    {
        /// <summary>
        /// Get list of VTPSectorJobRole
        /// </summary>
        /// <returns></returns>
        IQueryable<VTPSectorJobRole> GetVTPSectorJobRoles();

        /// <summary>
        /// Get list of VTPSectorJobRole by vtpSectorJobRoleName
        /// </summary>
        /// <param name="vtpSectorJobRoleName"></param>
        /// <returns></returns>
        IQueryable<VTPSectorJobRole> GetVTPSectorJobRolesByName(string vtpSectorJobRoleName);

        /// <summary>
        /// Get VTPSectorJobRole by VTPSectorJobRoleId
        /// </summary>
        /// <param name="vtpSectorJobRoleId"></param>
        /// <returns></returns>
        VTPSectorJobRole GetVTPSectorJobRoleById(Guid vtpSectorJobRoleId);

        /// <summary>
        /// Get VTPSectorJobRole by VTPSectorJobRoleId using async
        /// </summary>
        /// <param name="vtpSectorJobRoleId"></param>
        /// <returns></returns>
        Task<VTPSectorJobRole> GetVTPSectorJobRoleByIdAsync(Guid vtpSectorJobRoleId);

        /// <summary>
        /// Insert/Update VTPSectorJobRole entity
        /// </summary>
        /// <param name="vtpSectorJobRole"></param>
        /// <returns></returns>
        bool SaveOrUpdateVTPSectorJobRoleDetails(VTPSectorJobRole vtpSectorJobRole);

        /// <summary>
        /// Delete a record by VTPSectorJobRoleId
        /// </summary>
        /// <param name="vtpSectorJobRoleId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtpSectorJobRoleId);

        /// <summary>
        /// Check duplicate VTPSectorJobRole by Name
        /// </summary>
        /// <param name="vtpSectorJobRoleModel"></param>
        /// <returns></returns>
        bool CheckVTPSectorJobRoleExistByName(VTPSectorJobRoleModel vtpSectorJobRoleModel);

        /// <summary>
        /// List of VTPSectorJobRole with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTPSectorJobRoleViewModel> GetVTPSectorJobRolesByCriteria(SearchVTPSectorJobRoleModel searchModel);
    }
}
