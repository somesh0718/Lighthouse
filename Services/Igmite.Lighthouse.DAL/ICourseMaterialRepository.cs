using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the CourseMaterial entity
    /// </summary>
    public interface ICourseMaterialRepository : IGenericRepository<CourseMaterial>
    {
        /// <summary>
        /// Get list of CourseMaterial
        /// </summary>
        /// <returns></returns>
        IQueryable<CourseMaterial> GetCourseMaterials();

        /// <summary>
        /// Get list of CourseMaterial by courseMaterialName
        /// </summary>
        /// <param name="courseMaterialName"></param>
        /// <returns></returns>
        IQueryable<CourseMaterial> GetCourseMaterialsByName(string courseMaterialName);

        /// <summary>
        /// Get CourseMaterial by CourseMaterialId
        /// </summary>
        /// <param name="courseMaterialId"></param>
        /// <returns></returns>
        CourseMaterial GetCourseMaterialById(Guid courseMaterialId);

        /// <summary>
        /// Get CourseMaterial by CourseMaterialId using async
        /// </summary>
        /// <param name="courseMaterialId"></param>
        /// <returns></returns>
        Task<CourseMaterial> GetCourseMaterialByIdAsync(Guid courseMaterialId);

        /// <summary>
        /// Insert/Update CourseMaterial entity
        /// </summary>
        /// <param name="courseMaterial"></param>
        /// <returns></returns>
        bool SaveOrUpdateCourseMaterialDetails(CourseMaterial courseMaterial);

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