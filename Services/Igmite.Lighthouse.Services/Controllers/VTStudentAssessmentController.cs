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
    /// Expose all vtStudentAssessment WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VTStudentAssessmentController : BaseController
    {
        private readonly IVTStudentAssessmentManager vtStudentAssessmentManager;

        /// <summary>
        /// Initializes the VTStudentAssessment controller class.
        /// </summary>
        /// <param name="_vtStudentAssessmentManager"></param>
        public VTStudentAssessmentController(IVTStudentAssessmentManager _vtStudentAssessmentManager)
        {
            this.vtStudentAssessmentManager = _vtStudentAssessmentManager;
        }

        /// <summary>
        /// Get list of vtStudentAssessment data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVTStudentAssessments")]
        public async Task<ListResponse<VTStudentAssessmentModel>> GetVTStudentAssessments()
        {
            ListResponse<VTStudentAssessmentModel> response = new ListResponse<VTStudentAssessmentModel>();

            try
            {
                IQueryable<VTStudentAssessmentModel> vtStudentAssessmentModels = await Task.Run(() =>
                {
                    return this.vtStudentAssessmentManager.GetVTStudentAssessments();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtStudentAssessmentModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VTStudentAssessment with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTStudentAssessmentsByCriteria")]
        public async Task<ListResponse<VTStudentAssessmentViewModel>> GetVTStudentAssessmentsByCriteria([FromBody] SearchVTStudentAssessmentModel searchModel)
        {
            ListResponse<VTStudentAssessmentViewModel> response = new ListResponse<VTStudentAssessmentViewModel>();

            try
            {
                var vtStudentAssessmentModels = await Task.Run(() =>
                {
                    return this.vtStudentAssessmentManager.GetVTStudentAssessmentsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtStudentAssessmentModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vtStudentAssessment data by name
        /// </summary>
        /// <param name="vtStudentAssessmentName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVTStudentAssessmentsByName")]
        public async Task<ListResponse<VTStudentAssessmentModel>> GetVTStudentAssessmentsByName([FromQuery] string vtStudentAssessmentName)
        {
            ListResponse<VTStudentAssessmentModel> response = new ListResponse<VTStudentAssessmentModel>();

            try
            {
                var vtStudentAssessmentModels = await Task.Run(() =>
                {
                    return this.vtStudentAssessmentManager.GetVTStudentAssessmentsByName(vtStudentAssessmentName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtStudentAssessmentModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vtStudentAssessment data by Id
        /// </summary>
        /// <param name="vtStudentAssessmentId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTStudentAssessmentById")]
        public async Task<SingularResponse<VTStudentAssessmentModel>> GetVTStudentAssessmentById([FromBody] DataRequest vtStudentAssessmentRequest)
        {
            SingularResponse<VTStudentAssessmentModel> response = new SingularResponse<VTStudentAssessmentModel>();

            try
            {
                var vtStudentAssessmentModel = await Task.Run(() =>
                {
                    return this.vtStudentAssessmentManager.GetVTStudentAssessmentById(Guid.Parse(vtStudentAssessmentRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vtStudentAssessmentModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vtStudentAssessment
        /// </summary>
        /// <param name="vtStudentAssessmentRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVTStudentAssessment"), Route("CreateOrUpdateVTStudentAssessmentDetails")]
        public async Task<SingularResponse<string>> CreateVTStudentAssessment([FromBody] VTStudentAssessmentRequest vtStudentAssessmentRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vtStudentAssessmentRequest.RequestType = RequestType.New;
                    return this.vtStudentAssessmentManager.SaveOrUpdateVTStudentAssessmentDetails(vtStudentAssessmentRequest);
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
        /// Update vtStudentAssessment by Id
        /// </summary>
        /// <param name="vtStudentAssessmentRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVTStudentAssessment")]
        public async Task<SingularResponse<string>> UpdateVTStudentAssessment([FromBody] VTStudentAssessmentRequest vtStudentAssessmentRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vtStudentAssessmentRequest.RequestType = RequestType.Edit;
                    return this.vtStudentAssessmentManager.SaveOrUpdateVTStudentAssessmentDetails(vtStudentAssessmentRequest);
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
        /// Delete vtStudentAssessment by Id
        /// </summary>
        /// <param name="vtStudentAssessmentRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVTStudentAssessmentById")]
        public async Task<SingularResponse<bool>> DeleteVTStudentAssessmentById([FromBody] DeleteRequest<Guid> vtStudentAssessmentRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vtStudentAssessmentManager.DeleteById(vtStudentAssessmentRequest.DataId);
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