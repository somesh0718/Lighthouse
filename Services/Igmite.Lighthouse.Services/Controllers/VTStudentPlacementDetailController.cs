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
    /// Expose all vtStudentPlacementDetail WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VTStudentPlacementDetailController : BaseController
    {
        private readonly IVTStudentPlacementDetailManager vtStudentPlacementDetailManager;

        /// <summary>
        /// Initializes the VTStudentPlacementDetail controller class.
        /// </summary>
        /// <param name="_vtStudentPlacementDetailManager"></param>
        public VTStudentPlacementDetailController(IVTStudentPlacementDetailManager _vtStudentPlacementDetailManager)
        {
            this.vtStudentPlacementDetailManager = _vtStudentPlacementDetailManager;
        }

        /// <summary>
        /// Get list of vtStudentPlacementDetail data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVTStudentPlacementDetails")]
        public async Task<ListResponse<VTStudentPlacementDetailModel>> GetVTStudentPlacementDetails()
        {
            ListResponse<VTStudentPlacementDetailModel> response = new ListResponse<VTStudentPlacementDetailModel>();

            try
            {
                IQueryable<VTStudentPlacementDetailModel> vtStudentPlacementDetailModels = await Task.Run(() =>
                {
                    return this.vtStudentPlacementDetailManager.GetVTStudentPlacementDetails();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtStudentPlacementDetailModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
                
            }

            return response;
        }

        /// <summary>
        /// List of VTStudentPlacementDetail with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTStudentPlacementDetailsByCriteria")]
        public async Task<ListResponse<VTStudentPlacementDetailViewModel>> GetVTStudentPlacementDetailsByCriteria([FromBody] SearchVTStudentPlacementDetailModel searchModel)
        {
            ListResponse<VTStudentPlacementDetailViewModel> response = new ListResponse<VTStudentPlacementDetailViewModel>();

            try
            {
                var vtStudentPlacementDetailModels = await Task.Run(() =>
                {
                    return this.vtStudentPlacementDetailManager.GetVTStudentPlacementDetailsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtStudentPlacementDetailModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
                
            }

            return response;
        }

        /// <summary>
        /// Get list of vtStudentPlacementDetail data by name
        /// </summary>
        /// <param name="vtStudentPlacementDetailName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVTStudentPlacementDetailsByName")]
        public async Task<ListResponse<VTStudentPlacementDetailModel>> GetVTStudentPlacementDetailsByName([FromQuery] string vtStudentPlacementDetailName)
        {
            ListResponse<VTStudentPlacementDetailModel> response = new ListResponse<VTStudentPlacementDetailModel>();

            try
            {
                var vtStudentPlacementDetailModels = await Task.Run(() =>
                {
                    return this.vtStudentPlacementDetailManager.GetVTStudentPlacementDetailsByName(vtStudentPlacementDetailName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtStudentPlacementDetailModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
                
            }

            return response;
        }

        /// <summary>
        /// Get vtStudentPlacementDetail data by Id
        /// </summary>
        /// <param name="vtStudentPlacementDetailId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTStudentPlacementDetailById")]
        public async Task<SingularResponse<VTStudentPlacementDetailModel>> GetVTStudentPlacementDetailById([FromBody] DataRequest vtStudentPlacementDetailRequest)
        {
            SingularResponse<VTStudentPlacementDetailModel> response = new SingularResponse<VTStudentPlacementDetailModel>();

            try
            {
                var vtStudentPlacementDetailModel = await Task.Run(() =>
                {
                    return this.vtStudentPlacementDetailManager.GetVTStudentPlacementDetailById(Guid.Parse(vtStudentPlacementDetailRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vtStudentPlacementDetailModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
                
            }

            return response;
        }

        /// <summary>
        /// Create new vtStudentPlacementDetail
        /// </summary>
        /// <param name="vtStudentPlacementDetailRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVTStudentPlacementDetail"), Route("CreateOrUpdateVTStudentPlacementDetailDetails")]
        public async Task<SingularResponse<string>> CreateVTStudentPlacementDetail([FromBody] VTStudentPlacementDetailRequest vtStudentPlacementDetailRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vtStudentPlacementDetailRequest.RequestType = RequestType.New;
                    return this.vtStudentPlacementDetailManager.SaveOrUpdateVTStudentPlacementDetailDetails(vtStudentPlacementDetailRequest);
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
        /// Update vtStudentPlacementDetail by Id
        /// </summary>
        /// <param name="vtStudentPlacementDetailRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVTStudentPlacementDetail")]
        public async Task<SingularResponse<string>> UpdateVTStudentPlacementDetail([FromBody] VTStudentPlacementDetailRequest vtStudentPlacementDetailRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vtStudentPlacementDetailRequest.RequestType = RequestType.Edit;
                    return this.vtStudentPlacementDetailManager.SaveOrUpdateVTStudentPlacementDetailDetails(vtStudentPlacementDetailRequest);
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
        /// Delete vtStudentPlacementDetail by Id
        /// </summary>
        /// <param name="vtStudentPlacementDetailRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVTStudentPlacementDetailById")]
        public async Task<SingularResponse<bool>> DeleteVTStudentPlacementDetailById([FromBody] DeleteRequest<Guid> vtStudentPlacementDetailRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vtStudentPlacementDetailManager.DeleteById(vtStudentPlacementDetailRequest.DataId);
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