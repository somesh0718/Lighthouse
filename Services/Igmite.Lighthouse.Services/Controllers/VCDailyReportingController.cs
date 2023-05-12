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
    /// Expose all vcDailyReporting WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VCDailyReportingController : BaseController
    {
        private readonly IVCDailyReportingManager vcDailyReportingManager;

        /// <summary>
        /// Initializes the VCDailyReporting controller class.
        /// </summary>
        /// <param name="_vcDailyReportingManager"></param>
        public VCDailyReportingController(IVCDailyReportingManager _vcDailyReportingManager)
        {
            this.vcDailyReportingManager = _vcDailyReportingManager;
        }

        /// <summary>
        /// Get list of vcDailyReporting data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVCDailyReportings")]
        public async Task<ListResponse<VCDailyReportingModel>> GetVCDailyReportings()
        {
            ListResponse<VCDailyReportingModel> response = new ListResponse<VCDailyReportingModel>();

            try
            {
                IQueryable<VCDailyReportingModel> vcDailyReportingModels = await Task.Run(() =>
                {
                    return this.vcDailyReportingManager.GetVCDailyReportings();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vcDailyReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VCDailyReporting with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVCDailyReportingsByCriteria")]
        public async Task<ListResponse<VCDailyReportingViewModel>> GetVCDailyReportingsByCriteria([FromBody] SearchVCDailyReportingModel searchModel)
        {
            ListResponse<VCDailyReportingViewModel> response = new ListResponse<VCDailyReportingViewModel>();

            try
            {
                var vcDailyReportingModels = await Task.Run(() =>
                {
                    return this.vcDailyReportingManager.GetVCDailyReportingsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vcDailyReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vcDailyReporting data by name
        /// </summary>
        /// <param name="vcDailyReportingName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVCDailyReportingsByName")]
        public async Task<ListResponse<VCDailyReportingModel>> GetVCDailyReportingsByName([FromQuery] string vcDailyReportingName)
        {
            ListResponse<VCDailyReportingModel> response = new ListResponse<VCDailyReportingModel>();

            try
            {
                var vcDailyReportingModels = await Task.Run(() =>
                {
                    return this.vcDailyReportingManager.GetVCDailyReportingsByName(vcDailyReportingName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vcDailyReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vcDailyReporting data by Id
        /// </summary>
        /// <param name="vcDailyReportingId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVCDailyReportingById")]
        public async Task<SingularResponse<VCDailyReportingModel>> GetVCDailyReportingById([FromBody] DataRequest vcDailyReportingRequest)
        {
            SingularResponse<VCDailyReportingModel> response = new SingularResponse<VCDailyReportingModel>();

            try
            {
                var vcDailyReportingModel = await Task.Run(() =>
                {
                    return this.vcDailyReportingManager.GetVCDailyReportingById(Guid.Parse(vcDailyReportingRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vcDailyReportingModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vcDailyReporting
        /// </summary>
        /// <param name="vcDailyReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVCDailyReporting"), Route("CreateOrUpdateVCDailyReportingDetails")]
        public async Task<SingularResponse<string>> CreateVCDailyReporting([FromBody] VCDailyReportingRequest vcDailyReportingRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vcDailyReportingRequest.RequestType = RequestType.New;
                    return this.vcDailyReportingManager.SaveOrUpdateVCDailyReportingDetails(vcDailyReportingRequest);
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
        /// Update vcDailyReporting by Id
        /// </summary>
        /// <param name="vcDailyReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVCDailyReporting")]
        public async Task<SingularResponse<string>> UpdateVCDailyReporting([FromBody] VCDailyReportingRequest vcDailyReportingRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vcDailyReportingRequest.RequestType = RequestType.Edit;
                    return this.vcDailyReportingManager.SaveOrUpdateVCDailyReportingDetails(vcDailyReportingRequest);
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
        /// Delete vcDailyReporting by Id
        /// </summary>
        /// <param name="vcDailyReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVCDailyReportingById")]
        public async Task<SingularResponse<bool>> DeleteVCDailyReportingById([FromBody] DeleteRequest<Guid> vcDailyReportingRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vcDailyReportingManager.DeleteById(vcDailyReportingRequest.DataId);
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