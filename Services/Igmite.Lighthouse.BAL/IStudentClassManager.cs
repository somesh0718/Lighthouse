using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the StudentClass entity
    /// </summary>
    public interface IStudentClassManager : IGenericManager<StudentClassModel>
    {
        /// <summary>
        /// Get list of StudentClasses
        /// </summary>
        /// <returns></returns>
        IQueryable<StudentClassModel> GetStudentClasses();

        /// <summary>
        /// Get list of StudentClasses by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<StudentClassModel> GetStudentClassesByName(string studentClassName);

        /// <summary>
        /// Get list of Student by names
        /// </summary>
        /// <param name="studentNames"></param>
        /// <returns></returns>
        IQueryable<StudentClassModel> GetStudentsByNames(List<string> studentNames);

        /// <summary>
        /// Get StudentClass by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        StudentClassModel GetStudentClassById(Guid studentId);

        /// <summary>
        /// Get StudentClass by StudentId using async
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<StudentClassModel> GetStudentClassByIdAsync(Guid studentId);

        /// <summary>
        /// Insert/Update StudentClass entity
        /// </summary>
        /// <param name="studentClassModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateStudentClassDetails(StudentClassModel studentClassModel);

        /// <summary>
        /// Delete a record by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        bool DeleteById(Guid studentId);

        /// <summary>
        /// List of StudentClass with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<StudentClassViewModel> GetStudentClassesByCriteria(SearchStudentClassModel searchModel);
    }
}