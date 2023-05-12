using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the UserAcceptance entity
    /// </summary>
    public class UserAcceptanceRepository : GenericRepository<UserAcceptance>, IUserAcceptanceRepository
    {
        /// <summary>
        /// Get list of UserAcceptance
        /// </summary>
        /// <returns></returns>
        public IQueryable<UserAcceptance> GetUserAcceptances()
        {
            return this.Context.UserAcceptances.AsQueryable();
        }

        /// <summary>
        /// Get list of UserAcceptance by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<UserAcceptance> GetUserAcceptancesByName(string name)
        {
            var userAcceptances = (from u in this.Context.UserAcceptances
                                   select u).AsQueryable();

            return userAcceptances;
        }

        /// <summary>
        /// Get UserAcceptance by UserAcceptanceId
        /// </summary>
        /// <param name="userAcceptanceId"></param>
        /// <returns></returns>
        public UserAcceptance GetUserAcceptanceById(Guid userAcceptanceId)
        {
            return this.Context.UserAcceptances.FirstOrDefault(u => u.UserAcceptanceId == userAcceptanceId);
        }

        /// <summary>
        /// Get UserAcceptance by UserAcceptanceId using async
        /// </summary>
        /// <param name="userAcceptanceId"></param>
        /// <returns></returns>
        public async Task<UserAcceptance> GetUserAcceptanceByIdAsync(Guid userAcceptanceId)
        {
            var userAcceptance = await (from u in this.Context.UserAcceptances
                                        where u.UserAcceptanceId == userAcceptanceId
                                        select u).FirstOrDefaultAsync();

            return userAcceptance;
        }

        /// <summary>
        /// Insert/Update UserAcceptance entity
        /// </summary>
        /// <param name="userAcceptance"></param>
        /// <returns></returns>
        public bool SaveOrUpdateUserAcceptanceDetails(UserAcceptance userAcceptance)
        {
            if (RequestType.New == userAcceptance.RequestType)
                Context.UserAcceptances.Add(userAcceptance);
            else
            {
                Context.Entry<UserAcceptance>(userAcceptance).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by UserAcceptanceId
        /// </summary>
        /// <param name="userAcceptanceId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid userAcceptanceId)
        {
            UserAcceptance userAcceptance = this.Context.UserAcceptances.FirstOrDefault(u => u.UserAcceptanceId == userAcceptanceId);

            if (userAcceptance != null)
            {
                Context.Entry<UserAcceptance>(userAcceptance).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate UserAcceptance by Name
        /// </summary>
        /// <param name="userAcceptanceModel"></param>
        /// <returns></returns>
        public bool CheckUserAcceptanceExistByName(UserAcceptanceModel userAcceptanceModel)
        {
            UserAcceptance userAcceptance = this.Context.UserAcceptances.FirstOrDefault(u => u.CreatedOn == userAcceptanceModel.CreatedOn);

            return userAcceptance != null;
        }
    }
}