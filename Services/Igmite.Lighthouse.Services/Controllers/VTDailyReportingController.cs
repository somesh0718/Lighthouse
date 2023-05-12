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
    /// Expose all vtDailyReporting WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VTDailyReportingController : BaseController
    {
        private readonly IVTDailyReportingManager vtDailyReportingManager;

        /// <summary>
        /// Initializes the VTDailyReporting controller class.
        /// </summary>
        /// <param name="_vtDailyReportingManager"></param>
        public VTDailyReportingController(IVTDailyReportingManager _vtDailyReportingManager)
        {
            this.vtDailyReportingManager = _vtDailyReportingManager;
        }

        /// <summary>
        /// Get list of vtDailyReporting data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVTDailyReportings")]
        public async Task<ListResponse<VTDailyReportingModel>> GetVTDailyReportings()
        {
            ListResponse<VTDailyReportingModel> response = new ListResponse<VTDailyReportingModel>();

            try
            {
                IQueryable<VTDailyReportingModel> vtDailyReportingModels = await Task.Run(() =>
                {
                    return this.vtDailyReportingManager.GetVTDailyReportings();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtDailyReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VTDailyReporting with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTDailyReportingsByCriteria")]
        public async Task<ListResponse<VTDailyReportingViewModel>> GetVTDailyReportingsByCriteria([FromBody] SearchVTDailyReportingModel searchModel)
        {
            ListResponse<VTDailyReportingViewModel> response = new ListResponse<VTDailyReportingViewModel>();

            try
            {
                var vtDailyReportingModels = await Task.Run(() =>
                {
                    return this.vtDailyReportingManager.GetVTDailyReportingsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtDailyReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vtDailyReporting data by name
        /// </summary>
        /// <param name="vtDailyReportingName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVTDailyReportingsByName")]
        public async Task<ListResponse<VTDailyReportingModel>> GetVTDailyReportingsByName([FromQuery] string vtDailyReportingName)
        {
            ListResponse<VTDailyReportingModel> response = new ListResponse<VTDailyReportingModel>();

            try
            {
                var vtDailyReportingModels = await Task.Run(() =>
                {
                    return this.vtDailyReportingManager.GetVTDailyReportingsByName(vtDailyReportingName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtDailyReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vtDailyReporting data by Id
        /// </summary>
        /// <param name="vtDailyReportingId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTDailyReportingById")]
        public async Task<SingularResponse<VTDailyReportingModel>> GetVTDailyReportingById([FromBody] DataRequest vtDailyReportingRequest)
        {
            SingularResponse<VTDailyReportingModel> response = new SingularResponse<VTDailyReportingModel>();

            try
            {
                var vtDailyReportingModel = await Task.Run(() =>
                {
                    return this.vtDailyReportingManager.GetVTDailyReportingById(Guid.Parse(vtDailyReportingRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vtDailyReportingModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vtDailyReporting
        /// </summary>
        /// <param name="vtDailyReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVTDailyReporting"), Route("CreateOrUpdateVTDailyReportingDetails")]
        public async Task<SingularResponse<string>> CreateVTDailyReporting([FromBody] VTDailyReportingRequest vtDailyReportingRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vtDailyReportingRequest.RequestType = RequestType.New;
                    return this.vtDailyReportingManager.SaveOrUpdateVTDailyReportingDetails(vtDailyReportingRequest);
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
        /// Update vtDailyReporting by Id
        /// </summary>
        /// <param name="vtDailyReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVTDailyReporting")]
        public async Task<SingularResponse<string>> UpdateVTDailyReporting([FromBody] VTDailyReportingRequest vtDailyReportingRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vtDailyReportingRequest.RequestType = RequestType.Edit;
                    return this.vtDailyReportingManager.SaveOrUpdateVTDailyReportingDetails(vtDailyReportingRequest);
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
        /// Delete vtDailyReporting by Id
        /// </summary>
        /// <param name="vtDailyReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVTDailyReportingById")]
        public async Task<SingularResponse<bool>> DeleteVTDailyReportingById([FromBody] DeleteRequest<Guid> vtDailyReportingRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vtDailyReportingManager.DeleteById(vtDailyReportingRequest.DataId);
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
        /// Approved VT Daily Reporting
        /// </summary>
        /// <param name="vtApprovalRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("ApprovedVTDailyReporting")]
        public async Task<SingularResponse<string>> ApprovedVTDailyReporting([FromBody] VTDailyReportingApprovalRequest vtApprovalRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    return this.vtDailyReportingManager.ApprovedVTDailyReporting(vtApprovalRequest);
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