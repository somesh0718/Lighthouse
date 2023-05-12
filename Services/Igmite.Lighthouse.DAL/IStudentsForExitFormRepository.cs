using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the StudentsForExitForm entity
    /// </summary>
    public interface IStudentsForExitFormRepository : IGenericRepository<StudentsForExitForm>
    {
        /// <summary>
        /// Get list of StudentsForExitForm
        /// </summary>
        /// <returns></returns>
        IList<StudentsForExitSurveyViewModel> GetStudentsForExitForm(ExitSurveyRequestModel exitSurveyDetailsModel);

        IQueryable<ExitSurveyReportModel> GetExitSurveyReport(ExitSurveyRequestModel exitSurveyDetailsModel);

        /// <summary>
        /// Get list of StudentsForExitForm by studentsForExitFormName
        /// </summary>
        /// <param name="studentsForExitFormName"></param>
        /// <returns></returns>
        IQueryable<StudentsForExitForm> GetStudentsForExitFormByName(string name);

        /// <summary>
        /// Get list of Student by names
        /// </summary>
        /// <param name="studentNames"></param>
        /// <returns></returns>
        IQueryable<StudentsForExitForm> GetStudentsByNames(List<string> studentNames);

        /// <summary>
        /// Get StudentsForExitForm by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        StudentsForExitForm GetStudentsForExitFormById(Guid studentId);

        /// <summary>
        /// Get StudentsForExitForm by StudentId using async
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<StudentsForExitForm> GetStudentsForExitFormByIdAsync(Guid studentId);

        /// <summary>
        /// Insert/Update StudentsForExitForm entity
        /// </summary>
        /// <param name="studentsForExitForm"></param>
        /// <returns></returns>
        bool SaveOrUpdateStudentsForExitFormDetails(StudentsForExitForm studentsForExitForm);

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