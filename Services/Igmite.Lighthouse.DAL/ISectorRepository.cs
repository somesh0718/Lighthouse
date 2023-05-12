using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the Sector entity
    /// </summary>
    public interface ISectorRepository : IGenericRepository<Sector>
    {
        /// <summary>
        /// Get list of Sector
        /// </summary>
        /// <returns></returns>
        IQueryable<Sector> GetSectors();

        /// <summary>
        /// Get list of Sector by sectorName
        /// </summary>
        /// <param name="sectorName"></param>
        /// <returns></returns>
        IQueryable<Sector> GetSectorsByName(string sectorName);

        /// <summary>
        /// Get Sector by SectorId
        /// </summary>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        Sector GetSectorById(Guid sectorId);

        /// <summary>
        /// Get Sector by SectorId using async
        /// </summary>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        Task<Sector> GetSectorByIdAsync(Guid sectorId);

        /// <summary>
        /// Insert/Update Sector entity
        /// </summary>
        /// <param name="sector"></param>
        /// <returns></returns>
        bool SaveOrUpdateSectorDetails(Sector sector);

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
