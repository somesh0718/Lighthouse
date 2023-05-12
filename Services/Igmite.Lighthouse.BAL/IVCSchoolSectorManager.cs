using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VCSchoolSector entity
    /// </summary>
    public interface IVCSchoolSectorManager : IGenericManager<VCSchoolSectorModel>
    {
        /// <summary>
        /// Get list of VCSchoolSectors
        /// </summary>
        /// <returns></returns>
        IQueryable<VCSchoolSectorModel> GetVCSchoolSectors();

        /// <summary>
        /// Get list of VCSchoolSectors by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VCSchoolSectorModel> GetVCSchoolSectorsByName(string vcSchoolSectorName);

        /// <summary>
        /// Get VCSchoolSector by VCSchoolSectorId
        /// </summary>
        /// <param name="vcSchoolSectorId"></param>
        /// <returns></returns>
        VCSchoolSectorModel GetVCSchoolSectorById(Guid vcSchoolSectorId);

        /// <summary>
        /// Get VCSchoolSector by VCSchoolSectorId using async
        /// </summary>
        /// <param name="vcSchoolSectorId"></param>
        /// <returns></returns>
        Task<VCSchoolSectorModel> GetVCSchoolSectorByIdAsync(Guid vcSchoolSectorId);

        /// <summary>
        /// Insert/Update VCSchoolSector entity
        /// </summary>
        /// <param name="vcSchoolSectorModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVCSchoolSectorDetails(VCSchoolSectorModel vcSchoolSectorModel);

        /// <summary>
        /// Delete a record by VCSchoolSectorId
        /// </summary>
        /// <param name="vcSchoolSectorId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vcSchoolSectorId);

        /// <summary>
        /// List of VCSchoolSector with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VCSchoolSectorViewModel> GetVCSchoolSectorsByCriteria(SearchVCSchoolSectorModel searchModel);
    }
}