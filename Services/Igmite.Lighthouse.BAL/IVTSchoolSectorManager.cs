using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VTSchoolSector entity
    /// </summary>
    public interface IVTSchoolSectorManager : IGenericManager<VTSchoolSectorModel>
    {
        /// <summary>
        /// Get list of VTSchoolSectors
        /// </summary>
        /// <returns></returns>
        IQueryable<VTSchoolSectorModel> GetVTSchoolSectors();

        /// <summary>
        /// Get list of VTSchoolSectors by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VTSchoolSectorModel> GetVTSchoolSectorsByName(string vtSchoolSectorName);

        /// <summary>
        /// Get VTSchoolSector by VTSchoolSectorId
        /// </summary>
        /// <param name="vtSchoolSectorId"></param>
        /// <returns></returns>
        VTSchoolSectorModel GetVTSchoolSectorById(Guid vtSchoolSectorId);

        VTSchoolSectorModel GetVTSchoolSectorBySchoolIdANDSectorId(Guid SchoolId, Guid SectorId);

        /// <summary>
        /// Get VTSchoolSector by VTSchoolSectorId using async
        /// </summary>
        /// <param name="vtSchoolSectorId"></param>
        /// <returns></returns>
        Task<VTSchoolSectorModel> GetVTSchoolSectorByIdAsync(Guid vtSchoolSectorId);

        /// <summary>
        /// Insert/Update VTSchoolSector entity
        /// </summary>
        /// <param name="vtSchoolSectorModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVTSchoolSectorDetails(VTSchoolSectorModel vtSchoolSectorModel);

        /// <summary>
        /// Delete a record by VTSchoolSectorId
        /// </summary>
        /// <param name="vtSchoolSectorId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtSchoolSectorId);

        /// <summary>
        /// List of VTSchoolSector with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTSchoolSectorViewModel> GetVTSchoolSectorsByCriteria(SearchVTSchoolSectorModel searchModel);
    }
}