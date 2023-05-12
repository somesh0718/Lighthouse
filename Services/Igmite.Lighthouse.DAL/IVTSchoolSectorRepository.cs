using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VTSchoolSector entity
    /// </summary>
    public interface IVTSchoolSectorRepository : IGenericRepository<VTSchoolSector>
    {
        /// <summary>
        /// Get list of VTSchoolSector
        /// </summary>
        /// <returns></returns>
        IQueryable<VTSchoolSector> GetVTSchoolSectors();

        /// <summary>
        /// Get list of VTSchoolSector by vtSchoolSectorName
        /// </summary>
        /// <param name="vtSchoolSectorName"></param>
        /// <returns></returns>
        IQueryable<VTSchoolSector> GetVTSchoolSectorsByName(string vtSchoolSectorName);

        /// <summary>
        /// Get VTSchoolSector by VTSchoolSectorId
        /// </summary>
        /// <param name="vtSchoolSectorId"></param>
        /// <returns></returns>
        VTSchoolSector GetVTSchoolSectorById(Guid vtSchoolSectorId);

        /// <summary>
        /// Get VTSchoolSector by SchoolId & SectorId
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <param name="SectorId"></param>
        /// <returns></returns>
        VTSchoolSector GetVTSchoolSectorBySchoolIdAndSectorId(Guid schoolId, Guid sectorId);

        /// <summary>
        /// Get VTSchoolSector by VTSchoolSectorId using async
        /// </summary>
        /// <param name="vtSchoolSectorId"></param>
        /// <returns></returns>
        Task<VTSchoolSector> GetVTSchoolSectorByIdAsync(Guid vtSchoolSectorId);

        /// <summary>
        /// Insert/Update VTSchoolSector entity
        /// </summary>
        /// <param name="vtSchoolSector"></param>
        /// <returns></returns>
        bool SaveOrUpdateVTSchoolSectorDetails(VTSchoolSector schoolSector, VTSchoolSectorModel schoolSectorModel, bool isChangeStatus);

        /// <summary>
        /// Delete a record by VTSchoolSectorId
        /// </summary>
        /// <param name="vtSchoolSectorId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtSchoolSectorId);

        /// <summary>
        /// Check duplicate VTSchoolSector by Name
        /// </summary>
        /// <param name="schoolSector"></param>
        /// <param name="schoolSectorModel"></param>
        /// <returns></returns>
        string CheckVTSchoolSectorExistByName(VTSchoolSector schoolSector, VTSchoolSectorModel schoolSectorModel);

        /// <summary>
        /// Check VT School Sector can be inactive
        /// </summary>
        /// <param name="vtSchoolSector"></param>
        /// <returns></returns>
        bool CheckUserCanInactiveVTSchoolSectorById(VTSchoolSector vtSchoolSector);

        /// <summary>
        /// List of VTSchoolSector with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTSchoolSectorViewModel> GetVTSchoolSectorsByCriteria(SearchVTSchoolSectorModel searchModel);

        /// <summary>
        /// Get list of JobRoles by VT School Sector Id
        /// </summary>
        /// <param name="schoolSectorId"></param>
        /// <returns></returns>
        IList<Guid> GetJobRolesByVTSchoolSectorId(Guid schoolSectorId);
    }
}