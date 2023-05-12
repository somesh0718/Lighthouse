using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the UserAcceptance entity
    /// </summary>
    public interface IUserAcceptanceRepository : IGenericRepository<UserAcceptance>
    {
        /// <summary>
        /// Get list of UserAcceptance
        /// </summary>
        /// <returns></returns>
        IQueryable<UserAcceptance> GetUserAcceptances();

        /// <summary>
        /// Get list of UserAcceptance by userAcceptanceName
        /// </summary>
        /// <param name="userAcceptanceName"></param>
        /// <returns></returns>
        IQueryable<UserAcceptance> GetUserAcceptancesByName(string userAcceptanceName);

        /// <summary>
        /// Get UserAcceptance by UserAcceptanceId
        /// </summary>
        /// <param name="userAcceptanceId"></param>
        /// <returns></returns>
        UserAcceptance GetUserAcceptanceById(Guid userAcceptanceId);

        /// <summary>
        /// Get UserAcceptance by UserAcceptanceId using async
        /// </summary>
        /// <param name="userAcceptanceId"></param>
        /// <returns></returns>
        Task<UserAcceptance> GetUserAcceptanceByIdAsync(Guid userAcceptanceId);

        /// <summary>
        /// Insert/Update UserAcceptance entity
        /// </summary>
        /// <param name="userAcceptance"></param>
        /// <returns></returns>
        bool SaveOrUpdateUserAcceptanceDetails(UserAcceptance userAcceptance);

        /// <summary>
        /// Delete a record by UserAcceptanceId
        /// </summary>
        /// <param name="userAcceptanceId"></param>
        /// <returns></returns>
        bool DeleteById(Guid userAcceptanceId);

        /// <summary>
        /// Check duplicate UserAcceptance by Name
        /// </summary>
        /// <param name="userAcceptanceModel"></param>
        /// <returns></returns>
        bool CheckUserAcceptanceExistByName(UserAcceptanceModel userAcceptanceModel);
    }
}