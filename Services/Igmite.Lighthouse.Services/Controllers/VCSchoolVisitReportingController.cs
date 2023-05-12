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
    /// Expose all VC School Visit Reporting WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VCSchoolVisitReportingController : BaseController
    {
        private readonly IVCSchoolVisitReportingManager vcSchoolVisitReportingManager;

        /// <summary>
        /// Initializes the VCSchoolVisitReporting controller class.
        /// </summary>
        /// <param name="_vcSchoolVisitReportingManager"></param>
        public VCSchoolVisitReportingController(IVCSchoolVisitReportingManager _vcSchoolVisitReportingManager)
        {
            this.vcSchoolVisitReportingManager = _vcSchoolVisitReportingManager;
        }

        /// <summary>
        /// Get list of vcSchoolVisitReporting data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVCSchoolVisitReporting")]
        public async Task<ListResponse<VCSchoolVisitReportingModel>> GetVCSchoolVisitReporting()
        {
            ListResponse<VCSchoolVisitReportingModel> response = new ListResponse<VCSchoolVisitReportingModel>();

            try
            {
                IQueryable<VCSchoolVisitReportingModel> vcSchoolVisitReportingModels = await Task.Run(() =>
                {
                    return this.vcSchoolVisitReportingManager.GetVCSchoolVisitReporting();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vcSchoolVisitReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VCSchoolVisitReporting with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVCSchoolVisitReportingByCriteria")]
        public async Task<ListResponse<VCSchoolVisitReportingViewModel>> GetVCSchoolVisitReportingByCriteria([FromBody] SearchVCSchoolVisitReportingModel searchModel)
        {
            ListResponse<VCSchoolVisitReportingViewModel> response = new ListResponse<VCSchoolVisitReportingViewModel>();

            try
            {
                var vcSchoolVisitReportingModels = await Task.Run(() =>
                {
                    return this.vcSchoolVisitReportingManager.GetVCSchoolVisitReportingByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vcSchoolVisitReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vcSchoolVisitReporting data by name
        /// </summary>
        /// <param name="vcSchoolVisitReportingName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVCSchoolVisitReportingByName")]
        public async Task<ListResponse<VCSchoolVisitReportingModel>> GetVCSchoolVisitReportingByName([FromQuery] string vcSchoolVisitReportingName)
        {
            ListResponse<VCSchoolVisitReportingModel> response = new ListResponse<VCSchoolVisitReportingModel>();

            try
            {
                var vcSchoolVisitReportingModels = await Task.Run(() =>
                {
                    return this.vcSchoolVisitReportingManager.GetVCSchoolVisitReportingByName(vcSchoolVisitReportingName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vcSchoolVisitReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vcSchoolVisitReporting data by Id
        /// </summary>
        /// <param name="vcSchoolVisitReportingId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVCSchoolVisitReportingById")]
        public async Task<SingularResponse<VCSchoolVisitReportingModel>> GetVCSchoolVisitReportingById([FromBody] DataRequest vcSchoolVisitReportingRequest)
        {
            SingularResponse<VCSchoolVisitReportingModel> response = new SingularResponse<VCSchoolVisitReportingModel>();

            try
            {
                var vcSchoolVisitReportingModel = await Task.Run(() =>
                {
                    return this.vcSchoolVisitReportingManager.GetVCSchoolVisitReportingById(Guid.Parse(vcSchoolVisitReportingRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vcSchoolVisitReportingModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vcSchoolVisitReporting
        /// </summary>
        /// <param name="vcSchoolVisitReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVCSchoolVisitReporting"), Route("CreateOrUpdateVCSchoolVisitReportingDetails")]
        public async Task<SingularResponse<string>> CreateVCSchoolVisitReporting([FromBody] VCSchoolVisitReportingRequest vcSchoolVisitReportingRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vcSchoolVisitReportingRequest.RequestType = RequestType.New;
                    return this.vcSchoolVisitReportingManager.SaveOrUpdateVCSchoolVisitReportingDetails(vcSchoolVisitReportingRequest);
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
        /// Update vcSchoolVisitReporting by Id
        /// </summary>
        /// <param name="vcSchoolVisitReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVCSchoolVisitReporting")]
        public async Task<SingularResponse<string>> UpdateVCSchoolVisitReporting([FromBody] VCSchoolVisitReportingRequest vcSchoolVisitReportingRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vcSchoolVisitReportingRequest.RequestType = RequestType.Edit;
                    return this.vcSchoolVisitReportingManager.SaveOrUpdateVCSchoolVisitReportingDetails(vcSchoolVisitReportingRequest);
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
        /// Delete vcSchoolVisitReporting by Id
        /// </summary>
        /// <param name="vcSchoolVisitReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVCSchoolVisitReportingById")]
        public async Task<SingularResponse<bool>> DeleteVCSchoolVisitReportingById([FromBody] DeleteRequest<Guid> vcSchoolVisitReportingRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vcSchoolVisitReportingManager.DeleteById(vcSchoolVisitReportingRequest.DataId);
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