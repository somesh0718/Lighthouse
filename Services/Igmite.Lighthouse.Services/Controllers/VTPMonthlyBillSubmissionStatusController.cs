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
    /// Expose all vtpMonthlyBillSubmissionStatus WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VTPMonthlyBillSubmissionStatusController : BaseController
    {
        private readonly IVTPMonthlyBillSubmissionStatusManager vtpMonthlyBillSubmissionStatusManager;

        /// <summary>
        /// Initializes the VTPMonthlyBillSubmissionStatus controller class.
        /// </summary>
        /// <param name="_vtpMonthlyBillSubmissionStatusManager"></param>
        public VTPMonthlyBillSubmissionStatusController(IVTPMonthlyBillSubmissionStatusManager _vtpMonthlyBillSubmissionStatusManager)
        {
            this.vtpMonthlyBillSubmissionStatusManager = _vtpMonthlyBillSubmissionStatusManager;
        }

        /// <summary>
        /// Get list of vtpMonthlyBillSubmissionStatus data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVTPMonthlyBillSubmissionStatus")]
        public async Task<ListResponse<VTPMonthlyBillSubmissionStatusModel>> GetVTPMonthlyBillSubmissionStatus()
        {
            ListResponse<VTPMonthlyBillSubmissionStatusModel> response = new ListResponse<VTPMonthlyBillSubmissionStatusModel>();

            try
            {
                IQueryable<VTPMonthlyBillSubmissionStatusModel> vtpMonthlyBillSubmissionStatusModels = await Task.Run(() =>
                {
                    return this.vtpMonthlyBillSubmissionStatusManager.GetVTPMonthlyBillSubmissionStatus();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtpMonthlyBillSubmissionStatusModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VTPMonthlyBillSubmissionStatus with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTPMonthlyBillSubmissionStatusByCriteria")]
        public async Task<ListResponse<VTPMonthlyBillSubmissionStatusViewModel>> GetVTPMonthlyBillSubmissionStatusByCriteria([FromBody] SearchVTPMonthlyBillSubmissionStatusModel searchModel)
        {
            ListResponse<VTPMonthlyBillSubmissionStatusViewModel> response = new ListResponse<VTPMonthlyBillSubmissionStatusViewModel>();

            try
            {
                var vtpMonthlyBillSubmissionStatusModels = await Task.Run(() =>
                {
                    return this.vtpMonthlyBillSubmissionStatusManager.GetVTPMonthlyBillSubmissionStatusByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtpMonthlyBillSubmissionStatusModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vtpMonthlyBillSubmissionStatus data by name
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVTPMonthlyBillSubmissionStatusByName")]
        public async Task<ListResponse<VTPMonthlyBillSubmissionStatusModel>> GetVTPMonthlyBillSubmissionStatusByName([FromQuery] string vtpMonthlyBillSubmissionStatusName)
        {
            ListResponse<VTPMonthlyBillSubmissionStatusModel> response = new ListResponse<VTPMonthlyBillSubmissionStatusModel>();

            try
            {
                var vtpMonthlyBillSubmissionStatusModels = await Task.Run(() =>
                {
                    return this.vtpMonthlyBillSubmissionStatusManager.GetVTPMonthlyBillSubmissionStatusByName(vtpMonthlyBillSubmissionStatusName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtpMonthlyBillSubmissionStatusModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vtpMonthlyBillSubmissionStatus data by Id
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTPMonthlyBillSubmissionStatusById")]
        public async Task<SingularResponse<VTPMonthlyBillSubmissionStatusModel>> GetVTPMonthlyBillSubmissionStatusById([FromBody] DataRequest vtpMonthlyBillSubmissionStatusRequest)
        {
            SingularResponse<VTPMonthlyBillSubmissionStatusModel> response = new SingularResponse<VTPMonthlyBillSubmissionStatusModel>();

            try
            {
                var vtpMonthlyBillSubmissionStatusModel = await Task.Run(() =>
                {
                    return this.vtpMonthlyBillSubmissionStatusManager.GetVTPMonthlyBillSubmissionStatusById(Guid.Parse(vtpMonthlyBillSubmissionStatusRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vtpMonthlyBillSubmissionStatusModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vtpMonthlyBillSubmissionStatus
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVTPMonthlyBillSubmissionStatus"), Route("CreateOrUpdateVTPMonthlyBillSubmissionStatusDetails")]
        public async Task<SingularResponse<string>> CreateVTPMonthlyBillSubmissionStatus([FromBody] VTPMonthlyBillSubmissionStatusRequest vtpMonthlyBillSubmissionStatusRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vtpMonthlyBillSubmissionStatusRequest.RequestType = RequestType.New;
                    return this.vtpMonthlyBillSubmissionStatusManager.SaveOrUpdateVTPMonthlyBillSubmissionStatusDetails(vtpMonthlyBillSubmissionStatusRequest);
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
        /// Update vtpMonthlyBillSubmissionStatus by Id
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVTPMonthlyBillSubmissionStatus")]
        public async Task<SingularResponse<string>> UpdateVTPMonthlyBillSubmissionStatus([FromBody] VTPMonthlyBillSubmissionStatusRequest vtpMonthlyBillSubmissionStatusRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vtpMonthlyBillSubmissionStatusRequest.RequestType = RequestType.Edit;
                    return this.vtpMonthlyBillSubmissionStatusManager.SaveOrUpdateVTPMonthlyBillSubmissionStatusDetails(vtpMonthlyBillSubmissionStatusRequest);
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
        /// Delete vtpMonthlyBillSubmissionStatus by Id
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVTPMonthlyBillSubmissionStatusById")]
        public async Task<SingularResponse<bool>> DeleteVTPMonthlyBillSubmissionStatusById([FromBody] DeleteRequest<Guid> vtpMonthlyBillSubmissionStatusRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vtpMonthlyBillSubmissionStatusManager.DeleteById(vtpMonthlyBillSubmissionStatusRequest.DataId);
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