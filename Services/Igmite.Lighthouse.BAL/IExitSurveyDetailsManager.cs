using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the ExitSurveyDetails entity
    /// </summary>
    public interface IExitSurveyDetailsManager : IGenericManager<ExitSurveyDetailsModel>
    {
        /// <summary>
        /// Get list of ExitSurveyDetailses
        /// </summary>
        /// <returns></returns>
        IQueryable<ExitSurveyDetailsModel> GetExitSurveyDetails();

        /// <summary>
        /// Get list of ExitSurveyDetailses by name
        /// </summary>
        /// <param name="exitStudentId"></param>
        /// <returns></returns>
        // IQueryable<ExitSurveyDetailsModel> GetExitSurveyDetailsByStudentId(Guid exitStudentId);
        ExitSurveyDetailsModel GetExitSurveyDetailsByStudentId(Guid exitStudentId);

        /// <summary>
        /// Get ExitSurveyDetails by StudentId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ExitSurveyDetailsModel GetExitSurveyDetailsById(int id);

        /// <summary>
        /// Get ExitSurveyDetails by StudentId using async
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<ExitSurveyDetailsModel> GetExitSurveyDetailsByIdAsync(int studentId);

        /// <summary>
        /// Insert/Update ExitSurveyDetails entity
        /// </summary>
        /// <param name="exitSurveyDetailsModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateExitSurveyDetailsDetails(ExitSurveyDetailsModel exitSurveyDetailsModel);

        /// <summary>
        /// Delete a record by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        bool DeleteById(Guid Id);

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