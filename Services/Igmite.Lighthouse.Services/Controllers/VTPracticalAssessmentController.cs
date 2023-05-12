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
    /// Expose all vtPracticalAssessment WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VTPracticalAssessmentController : BaseController
    {
        private readonly IVTPracticalAssessmentManager vtPracticalAssessmentManager;

        /// <summary>
        /// Initializes the VTPracticalAssessment controller class.
        /// </summary>
        /// <param name="_vtPracticalAssessmentManager"></param>
        public VTPracticalAssessmentController(IVTPracticalAssessmentManager _vtPracticalAssessmentManager)
        {
            this.vtPracticalAssessmentManager = _vtPracticalAssessmentManager;
        }

        /// <summary>
        /// Get list of vtPracticalAssessment data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVTPracticalAssessments")]
        public async Task<ListResponse<VTPracticalAssessmentModel>> GetVTPracticalAssessments()
        {
            ListResponse<VTPracticalAssessmentModel> response = new ListResponse<VTPracticalAssessmentModel>();

            try
            {
                IQueryable<VTPracticalAssessmentModel> vtPracticalAssessmentModels = await Task.Run(() =>
                {
                    return this.vtPracticalAssessmentManager.GetVTPracticalAssessments();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtPracticalAssessmentModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VTPracticalAssessment with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTPracticalAssessmentsByCriteria")]
        public async Task<ListResponse<VTPracticalAssessmentViewModel>> GetVTPracticalAssessmentsByCriteria([FromBody] SearchVTPracticalAssessmentModel searchModel)
        {
            ListResponse<VTPracticalAssessmentViewModel> response = new ListResponse<VTPracticalAssessmentViewModel>();

            try
            {
                var vtPracticalAssessmentModels = await Task.Run(() =>
                {
                    return this.vtPracticalAssessmentManager.GetVTPracticalAssessmentsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtPracticalAssessmentModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vtPracticalAssessment data by name
        /// </summary>
        /// <param name="vtPracticalAssessmentName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVTPracticalAssessmentsByName")]
        public async Task<ListResponse<VTPracticalAssessmentModel>> GetVTPracticalAssessmentsByName([FromQuery] string vtPracticalAssessmentName)
        {
            ListResponse<VTPracticalAssessmentModel> response = new ListResponse<VTPracticalAssessmentModel>();

            try
            {
                var vtPracticalAssessmentModels = await Task.Run(() =>
                {
                    return this.vtPracticalAssessmentManager.GetVTPracticalAssessmentsByName(vtPracticalAssessmentName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtPracticalAssessmentModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vtPracticalAssessment data by Id
        /// </summary>
        /// <param name="vtPracticalAssessmentId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTPracticalAssessmentById")]
        public async Task<SingularResponse<VTPracticalAssessmentModel>> GetVTPracticalAssessmentById([FromBody] DataRequest vtPracticalAssessmentRequest)
        {
            SingularResponse<VTPracticalAssessmentModel> response = new SingularResponse<VTPracticalAssessmentModel>();

            try
            {
                var vtPracticalAssessmentModel = await Task.Run(() =>
                {
                    return this.vtPracticalAssessmentManager.GetVTPracticalAssessmentById(Guid.Parse(vtPracticalAssessmentRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vtPracticalAssessmentModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vtPracticalAssessment
        /// </summary>
        /// <param name="vtPracticalAssessmentRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVTPracticalAssessment"), Route("CreateOrUpdateVTPracticalAssessmentDetails")]
        public async Task<SingularResponse<string>> CreateVTPracticalAssessment([FromBody] VTPracticalAssessmentRequest vtPracticalAssessmentRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vtPracticalAssessmentRequest.RequestType = RequestType.New;
                    return this.vtPracticalAssessmentManager.SaveOrUpdateVTPracticalAssessmentDetails(vtPracticalAssessmentRequest);
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
        /// Update vtPracticalAssessment by Id
        /// </summary>
        /// <param name="vtPracticalAssessmentRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVTPracticalAssessment")]
        public async Task<SingularResponse<string>> UpdateVTPracticalAssessment([FromBody] VTPracticalAssessmentRequest vtPracticalAssessmentRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vtPracticalAssessmentRequest.RequestType = RequestType.Edit;
                    return this.vtPracticalAssessmentManager.SaveOrUpdateVTPracticalAssessmentDetails(vtPracticalAssessmentRequest);
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
        /// Delete vtPracticalAssessment by Id
        /// </summary>
        /// <param name="vtPracticalAssessmentRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVTPracticalAssessmentById")]
        public async Task<SingularResponse<bool>> DeleteVTPracticalAssessmentById([FromBody] DeleteRequest<Guid> vtPracticalAssessmentRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vtPracticalAssessmentManager.DeleteById(vtPracticalAssessmentRequest.DataId);
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