using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the SchoolCategory entity
    /// </summary>
    public interface ISchoolCategoryRepository : IGenericRepository<SchoolCategory>
    {
        /// <summary>
        /// Get list of SchoolCategory
        /// </summary>
        /// <returns></returns>
        IQueryable<SchoolCategory> GetSchoolCategories();

        /// <summary>
        /// Get list of SchoolCategory by schoolCategoryName
        /// </summary>
        /// <param name="schoolCategoryName"></param>
        /// <returns></returns>
        IQueryable<SchoolCategory> GetSchoolCategoriesByName(string schoolCategoryName);

        /// <summary>
        /// Get SchoolCategory by SchoolCategoryId
        /// </summary>
        /// <param name="schoolCategoryId"></param>
        /// <returns></returns>
        SchoolCategory GetSchoolCategoryById(Guid schoolCategoryId);

        /// <summary>
        /// Get SchoolCategory by SchoolCategoryId using async
        /// </summary>
        /// <param name="schoolCategoryId"></param>
        /// <returns></returns>
        Task<SchoolCategory> GetSchoolCategoryByIdAsync(Guid schoolCategoryId);

        /// <summary>
        /// Insert/Update SchoolCategory entity
        /// </summary>
        /// <param name="schoolCategory"></param>
        /// <returns></returns>
        bool SaveOrUpdateSchoolCategoryDetails(SchoolCategory schoolCategory);

        /// <summary>
        /// Delete a record by SchoolCategoryId
        /// </summary>
        /// <param name="schoolCategoryId"></param>
        /// <returns></returns>
        bool DeleteById(Guid schoolCategoryId);

        /// <summary>
        /// Check duplicate SchoolCategory by Name
        /// </summary>
        /// <param name="schoolCategoryModel"></param>
        /// <returns></returns>
        bool CheckSchoolCategoryExistByName(SchoolCategoryModel schoolCategoryModel);

        /// <summary>
        /// List of SchoolCategory with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<SchoolCategoryViewModel> GetSchoolCategoriesByCriteria(SearchSchoolCategoryModel searchModel);
    }
}
