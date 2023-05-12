using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the SectorJobRole entity
    /// </summary>
    public interface ISectorJobRoleManager : IGenericManager<SectorJobRoleModel>
    {
        /// <summary>
        /// Get list of SectorJobRoles
        /// </summary>
        /// <returns></returns>
        IQueryable<SectorJobRoleModel> GetSectorJobRoles();

        /// <summary>
        /// Get list of SectorJobRoles by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<SectorJobRoleModel> GetSectorJobRolesByName(string sectorJobRoleName);

        /// <summary>
        /// Get SectorJobRole by SectorJobRoleId
        /// </summary>
        /// <param name="sectorJobRoleId"></param>
        /// <returns></returns>
        SectorJobRoleModel GetSectorJobRoleById(Guid sectorJobRoleId);

        /// <summary>
        /// Get SectorJobRole by SectorJobRoleId using async
        /// </summary>
        /// <param name="sectorJobRoleId"></param>
        /// <returns></returns>
        Task<SectorJobRoleModel> GetSectorJobRoleByIdAsync(Guid sectorJobRoleId);

        /// <summary>
        /// Insert/Update SectorJobRole entity
        /// </summary>
        /// <param name="sectorJobRoleModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateSectorJobRoleDetails(SectorJobRoleModel sectorJobRoleModel);

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
