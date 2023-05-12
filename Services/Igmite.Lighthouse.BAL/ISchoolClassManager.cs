using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the SchoolClass entity
    /// </summary>
    public interface ISchoolClassManager : IGenericManager<SchoolClassModel>
    {
        /// <summary>
        /// Get list of SchoolClasses
        /// </summary>
        /// <returns></returns>
        IQueryable<SchoolClassModel> GetSchoolClasses();

        /// <summary>
        /// Get list of SchoolClasses by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<SchoolClassModel> GetSchoolClassesByName(string schoolClassName);

        /// <summary>
        /// Get SchoolClass by ClassId
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        SchoolClassModel GetSchoolClassById(Guid classId);

        /// <summary>
        /// Get SchoolClass by ClassId using async
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        Task<SchoolClassModel> GetSchoolClassByIdAsync(Guid classId);

        /// <summary>
        /// Insert/Update SchoolClass entity
        /// </summary>
        /// <param name="schoolClassModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateSchoolClassDetails(SchoolClassModel schoolClassModel);

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
