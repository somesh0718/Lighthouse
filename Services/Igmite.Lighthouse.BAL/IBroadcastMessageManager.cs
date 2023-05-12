using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the BroadcastMessage entity
    /// </summary>
    public interface IBroadcastMessageManager : IGenericManager<BroadcastMessageModel>
    {
        /// <summary>
        /// Get list of BroadcastMessages
        /// </summary>
        /// <returns></returns>
        IQueryable<BroadcastMessageModel> GetBroadcastMessages();

        /// <summary>
        /// Get list of BroadcastMessages by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<BroadcastMessageModel> GetBroadcastMessagesByName(string BroadcastMessageName);

        /// <summary>
        /// Get BroadcastMessage by BroadcastMessageId
        /// </summary>
        /// <param name="BroadcastMessageId"></param>
        /// <returns></returns>
        BroadcastMessageModel GetBroadcastMessageById(Guid BroadcastMessageId);

        /// <summary>
        /// Get BroadcastMessage by broadcastMessageId using async
        /// </summary>
        /// <param name="broadcastMessageId"></param>
        /// <returns></returns>
        Task<BroadcastMessageModel> GetBroadcastMessageByIdAsync(Guid broadcastMessageId);

        /// <summary>
        /// Insert/Update BroadcastMessage entity
        /// </summary>
        /// <param name="BroadcastMessageModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateBroadcastMessageDetails(BroadcastMessageModel BroadcastMessageModel);

        /// <summary>
        /// Delete a record by broadcastMessageId
        /// </summary>
        /// <param name="broadcastMessageId"></param>
        /// <returns></returns>
        bool DeleteById(Guid broadcastMessageId);

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
