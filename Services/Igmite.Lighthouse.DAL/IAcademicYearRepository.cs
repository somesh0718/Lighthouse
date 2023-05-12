using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the AcademicYear entity
    /// </summary>
    public interface IAcademicYearRepository : IGenericRepository<AcademicYear>
    {
        /// <summary>
        /// Get list of AcademicYear
        /// </summary>
        /// <returns></returns>
        IQueryable<AcademicYear> GetAcademicYears();

        /// <summary>
        /// Get list of AcademicYear by academicYearName
        /// </summary>
        /// <param name="academicYearName"></param>
        /// <returns></returns>
        IQueryable<AcademicYear> GetAcademicYearsByName(string academicYearName);

        /// <summary>
        /// Get AcademicYear by AcademicYearId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        AcademicYear GetAcademicYearById(Guid academicYearId);

        /// <summary>
        /// Get AcademicYear by AcademicYearId using async
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        Task<AcademicYear> GetAcademicYearByIdAsync(Guid academicYearId);

        /// <summary>
        /// Insert/Update AcademicYear entity
        /// </summary>
        /// <param name="academicYear"></param>
        /// <returns></returns>
        bool SaveOrUpdateAcademicYearDetails(AcademicYear academicYear);

        /// <summary>
        /// Delete a record by AcademicYearId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        bool DeleteById(Guid academicYearId);

        /// <summary>
        /// Check duplicate AcademicYear by Name
        /// </summary>
        /// <param name="academicYearModel"></param>
        /// <returns></returns>
        bool CheckAcademicYearExistByName(AcademicYearModel academicYearModel);

        /// <summary>
        /// List of AcademicYear with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<AcademicYearViewModel> GetAcademicYearsByCriteria(SearchAcademicYearModel searchModel);
    }
}
