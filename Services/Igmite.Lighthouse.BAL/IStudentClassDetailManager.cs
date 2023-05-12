using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the StudentClassDetail entity
    /// </summary>
    public interface IStudentClassDetailManager : IGenericManager<StudentClassDetailModel>
    {
        /// <summary>
        /// Get list of StudentClassDetails
        /// </summary>
        /// <returns></returns>
        IQueryable<StudentClassDetailModel> GetStudentClassDetails();

        /// <summary>
        /// Get list of StudentClassDetails by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<StudentClassDetailModel> GetStudentClassDetailsByName(string studentClassDetailName);

        /// <summary>
        /// Get StudentClassDetail by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        StudentClassDetailModel GetStudentClassDetailById(Guid studentId);

        /// <summary>
        /// Get StudentClassDetail by StudentId using async
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<StudentClassDetailModel> GetStudentClassDetailByIdAsync(Guid studentId);

        /// <summary>
        /// Insert/Update StudentClassDetail entity
        /// </summary>
        /// <param name="studentClassDetailModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateStudentClassDetailDetails(StudentClassDetailModel studentClassDetailModel);

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
