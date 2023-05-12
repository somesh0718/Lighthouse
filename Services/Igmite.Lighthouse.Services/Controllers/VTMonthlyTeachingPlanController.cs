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
    /// Expose all vtMonthlyTeachingPlan WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VTMonthlyTeachingPlanController : BaseController
    {
        private readonly IVTMonthlyTeachingPlanManager vtMonthlyTeachingPlanManager;

        /// <summary>
        /// Initializes the VTMonthlyTeachingPlan controller class.
        /// </summary>
        /// <param name="_vtMonthlyTeachingPlanManager"></param>
        public VTMonthlyTeachingPlanController(IVTMonthlyTeachingPlanManager _vtMonthlyTeachingPlanManager)
        {
            this.vtMonthlyTeachingPlanManager = _vtMonthlyTeachingPlanManager;
        }

        /// <summary>
        /// Get list of vtMonthlyTeachingPlan data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVTMonthlyTeachingPlans")]
        public async Task<ListResponse<VTMonthlyTeachingPlanModel>> GetVTMonthlyTeachingPlans()
        {
            ListResponse<VTMonthlyTeachingPlanModel> response = new ListResponse<VTMonthlyTeachingPlanModel>();

            try
            {
                IQueryable<VTMonthlyTeachingPlanModel> vtMonthlyTeachingPlanModels = await Task.Run(() =>
                {
                    return this.vtMonthlyTeachingPlanManager.GetVTMonthlyTeachingPlans();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtMonthlyTeachingPlanModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VTMonthlyTeachingPlan with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTMonthlyTeachingPlansByCriteria")]
        public async Task<ListResponse<VTMonthlyTeachingPlanViewModel>> GetVTMonthlyTeachingPlansByCriteria([FromBody] SearchVTMonthlyTeachingPlanModel searchModel)
        {
            ListResponse<VTMonthlyTeachingPlanViewModel> response = new ListResponse<VTMonthlyTeachingPlanViewModel>();

            try
            {
                var vtMonthlyTeachingPlanModels = await Task.Run(() =>
                {
                    return this.vtMonthlyTeachingPlanManager.GetVTMonthlyTeachingPlansByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtMonthlyTeachingPlanModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vtMonthlyTeachingPlan data by name
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVTMonthlyTeachingPlansByName")]
        public async Task<ListResponse<VTMonthlyTeachingPlanModel>> GetVTMonthlyTeachingPlansByName([FromQuery] string vtMonthlyTeachingPlanName)
        {
            ListResponse<VTMonthlyTeachingPlanModel> response = new ListResponse<VTMonthlyTeachingPlanModel>();

            try
            {
                var vtMonthlyTeachingPlanModels = await Task.Run(() =>
                {
                    return this.vtMonthlyTeachingPlanManager.GetVTMonthlyTeachingPlansByName(vtMonthlyTeachingPlanName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtMonthlyTeachingPlanModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vtMonthlyTeachingPlan data by Id
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTMonthlyTeachingPlanById")]
        public async Task<SingularResponse<VTMonthlyTeachingPlanModel>> GetVTMonthlyTeachingPlanById([FromBody] DataRequest vtMonthlyTeachingPlanRequest)
        {
            SingularResponse<VTMonthlyTeachingPlanModel> response = new SingularResponse<VTMonthlyTeachingPlanModel>();

            try
            {
                var vtMonthlyTeachingPlanModel = await Task.Run(() =>
                {
                    return this.vtMonthlyTeachingPlanManager.GetVTMonthlyTeachingPlanById(Guid.Parse(vtMonthlyTeachingPlanRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vtMonthlyTeachingPlanModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vtMonthlyTeachingPlan
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVTMonthlyTeachingPlan"), Route("CreateOrUpdateVTMonthlyTeachingPlanDetails")]
        public async Task<SingularResponse<string>> CreateVTMonthlyTeachingPlan([FromBody] VTMonthlyTeachingPlanRequest vtMonthlyTeachingPlanRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vtMonthlyTeachingPlanRequest.RequestType = RequestType.New;
                    return this.vtMonthlyTeachingPlanManager.SaveOrUpdateVTMonthlyTeachingPlanDetails(vtMonthlyTeachingPlanRequest);
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
        /// Update vtMonthlyTeachingPlan by Id
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVTMonthlyTeachingPlan")]
        public async Task<SingularResponse<string>> UpdateVTMonthlyTeachingPlan([FromBody] VTMonthlyTeachingPlanRequest vtMonthlyTeachingPlanRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vtMonthlyTeachingPlanRequest.RequestType = RequestType.Edit;
                    return this.vtMonthlyTeachingPlanManager.SaveOrUpdateVTMonthlyTeachingPlanDetails(vtMonthlyTeachingPlanRequest);
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
        /// Delete vtMonthlyTeachingPlan by Id
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVTMonthlyTeachingPlanById")]
        public async Task<SingularResponse<bool>> DeleteVTMonthlyTeachingPlanById([FromBody] DeleteRequest<Guid> vtMonthlyTeachingPlanRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vtMonthlyTeachingPlanManager.DeleteById(vtMonthlyTeachingPlanRequest.DataId);
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