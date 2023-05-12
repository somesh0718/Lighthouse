 
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the ComplaintRegistration entity
    /// </summary>
    public interface IComplaintRegistrationManager : IGenericManager<ComplaintRegistrationModel>
    {
        /// <summary>
        /// Get list of ComplaintRegistrations
        /// </summary>
        /// <returns></returns>
        IQueryable<ComplaintRegistrationModel> GetComplaintRegistrations();

        /// <summary>
        /// Get list of ComplaintRegistrations by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<ComplaintRegistrationModel> GetComplaintRegistrationsByName(string complaintRegistrationName);

        /// <summary>
        /// Get ComplaintRegistration by ComplaintRegistrationId
        /// </summary>
        /// <param name="complaintRegistrationId"></param>
        /// <returns></returns>
        ComplaintRegistrationModel GetComplaintRegistrationById(Guid complaintRegistrationId);

        /// <summary>
        /// Get ComplaintRegistration by ComplaintRegistrationId using async
        /// </summary>
        /// <param name="complaintRegistrationId"></param>
        /// <returns></returns>
        Task<ComplaintRegistrationModel> GetComplaintRegistrationByIdAsync(Guid complaintRegistrationId);

        /// <summary>
        /// Insert/Update ComplaintRegistration entity
        /// </summary>
        /// <param name="complaintRegistrationModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateComplaintRegistrationDetails(ComplaintRegistrationModel complaintRegistrationModel);

        /// <summary>
        /// Delete a record by ComplaintRegistrationId
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