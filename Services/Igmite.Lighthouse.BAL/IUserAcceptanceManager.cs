using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the UserAcceptance entity
    /// </summary>
    public interface IUserAcceptanceManager : IGenericManager<UserAcceptanceModel>
    {
        /// <summary>
        /// Get list of UserAcceptances
        /// </summary>
        /// <returns></returns>
        IQueryable<UserAcceptanceModel> GetUserAcceptances();

        /// <summary>
        /// Get list of UserAcceptances by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<UserAcceptanceModel> GetUserAcceptancesByName(string userAcceptanceName);

        /// <summary>
        /// Get UserAcceptance by UserAcceptanceId
        /// </summary>
        /// <param name="userAcceptanceId"></param>
        /// <returns></returns>
        UserAcceptanceModel GetUserAcceptanceById(Guid userAcceptanceId);

        /// <summary>
        /// Get UserAcceptance by UserAcceptanceId using async
        /// </summary>
        /// <param name="userAcceptanceId"></param>
        /// <returns></returns>
        Task<UserAcceptanceModel> GetUserAcceptanceByIdAsync(Guid userAcceptanceId);

        /// <summary>
        /// Insert/Update UserAcceptance entity
        /// </summary>
        /// <param name="userAcceptanceModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateUserAcceptanceDetails(UserAcceptanceModel userAcceptanceModel);

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
