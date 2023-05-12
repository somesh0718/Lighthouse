using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the AcademicYear entity
    /// </summary>
    public interface IAcademicYearManager : IGenericManager<AcademicYearModel>
    {
        /// <summary>
        /// Get list of AcademicYears
        /// </summary>
        /// <returns></returns>
        IQueryable<AcademicYearModel> GetAcademicYears();

        /// <summary>
        /// Get list of AcademicYears by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<AcademicYearModel> GetAcademicYearsByName(string academicYearName);

        /// <summary>
        /// Get AcademicYear by AcademicYearId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        AcademicYearModel GetAcademicYearById(Guid academicYearId);

        /// <summary>
        /// Get AcademicYear by AcademicYearId using async
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        Task<AcademicYearModel> GetAcademicYearByIdAsync(Guid academicYearId);

        /// <summary>
        /// Insert/Update AcademicYear entity
        /// </summary>
        /// <param name="academicYearModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateAcademicYearDetails(AcademicYearModel academicYearModel);

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
