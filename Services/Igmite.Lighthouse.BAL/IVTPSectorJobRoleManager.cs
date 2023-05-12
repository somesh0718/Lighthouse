using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VTPSectorJobRole entity
    /// </summary>
    public interface IVTPSectorJobRoleManager : IGenericManager<VTPSectorJobRoleModel>
    {
        /// <summary>
        /// Get list of VTPSectorJobRoles
        /// </summary>
        /// <returns></returns>
        IQueryable<VTPSectorJobRoleModel> GetVTPSectorJobRoles();

        /// <summary>
        /// Get list of VTPSectorJobRoles by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VTPSectorJobRoleModel> GetVTPSectorJobRolesByName(string vtpSectorJobRoleName);

        /// <summary>
        /// Get VTPSectorJobRole by VTPSectorJobRoleId
        /// </summary>
        /// <param name="vtpSectorJobRoleId"></param>
        /// <returns></returns>
        VTPSectorJobRoleModel GetVTPSectorJobRoleById(Guid vtpSectorJobRoleId);

        /// <summary>
        /// Get VTPSectorJobRole by VTPSectorJobRoleId using async
        /// </summary>
        /// <param name="vtpSectorJobRoleId"></param>
        /// <returns></returns>
        Task<VTPSectorJobRoleModel> GetVTPSectorJobRoleByIdAsync(Guid vtpSectorJobRoleId);

        /// <summary>
        /// Insert/Update VTPSectorJobRole entity
        /// </summary>
        /// <param name="vtpSectorJobRoleModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVTPSectorJobRoleDetails(VTPSectorJobRoleModel vtpSectorJobRoleModel);

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
