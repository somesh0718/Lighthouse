using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.Controllers
{
    /// <summary>
    /// Expose all exitSurveyDetails WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class ExitSurveyDetailsController : BaseController
    {
        private readonly IExitSurveyDetailsManager exitSurveyDetailsManager;

        /// <summary>
        /// Initializes the ExitSurveyDetails controller class.
        /// </summary>
        /// <param name="_exitSurveyDetailsManager"></param>
        public ExitSurveyDetailsController(IExitSurveyDetailsManager _exitSurveyDetailsManager)
        {
            this.exitSurveyDetailsManager = _exitSurveyDetailsManager;
        }

        /// <summary>
        /// Get list of exitSurveyDetails data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetExitSurveyDetails")]
        public async Task<ListResponse<ExitSurveyDetailsModel>> GetExitSurveyDetails()
        {
            ListResponse<ExitSurveyDetailsModel> response = new ListResponse<ExitSurveyDetailsModel>();

            try
            {
                IQueryable<ExitSurveyDetailsModel> exitSurveyDetailsModels = await Task.Run(() =>
                {
                    return this.exitSurveyDetailsManager.GetExitSurveyDetails();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = exitSurveyDetailsModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of exitSurveyDetails data by name
        /// </summary>
        /// <param name="exitSurveyDetailsName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetExitSurveyDetailsByStudentId")]
        public async Task<SingularResponse<ExitSurveyDetailsModel>> GetExitSurveyDetailsByStudentId([FromQuery] Guid exitStudentId)
        {
            SingularResponse<ExitSurveyDetailsModel> response = new SingularResponse<ExitSurveyDetailsModel>();

            try
            {
                var exitSurveyDetailsModels = await Task.Run(() =>
                {
                    return this.exitSurveyDetailsManager.GetExitSurveyDetailsByStudentId(exitStudentId);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = exitSurveyDetailsModels;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get exitSurveyDetails data by Id
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetExitSurveyDetailsById")]
        public async Task<SingularResponse<ExitSurveyReportModel>> GetExitSurveyDetailsById([FromBody] ExitSurveyRequestModel exitSurveyRequest)
        {
            SingularResponse<ExitSurveyReportModel> response = new SingularResponse<ExitSurveyReportModel>();

            try
            {
                var exitSurveyDetailsModel = await Task.Run(() =>
                {
                    return this.exitSurveyDetailsManager.GetStudentExitSurveyById(exitSurveyRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = exitSurveyDetailsModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new exitSurveyDetails
        /// </summary>
        /// <param name="exitSurveyDetailsRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateExitSurveyDetails")]
        public async Task<SingularResponse<string>> CreateExitSurveyDetails([FromBody] ExitSurveyDetailsModel exitSurveyDetailsRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //exitSurveyDetailsRequest.RequestType = RequestType.New;
                    return this.exitSurveyDetailsManager.SaveOrUpdateExitSurveyDetailsDetails(exitSurveyDetailsRequest);
                });

                if (response.Errors.Count == 0)
                {
                    response.Messages.Add(Constants.CreatedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Update exitSurveyDetails by Id
        /// </summary>
        /// <param name="exitSurveyDetailsRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateExitSurveyDetails")]
        public async Task<SingularResponse<string>> UpdateExitSurveyDetails([FromBody] ExitSurveyDetailsModel exitSurveyDetailsRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    exitSurveyDetailsRequest.RequestType = RequestType.Edit;
                    return this.exitSurveyDetailsManager.SaveOrUpdateExitSurveyDetailsDetails(exitSurveyDetailsRequest);
                });

                if (response.Errors.Count == 0)
                {
                    response.Messages.Add(Constants.UpdatedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Delete exitSurveyDetails by Id
        /// </summary>
        /// <param name="exitSurveyDetailsRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteExitSurveyDetailsById")]
        public async Task<SingularResponse<bool>> DeleteExitSurveyDetailsById([FromBody] ExitSurveyDetailsModel exitSurveyDetailsRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.exitSurveyDetailsManager.DeleteById(exitSurveyDetailsRequest.ExitStudentId);
                });

                if (response.Result)
                {
                    response.Messages.Add(Constants.DeletedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }
    }
}