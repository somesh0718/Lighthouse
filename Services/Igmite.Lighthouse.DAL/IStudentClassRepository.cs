using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the StudentClass entity
    /// </summary>
    public interface IStudentClassRepository : IGenericRepository<StudentClass>
    {
        /// <summary>
        /// Get list of StudentClass
        /// </summary>
        /// <returns></returns>
        IQueryable<StudentClass> GetStudentClasses();

        /// <summary>
        /// Get list of StudentClass by studentClassName
        /// </summary>
        /// <param name="studentClassName"></param>
        /// <returns></returns>
        IQueryable<StudentClass> GetStudentClassesByName(string studentClassName);

        /// <summary>
        /// Get list of Student by names
        /// </summary>
        /// <param name="studentNames"></param>
        /// <returns></returns>
        IQueryable<StudentClass> GetStudentsByNames(List<string> studentNames);

        /// <summary>
        /// Get StudentClass by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        StudentClass GetStudentClassById(Guid studentId);

        /// <summary>
        /// Get StudentClass by StudentId using async
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<StudentClass> GetStudentClassByIdAsync(Guid studentId);

        /// <summary>
        /// Insert/Update StudentClass entity
        /// </summary>
        /// <param name="studentClass"></param>
        /// <returns></returns>
        bool SaveOrUpdateStudentClassDetails(StudentClass studentClass);

        /// <summary>
        /// Delete a record by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        bool DeleteById(Guid studentId, string userId);

        /// <summary>
        /// Check duplicate StudentClass by Name
        /// </summary>
        /// <param name="studentClass"></param>
        /// <param name="studentClassModel"></param>
        /// <returns></returns>
        string CheckStudentClassExistByName(StudentClass studentClass, StudentClassModel studentClassModel);

        /// <summary>
        /// List of StudentClass with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<StudentClassViewModel> GetStudentClassesByCriteria(SearchStudentClassModel searchModel);
    }
}