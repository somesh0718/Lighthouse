using Igmite.Lighthouse.BAL.Validations;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Mappers;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Manager of the MessageTemplate entity
    /// </summary>
    public class MessageTemplateManager : GenericManager<MessageTemplateModel>, IMessageTemplateManager
    {
        private readonly IMessageTemplateRepository messageTemplateRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the messageTemplate manager.
        /// </summary>
        /// <param name="messageTemplateRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public MessageTemplateManager(IMessageTemplateRepository _messageTemplateRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.messageTemplateRepository = _messageTemplateRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of MessageTemplates
        /// </summary>
        /// <returns></returns>
        public IQueryable<MessageTemplateModel> GetMessageTemplates()
        {
            var messageTemplates = this.messageTemplateRepository.GetMessageTemplates();

            IList<MessageTemplateModel> messageTemplateModels = new List<MessageTemplateModel>();
            messageTemplates.ForEach((user) => messageTemplateModels.Add(user.ToModel()));

            return messageTemplateModels.AsQueryable();
        }

        /// <summary>
        /// Get list of MessageTemplates by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<MessageTemplateModel> GetMessageTemplatesByName(string messageTemplateName)
        {
            var messageTemplates = this.messageTemplateRepository.GetMessageTemplatesByName(messageTemplateName);

            IList<MessageTemplateModel> messageTemplateModels = new List<MessageTemplateModel>();
            messageTemplates.ForEach((user) => messageTemplateModels.Add(user.ToModel()));

            return messageTemplateModels.AsQueryable();
        }

        /// <summary>
        /// Get MessageTemplate by MessageTemplateId
        /// </summary>
        /// <param name="messageTemplateId"></param>
        /// <returns></returns>
        public MessageTemplateModel GetMessageTemplateById(int messageTemplateId)
        {
            MessageTemplate messageTemplate = this.messageTemplateRepository.GetMessageTemplateById(messageTemplateId);

            return (messageTemplate != null) ? messageTemplate.ToModel() : null;
        }

        /// <summary>
        /// Get MessageTemplate by MessageTemplateId using async
        /// </summary>
        /// <param name="messageTemplateId"></param>
        /// <returns></returns>
        public async Task<MessageTemplateModel> GetMessageTemplateByIdAsync(int messageTemplateId)
        {
            var messageTemplate = await this.messageTemplateRepository.GetMessageTemplateByIdAsync(messageTemplateId);

            return (messageTemplate != null) ? messageTemplate.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update MessageTemplate entity
        /// </summary>
        /// <param name="messageTemplateModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateMessageTemplateDetails(MessageTemplateModel messageTemplateModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            MessageTemplate messageTemplate = null;

            //Validate model data
            messageTemplateModel = messageTemplateModel.GetModelValidationErrors<MessageTemplateModel>();

            if (messageTemplateModel.ErrorMessages.Count > 0)
            {
                response.Errors = messageTemplateModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (messageTemplateModel.RequestType == RequestType.Edit)
            {
                messageTemplate = this.messageTemplateRepository.GetMessageTemplateById(messageTemplateModel.MessageTemplateId);
            }
            else
            {
                messageTemplate = new MessageTemplate();
            }

            if (messageTemplateModel.ErrorMessages.Count == 0)
            {
                string existMessageTemplateMessage = this.messageTemplateRepository.CheckMessageTemplateExistByName(messageTemplate, messageTemplateModel);

                if (!string.IsNullOrEmpty(existMessageTemplateMessage))
                {
                    response.Errors.Add(existMessageTemplateMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                messageTemplate.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                messageTemplate = messageTemplateModel.FromModel(messageTemplate);

                //Save Or Update messageTemplate details
                bool isSaved = this.messageTemplateRepository.SaveOrUpdateMessageTemplateDetails(messageTemplate);

                response.Result = isSaved ? "Success" : "Failed";
            }
            else
            {
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            return response;
        }

        /// <summary>
        /// Delete a record by MessageTemplateId
        /// </summary>
        /// <param name="messageTemplateId"></param>
        /// <returns></returns>
        public bool DeleteById(int messageTemplateId)
        {
            return this.messageTemplateRepository.DeleteById(messageTemplateId);
        }

        /// <summary>}
        /// List of MessageTemplate with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<MessageTemplateViewModel> GetMessageTemplatesByCriteria(SearchMessageTemplateRequest searchModel)
        {
            return this.messageTemplateRepository.GetMessageTemplatesByCriteria(searchModel);
        }
    }
}