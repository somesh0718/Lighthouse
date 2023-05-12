using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the ComplaintRegistration entity
    /// </summary>
    public class ComplaintRegistrationRepository : GenericRepository<ComplaintRegistration>, IComplaintRegistrationRepository
    {
        /// <summary>
        /// Get list of ComplaintRegistration
        /// </summary>
        /// <returns></returns>
        public IQueryable<ComplaintRegistration> GetComplaintRegistrations()
        {
            return this.Context.ComplaintRegistrations.AsQueryable();
        }

        /// <summary>
        /// Get list of ComplaintRegistration by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<ComplaintRegistration> GetComplaintRegistrationsByName(string name)
        {
            var complaintRegistrations = (from d in this.Context.ComplaintRegistrations
                                          where d.Subject.Contains(name)
                                          select d).AsQueryable();

            return complaintRegistrations;
        }

        /// <summary>
        /// Get ComplaintRegistration by complaintRegistrationId
        /// </summary>
        /// <param name="complaintRegistrationId"></param>
        /// <returns></returns>
        public ComplaintRegistration GetComplaintRegistrationById(Guid complaintRegistrationId)
        {
            return this.Context.ComplaintRegistrations.FirstOrDefault(c => c.ComplaintRegistrationId == complaintRegistrationId);
        }

        /// <summary>
        /// Get ComplaintRegistration by complaintRegistrationId using async
        /// </summary>
        /// <param name="complaintRegistrationId"></param>
        /// <returns></returns>
        public async Task<ComplaintRegistration> GetComplaintRegistrationByIdAsync(Guid complaintRegistrationId)
        {
            var complaintRegistration = await (from d in this.Context.ComplaintRegistrations
                                               where d.ComplaintRegistrationId == complaintRegistrationId
                                               select d).FirstOrDefaultAsync();

            return complaintRegistration;
        }

        /// <summary>
        /// Insert/Update ComplaintRegistration entity
        /// </summary>
        /// <param name="complaintRegistration"></param>
        /// <returns></returns>
        public bool SaveOrUpdateComplaintRegistrationDetails(ComplaintRegistration complaintRegistration)
        {
            if (RequestType.New == complaintRegistration.RequestType)
                Context.ComplaintRegistrations.Add(complaintRegistration);
            else
            {
                Context.Entry<ComplaintRegistration>(complaintRegistration).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by ComplaintRegistrationCode
        /// </summary>
        /// <param name="complaintRegistrationCode"></param>
        /// <returns></returns>
        public bool DeleteById(Guid complaintRegistrationId)
        {
            ComplaintRegistration complaintRegistration = this.Context.ComplaintRegistrations.FirstOrDefault(d => d.ComplaintRegistrationId == complaintRegistrationId);

            if (complaintRegistration != null)
            {
                Context.Entry<ComplaintRegistration>(complaintRegistration).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate ComplaintRegistration by Name
        /// </summary>
        /// <param name="complaintRegistrationModel"></param>
        /// <returns></returns>
        public bool CheckComplaintRegistrationExistByName(ComplaintRegistrationModel complaintRegistrationModel)
        {
            ComplaintRegistration complaintRegistration = this.Context.ComplaintRegistrations.FirstOrDefault(d => d.UserType == complaintRegistrationModel.UserType && d.UserName == complaintRegistrationModel.UserName && d.EmailId == complaintRegistrationModel.EmailId && d.Subject == complaintRegistrationModel.Subject);

            return complaintRegistration != null;
        }

        /// <summary>}
        /// List of ComplaintRegistration with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<ComplaintRegistrationViewModel> GetComplaintRegistrationsByCriteria(SearchComplaintRegistrationModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.ComplaintRegistrationViewModels.FromSql<ComplaintRegistrationViewModel>("CALL GetComplaintRegistrationsByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}