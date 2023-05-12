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
    /// Expose all vtGuestLectureConducted WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VTGuestLectureConductedController : BaseController
    {
        private readonly IVTGuestLectureConductedManager vtGuestLectureConductedManager;

        /// <summary>
        /// Initializes the VTGuestLectureConducted controller class.
        /// </summary>
        /// <param name="_vtGuestLectureConductedManager"></param>
        public VTGuestLectureConductedController(IVTGuestLectureConductedManager _vtGuestLectureConductedManager)
        {
            this.vtGuestLectureConductedManager = _vtGuestLectureConductedManager;
        }

        /// <summary>
        /// Get list of vtGuestLectureConducted data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVTGuestLectureConducteds")]
        public async Task<ListResponse<VTGuestLectureConductedModel>> GetVTGuestLectureConducteds()
        {
            ListResponse<VTGuestLectureConductedModel> response = new ListResponse<VTGuestLectureConductedModel>();

            try
            {
                IQueryable<VTGuestLectureConductedModel> vtGuestLectureConductedModels = await Task.Run(() =>
                {
                    return this.vtGuestLectureConductedManager.GetVTGuestLectureConducteds();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtGuestLectureConductedModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VTGuestLectureConducted with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTGuestLectureConductedsByCriteria")]
        public async Task<ListResponse<VTGuestLectureConductedViewModel>> GetVTGuestLectureConductedsByCriteria([FromBody] SearchVTGuestLectureConductedModel searchModel)
        {
            ListResponse<VTGuestLectureConductedViewModel> response = new ListResponse<VTGuestLectureConductedViewModel>();

            try
            {
                var vtGuestLectureConductedModels = await Task.Run(() =>
                {
                    return this.vtGuestLectureConductedManager.GetVTGuestLectureConductedsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtGuestLectureConductedModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vtGuestLectureConducted data by name
        /// </summary>
        /// <param name="vtGuestLectureConductedName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVTGuestLectureConductedsByName")]
        public async Task<ListResponse<VTGuestLectureConductedModel>> GetVTGuestLectureConductedsByName([FromQuery] string vtGuestLectureConductedName)
        {
            ListResponse<VTGuestLectureConductedModel> response = new ListResponse<VTGuestLectureConductedModel>();

            try
            {
                var vtGuestLectureConductedModels = await Task.Run(() =>
                {
                    return this.vtGuestLectureConductedManager.GetVTGuestLectureConductedsByName(vtGuestLectureConductedName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtGuestLectureConductedModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vtGuestLectureConducted data by Id
        /// </summary>
        /// <param name="vtGuestLectureId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTGuestLectureConductedById")]
        public async Task<SingularResponse<VTGuestLectureConductedModel>> GetVTGuestLectureConductedById([FromBody] DataRequest vtGuestLectureRequest)
        {
            SingularResponse<VTGuestLectureConductedModel> response = new SingularResponse<VTGuestLectureConductedModel>();

            try
            {
                var vtGuestLectureConductedModel = await Task.Run(() =>
                {
                    return this.vtGuestLectureConductedManager.GetVTGuestLectureConductedById(Guid.Parse(vtGuestLectureRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vtGuestLectureConductedModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vtGuestLectureConducted
        /// </summary>
        /// <param name="vtGuestLectureConductedRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVTGuestLectureConducted"), Route("CreateOrUpdateVTGuestLectureConductedDetails")]
        public async Task<SingularResponse<string>> CreateVTGuestLectureConducted([FromBody] VTGuestLectureConductedRequest vtGuestLectureConductedRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vtGuestLectureConductedRequest.RequestType = RequestType.New;
                    return this.vtGuestLectureConductedManager.SaveOrUpdateVTGuestLectureConductedDetails(vtGuestLectureConductedRequest);
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
        /// Update vtGuestLectureConducted by Id
        /// </summary>
        /// <param name="vtGuestLectureConductedRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVTGuestLectureConducted")]
        public async Task<SingularResponse<string>> UpdateVTGuestLectureConducted([FromBody] VTGuestLectureConductedRequest vtGuestLectureConductedRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vtGuestLectureConductedRequest.RequestType = RequestType.Edit;
                    return this.vtGuestLectureConductedManager.SaveOrUpdateVTGuestLectureConductedDetails(vtGuestLectureConductedRequest);
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
        /// Delete vtGuestLectureConducted by Id
        /// </summary>
        /// <param name="vtGuestLectureConductedRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVTGuestLectureConductedById")]
        public async Task<SingularResponse<bool>> DeleteVTGuestLectureConductedById([FromBody] DeleteRequest<Guid> vtGuestLectureConductedRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vtGuestLectureConductedManager.DeleteById(vtGuestLectureConductedRequest.DataId);
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

        /// <summary>
        /// Approved VT Guest Lecture Conducted
        /// </summary>
        /// <param name="vtApprovalRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("ApprovedVTGuestLectureConducted")]
        public async Task<SingularResponse<string>> ApprovedVTGuestLectureConducted([FromBody] VTGuestLectureApprovalRequest vtApprovalRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    return this.vtGuestLectureConductedManager.ApprovedVTGuestLectureConducted(vtApprovalRequest);
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
    }
}