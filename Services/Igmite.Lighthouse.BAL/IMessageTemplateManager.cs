using Igmite.Lighthouse.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the MessageTemplate entity
    /// </summary>
    public interface IMessageTemplateManager : IGenericManager<MessageTemplateModel>
    {
        /// <summary>
        /// Get list of MessageTemplates
        /// </summary>
        /// <returns></returns>
        IQueryable<MessageTemplateModel> GetMessageTemplates();

        /// <summary>
        /// Get list of MessageTemplates by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<MessageTemplateModel> GetMessageTemplatesByName(string messageTemplateName);

        /// <summary>
        /// Get MessageTemplate by MessageTemplateId
        /// </summary>
        /// <param name="messageTemplateId"></param>
        /// <returns></returns>
        MessageTemplateModel GetMessageTemplateById(int messageTemplateId);

        /// <summary>
        /// Get MessageTemplate by MessageTemplateId using async
        /// </summary>
        /// <param name="messageTemplateId"></param>
        /// <returns></returns>
        Task<MessageTemplateModel> GetMessageTemplateByIdAsync(int messageTemplateId);

        /// <summary>
        /// Insert/Update MessageTemplate entity
        /// </summary>
        /// <param name="messageTemplateModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateMessageTemplateDetails(MessageTemplateModel messageTemplateModel);

        /// <summary>
        /// Delete a record by MessageTemplateId
        /// </summary>
        /// <param name="messageTemplateId"></param>
        /// <returns></returns>
        bool DeleteById(int messageTemplateId);

        /// <summary>
        /// List of MessageTemplate with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<MessageTemplateViewModel> GetMessageTemplatesByCriteria(SearchMessageTemplateRequest searchModel);
    }
}