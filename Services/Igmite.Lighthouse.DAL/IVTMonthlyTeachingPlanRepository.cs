using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the VTMonthlyTeachingPlan entity
    /// </summary>
    public interface IVTMonthlyTeachingPlanRepository : IGenericRepository<VTMonthlyTeachingPlan>
    {
        /// <summary>
        /// Get list of VTMonthlyTeachingPlan
        /// </summary>
        /// <returns></returns>
        IQueryable<VTMonthlyTeachingPlan> GetVTMonthlyTeachingPlans();

        /// <summary>
        /// Get list of VTMonthlyTeachingPlan by vtMonthlyTeachingPlanName
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanName"></param>
        /// <returns></returns>
        IQueryable<VTMonthlyTeachingPlan> GetVTMonthlyTeachingPlansByName(string vtMonthlyTeachingPlanName);

        /// <summary>
        /// Get VTMonthlyTeachingPlan by VTMonthlyTeachingPlanId
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanId"></param>
        /// <returns></returns>
        VTMonthlyTeachingPlan GetVTMonthlyTeachingPlanById(Guid vtMonthlyTeachingPlanId);

        /// <summary>
        /// Get VTMonthlyTeachingPlan by VTMonthlyTeachingPlanId using async
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanId"></param>
        /// <returns></returns>
        Task<VTMonthlyTeachingPlan> GetVTMonthlyTeachingPlanByIdAsync(Guid vtMonthlyTeachingPlanId);

        /// <summary>
        /// Insert/Update VTMonthlyTeachingPlan entity
        /// </summary>
        /// <param name="vtMonthlyTeachingPlan"></param>
        /// <returns></returns>
        bool SaveOrUpdateVTMonthlyTeachingPlanDetails(VTMonthlyTeachingPlan vtMonthlyTeachingPlan);

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
