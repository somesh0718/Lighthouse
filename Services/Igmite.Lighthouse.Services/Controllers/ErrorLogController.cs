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
    /// Expose all errorLog WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController, ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorLogController : BaseController
    {
        private readonly IErrorLogManager errorLogManager;

        /// <summary>
        /// Initializes the ErrorLog controller class.
        /// </summary>
        /// <param name="_errorLogManager"></param>
        public ErrorLogController(IErrorLogManager _errorLogManager)
        {
            this.errorLogManager = _errorLogManager;
        }

        /// <summary>
        /// Get list of errorLog data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetErrorLogs")]
        public async Task<ListResponse<ErrorLogModel>> GetErrorLogs()
        {
            ListResponse<ErrorLogModel> response = new ListResponse<ErrorLogModel>();

            try
            {
                IQueryable<ErrorLogModel> errorLogModels = await Task.Run(() =>
                {
                    return this.errorLogManager.GetErrorLogs();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = errorLogModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of ErrorLog with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetErrorLogsByCriteria")]
        public async Task<ListResponse<ErrorLogViewModel>> GetErrorLogsByCriteria([FromBody] SearchErrorLogModel searchModel)
        {
            ListResponse<ErrorLogViewModel> response = new ListResponse<ErrorLogViewModel>();

            try
            {
                var errorLogModels = await Task.Run(() =>
                {
                    return this.errorLogManager.GetErrorLogsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = errorLogModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of errorLog data by name
        /// </summary>
        /// <param name="errorLogName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetErrorLogsByName")]
        public async Task<ListResponse<ErrorLogModel>> GetErrorLogsByName([FromQuery] string errorLogName)
        {
            ListResponse<ErrorLogModel> response = new ListResponse<ErrorLogModel>();

            try
            {
                var errorLogModels = await Task.Run(() =>
                {
                    return this.errorLogManager.GetErrorLogsByName(errorLogName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = errorLogModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get errorLog data by Id
        /// </summary>
        /// <param name="errorLogId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetErrorLogById")]
        public async Task<SingularResponse<ErrorLogModel>> GetErrorLogById([FromBody] DataRequest errorLogRequest)
        {
            SingularResponse<ErrorLogModel> response = new SingularResponse<ErrorLogModel>();

            try
            {
                var errorLogModel = await Task.Run(() =>
                {
                    return this.errorLogManager.GetErrorLogById(Guid.Parse(errorLogRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = errorLogModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new errorLog
        /// </summary>
        /// <param name="errorLogRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateErrorLog"), Route("CreateOrUpdateErrorLogDetails")]
        public async Task<SingularResponse<string>> CreateErrorLog([FromBody] ErrorLogRequest errorLogRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //errorLogRequest.RequestType = RequestType.New;
                    return this.errorLogManager.SaveOrUpdateErrorLogDetails(errorLogRequest);
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
        /// Update errorLog by Id
        /// </summary>
        /// <param name="errorLogRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateErrorLog")]
        public async Task<SingularResponse<string>> UpdateErrorLog([FromBody] ErrorLogRequest errorLogRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    errorLogRequest.RequestType = RequestType.Edit;
                    return this.errorLogManager.SaveOrUpdateErrorLogDetails(errorLogRequest);
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
        /// Delete errorLog by Id
        /// </summary>
        /// <param name="errorLogRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteErrorLogById")]
        public async Task<SingularResponse<bool>> DeleteErrorLogById([FromBody] DeleteRequest<Guid> errorLogRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.errorLogManager.DeleteById(errorLogRequest.DataId);
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