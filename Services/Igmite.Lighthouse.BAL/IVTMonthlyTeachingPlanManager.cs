using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the VTMonthlyTeachingPlan entity
    /// </summary>
    public interface IVTMonthlyTeachingPlanManager : IGenericManager<VTMonthlyTeachingPlanModel>
    {
        /// <summary>
        /// Get list of VTMonthlyTeachingPlans
        /// </summary>
        /// <returns></returns>
        IQueryable<VTMonthlyTeachingPlanModel> GetVTMonthlyTeachingPlans();

        /// <summary>
        /// Get list of VTMonthlyTeachingPlans by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<VTMonthlyTeachingPlanModel> GetVTMonthlyTeachingPlansByName(string vtMonthlyTeachingPlanName);

        /// <summary>
        /// Get VTMonthlyTeachingPlan by VTMonthlyTeachingPlanId
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanId"></param>
        /// <returns></returns>
        VTMonthlyTeachingPlanModel GetVTMonthlyTeachingPlanById(Guid vtMonthlyTeachingPlanId);

        /// <summary>
        /// Get VTMonthlyTeachingPlan by VTMonthlyTeachingPlanId using async
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanId"></param>
        /// <returns></returns>
        Task<VTMonthlyTeachingPlanModel> GetVTMonthlyTeachingPlanByIdAsync(Guid vtMonthlyTeachingPlanId);

        /// <summary>
        /// Insert/Update VTMonthlyTeachingPlan entity
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateVTMonthlyTeachingPlanDetails(VTMonthlyTeachingPlanModel vtMonthlyTeachingPlanModel);

        /// <summary>
        /// Delete a record by VTMonthlyTeachingPlanId
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanId"></param>
        /// <returns></returns>
        bool DeleteById(Guid vtMonthlyTeachingPlanId);

        /// <summary>
        /// Check duplicate VTMonthlyTeachingPlan by Name
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanModel"></param>
        /// <returns></returns>
        bool CheckVTMonthlyTeachingPlanExistByName(VTMonthlyTeachingPlanModel vtMonthlyTeachingPlanModel);

        /// <summary>
        /// List of VTMonthlyTeachingPlan with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<VTMonthlyTeachingPlanViewModel> GetVTMonthlyTeachingPlansByCriteria(SearchVTMonthlyTeachingPlanModel searchModel);
    }
}
