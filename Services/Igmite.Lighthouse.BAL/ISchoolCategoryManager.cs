using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the SchoolCategory entity
    /// </summary>
    public interface ISchoolCategoryManager : IGenericManager<SchoolCategoryModel>
    {
        /// <summary>
        /// Get list of SchoolCategories
        /// </summary>
        /// <returns></returns>
        IQueryable<SchoolCategoryModel> GetSchoolCategories();

        /// <summary>
        /// Get list of SchoolCategories by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<SchoolCategoryModel> GetSchoolCategoriesByName(string schoolCategoryName);

        /// <summary>
        /// Get SchoolCategory by SchoolCategoryId
        /// </summary>
        /// <param name="schoolCategoryId"></param>
        /// <returns></returns>
        SchoolCategoryModel GetSchoolCategoryById(Guid schoolCategoryId);

        /// <summary>
        /// Get SchoolCategory by SchoolCategoryId using async
        /// </summary>
        /// <param name="schoolCategoryId"></param>
        /// <returns></returns>
        Task<SchoolCategoryModel> GetSchoolCategoryByIdAsync(Guid schoolCategoryId);

        /// <summary>
        /// Insert/Update SchoolCategory entity
        /// </summary>
        /// <param name="schoolCategoryModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateSchoolCategoryDetails(SchoolCategoryModel schoolCategoryModel);

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
