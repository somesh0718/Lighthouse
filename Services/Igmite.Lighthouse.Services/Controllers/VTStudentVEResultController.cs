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
    /// Expose all vtStudentVEResult WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VTStudentVEResultController : BaseController
    {
        private readonly IVTStudentVEResultManager vtStudentVEResultManager;

        /// <summary>
        /// Initializes the VTStudentVEResult controller class.
        /// </summary>
        /// <param name="_vtStudentVEResultManager"></param>
        public VTStudentVEResultController(IVTStudentVEResultManager _vtStudentVEResultManager)
        {
            this.vtStudentVEResultManager = _vtStudentVEResultManager;
        }

        /// <summary>
        /// Get list of vtStudentVEResult data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVTStudentVEResults")]
        public async Task<ListResponse<VTStudentVEResultModel>> GetVTStudentVEResults()
        {
            ListResponse<VTStudentVEResultModel> response = new ListResponse<VTStudentVEResultModel>();

            try
            {
                IQueryable<VTStudentVEResultModel> vtStudentVEResultModels = await Task.Run(() =>
                {
                    return this.vtStudentVEResultManager.GetVTStudentVEResults();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtStudentVEResultModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VTStudentVEResult with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTStudentVEResultsByCriteria")]
        public async Task<ListResponse<VTStudentVEResultViewModel>> GetVTStudentVEResultsByCriteria([FromBody] SearchVTStudentVEResultModel searchModel)
        {
            ListResponse<VTStudentVEResultViewModel> response = new ListResponse<VTStudentVEResultViewModel>();

            try
            {
                var vtStudentVEResultModels = await Task.Run(() =>
                {
                    return this.vtStudentVEResultManager.GetVTStudentVEResultsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtStudentVEResultModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vtStudentVEResult data by name
        /// </summary>
        /// <param name="vtStudentVEResultName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVTStudentVEResultsByName")]
        public async Task<ListResponse<VTStudentVEResultModel>> GetVTStudentVEResultsByName([FromQuery] string vtStudentVEResultName)
        {
            ListResponse<VTStudentVEResultModel> response = new ListResponse<VTStudentVEResultModel>();

            try
            {
                var vtStudentVEResultModels = await Task.Run(() =>
                {
                    return this.vtStudentVEResultManager.GetVTStudentVEResultsByName(vtStudentVEResultName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtStudentVEResultModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vtStudentVEResult data by Id
        /// </summary>
        /// <param name="vtStudentVEResultId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTStudentVEResultById")]
        public async Task<SingularResponse<VTStudentVEResultModel>> GetVTStudentVEResultById([FromBody] DataRequest vtStudentVEResultRequest)
        {
            SingularResponse<VTStudentVEResultModel> response = new SingularResponse<VTStudentVEResultModel>();

            try
            {
                var vtStudentVEResultModel = await Task.Run(() =>
                {
                    return this.vtStudentVEResultManager.GetVTStudentVEResultById(Guid.Parse(vtStudentVEResultRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vtStudentVEResultModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vtStudentVEResult
        /// </summary>
        /// <param name="vtStudentVEResultRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVTStudentVEResult"), Route("CreateOrUpdateVTStudentVEResultDetails")]
        public async Task<SingularResponse<string>> CreateVTStudentVEResult([FromBody] VTStudentVEResultRequest vtStudentVEResultRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vtStudentVEResultRequest.RequestType = RequestType.New;
                    return this.vtStudentVEResultManager.SaveOrUpdateVTStudentVEResultDetails(vtStudentVEResultRequest);
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
        /// Update vtStudentVEResult by Id
        /// </summary>
        /// <param name="vtStudentVEResultRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVTStudentVEResult")]
        public async Task<SingularResponse<string>> UpdateVTStudentVEResult([FromBody] VTStudentVEResultRequest vtStudentVEResultRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vtStudentVEResultRequest.RequestType = RequestType.Edit;
                    return this.vtStudentVEResultManager.SaveOrUpdateVTStudentVEResultDetails(vtStudentVEResultRequest);
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
        /// Delete vtStudentVEResult by Id
        /// </summary>
        /// <param name="vtStudentVEResultRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVTStudentVEResultById")]
        public async Task<SingularResponse<bool>> DeleteVTStudentVEResultById([FromBody] DeleteRequest<Guid> vtStudentVEResultRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vtStudentVEResultManager.DeleteById(vtStudentVEResultRequest.DataId);
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