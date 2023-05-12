using Igmite.Lighthouse.BAL.Validations;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Mappers;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Manager of the ExitSurveyDetails entity
    /// </summary>
    public class ExitSurveyDetailsManager : GenericManager<ExitSurveyDetailsModel>, IExitSurveyDetailsManager
    {
        private readonly IExitSurveyDetailsRepository exitSurveyDetailsRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the exitSurveyDetails manager.
        /// </summary>
        /// <param name="exitSurveyDetailsRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public ExitSurveyDetailsManager(IExitSurveyDetailsRepository _exitSurveyDetailsRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository)
        {
            this.exitSurveyDetailsRepository = _exitSurveyDetailsRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
        }

        /// <summary>
        /// Get list of ExitSurveyDetailses
        /// </summary>
        /// <returns></returns>
        public IQueryable<ExitSurveyDetailsModel> GetExitSurveyDetails()
        {
            var exitSurveyDetailses = this.exitSurveyDetailsRepository.GetExitSurveyDetails();

            IList<ExitSurveyDetailsModel> exitSurveyDetailsModels = new List<ExitSurveyDetailsModel>();
            exitSurveyDetailses.ForEach((user) => exitSurveyDetailsModels.Add(user.ToModel()));

            return exitSurveyDetailsModels.AsQueryable();
        }

        /// <summary>
        /// Get list of ExitSurveyDetailses by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ExitSurveyDetailsModel GetExitSurveyDetailsByStudentId(Guid exitStudentId)
        {
            var exitSurveyDetails = this.exitSurveyDetailsRepository.GetExitSurveyDetailsByStudentId(exitStudentId);

            //IList<ExitSurveyDetailsModel> exitSurveyDetailsModels = new List<ExitSurveyDetailsModel>();
            // exitSurveyDetailses.ForEach((user) => exitSurveyDetailsModels.Add(user.ToModel()));

            return (exitSurveyDetails != null) ? exitSurveyDetails.ToModel() : null;
            //return exitSurveyDetailsModels.AsQueryable();
        }

        /// <summary>
        /// Get ExitSurveyDetails by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public ExitSurveyDetailsModel GetExitSurveyDetailsById(int id)
        {
            ExitSurveyDetails exitSurveyDetails = this.exitSurveyDetailsRepository.GetExitSurveyDetailsById(id);

            return (exitSurveyDetails != null) ? exitSurveyDetails.ToModel() : null;
        }

        /// <summary>
        /// Get ExitSurveyDetails by StudentId using async
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<ExitSurveyDetailsModel> GetExitSurveyDetailsByIdAsync(int studentId)
        {
            var exitSurveyDetails = await this.exitSurveyDetailsRepository.GetExitSurveyDetailsByIdAsync(studentId);

            return (exitSurveyDetails != null) ? exitSurveyDetails.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update ExitSurveyDetails entity
        /// </summary>
        /// <param name="exitSurveyDetailsModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateExitSurveyDetailsDetails(ExitSurveyDetailsModel exitSurveyDetailsModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                ExitSurveyDetails exitSurveyDetails = null;

                //Validate model data
                exitSurveyDetailsModel = exitSurveyDetailsModel.GetModelValidationErrors<ExitSurveyDetailsModel>();

                if (exitSurveyDetailsModel.ErrorMessages.Count > 0)
                {
                    response.Errors = exitSurveyDetailsModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                exitSurveyDetails = this.exitSurveyDetailsRepository.GetExitSurveyDetailsById(exitSurveyDetailsModel.AcademicYearId, exitSurveyDetailsModel.ExitStudentId);

                if (exitSurveyDetails == null)
                {
                    exitSurveyDetails = new ExitSurveyDetails();
                    exitSurveyDetails.RequestType = RequestType.New;
                }
                else
                {
                    exitSurveyDetails.RequestType = RequestType.Edit;
                }

                if (response.Errors.Count == 0)
                {
                    exitSurveyDetails.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                    exitSurveyDetails = exitSurveyDetailsModel.FromModel(exitSurveyDetails);

                    //Save Or Update exitSurveyDetails details
                    bool isSaved = this.exitSurveyDetailsRepository.SaveOrUpdateExitSurveyDetailsDetails(exitSurveyDetails);

                    response.Result = isSaved ? "Success" : "Failed";
                }
                else
                {
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("BAL > SaveOrUpdateExitSurveyDetailsDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by StudentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid Id)
        {
            string authUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

            return this.exitSurveyDetailsRepository.DeleteById(Id);
        }

        /// <summary>
        /// Check duplicate ExitSurveyDetails by Name
        /// </summary>
        /// <param name="exitSurveyDetailsModel"></param>
        /// <returns></returns>
        public bool CheckExitSurveyDetailsExistByName(ExitSurveyDetailsModel exitSurveyDetailsModel)
        {
            return this.exitSurveyDetailsRepository.CheckExitSurveyDetailsExistByName(exitSurveyDetailsModel);
        }

        /// <summary>
        /// Get Student Exit Survey Details By Id
        /// </summary>
        /// <param name="exitSurveyModel"></param>
        /// <returns></returns>
        public ExitSurveyReportModel GetStudentExitSurveyById(ExitSurveyRequestModel exitSurveyModel)
        {
            return this.exitSurveyDetailsRepository.GetStudentExitSurveyById(exitSurveyModel);
        }
    }
}