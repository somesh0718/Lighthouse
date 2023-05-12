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
    /// Expose all vcIssueReporting WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VCIssueReportingController : BaseController
    {
        private readonly IVCIssueReportingManager vcIssueReportingManager;

        /// <summary>
        /// Initializes the VCIssueReporting controller class.
        /// </summary>
        /// <param name="_vcIssueReportingManager"></param>
        public VCIssueReportingController(IVCIssueReportingManager _vcIssueReportingManager)
        {
            this.vcIssueReportingManager = _vcIssueReportingManager;
        }

        /// <summary>
        /// Get list of vcIssueReporting data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVCIssueReportings")]
        public async Task<ListResponse<VCIssueReportingModel>> GetVCIssueReportings()
        {
            ListResponse<VCIssueReportingModel> response = new ListResponse<VCIssueReportingModel>();

            try
            {
                IQueryable<VCIssueReportingModel> vcIssueReportingModels = await Task.Run(() =>
                {
                    return this.vcIssueReportingManager.GetVCIssueReportings();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vcIssueReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VCIssueReporting with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVCIssueReportingsByCriteria")]
        public async Task<ListResponse<VCIssueReportingViewModel>> GetVCIssueReportingsByCriteria([FromBody] SearchVCIssueReportingModel searchModel)
        {
            ListResponse<VCIssueReportingViewModel> response = new ListResponse<VCIssueReportingViewModel>();

            try
            {
                var vcIssueReportingModels = await Task.Run(() =>
                {
                    return this.vcIssueReportingManager.GetVCIssueReportingsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vcIssueReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vcIssueReporting data by name
        /// </summary>
        /// <param name="vcIssueReportingName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVCIssueReportingsByName")]
        public async Task<ListResponse<VCIssueReportingModel>> GetVCIssueReportingsByName([FromQuery] string vcIssueReportingName)
        {
            ListResponse<VCIssueReportingModel> response = new ListResponse<VCIssueReportingModel>();

            try
            {
                var vcIssueReportingModels = await Task.Run(() =>
                {
                    return this.vcIssueReportingManager.GetVCIssueReportingsByName(vcIssueReportingName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vcIssueReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vcIssueReporting data by Id
        /// </summary>
        /// <param name="vcIssueReportingId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVCIssueReportingById")]
        public async Task<SingularResponse<VCIssueReportingModel>> GetVCIssueReportingById([FromBody] DataRequest vcIssueReportingRequest)
        {
            SingularResponse<VCIssueReportingModel> response = new SingularResponse<VCIssueReportingModel>();

            try
            {
                var vcIssueReportingModel = await Task.Run(() =>
                {
                    return this.vcIssueReportingManager.GetVCIssueReportingById(Guid.Parse(vcIssueReportingRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vcIssueReportingModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vcIssueReporting
        /// </summary>
        /// <param name="vcIssueReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVCIssueReporting"), Route("CreateOrUpdateVCIssueReportingDetails")]
        public async Task<SingularResponse<string>> CreateVCIssueReporting([FromBody] VCIssueReportingRequest vcIssueReportingRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vcIssueReportingRequest.RequestType = RequestType.New;
                    return this.vcIssueReportingManager.SaveOrUpdateVCIssueReportingDetails(vcIssueReportingRequest);
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
        /// Update vcIssueReporting by Id
        /// </summary>
        /// <param name="vcIssueReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVCIssueReporting")]
        public async Task<SingularResponse<string>> UpdateVCIssueReporting([FromBody] VCIssueReportingRequest vcIssueReportingRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vcIssueReportingRequest.RequestType = RequestType.Edit;
                    return this.vcIssueReportingManager.SaveOrUpdateVCIssueReportingDetails(vcIssueReportingRequest);
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
        /// Delete vcIssueReporting by Id
        /// </summary>
        /// <param name="vcIssueReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVCIssueReportingById")]
        public async Task<SingularResponse<bool>> DeleteVCIssueReportingById([FromBody] DeleteRequest<Guid> vcIssueReportingRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vcIssueReportingManager.DeleteById(vcIssueReportingRequest.DataId);
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
        /// Approved VC Issue Reporting
        /// </summary>
        /// <param name="vcApprovalRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("ApprovedVCIssueReporting")]
        public async Task<SingularResponse<string>> ApprovedVCIssueReporting([FromBody] VCIssueReportingApprovalRequest vcApprovalRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    return this.vcIssueReportingManager.ApprovedVCIssueReporting(vcApprovalRequest);
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