using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the StudentClassDetail entity
    /// </summary>
    public interface IStudentClassDetailRepository : IGenericRepository<StudentClassDetail>
    {
        /// <summary>
        /// Get list of StudentClassDetail
        /// </summary>
        /// <returns></returns>
        IQueryable<StudentClassDetail> GetStudentClassDetails();

        /// <summary>
        /// Get list of StudentClassDetail by studentClassDetailName
        /// </summary>
        /// <param name="studentClassDetailName"></param>
        /// <returns></returns>
        IQueryable<StudentClassDetail> GetStudentClassDetailsByName(string studentClassDetailName);

        /// <summary>
        /// Get StudentClassDetail by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        StudentClassDetail GetStudentClassDetailById(Guid studentId);

        /// <summary>
        /// Get StudentClassDetail by StudentId using async
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<StudentClassDetail> GetStudentClassDetailByIdAsync(Guid studentId);

        /// <summary>
        /// Insert/Update StudentClassDetail entity
        /// </summary>
        /// <param name="studentClassDetail"></param>
        /// <returns></returns>
        bool SaveOrUpdateStudentClassDetailDetails(StudentClassDetail studentClassDetail);

        /// <summary>
        /// Delete a record by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        bool DeleteById(Guid studentId);

        /// <summary>
        /// Check duplicate StudentClassDetail by Name
        /// </summary>
        /// <param name="studentClassDetailModel"></param>
        /// <returns></returns>
        IList<string> CheckStudentClassDetailExistByName(StudentClassDetailModel studentClassDetailModel);

        /// <summary>
        /// List of StudentClassDetail with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<StudentClassDetailViewModel> GetStudentClassDetailsByCriteria(SearchStudentClassDetailModel searchModel);

        /// <summary>
        /// Get VocationalEducationAssessment with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        VocationalEducationAssessmentModel GetVocationalEducationAssessmentBySchoolAndVTId(SearchStudentClassDetailModel searchModel);
    }
}