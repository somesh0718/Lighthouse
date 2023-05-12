using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the SchoolClass entity
    /// </summary>
    public interface ISchoolClassRepository : IGenericRepository<SchoolClass>
    {
        /// <summary>
        /// Get list of SchoolClass
        /// </summary>
        /// <returns></returns>
        IQueryable<SchoolClass> GetSchoolClasses();

        /// <summary>
        /// Get list of SchoolClass by schoolClassName
        /// </summary>
        /// <param name="schoolClassName"></param>
        /// <returns></returns>
        IQueryable<SchoolClass> GetSchoolClassesByName(string schoolClassName);

        /// <summary>
        /// Get SchoolClass by ClassId
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        SchoolClass GetSchoolClassById(Guid classId);

        /// <summary>
        /// Get SchoolClass by ClassId using async
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        Task<SchoolClass> GetSchoolClassByIdAsync(Guid classId);

        /// <summary>
        /// Insert/Update SchoolClass entity
        /// </summary>
        /// <param name="schoolClass"></param>
        /// <returns></returns>
        bool SaveOrUpdateSchoolClassDetails(SchoolClass schoolClass);

        /// <summary>
        /// Delete a record by ClassId
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        bool DeleteById(Guid classId);

        /// <summary>
        /// Check duplicate SchoolClass by Name
        /// </summary>
        /// <param name="schoolClassModel"></param>
        /// <returns></returns>
        bool CheckSchoolClassExistByName(SchoolClassModel schoolClassModel);

        /// <summary>
        /// List of SchoolClass with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<SchoolClassViewModel> GetSchoolClassesByCriteria(SearchSchoolClassModel searchModel);
    }
}
