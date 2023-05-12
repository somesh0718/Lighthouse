using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the CourseMaterial entity
    /// </summary>
    public interface ICourseMaterialManager : IGenericManager<CourseMaterialModel>
    {
        /// <summary>
        /// Get list of CourseMaterials
        /// </summary>
        /// <returns></returns>
        IQueryable<CourseMaterialModel> GetCourseMaterials();

        /// <summary>
        /// Get list of CourseMaterials by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<CourseMaterialModel> GetCourseMaterialsByName(string courseMaterialName);

        /// <summary>
        /// Get CourseMaterial by CourseMaterialId
        /// </summary>
        /// <param name="courseMaterialId"></param>
        /// <returns></returns>
        CourseMaterialModel GetCourseMaterialById(Guid courseMaterialId);

        /// <summary>
        /// Get CourseMaterial by CourseMaterialId using async
        /// </summary>
        /// <param name="courseMaterialId"></param>
        /// <returns></returns>
        Task<CourseMaterialModel> GetCourseMaterialByIdAsync(Guid courseMaterialId);

        /// <summary>
        /// Insert/Update CourseMaterial entity
        /// </summary>
        /// <param name="courseMaterialModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateCourseMaterialDetails(CourseMaterialModel courseMaterialModel);

        /// <summary>
        /// Delete a record by CourseMaterialId
        /// </summary>
        /// <param name="courseMaterialId"></param>
        /// <returns></returns>
        bool DeleteById(Guid courseMaterialId);

        /// <summary>
        /// Check duplicate CourseMaterial by Name
        /// </summary>
        /// <param name="courseMaterialModel"></param>
        /// <returns></returns>
        bool CheckCourseMaterialExistByName(CourseMaterialModel courseMaterialModel);

        /// <summary>
        /// List of CourseMaterial with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<CourseMaterialViewModel> GetCourseMaterialsByCriteria(SearchCourseMaterialModel searchModel);
    }
}
