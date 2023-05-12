using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the StudentsForExitForm entity
    /// </summary>
    public interface IStudentsForExitFormManager : IGenericManager<StudentsForExitFormModel>
    {
        /// <summary>
        /// Get list of StudentsForExitFormes
        /// </summary>
        /// <returns></returns>
        IList<StudentsForExitSurveyViewModel> GetStudentsForExitForm(ExitSurveyRequestModel exitSurveyDetailsModel);

        IQueryable<ExitSurveyReportModel> GetExitSurveyReport(ExitSurveyRequestModel exitSurveyDetailsModel);

        /// <summary>
        /// Get list of StudentsForExitFormes by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<StudentsForExitFormModel> GetStudentsForExitFormByName(string studentsForExitFormName);

        /// <summary>
        /// Get list of Student by names
        /// </summary>
        /// <param name="studentNames"></param>
        /// <returns></returns>
        IQueryable<StudentsForExitFormModel> GetStudentsByNames(List<string> studentNames);

        /// <summary>
        /// Get StudentsForExitForm by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        StudentsForExitFormModel GetStudentsForExitFormById(Guid studentId);

        /// <summary>
        /// Get StudentsForExitForm by StudentId using async
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<StudentsForExitFormModel> GetStudentsForExitFormByIdAsync(Guid studentId);

        /// <summary>
        /// Insert/Update StudentsForExitForm entity
        /// </summary>
        /// <param name="studentsForExitFormModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateStudentsForExitFormDetails(StudentsForExitFormModel studentsForExitFormModel);

        /// <summary>
        /// Delete a record by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        bool DeleteById(Guid studentId);

        /// <summary>
        /// Check duplicate StudentsForExitForm by Name
        /// </summary>
        /// <param name="studentsForExitFormModel"></param>
        /// <returns></returns>
        bool CheckStudentsForExitFormExistByName(StudentsForExitFormModel studentsForExitFormModel);

    }
}