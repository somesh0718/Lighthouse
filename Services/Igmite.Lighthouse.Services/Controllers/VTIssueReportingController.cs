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
    /// Expose all vtIssueReporting WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VTIssueReportingController : BaseController
    {
        private readonly IVTIssueReportingManager vtIssueReportingManager;

        /// <summary>
        /// Initializes the VTIssueReporting controller class.
        /// </summary>
        /// <param name="_vtIssueReportingManager"></param>
        public VTIssueReportingController(IVTIssueReportingManager _vtIssueReportingManager)
        {
            this.vtIssueReportingManager = _vtIssueReportingManager;
        }

        /// <summary>
        /// Get list of vtIssueReporting data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVTIssueReportings")]
        public async Task<ListResponse<VTIssueReportingModel>> GetVTIssueReportings()
        {
            ListResponse<VTIssueReportingModel> response = new ListResponse<VTIssueReportingModel>();

            try
            {
                IQueryable<VTIssueReportingModel> vtIssueReportingModels = await Task.Run(() =>
                {
                    return this.vtIssueReportingManager.GetVTIssueReportings();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtIssueReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VTIssueReporting with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTIssueReportingsByCriteria")]
        public async Task<ListResponse<VTIssueReportingViewModel>> GetVTIssueReportingsByCriteria([FromBody] SearchVTIssueReportingModel searchModel)
        {
            ListResponse<VTIssueReportingViewModel> response = new ListResponse<VTIssueReportingViewModel>();

            try
            {
                var vtIssueReportingModels = await Task.Run(() =>
                {
                    return this.vtIssueReportingManager.GetVTIssueReportingsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtIssueReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vtIssueReporting data by name
        /// </summary>
        /// <param name="vtIssueReportingName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVTIssueReportingsByName")]
        public async Task<ListResponse<VTIssueReportingModel>> GetVTIssueReportingsByName([FromQuery] string vtIssueReportingName)
        {
            ListResponse<VTIssueReportingModel> response = new ListResponse<VTIssueReportingModel>();

            try
            {
                var vtIssueReportingModels = await Task.Run(() =>
                {
                    return this.vtIssueReportingManager.GetVTIssueReportingsByName(vtIssueReportingName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtIssueReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vtIssueReporting data by Id
        /// </summary>
        /// <param name="vtIssueReportingId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTIssueReportingById")]
        public async Task<SingularResponse<VTIssueReportingModel>> GetVTIssueReportingById([FromBody] DataRequest vtIssueReportingRequest)
        {
            SingularResponse<VTIssueReportingModel> response = new SingularResponse<VTIssueReportingModel>();

            try
            {
                var vtIssueReportingModel = await Task.Run(() =>
                {
                    return this.vtIssueReportingManager.GetVTIssueReportingById(Guid.Parse(vtIssueReportingRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vtIssueReportingModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vtIssueReporting
        /// </summary>
        /// <param name="vtIssueReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVTIssueReporting"), Route("CreateOrUpdateVTIssueReportingDetails")]
        public async Task<SingularResponse<string>> CreateVTIssueReporting([FromBody] VTIssueReportingRequest vtIssueReportingRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vtIssueReportingRequest.RequestType = RequestType.New;
                    return this.vtIssueReportingManager.SaveOrUpdateVTIssueReportingDetails(vtIssueReportingRequest);
                });

                if (response.Errors.Count == 0)
                {
                    response.Messages.Add(Constants.CreatedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex, HttpContext.Request.HttpContext));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Update vtIssueReporting by Id
        /// </summary>
        /// <param name="vtIssueReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVTIssueReporting")]
        public async Task<SingularResponse<string>> UpdateVTIssueReporting([FromBody] VTIssueReportingRequest vtIssueReportingRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vtIssueReportingRequest.RequestType = RequestType.Edit;
                    return this.vtIssueReportingManager.SaveOrUpdateVTIssueReportingDetails(vtIssueReportingRequest);
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
        /// Delete vtIssueReporting by Id
        /// </summary>
        /// <param name="vtIssueReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVTIssueReportingById")]
        public async Task<SingularResponse<bool>> DeleteVTIssueReportingById([FromBody] DeleteRequest<Guid> vtIssueReportingRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vtIssueReportingManager.DeleteById(vtIssueReportingRequest.DataId);
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
        /// Approved VT Issue Reporting
        /// </summary>
        /// <param name="vtApprovalRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("ApprovedVTIssueReporting")]
        public async Task<SingularResponse<string>> ApprovedVTIssueReporting([FromBody] VTIssueReportingApprovalRequest vtApprovalRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    return this.vtIssueReportingManager.ApprovedVTIssueReporting(vtApprovalRequest);
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