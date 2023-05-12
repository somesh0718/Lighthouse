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
    /// Expose all dailyReporting WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class DRPDailyReportingController : BaseController
    {
        private readonly IDRPDailyReportingManager dailyReportingManager;

        /// <summary>
        /// Initializes the DRPDailyReporting controller class.
        /// </summary>
        /// <param name="_dailyReportingManager"></param>
        public DRPDailyReportingController(IDRPDailyReportingManager _dailyReportingManager)
        {
            this.dailyReportingManager = _dailyReportingManager;
        }

        /// <summary>
        /// Get list of dailyReporting data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetDRPDailyReportings")]
        public async Task<ListResponse<DRPDailyReportingModel>> GetDRPDailyReportings()
        {
            ListResponse<DRPDailyReportingModel> response = new ListResponse<DRPDailyReportingModel>();

            try
            {
                IQueryable<DRPDailyReportingModel> dailyReportingModels = await Task.Run(() =>
                {
                    return this.dailyReportingManager.GetDRPDailyReportings();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dailyReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of DRPDailyReporting with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetDRPDailyReportingsByCriteria")]
        public async Task<ListResponse<DRPDailyReportingViewModel>> GetDRPDailyReportingsByCriteria([FromBody] SearchDRPDailyReportingModel searchModel)
        {
            ListResponse<DRPDailyReportingViewModel> response = new ListResponse<DRPDailyReportingViewModel>();

            try
            {
                var dailyReportingModels = await Task.Run(() =>
                {
                    return this.dailyReportingManager.GetDRPDailyReportingsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dailyReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of dailyReporting data by name
        /// </summary>
        /// <param name="dailyReportingName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetDRPDailyReportingsByName")]
        public async Task<ListResponse<DRPDailyReportingModel>> GetDRPDailyReportingsByName([FromQuery] string dailyReportingName)
        {
            ListResponse<DRPDailyReportingModel> response = new ListResponse<DRPDailyReportingModel>();

            try
            {
                var dailyReportingModels = await Task.Run(() =>
                {
                    return this.dailyReportingManager.GetDRPDailyReportingsByName(dailyReportingName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dailyReportingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get dailyReporting data by Id
        /// </summary>
        /// <param name="dailyReportingId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetDRPDailyReportingById")]
        public async Task<SingularResponse<DRPDailyReportingModel>> GetDRPDailyReportingById([FromBody] DataRequest dailyReportingRequest)
        {
            SingularResponse<DRPDailyReportingModel> response = new SingularResponse<DRPDailyReportingModel>();

            try
            {
                var dailyReportingModel = await Task.Run(() =>
                {
                    return this.dailyReportingManager.GetDRPDailyReportingById(Guid.Parse(dailyReportingRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = dailyReportingModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new dailyReporting
        /// </summary>
        /// <param name="dailyReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateDRPDailyReporting"), Route("CreateOrUpdateDRPDailyReportingDetails")]
        public async Task<SingularResponse<string>> CreateDRPDailyReporting([FromBody] DRPDailyReportingRequest dailyReportingRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //dailyReportingRequest.RequestType = RequestType.New;
                    return this.dailyReportingManager.SaveOrUpdateDRPDailyReportingDetails(dailyReportingRequest);
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
        /// Update dailyReporting by Id
        /// </summary>
        /// <param name="dailyReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateDRPDailyReporting")]
        public async Task<SingularResponse<string>> UpdateDRPDailyReporting([FromBody] DRPDailyReportingRequest dailyReportingRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    dailyReportingRequest.RequestType = RequestType.Edit;
                    return this.dailyReportingManager.SaveOrUpdateDRPDailyReportingDetails(dailyReportingRequest);
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
        /// Delete dailyReporting by Id
        /// </summary>
        /// <param name="dailyReportingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteDRPDailyReportingById")]
        public async Task<SingularResponse<bool>> DeleteDRPDailyReportingById([FromBody] DeleteRequest<Guid> dailyReportingRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.dailyReportingManager.DeleteById(dailyReportingRequest.DataId);
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