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
    /// Expose all hmIssueReporting WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class HMIssueReportingController : BaseController
    {
        private readonly IHMIssueReportingManager hmIssueReportingManager;

        /// <summary>
        /// Initializes the HMIssueReporting controller class.
        /// </summary>
        /// <param name="_hmIssueReportingManager"></param>
        public HMIssueReportingController(IHMIssueReportingManager _hmIssueReportingManager)
        {
            this.hmIssueReportingManager = _hmIssueReportingManager;
        }

        /// <summary>
        /// Get list of hmIssueReporting data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetHMIssueReportings")]
        public async Task<ListResponse<HMIssueReportingModel>> GetHMIssueReportings()
        {
            ListResponse<HMIssueReportingModel> response = new ListResponse<HMIssueReportingModel>();

            try
            {
                IQueryable<HMIssueReportingModel> hmIssueReportingModels = await Task.Run(() =>
                {
                    return this.hmIssueReportingManager.GetHMIssueReportings();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = hmIssueReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of HMIssueReporting with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetHMIssueReportingsByCriteria")]
        public async Task<ListResponse<HMIssueReportingViewModel>> GetHMIssueReportingsByCriteria([FromBody] SearchHMIssueReportingModel searchModel)
        {
            ListResponse<HMIssueReportingViewModel> response = new ListResponse<HMIssueReportingViewModel>();

            try
            {
                var hmIssueReportingModels = await Task.Run(() =>
                {
                    return this.hmIssueReportingManager.GetHMIssueReportingsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = hmIssueReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of hmIssueReporting data by name
        /// </summary>
        /// <param name="hmIssueReportingName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetHMIssueReportingsByName")]
        public async Task<ListResponse<HMIssueReportingModel>> GetHMIssueReportingsByName([FromQuery] string hmIssueReportingName)
        {
            ListResponse<HMIssueReportingModel> response = new ListResponse<HMIssueReportingModel>();

            try
            {
                var hmIssueReportingModels = await Task.Run(() =>
                {
                    return this.hmIssueReportingManager.GetHMIssueReportingsByName(hmIssueReportingName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = hmIssueReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get hmIssueReporting data by Id
        /// </summary>
        /// <param name="hmIssueReportingId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetHMIssueReportingById")]
        public async Task<SingularResponse<HMIssueReportingModel>> GetHMIssueReportingById([FromBody] DataRequest hmIssueReportingRequest)
        {
            SingularResponse<HMIssueReportingModel> response = new SingularResponse<HMIssueReportingModel>();

            try
            {
                var hmIssueReportingModel = await Task.Run(() =>
                {
                    return this.hmIssueReportingManager.GetHMIssueReportingById(Guid.Parse(hmIssueReportingRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = hmIssueReportingModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new hmIssueReporting
        /// </summary>
        /// <param name="hmIssueReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateHMIssueReporting"), Route("CreateOrUpdateHMIssueReportingDetails")]
        public async Task<SingularResponse<string>> CreateHMIssueReporting([FromBody] HMIssueReportingRequest hmIssueReportingRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //hmIssueReportingRequest.RequestType = RequestType.New;
                    return this.hmIssueReportingManager.SaveOrUpdateHMIssueReportingDetails(hmIssueReportingRequest);
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
        /// Update hmIssueReporting by Id
        /// </summary>
        /// <param name="hmIssueReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateHMIssueReporting")]
        public async Task<SingularResponse<string>> UpdateHMIssueReporting([FromBody] HMIssueReportingRequest hmIssueReportingRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    hmIssueReportingRequest.RequestType = RequestType.Edit;
                    return this.hmIssueReportingManager.SaveOrUpdateHMIssueReportingDetails(hmIssueReportingRequest);
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
        /// Delete hmIssueReporting by Id
        /// </summary>
        /// <param name="hmIssueReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteHMIssueReportingById")]
        public async Task<SingularResponse<bool>> DeleteHMIssueReportingById([FromBody] DeleteRequest<Guid> hmIssueReportingRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.hmIssueReportingManager.DeleteById(hmIssueReportingRequest.DataId);
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
        /// Approved HM Issue Reporting
        /// </summary>
        /// <param name="hmApprovalRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("ApprovedHMIssueReporting")]
        public async Task<SingularResponse<string>> ApprovedHMIssueReporting([FromBody] HMIssueReportingApprovalRequest hmApprovalRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    return this.hmIssueReportingManager.ApprovedHMIssueReporting(hmApprovalRequest);
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