using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the MessageTemplate entity
    /// </summary>
    public class MessageTemplateRepository : GenericRepository<MessageTemplate>, IMessageTemplateRepository
    {
        /// <summary>
        /// Get list of MessageTemplate
        /// </summary>
        /// <returns></returns>
        public IQueryable<MessageTemplate> GetMessageTemplates()
        {
            return this.Context.MessageTemplates.AsQueryable();
        }

        /// <summary>
        /// Get list of MessageTemplate by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<MessageTemplate> GetMessageTemplatesByName(string name)
        {
            var messageTemplates = (from r in this.Context.MessageTemplates
                                    where r.TemplateName.Contains(name)
                                    select r).AsQueryable();

            return messageTemplates;
        }

        /// <summary>
        /// Get MessageTemplate by MessageTemplateId
        /// </summary>
        /// <param name="messageTemplateId"></param>
        /// <returns></returns>
        public MessageTemplate GetMessageTemplateById(int messageTemplateId)
        {
            return this.Context.MessageTemplates.FirstOrDefault(r => r.MessageTemplateId == messageTemplateId);
        }

        /// <summary>
        /// Get MessageTemplate by MessageTemplateId using async
        /// </summary>
        /// <param name="messageTemplateId"></param>
        /// <returns></returns>
        public async Task<MessageTemplate> GetMessageTemplateByIdAsync(int messageTemplateId)
        {
            var messageTemplate = await (from r in this.Context.MessageTemplates
                                         where r.MessageTemplateId == messageTemplateId
                                         select r).FirstOrDefaultAsync();

            return messageTemplate;
        }

        /// <summary>
        /// Insert/Update MessageTemplate entity
        /// </summary>
        /// <param name="messageTemplate"></param>
        /// <returns></returns>
        public bool SaveOrUpdateMessageTemplateDetails(MessageTemplate messageTemplate)
        {
            if (RequestType.New == messageTemplate.RequestType)
                Context.MessageTemplates.Add(messageTemplate);
            else
            {
                Context.Entry<MessageTemplate>(messageTemplate).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by MessageTemplateId
        /// </summary>
        /// <param name="messageTemplateId"></param>
        /// <returns></returns>
        public bool DeleteById(int messageTemplateId)
        {
            MessageTemplate messageTemplate = this.Context.MessageTemplates.FirstOrDefault(r => r.MessageTemplateId == messageTemplateId);

            if (messageTemplate != null)
            {
                Context.Entry<MessageTemplate>(messageTemplate).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate MessageTemplate by Name
        /// </summary>
        /// <param name="messageTemplate"></param>
        /// <param name="messageTemplateModel"></param>
        /// <returns></returns>
        public string CheckMessageTemplateExistByName(MessageTemplate messageTemplate, MessageTemplateModel messageTemplateModel)
        {
            List<MessageTemplate> messageTemplates = this.Context.MessageTemplates.Where(m => m.IsActive && m.MessageTypeId == messageTemplateModel.MessageTypeId && m.MessageSubTypeId == messageTemplateModel.MessageSubTypeId).ToList();

            if (messageTemplateModel.RequestType == RequestType.New)
            {
                if (messageTemplates.Count > 0)
                {
                    return string.Format("Message Template is already exists for {0}. Please inactive current message template and add new message template again!!!", messageTemplateModel.MessageTypeId);
                }
                else
                {
                    MessageTemplate messageTemplateItem = messageTemplates.FirstOrDefault(r => r.TemplateFlowId == messageTemplateModel.TemplateFlowId);
                    if (messageTemplateItem != null)
                    {
                        return string.Format("Flow Id is already exists for {0}", messageTemplateModel.MessageTypeId);
                    }
                }
            }
            else if (messageTemplateModel.RequestType == RequestType.Edit)
            {
                MessageTemplate messageTemplateItem = messageTemplates.FirstOrDefault(r => r.TemplateFlowId == messageTemplateModel.TemplateFlowId);
                if (messageTemplateItem != null && !string.Equals(messageTemplate.TemplateFlowId, messageTemplateItem.TemplateFlowId))
                { 
                    return string.Format("Flow Id is already exists for different Message Template");
                }

                if (messageTemplates.Count > 0 && messageTemplate.IsActive == false && messageTemplateModel.IsActive)
                {
                    return string.Format("Message Template cannot be active because another template is already active");
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// List of MessageTemplate with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IList<MessageTemplateViewModel> GetMessageTemplatesByCriteria(SearchMessageTemplateRequest searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[6];
            sqlParams[0] = new MySqlParameter { ParameterName = "messageTypeId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.MessageTypeId };
            sqlParams[1] = new MySqlParameter { ParameterName = "status", MySqlDbType = MySqlDbType.Bool, Value = searchModel.Status };
            sqlParams[2] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name };
            sqlParams[3] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy };
            sqlParams[4] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex };
            sqlParams[5] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize };

            return Context.MessageTemplateViewModels.FromSql<MessageTemplateViewModel>("CALL GetMessageTemplatesByCriteria (@messageTypeId, @status, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}