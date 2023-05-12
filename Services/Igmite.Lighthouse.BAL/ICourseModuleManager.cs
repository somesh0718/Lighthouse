using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the CourseModule entity
    /// </summary>
    public interface ICourseModuleManager : IGenericManager<CourseModuleModel>
    {
        /// <summary>
        /// Get list of CourseModules
        /// </summary>
        /// <returns></returns>
        IQueryable<CourseModuleModel> GetCourseModules();

        /// <summary>
        /// Get list of CourseModules by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<CourseModuleModel> GetCourseModulesByName(string name);

        /// <summary>
        /// Get CourseModule by CourseModuleId
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        CourseModuleModel GetCourseModuleById(Guid courseModuleId);

        /// <summary>
        /// Get CourseModule by CourseModuleId using async
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        Task<CourseModuleModel> GetCourseModuleByIdAsync(Guid courseModuleId);

        /// <summary>
        /// Insert/Update CourseModule entity
        /// </summary>
        /// <param name="courseModuleModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateCourseModuleDetails(CourseModuleModel courseModuleModel, bool isBulkUpload = false);

        /// <summary>
        /// Delete a record by CourseModuleId
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        bool DeleteById(Guid courseModuleId);

        /// <summary>
        /// Check duplicate CourseModule by Name
        /// </summary>
        /// <param name="courseModuleModel"></param>
        /// <returns></returns>
        bool CheckCourseModuleExistByName(CourseModuleModel courseModuleModel);

        /// <summary>
        /// List of CourseModule with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<CourseModuleViewModel> GetCourseModulesByCriteria(SearchCourseModuleModel searchModel);
    }
}