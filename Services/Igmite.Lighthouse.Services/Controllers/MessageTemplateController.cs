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
    /// Expose all messageTemplate WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class MessageTemplateController : BaseController
    {
        private readonly IMessageTemplateManager messageTemplateManager;

        /// <summary>
        /// Initializes the MessageTemplate controller class.
        /// </summary>
        /// <param name="_messageTemplateManager"></param>
        public MessageTemplateController(IMessageTemplateManager _messageTemplateManager)
        {
            this.messageTemplateManager = _messageTemplateManager;
        }

        /// <summary>
        /// Get list of messageTemplate data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetMessageTemplates")]
        public async Task<ListResponse<MessageTemplateModel>> GetMessageTemplates()
        {
            ListResponse<MessageTemplateModel> response = new ListResponse<MessageTemplateModel>();

            try
            {
                IQueryable<MessageTemplateModel> messageTemplateModels = await Task.Run(() =>
                {
                    return this.messageTemplateManager.GetMessageTemplates();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = messageTemplateModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of user messageTemplates with filters
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns>
        /// All active messageTemplates
        /// </returns>
        [HttpPost, Route("GetMessageTemplatesByCriteria")]
        public async Task<ListResponse<MessageTemplateViewModel>> GetMessageTemplatesByCriteria([FromBody] SearchMessageTemplateRequest searchModel)
        {
            ListResponse<MessageTemplateViewModel> response = new ListResponse<MessageTemplateViewModel>();

            try
            {
                var messageTemplateModels = await Task.Run(() =>
                {
                    return this.messageTemplateManager.GetMessageTemplatesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = messageTemplateModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of messageTemplate data by name
        /// </summary>
        /// <param name="messageTemplateName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetMessageTemplatesByName")]
        public async Task<ListResponse<MessageTemplateModel>> GetMessageTemplatesByName([FromQuery] string messageTemplateName)
        {
            ListResponse<MessageTemplateModel> response = new ListResponse<MessageTemplateModel>();

            try
            {
                var messageTemplateModels = await Task.Run(() =>
                {
                    return this.messageTemplateManager.GetMessageTemplatesByName(messageTemplateName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = messageTemplateModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get messageTemplate data by Id
        /// </summary>
        /// <param name="messageTemplateId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetMessageTemplateById")]
        public async Task<SingularResponse<MessageTemplateModel>> GetMessageTemplateById([FromBody] DataRequest messageTemplateRequest)
        {
            SingularResponse<MessageTemplateModel> response = new SingularResponse<MessageTemplateModel>();

            try
            {
                var messageTemplateModel = await Task.Run(() =>
                {
                    return this.messageTemplateManager.GetMessageTemplateById(int.Parse(messageTemplateRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = messageTemplateModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new messageTemplate
        /// </summary>
        /// <param name="messageTemplateRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateMessageTemplate"), Route("CreateOrUpdateMessageTemplateDetails")]
        public async Task<SingularResponse<string>> CreateMessageTemplate([FromBody] MessageTemplateRequest messageTemplateRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //messageTemplateRequest.RequestType = RequestType.New;
                    return this.messageTemplateManager.SaveOrUpdateMessageTemplateDetails(messageTemplateRequest);
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
        /// Update messageTemplate by Id
        /// </summary>
        /// <param name="messageTemplateRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateMessageTemplate")]
        public async Task<SingularResponse<string>> UpdateMessageTemplate([FromBody] MessageTemplateRequest messageTemplateRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    messageTemplateRequest.RequestType = RequestType.Edit;
                    return this.messageTemplateManager.SaveOrUpdateMessageTemplateDetails(messageTemplateRequest);
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
        /// Delete messageTemplate by Id
        /// </summary>
        /// <param name="messageTemplateRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteMessageTemplateById")]
        public async Task<SingularResponse<bool>> DeleteMessageTemplateById([FromBody] DeleteRequest<int> messageTemplateRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.messageTemplateManager.DeleteById(messageTemplateRequest.DataId);
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