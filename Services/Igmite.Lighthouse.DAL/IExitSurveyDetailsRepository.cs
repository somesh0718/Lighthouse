using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the ExitSurveyDetails entity
    /// </summary>
    public interface IExitSurveyDetailsRepository : IGenericRepository<ExitSurveyDetails>
    {
        /// <summary>
        /// Get list of ExitSurveyDetails
        /// </summary>
        /// <returns></returns>
        IQueryable<ExitSurveyDetails> GetExitSurveyDetails();

        /// <summary>
        /// Get list of ExitSurveyDetails by studentClassName
        /// </summary>
        /// <param name="exitStudentId"></param>
        /// <returns></returns>
        ExitSurveyDetails GetExitSurveyDetailsByStudentId(Guid exitStudentId);

        /// <summary>
        /// Get ExitSurveyDetails by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ExitSurveyDetails GetExitSurveyDetailsById(int id);

        /// <summary>
        /// Get ExitSurveyDetails by AcademicYear & StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        ExitSurveyDetails GetExitSurveyDetailsById(Guid? academicYearId, Guid studentId);

        /// <summary>
        /// Get ExitSurveyDetails by StudentId using async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ExitSurveyDetails> GetExitSurveyDetailsByIdAsync(int id);

        /// <summary>
        /// Insert/Update ExitSurveyDetails entity
        /// </summary>
        /// <param name="exitSurveyDetails"></param>
        /// <returns></returns>
        bool SaveOrUpdateExitSurveyDetailsDetails(ExitSurveyDetails exitSurveyDetails);

        /// <summary>
        /// Delete a record by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        bool DeleteById(Guid studentId);

        /// <summary>
        /// Check duplicate ExitSurveyDetails by Name
        /// </summary>
        /// <param name="exitSurveyDetailsModel"></param>
        /// <returns></returns>
        bool CheckExitSurveyDetailsExistByName(ExitSurveyDetailsModel exitSurveyDetailsModel);

        /// <summary>
        /// Get Student Exit Survey Details By Id
        /// </summary>
        /// <param name="exitSurveyModel"></param>
        /// <returns></returns>
        ExitSurveyReportModel GetStudentExitSurveyById(ExitSurveyRequestModel exitSurveyModel);
    }
}