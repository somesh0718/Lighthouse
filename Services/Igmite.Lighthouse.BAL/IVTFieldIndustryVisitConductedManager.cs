using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VTFieldIndustryVisitConducted entity
    /// </summary>
    public interface IVTFieldIndustryVisitConductedManager : IGenericManager<VTFieldIndustryVisitConductedModel>
    {
        /// <summary>
        /// Get list of VTFieldIndustryVisitConducteds
        /// </summary>
        /// <returns></returns>
        IQueryable<VTFieldIndustryVisitConductedModel> GetVTFieldIndustryVisitConducteds();

        /// <summary>
        /// Get list of VTFieldIndustryVisitConducteds by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VTFieldIndustryVisitConductedModel> GetVTFieldIndustryVisitConductedsByName(string vtFieldIndustryVisitConductedName);

        /// <summary>
        /// Get VTFieldIndustryVisitConducted by VTFieldIndustryVisitConductedId
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedId"></param>
        /// <returns></returns>
        VTFieldIndustryVisitConductedModel GetVTFieldIndustryVisitConductedById(Guid vtFieldIndustryVisitConductedId);

        /// <summary>
        /// Get VTFieldIndustryVisitConducted by VTFieldIndustryVisitConductedId using async
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedId"></param>
        /// <returns></returns>
        Task<VTFieldIndustryVisitConductedModel> GetVTFieldIndustryVisitConductedByIdAsync(Guid vtFieldIndustryVisitConductedId);

        /// <summary>
        /// Insert/Update VTFieldIndustryVisitConducted entity
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVTFieldIndustryVisitConductedDetails(VTFieldIndustryVisitConductedModel vtFieldIndustryVisitConductedModel);

        /// <summary>
        /// Delete a record by VTFieldIndustryVisitConductedId
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtFieldIndustryVisitConductedId);

        /// <summary>
        /// Check duplicate VTFieldIndustryVisitConducted by Name
        /// </summary>
        /// <param name="vtFieldIndustryVisitConductedModel"></param>
        /// <returns></returns>
        List<string> CheckVTFieldIndustryVisitConductedExistByName(VTFieldIndustryVisitConductedModel vtFieldIndustryVisitConductedModel);

        /// <summary>
        /// List of VTFieldIndustryVisitConducted with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTFieldIndustryVisitConductedViewModel> GetVTFieldIndustryVisitConductedsByCriteria(SearchVTFieldIndustryVisitConductedModel searchModel);

        /// <summary>
        /// Approved VT Field Industry Visit Conducted
        /// </summary>
        /// <param name="vtApprovalRequest"></param>
        /// <returns></returns>
        SingularResponse<string> ApprovedVTFieldIndustry(VTFieldIndustryApprovalRequest vtApprovalRequest);
    }
}