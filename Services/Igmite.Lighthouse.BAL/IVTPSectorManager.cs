using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VTPSector entity
    /// </summary>
    public interface IVTPSectorManager : IGenericManager<VTPSectorModel>
    {
        /// <summary>
        /// Get list of VTPSectors
        /// </summary>
        /// <returns></returns>
        IQueryable<VTPSectorModel> GetVTPSectors();

        /// <summary>
        /// Get list of VTPSectors by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VTPSectorModel> GetVTPSectorsByName(string vtpSectorName);

        /// <summary>
        /// Get VTPSector by VTPSectorId
        /// </summary>
        /// <param name="vtpSectorId"></param>
        /// <returns></returns>
        VTPSectorModel GetVTPSectorById(Guid vtpSectorId);

        /// <summary>
        /// Get VTPSector by VTPSectorId using async
        /// </summary>
        /// <param name="vtpSectorId"></param>
        /// <returns></returns>
        Task<VTPSectorModel> GetVTPSectorByIdAsync(Guid vtpSectorId);

        /// <summary>
        /// Insert/Update VTPSector entity
        /// </summary>
        /// <param name="vtpSectorModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVTPSectorDetails(VTPSectorModel vtpSectorModel);

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
        /// Get list of VTPSector
        /// </summary>
        /// <returns></returns>
        IList<VTPSectorModel> GetVTPSectorList();

        /// <summary>
        /// List of VTPSector with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTPSectorViewModel> GetVTPSectorsByCriteria(SearchVTPSectorModel searchModel);
    }
}