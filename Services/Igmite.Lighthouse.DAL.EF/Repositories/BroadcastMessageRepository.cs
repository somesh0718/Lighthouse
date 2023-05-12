using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the BroadcastMessage entity
    /// </summary>
    public class BroadcastMessageRepository : GenericRepository<BroadcastMessage>, IBroadcastMessageRepository
    {
        /// <summary>
        /// Get list of BroadcastMessage
        /// </summary>
        /// <returns></returns>
        public IQueryable<BroadcastMessage> GetBroadcastMessages()
        {
            return this.Context.BroadcastMessages.AsQueryable();
        }

        /// <summary>
        /// Get list of BroadcastMessage by name
        /// </summary>
        /// <param name="messageText"></param>
        /// <returns></returns>
        public IQueryable<BroadcastMessage> GetBroadcastMessagesByName(string messageText)
        {
            var broadcastMessages = (from b in this.Context.BroadcastMessages
                                     where b.MessageText.Contains(messageText)
                                     select b).AsQueryable();

            return broadcastMessages;
        }

        /// <summary>
        /// Get BroadcastMessage by BroadcastMessageId
        /// </summary>
        /// <param name="broadcastMessageId"></param>
        /// <returns></returns>
        public BroadcastMessage GetBroadcastMessageById(Guid broadcastMessageId)
        {
            return this.Context.BroadcastMessages.FirstOrDefault(h => h.BroadcastMessageId == broadcastMessageId);
        }

        /// <summary>
        /// Get BroadcastMessage by BroadcastMessageId using async
        /// </summary>
        /// <param name="BroadcastMessageId"></param>
        /// <returns></returns>
        public async Task<BroadcastMessage> GetBroadcastMessageByIdAsync(Guid broadcastMessageId)
        {
            var broadcastMessage = await (from h in this.Context.BroadcastMessages
                                    where h.BroadcastMessageId == broadcastMessageId
                                          select h).FirstOrDefaultAsync();

            return broadcastMessage;
        }

        /// <summary>
        /// Insert/Update BroadcastMessage entity
        /// </summary>
        /// <param name="BroadcastMessage"></param>
        /// <returns></returns>
        public bool SaveOrUpdateBroadcastMessageDetails(BroadcastMessage broadcastMessage)
        {
            if (RequestType.New == broadcastMessage.RequestType)
                Context.BroadcastMessages.Add(broadcastMessage);
            else
            {
                Context.Entry<BroadcastMessage>(broadcastMessage).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by BroadcastMessageId
        /// </summary>
        /// <param name="BroadcastMessageId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid broadcastMessageId)
        {
            BroadcastMessage broadcastMessage = this.Context.BroadcastMessages.FirstOrDefault(h => h.BroadcastMessageId == broadcastMessageId);

            if (broadcastMessage != null)
            {
                Context.Entry<BroadcastMessage>(broadcastMessage).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate BroadcastMessage by Name
        /// </summary>
        /// <param name="broadcastMessageModel"></param>
        /// <returns></returns>
        public string CheckBroadcastMessageExistByName(BroadcastMessageModel broadcastMessageModel)
        {
            string errorMessage = string.Empty;

            if (broadcastMessageModel.RequestType == RequestType.New)
            {
                BroadcastMessage broadcastMessage = this.Context.BroadcastMessages.FirstOrDefault(h => h.IsActive == true && h.BroadcastMessageId == broadcastMessageModel.BroadcastMessageId);

                if (broadcastMessage != null)
                {
                    errorMessage = string.Format("{0} - Record already exists.", broadcastMessage.MessageText);
                }
            }

            return errorMessage;
        }

        /// <summary>}
        /// List of BroadcastMessage with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<BroadcastMessageViewModel> GetBroadcastMessagesByCriteria(SearchBroadcastMessageModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.UserTypeId };
            sqlParams[1] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[4] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.BroadcastMessageViewModels.FromSql<BroadcastMessageViewModel>("CALL GetBroadcastMessagesByCriteria (@userId, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
