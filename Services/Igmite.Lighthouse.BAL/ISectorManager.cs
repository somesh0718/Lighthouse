using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the Sector entity
    /// </summary>
    public interface ISectorManager : IGenericManager<SectorModel>
    {
        /// <summary>
        /// Get list of Sectors
        /// </summary>
        /// <returns></returns>
        IQueryable<SectorModel> GetSectors();

        /// <summary>
        /// Get list of Sectors by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<SectorModel> GetSectorsByName(string sectorName);

        /// <summary>
        /// Get Sector by SectorId
        /// </summary>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        SectorModel GetSectorById(Guid sectorId);

        /// <summary>
        /// Get Sector by SectorId using async
        /// </summary>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        Task<SectorModel> GetSectorByIdAsync(Guid sectorId);

        /// <summary>
        /// Insert/Update Sector entity
        /// </summary>
        /// <param name="sectorModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateSectorDetails(SectorModel sectorModel);

        /// <summary>
        /// Delete a record by SectorId
        /// </summary>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        bool DeleteById(Guid sectorId);

        /// <summary>
        /// Check duplicate Sector by Name
        /// </summary>
        /// <param name="sectorModel"></param>
        /// <returns></returns>
        bool CheckSectorExistByName(SectorModel sectorModel);

        /// <summary>
        /// List of Sector with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<SectorViewModel> GetSectorsByCriteria(SearchSectorModel searchModel);
    }
}
