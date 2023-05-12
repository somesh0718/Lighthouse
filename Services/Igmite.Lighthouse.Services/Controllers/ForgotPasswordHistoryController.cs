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
    /// Expose all forgotPasswordHistory WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController, ApiExplorerSettings(IgnoreApi = true)]
    public class ForgotPasswordHistoryController : BaseController
    {
        private readonly IForgotPasswordHistoryManager forgotPasswordHistoryManager;

        /// <summary>
        /// Initializes the ForgotPasswordHistory controller class.
        /// </summary>
        /// <param name="_forgotPasswordHistoryManager"></param>
        public ForgotPasswordHistoryController(IForgotPasswordHistoryManager _forgotPasswordHistoryManager)
        {
            this.forgotPasswordHistoryManager = _forgotPasswordHistoryManager;
        }

        /// <summary>
        /// Get list of forgotPasswordHistory data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetForgotPasswordHistories")]
        public async Task<ListResponse<ForgotPasswordHistoryModel>> GetForgotPasswordHistories()
        {
            ListResponse<ForgotPasswordHistoryModel> response = new ListResponse<ForgotPasswordHistoryModel>();

            try
            {
                IQueryable<ForgotPasswordHistoryModel> forgotPasswordHistoryModels = await Task.Run(() =>
                {
                    return this.forgotPasswordHistoryManager.GetForgotPasswordHistories();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = forgotPasswordHistoryModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of ForgotPasswordHistory with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetForgotPasswordHistoriesByCriteria")]
        public async Task<ListResponse<ForgotPasswordHistoryViewModel>> GetForgotPasswordHistoriesByCriteria([FromBody] SearchForgotPasswordHistoryModel searchModel)
        {
            ListResponse<ForgotPasswordHistoryViewModel> response = new ListResponse<ForgotPasswordHistoryViewModel>();

            try
            {
                var forgotPasswordHistoryModels = await Task.Run(() =>
                {
                    return this.forgotPasswordHistoryManager.GetForgotPasswordHistoriesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = forgotPasswordHistoryModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of forgotPasswordHistory data by name
        /// </summary>
        /// <param name="forgotPasswordHistoryName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetForgotPasswordHistoriesByName")]
        public async Task<ListResponse<ForgotPasswordHistoryModel>> GetForgotPasswordHistoriesByName([FromQuery] string forgotPasswordHistoryName)
        {
            ListResponse<ForgotPasswordHistoryModel> response = new ListResponse<ForgotPasswordHistoryModel>();

            try
            {
                var forgotPasswordHistoryModels = await Task.Run(() =>
                {
                    return this.forgotPasswordHistoryManager.GetForgotPasswordHistoriesByName(forgotPasswordHistoryName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = forgotPasswordHistoryModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get forgotPasswordHistory data by Id
        /// </summary>
        /// <param name="forgotPasswordId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetForgotPasswordHistoryById")]
        public async Task<SingularResponse<ForgotPasswordHistoryModel>> GetForgotPasswordHistoryById([FromBody] DataRequest forgotPasswordRequest)
        {
            SingularResponse<ForgotPasswordHistoryModel> response = new SingularResponse<ForgotPasswordHistoryModel>();

            try
            {
                var forgotPasswordHistoryModel = await Task.Run(() =>
                {
                    return this.forgotPasswordHistoryManager.GetForgotPasswordHistoryById(Guid.Parse(forgotPasswordRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = forgotPasswordHistoryModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new forgotPasswordHistory
        /// </summary>
        /// <param name="forgotPasswordHistoryRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateForgotPasswordHistory"), Route("CreateOrUpdateForgotPasswordHistoryDetails")]
        public async Task<SingularResponse<string>> CreateForgotPasswordHistory([FromBody] ForgotPasswordHistoryRequest forgotPasswordHistoryRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //forgotPasswordHistoryRequest.RequestType = RequestType.New;
                    return this.forgotPasswordHistoryManager.SaveOrUpdateForgotPasswordHistoryDetails(forgotPasswordHistoryRequest);
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
        /// Update forgotPasswordHistory by Id
        /// </summary>
        /// <param name="forgotPasswordHistoryRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateForgotPasswordHistory")]
        public async Task<SingularResponse<string>> UpdateForgotPasswordHistory([FromBody] ForgotPasswordHistoryRequest forgotPasswordHistoryRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    forgotPasswordHistoryRequest.RequestType = RequestType.Edit;
                    return this.forgotPasswordHistoryManager.SaveOrUpdateForgotPasswordHistoryDetails(forgotPasswordHistoryRequest);
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
        /// Delete forgotPasswordHistory by Id
        /// </summary>
        /// <param name="forgotPasswordHistoryRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteForgotPasswordHistoryById")]
        public async Task<SingularResponse<bool>> DeleteForgotPasswordHistoryById([FromBody] DeleteRequest<Guid> forgotPasswordHistoryRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.forgotPasswordHistoryManager.DeleteById(forgotPasswordHistoryRequest.DataId);
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