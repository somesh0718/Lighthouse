using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the SchoolVTPSector entity
    /// </summary>
    public interface ISchoolVTPSectorManager : IGenericManager<SchoolVTPSectorModel>
    {
        /// <summary>
        /// Get list of SchoolVTPSectors
        /// </summary>
        /// <returns></returns>
        IQueryable<SchoolVTPSectorModel> GetSchoolVTPSectors();

        /// <summary>
        /// Get list of SchoolVTPSectors by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<SchoolVTPSectorModel> GetSchoolVTPSectorsByName(string schoolVTPSectorName);

        /// <summary>
        /// Get list of SchoolVTPSector by VTP & Sector
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="schoolId"></param>
        /// <param name="vtpId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        SchoolVTPSectorModel GetSchoolVTPSectorsBy3Ids(Guid academicYearId, Guid schoolId, Guid vtpId, Guid sectorId);

        /// <summary>
        /// Get SchoolVTPSector by SchoolVTPSectorId
        /// </summary>
        /// <param name="schoolVTPSectorId"></param>
        /// <returns></returns>
        SchoolVTPSectorModel GetSchoolVTPSectorById(Guid schoolVTPSectorId);

        /// <summary>
        /// Get SchoolVTPSector by SchoolVTPSectorId using async
        /// </summary>
        /// <param name="schoolVTPSectorId"></param>
        /// <returns></returns>
        Task<SchoolVTPSectorModel> GetSchoolVTPSectorByIdAsync(Guid schoolVTPSectorId);

        /// <summary>
        /// Insert/Update SchoolVTPSector entity
        /// </summary>
        /// <param name="schoolVTPSectorModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateSchoolVTPSectorDetails(SchoolVTPSectorModel schoolVTPSectorModel);

        /// <summary>
        /// Delete a record by SchoolVTPSectorId
        /// </summary>
        /// <param name="schoolVTPSectorId"></param>
        /// <returns></returns>
        bool DeleteById(Guid schoolVTPSectorId);

        /// <summary>
        /// List of SchoolVTPSector with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<SchoolVTPSectorViewModel> GetSchoolVTPSectorsByCriteria(SearchSchoolVTPSectorModel searchModel);
    }
}