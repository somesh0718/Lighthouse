using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VTPSector entity
    /// </summary>
    public interface IVTPSectorRepository : IGenericRepository<VTPSector>
    {
        /// <summary>
        /// Get list of VTPSector
        /// </summary>
        /// <returns></returns>
        IQueryable<VTPSector> GetVTPSectors();

        /// <summary>
        /// Get list of VTPSector by vtpSectorName
        /// </summary>
        /// <param name="vtpSectorName"></param>
        /// <returns></returns>
        IQueryable<VTPSector> GetVTPSectorsByName(string vtpSectorName);

        /// <summary>
        /// Get VTPSector by VTPSectorId
        /// </summary>
        /// <param name="vtpSectorId"></param>
        /// <returns></returns>
        VTPSector GetVTPSectorById(Guid vtpSectorId);

        /// <summary>
        /// Get VTPSector by VTPSectorId using async
        /// </summary>
        /// <param name="vtpSectorId"></param>
        /// <returns></returns>
        Task<VTPSector> GetVTPSectorByIdAsync(Guid vtpSectorId);

        /// <summary>
        /// Insert/Update VTPSector entity
        /// </summary>
        /// <param name="vtpSector"></param>
        /// <returns></returns>
        bool SaveOrUpdateVTPSectorDetails(VTPSector vtpSector);

        /// <summary>
        /// Delete a record by VTPSectorId
        /// </summary>
        /// <param name="vtpSectorId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtpSectorId);

        /// <summary>
        /// Check duplicate VTPSector by Name
        /// </summary>
        /// <param name="vtpSectorModel"></param>
        /// <returns></returns>
        bool CheckVTPSectorExistByName(VTPSectorModel vtpSectorModel);

        /// <summary>
        /// Check VTP Sector can be inactive
        /// </summary>
        /// <param name="vtpSector"></param>
        /// <returns></returns>
        bool CheckUserCanInactiveVTPSectorById(VTPSector vtpSector);

        /// <summary>
        /// Get list of VTPSector
        /// </summary>
        /// <returns></returns>
        IList<VTPSector> GetVTPSectorList();

        /// <summary>
        /// List of VTPSector with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTPSectorViewModel> GetVTPSectorsByCriteria(SearchVTPSectorModel searchModel);
    }
}