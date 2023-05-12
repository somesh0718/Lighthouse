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
    /// Expose all vtFieldIndustryVisitConducted WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VTFieldIndustryVisitConductedController : BaseController
    {
        private readonly IVTFieldIndustryVisitConductedManager vtFieldIndustryVisitConductedManager;

        /// <summary>
        /// Initializes the VTFieldIndustryVisitConducted controller class.
        /// </summary>
        /// <param name="_vtFieldIndustryVisitConductedManager"></param>
        public VTFieldIndustryVisitConductedController(IVTFieldIndustryVisitConductedManager _vtFieldIndustryVisitConductedManager)
        {
            this.vtFieldIndustryVisitConductedManager = _vtFieldIndustryVisitConductedManager;
        }

        /// <summary>
        /// Get list of vtFieldIndustryVisitConducted data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVTFieldIndustryVisitConducteds")]
        public async Task<ListResponse<VTFieldIndustryVisitConductedModel>> GetVTFieldIndustryVisitConducteds()
        {
            ListResponse<VTFieldIndustryVisitConductedModel> response = new ListResponse<VTFieldIndustryVisitConductedModel>();

            try
            {
                IQueryable<VTFieldIndustryVisitConductedModel> vtFieldIndustryVisitConductedModels = await Task.Run(() =>
                {
                    return this.vtFieldIndustryVisitConductedManager.GetVTFieldIndustryVisitConducteds();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtFieldIndustryVisitConductedModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VTFieldIndustryVisitConducted with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTFieldIndustryVisitConductedsByCriteria")]
        public async Task<ListResponse<VTFieldIndustryVisitConductedViewModel>> GetVTFieldIndustryVisitConductedsByCriteria([FromBody] SearchVTFieldIndustryVisitConductedModel searchModel)
        {
            ListResponse<VTFieldIndustryVisitConductedViewModel> response = new ListResponse<VTFieldIndustryVisitConductedViewModel>();

            try
            {
                var vtFieldIndustryVisitConductedModels = await Task.Run(() =>
                {
                    return this.vtFieldIndustryVisitConductedManager.GetVTFieldIndustryVisitConductedsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtFieldIndustryVisitConductedModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vtFieldIndustryVisitConducted data by name
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVTFieldIndustryVisitConductedsByName")]
        public async Task<ListResponse<VTFieldIndustryVisitConductedModel>> GetVTFieldIndustryVisitConductedsByName([FromQuery] string vtFieldIndustryVisitConductedName)
        {
            ListResponse<VTFieldIndustryVisitConductedModel> response = new ListResponse<VTFieldIndustryVisitConductedModel>();

            try
            {
                var vtFieldIndustryVisitConductedModels = await Task.Run(() =>
                {
                    return this.vtFieldIndustryVisitConductedManager.GetVTFieldIndustryVisitConductedsByName(vtFieldIndustryVisitConductedName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtFieldIndustryVisitConductedModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vtFieldIndustryVisitConducted data by Id
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTFieldIndustryVisitConductedById")]
        public async Task<SingularResponse<VTFieldIndustryVisitConductedModel>> GetVTFieldIndustryVisitConductedById([FromBody] DataRequest vtFieldIndustryVisitConductedRequest)
        {
            SingularResponse<VTFieldIndustryVisitConductedModel> response = new SingularResponse<VTFieldIndustryVisitConductedModel>();

            try
            {
                var vtFieldIndustryVisitConductedModel = await Task.Run(() =>
                {
                    return this.vtFieldIndustryVisitConductedManager.GetVTFieldIndustryVisitConductedById(Guid.Parse(vtFieldIndustryVisitConductedRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vtFieldIndustryVisitConductedModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vtFieldIndustryVisitConducted
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVTFieldIndustryVisitConducted"), Route("CreateOrUpdateVTFieldIndustryVisitConductedDetails")]
        public async Task<SingularResponse<string>> CreateVTFieldIndustryVisitConducted([FromBody] VTFieldIndustryVisitConductedRequest vtFieldIndustryVisitConductedRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vtFieldIndustryVisitConductedRequest.RequestType = RequestType.New;
                    return this.vtFieldIndustryVisitConductedManager.SaveOrUpdateVTFieldIndustryVisitConductedDetails(vtFieldIndustryVisitConductedRequest);
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
        /// Update vtFieldIndustryVisitConducted by Id
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVTFieldIndustryVisitConducted")]
        public async Task<SingularResponse<string>> UpdateVTFieldIndustryVisitConducted([FromBody] VTFieldIndustryVisitConductedRequest vtFieldIndustryVisitConductedRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vtFieldIndustryVisitConductedRequest.RequestType = RequestType.Edit;
                    return this.vtFieldIndustryVisitConductedManager.SaveOrUpdateVTFieldIndustryVisitConductedDetails(vtFieldIndustryVisitConductedRequest);
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
        /// Delete vtFieldIndustryVisitConducted by Id
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVTFieldIndustryVisitConductedById")]
        public async Task<SingularResponse<bool>> DeleteVTFieldIndustryVisitConductedById([FromBody] DeleteRequest<Guid> vtFieldIndustryVisitConductedRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vtFieldIndustryVisitConductedManager.DeleteById(vtFieldIndustryVisitConductedRequest.DataId);
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
        /// Approved VT Field Industry Visit Conducted
        /// </summary>
        /// <param name="vtApprovalRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("ApprovedVTFieldIndustry")]
        public async Task<SingularResponse<string>> ApprovedVTFieldIndustry([FromBody] VTFieldIndustryApprovalRequest vtApprovalRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    return this.vtFieldIndustryVisitConductedManager.ApprovedVTFieldIndustry(vtApprovalRequest);
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