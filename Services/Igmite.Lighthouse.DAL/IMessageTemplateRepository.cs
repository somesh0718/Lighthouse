using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the MessageTemplate entity
    /// </summary>
    public interface IMessageTemplateRepository : IGenericRepository<MessageTemplate>
    {
        /// <summary>
        /// Get list of MessageTemplate
        /// </summary>
        /// <returns></returns>
        IQueryable<MessageTemplate> GetMessageTemplates();

        /// <summary>
        /// Get list of MessageTemplate by messageTemplateName
        /// </summary>
        /// <param name="messageTemplateName"></param>
        /// <returns></returns>
        IQueryable<MessageTemplate> GetMessageTemplatesByName(string messageTemplateName);

        /// <summary>
        /// Get MessageTemplate by MessageTemplateId
        /// </summary>
        /// <param name="messageTemplateId"></param>
        /// <returns></returns>
        MessageTemplate GetMessageTemplateById(int messageTemplateId);

        /// <summary>
        /// Get MessageTemplate by MessageTemplateId using async
        /// </summary>
        /// <param name="messageTemplateId"></param>
        /// <returns></returns>
        Task<MessageTemplate> GetMessageTemplateByIdAsync(int messageTemplateId);

        /// <summary>
        /// Insert/Update MessageTemplate entity
        /// </summary>
        /// <param name="messageTemplate"></param>
        /// <returns></returns>
        bool SaveOrUpdateMessageTemplateDetails(MessageTemplate messageTemplate);

        /// <summary>
        /// Delete a record by MessageTemplateId
        /// </summary>
        /// <param name="messageTemplateId"></param>
        /// <returns></returns>
        bool DeleteById(int messageTemplateId);

        /// <summary>
        /// Check duplicate MessageTemplate by Name
        /// </summary>
        /// <param name="messageTemplate"></param>
        /// <param name="messageTemplateModel"></param>
        /// <returns></returns>
        string CheckMessageTemplateExistByName(MessageTemplate messageTemplate, MessageTemplateModel messageTemplateModel);

        /// <summary>
        /// List of MessageTemplate with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<MessageTemplateViewModel> GetMessageTemplatesByCriteria(SearchMessageTemplateRequest searchModel);
    }
}