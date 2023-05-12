using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VCSchoolSector entity
    /// </summary>
    public interface IVCSchoolSectorRepository : IGenericRepository<VCSchoolSector>
    {
        /// <summary>
        /// Get list of VCSchoolSector
        /// </summary>
        /// <returns></returns>
        IQueryable<VCSchoolSector> GetVCSchoolSectors();

        /// <summary>
        /// Get list of VCSchoolSector by vcSchoolSectorName
        /// </summary>
        /// <param name="vcSchoolSectorName"></param>
        /// <returns></returns>
        IQueryable<VCSchoolSector> GetVCSchoolSectorsByName(string vcSchoolSectorName);

        /// <summary>
        /// Get VCSchoolSector by VCSchoolSectorId
        /// </summary>
        /// <param name="vcSchoolSectorId"></param>
        /// <returns></returns>
        VCSchoolSector GetVCSchoolSectorById(Guid vcSchoolSectorId);

        /// <summary>
        /// Get VCSchoolSector by VCSchoolSectorId using async
        /// </summary>
        /// <param name="vcSchoolSectorId"></param>
        /// <returns></returns>
        Task<VCSchoolSector> GetVCSchoolSectorByIdAsync(Guid vcSchoolSectorId);

        /// <summary>
        /// Get SchoolSector by VCId 
        /// </summary>
        /// <param name="vcId"></param>
        /// <returns></returns>
        Task<VCSchoolSector> GetSchoolSectorByVCId(Guid vcId);

        /// <summary>
        /// Insert/Update VCSchoolSector entity
        /// </summary>
        /// <param name="vcSchoolSector"></param>
        /// <returns></returns>
        bool SaveOrUpdateVCSchoolSectorDetails(VCSchoolSector vcSchoolSector);

        /// <summary>
        /// Delete a record by VCSchoolSectorId
        /// </summary>
        /// <param name="vcSchoolSectorId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vcSchoolSectorId);

        /// <summary>
        /// Check duplicate VCSchoolSector by Name
        /// </summary>
        /// <param name="vcSchoolSector"></param>
        /// <param name="vcSchoolSectorModel"></param>
        /// <returns></returns>
        string CheckVCSchoolSectorExistByName(VCSchoolSector vcSchoolSector, VCSchoolSectorModel vcSchoolSectorModel);

        /// <summary>
        /// List of VCSchoolSector with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VCSchoolSectorViewModel> GetVCSchoolSectorsByCriteria(SearchVCSchoolSectorModel searchModel);
    }
}
