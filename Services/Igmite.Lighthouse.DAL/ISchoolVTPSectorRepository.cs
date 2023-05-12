using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the SchoolVTPSector entity
    /// </summary>
    public interface ISchoolVTPSectorRepository : IGenericRepository<SchoolVTPSector>
    {
        /// <summary>
        /// Get list of SchoolVTPSector
        /// </summary>
        /// <returns></returns>
        IQueryable<SchoolVTPSector> GetSchoolVTPSectors();

        /// <summary>
        /// Get list of SchoolVTPSector by schoolVTPSectorName
        /// </summary>
        /// <param name="schoolVTPSectorName"></param>
        /// <returns></returns>
        IQueryable<SchoolVTPSector> GetSchoolVTPSectorsByName(string schoolVTPSectorName);

        /// <summary>
        /// Get list of SchoolVTPSector by VTP & Sector
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="schoolId"></param>
        /// <param name="vtpId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        SchoolVTPSector GetSchoolVTPSectorsBy3Ids(Guid academicYearId, Guid schoolId, Guid vtpId, Guid sectorId);

        /// <summary>
        /// Get SchoolVTPSector by SchoolVTPSectorId
        /// </summary>
        /// <param name="schoolVTPSectorId"></param>
        /// <returns></returns>
        SchoolVTPSector GetSchoolVTPSectorById(Guid schoolVTPSectorId);

        /// <summary>
        /// Get SchoolVTPSector by SchoolVTPSectorId using async
        /// </summary>
        /// <param name="schoolVTPSectorId"></param>
        /// <returns></returns>
        Task<SchoolVTPSector> GetSchoolVTPSectorByIdAsync(Guid schoolVTPSectorId);

        /// <summary>
        /// Insert/Update SchoolVTPSector entity
        /// </summary>
        /// <param name="schoolVTPSector"></param>
        /// <returns></returns>
        bool SaveOrUpdateSchoolVTPSectorDetails(SchoolVTPSector schoolVTPSector);

        /// <summary>
        /// Delete a record by SchoolVTPSectorId
        /// </summary>
        /// <param name="schoolVTPSectorId"></param>
        /// <returns></returns>
        bool DeleteById(Guid schoolVTPSectorId);

        /// <summary>
        /// Check duplicate SchoolVTPSector by Name
        /// </summary>
        /// <param name="schoolVTPSector"></param>
        /// <param name="schoolVTPSectorModel"></param>
        /// <returns></returns>
        string CheckSchoolVTPSectorExistByName(SchoolVTPSector schoolVTPSector, SchoolVTPSectorModel schoolVTPSectorModel);

        /// <summary>
        /// List of SchoolVTPSector with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<SchoolVTPSectorViewModel> GetSchoolVTPSectorsByCriteria(SearchSchoolVTPSectorModel searchModel);
    }
}