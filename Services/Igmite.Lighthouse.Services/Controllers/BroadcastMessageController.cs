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
    /// Expose all BroadcastMessage WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class BroadcastMessageController : BaseController
    {
        private readonly IBroadcastMessageManager broadcastMessageManager;

        /// <summary>
        /// Initializes the BroadcastMessage controller class.
        /// </summary>
        /// <param name="_broadcastMessageManager"></param>
        public BroadcastMessageController(IBroadcastMessageManager _broadcastMessageManager)
        {
            this.broadcastMessageManager = _broadcastMessageManager;
        }

        /// <summary>
        /// Get list of BroadcastMessage data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetBroadcastMessages")]
        public async Task<ListResponse<BroadcastMessageModel>> GetBroadcastMessages()
        {
            ListResponse<BroadcastMessageModel> response = new ListResponse<BroadcastMessageModel>();

            try
            {
                IQueryable<BroadcastMessageModel> broadcastMessageModels = await Task.Run(() =>
                {
                    return this.broadcastMessageManager.GetBroadcastMessages();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = broadcastMessageModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of BroadcastMessage with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetBroadcastMessagesByCriteria")]
        public async Task<ListResponse<BroadcastMessageViewModel>> GetBroadcastMessagesByCriteria([FromBody] SearchBroadcastMessageModel searchModel)
        {
            ListResponse<BroadcastMessageViewModel> response = new ListResponse<BroadcastMessageViewModel>();

            try
            {
                var broadcastMessageModels = await Task.Run(() =>
                {
                    return this.broadcastMessageManager.GetBroadcastMessagesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = broadcastMessageModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of BroadcastMessage data by name
        /// </summary>
        /// <param name="broadcastMessageName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetBroadcastMessagesByName")]
        public async Task<ListResponse<BroadcastMessageModel>> GetBroadcastMessagesByName([FromQuery] string broadcastMessageName)
        {
            ListResponse<BroadcastMessageModel> response = new ListResponse<BroadcastMessageModel>();

            try
            {
                var broadcastMessageModels = await Task.Run(() =>
                {
                    return this.broadcastMessageManager.GetBroadcastMessagesByName(broadcastMessageName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = broadcastMessageModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get BroadcastMessage data by Id
        /// </summary>
        /// <param name="broadcastMessageId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetBroadcastMessageById")]
        public async Task<SingularResponse<BroadcastMessageModel>> GetBroadcastMessageById([FromBody] DataRequest broadcastMessageRequest)
        {
            SingularResponse<BroadcastMessageModel> response = new SingularResponse<BroadcastMessageModel>();

            try
            {
                var broadcastMessageModel = await Task.Run(() =>
                {
                    return this.broadcastMessageManager.GetBroadcastMessageById(Guid.Parse(broadcastMessageRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = broadcastMessageModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new BroadcastMessage
        /// </summary>
        /// <param name="BroadcastMessageRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateBroadcastMessage"), Route("CreateOrUpdateBroadcastMessageDetails")]
        public async Task<SingularResponse<string>> CreateBroadcastMessage([FromBody] BroadcastMessageRequest BroadcastMessageRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //BroadcastMessageRequest.RequestType = RequestType.New;
                    return this.broadcastMessageManager.SaveOrUpdateBroadcastMessageDetails(BroadcastMessageRequest);
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
        /// Update BroadcastMessage by Id
        /// </summary>
        /// <param name="BroadcastMessageRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateBroadcastMessage")]
        public async Task<SingularResponse<string>> UpdateBroadcastMessage([FromBody] BroadcastMessageRequest BroadcastMessageRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    BroadcastMessageRequest.RequestType = RequestType.Edit;
                    return this.broadcastMessageManager.SaveOrUpdateBroadcastMessageDetails(BroadcastMessageRequest);
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
        /// Delete BroadcastMessage by Id
        /// </summary>
        /// <param name="BroadcastMessageRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteBroadcastMessageById")]
        public async Task<SingularResponse<bool>> DeleteBroadcastMessageById([FromBody] DeleteRequest<Guid> BroadcastMessageRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.broadcastMessageManager.DeleteById(BroadcastMessageRequest.DataId);
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