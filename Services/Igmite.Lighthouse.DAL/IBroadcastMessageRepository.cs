using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the BroadcastMessage entity
    /// </summary>
    public interface IBroadcastMessageRepository : IGenericRepository<BroadcastMessage>
    {
        /// <summary>
        /// Get list of BroadcastMessage
        /// </summary>
        /// <returns></returns>
        IQueryable<BroadcastMessage> GetBroadcastMessages();

        /// <summary>
        /// Get list of BroadcastMessage by BroadcastMessageName
        /// </summary>
        /// <param name="BroadcastMessageName"></param>
        /// <returns></returns>
        IQueryable<BroadcastMessage> GetBroadcastMessagesByName(string BroadcastMessageName);

        /// <summary>
        /// Get BroadcastMessage by BroadcastMessageId
        /// </summary>
        /// <param name="BroadcastMessageId"></param>
        /// <returns></returns>
        BroadcastMessage GetBroadcastMessageById(Guid BroadcastMessageId);

        /// <summary>
        /// Get BroadcastMessage by BroadcastMessageId using async
        /// </summary>
        /// <param name="BroadcastMessageId"></param>
        /// <returns></returns>
        Task<BroadcastMessage> GetBroadcastMessageByIdAsync(Guid BroadcastMessageId);

        /// <summary>
        /// Insert/Update BroadcastMessage entity
        /// </summary>
        /// <param name="BroadcastMessage"></param>
        /// <returns></returns>
        bool SaveOrUpdateBroadcastMessageDetails(BroadcastMessage BroadcastMessage);

        /// <summary>
        /// Delete a record by BroadcastMessageId
        /// </summary>
        /// <param name="BroadcastMessageId"></param>
        /// <returns></returns>
        bool DeleteById(Guid BroadcastMessageId);

        /// <summary>
        /// Check duplicate BroadcastMessage by Name
        /// </summary>
        /// <param name="BroadcastMessageModel"></param>
        /// <returns></returns>
        string CheckBroadcastMessageExistByName(BroadcastMessageModel BroadcastMessageModel);

        /// <summary>
        /// List of BroadcastMessage with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<BroadcastMessageViewModel> GetBroadcastMessagesByCriteria(SearchBroadcastMessageModel searchModel);
    }
}
