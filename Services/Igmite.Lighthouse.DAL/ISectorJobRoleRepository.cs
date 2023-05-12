using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the SectorJobRole entity
    /// </summary>
    public interface ISectorJobRoleRepository : IGenericRepository<SectorJobRole>
    {
        /// <summary>
        /// Get list of SectorJobRole
        /// </summary>
        /// <returns></returns>
        IQueryable<SectorJobRole> GetSectorJobRoles();

        /// <summary>
        /// Get list of SectorJobRole by sectorJobRoleName
        /// </summary>
        /// <param name="sectorJobRoleName"></param>
        /// <returns></returns>
        IQueryable<SectorJobRole> GetSectorJobRolesByName(string sectorJobRoleName);

        /// <summary>
        /// Get SectorJobRole by SectorJobRoleId
        /// </summary>
        /// <param name="sectorJobRoleId"></param>
        /// <returns></returns>
        SectorJobRole GetSectorJobRoleById(Guid sectorJobRoleId);

        /// <summary>
        /// Get SectorJobRole by SectorJobRoleId using async
        /// </summary>
        /// <param name="sectorJobRoleId"></param>
        /// <returns></returns>
        Task<SectorJobRole> GetSectorJobRoleByIdAsync(Guid sectorJobRoleId);

        /// <summary>
        /// Insert/Update SectorJobRole entity
        /// </summary>
        /// <param name="sectorJobRole"></param>
        /// <returns></returns>
        bool SaveOrUpdateSectorJobRoleDetails(SectorJobRole sectorJobRole);

        /// <summary>
        /// Delete a record by SectorJobRoleId
        /// </summary>
        /// <param name="sectorJobRoleId"></param>
        /// <returns></returns>
        bool DeleteById(Guid sectorJobRoleId);

        /// <summary>
        /// Check duplicate SectorJobRole by Name
        /// </summary>
        /// <param name="sectorJobRoleModel"></param>
        /// <returns></returns>
        bool CheckSectorJobRoleExistByName(SectorJobRoleModel sectorJobRoleModel);

        /// <summary>
        /// List of SectorJobRole with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<SectorJobRoleViewModel> GetSectorJobRolesByCriteria(SearchSectorJobRoleModel searchModel);
    }
}
