 
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the ComplaintRegistration entity
    /// </summary>
    public interface IComplaintRegistrationRepository : IGenericRepository<ComplaintRegistration>
    {
        /// <summary>
        /// Get list of ComplaintRegistration
        /// </summary>
        /// <returns></returns>
        IQueryable<ComplaintRegistration> GetComplaintRegistrations();

        /// <summary>
        /// Get list of ComplaintRegistration by complaintRegistrationName
        /// </summary>
        /// <param name="complaintRegistrationName"></param>
        /// <returns></returns>
        IQueryable<ComplaintRegistration> GetComplaintRegistrationsByName(string complaintRegistrationName);

        /// <summary>
        /// Get ComplaintRegistration by complaintRegistrationId
        /// </summary>
        /// <param name="complaintRegistrationId"></param>
        /// <returns></returns>
        ComplaintRegistration GetComplaintRegistrationById(Guid complaintRegistrationId);

        /// <summary>
        /// Get ComplaintRegistration by complaintRegistrationId using async
        /// </summary>
        /// <param name="complaintRegistrationId"></param>
        /// <returns></returns>
        Task<ComplaintRegistration> GetComplaintRegistrationByIdAsync(Guid complaintRegistrationId);

        /// <summary>
        /// Insert/Update ComplaintRegistration entity
        /// </summary>
        /// <param name="complaintRegistration"></param>
        /// <returns></returns>
        bool SaveOrUpdateComplaintRegistrationDetails(ComplaintRegistration complaintRegistration);

        /// <summary>
        /// Delete a record by complaintRegistrationId
        /// </summary>
        /// <param name="complaintRegistrationId"></param>
        /// <returns></returns>
        bool DeleteById(Guid complaintRegistrationId);

        /// <summary>
        /// Check duplicate ComplaintRegistration by Name
        /// </summary>
        /// <param name="complaintRegistrationModel"></param>
        /// <returns></returns>
        bool CheckComplaintRegistrationExistByName(ComplaintRegistrationModel complaintRegistrationModel);

        /// <summary>
        /// List of ComplaintRegistration with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<ComplaintRegistrationViewModel> GetComplaintRegistrationsByCriteria(SearchComplaintRegistrationModel searchModel);
    }
}