using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the CourseModule entity
    /// </summary>
    public interface ICourseModuleRepository : IGenericRepository<CourseModule>
    {
        /// <summary>
        /// Get list of CourseModule
        /// </summary>
        /// <returns></returns>
        IQueryable<CourseModule> GetCourseModules();

        /// <summary>
        /// Get list of CourseModule by name
        /// </summary>
        /// <param name="courseModuleName"></param>
        /// <returns></returns>
        IQueryable<CourseModule> GetCourseModulesByName(string name);

        /// <summary>
        /// Get CourseModule by CourseModuleId
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        CourseModule GetCourseModuleById(Guid courseModuleId);

        /// <summary>
        /// Get CourseModule by CourseModuleId using async
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        Task<CourseModule> GetCourseModuleByIdAsync(Guid courseModuleId);

        /// <summary>
        /// Insert/Update CourseModule entity
        /// </summary>
        /// <param name="courseModule"></param>
        /// <returns></returns>
        bool SaveOrUpdateCourseModuleDetails(CourseModule courseModule, IList<UnitSessionModel> unitSessionIds);

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
        CourseModule CheckCourseModuleExistByName(CourseModuleModel courseModuleModel);

        /// <summary>
        /// Get UnitSessions by CourseModuleId
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        IList<UnitSessionModel> GetUnitSessionsById(Guid courseModuleId);

        /// <summary>
        /// List of CourseModule with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<CourseModuleViewModel> GetCourseModulesByCriteria(SearchCourseModuleModel searchModel);
    }
}