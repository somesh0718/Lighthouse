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
    /// Repository of the VTMonthlyTeachingPlan entity
    /// </summary>
    public class VTMonthlyTeachingPlanRepository : GenericRepository<VTMonthlyTeachingPlan>, IVTMonthlyTeachingPlanRepository
    {
        /// <summary>
        /// Get list of VTMonthlyTeachingPlan
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTMonthlyTeachingPlan> GetVTMonthlyTeachingPlans()
        {
            return this.Context.VTMonthlyTeachingPlans.AsQueryable();
        }

        /// <summary>
        /// Get list of VTMonthlyTeachingPlan by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTMonthlyTeachingPlan> GetVTMonthlyTeachingPlansByName(string name)
        {
            var vtMonthlyTeachingPlans = (from v in this.Context.VTMonthlyTeachingPlans
                                          select v).AsQueryable();

            return vtMonthlyTeachingPlans;
        }

        /// <summary>
        /// Get VTMonthlyTeachingPlan by VTMonthlyTeachingPlanId
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanId"></param>
        /// <returns></returns>
        public VTMonthlyTeachingPlan GetVTMonthlyTeachingPlanById(Guid vtMonthlyTeachingPlanId)
        {
            return this.Context.VTMonthlyTeachingPlans.FirstOrDefault(v => v.VTMonthlyTeachingPlanId == vtMonthlyTeachingPlanId);
        }

        /// <summary>
        /// Get VTMonthlyTeachingPlan by VTMonthlyTeachingPlanId using async
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanId"></param>
        /// <returns></returns>
        public async Task<VTMonthlyTeachingPlan> GetVTMonthlyTeachingPlanByIdAsync(Guid vtMonthlyTeachingPlanId)
        {
            var vtMonthlyTeachingPlan = await (from v in this.Context.VTMonthlyTeachingPlans
                                               where v.VTMonthlyTeachingPlanId == vtMonthlyTeachingPlanId
                                               select v).FirstOrDefaultAsync();

            return vtMonthlyTeachingPlan;
        }

        /// <summary>
        /// Insert/Update VTMonthlyTeachingPlan entity
        /// </summary>
        /// <param name="vtMonthlyTeachingPlan"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVTMonthlyTeachingPlanDetails(VTMonthlyTeachingPlan vtMonthlyTeachingPlan)
        {
            if (RequestType.New == vtMonthlyTeachingPlan.RequestType)
                Context.VTMonthlyTeachingPlans.Add(vtMonthlyTeachingPlan);
            else
            {
                Context.Entry<VTMonthlyTeachingPlan>(vtMonthlyTeachingPlan).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by VTMonthlyTeachingPlanId
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtMonthlyTeachingPlanId)
        {
            VTMonthlyTeachingPlan vtMonthlyTeachingPlan = this.Context.VTMonthlyTeachingPlans.FirstOrDefault(v => v.VTMonthlyTeachingPlanId == vtMonthlyTeachingPlanId);

            if (vtMonthlyTeachingPlan != null)
            {
                Context.Entry<VTMonthlyTeachingPlan>(vtMonthlyTeachingPlan).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VTMonthlyTeachingPlan by Name
        /// </summary>
        /// <param name="vtMonthlyTeachingPlanModel"></param>
        /// <returns></returns>
        public bool CheckVTMonthlyTeachingPlanExistByName(VTMonthlyTeachingPlanModel vtMonthlyTeachingPlanModel)
        {
            VTMonthlyTeachingPlan vtMonthlyTeachingPlan = this.Context.VTMonthlyTeachingPlans.FirstOrDefault(v => v.Month == vtMonthlyTeachingPlanModel.Month && v.WeekStartDate == vtMonthlyTeachingPlanModel.WeekStartDate && v.WeekendDate == vtMonthlyTeachingPlanModel.WeekendDate);

            return vtMonthlyTeachingPlan != null;
        }

        /// <summary>}
        /// List of VTMonthlyTeachingPlan with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTMonthlyTeachingPlanViewModel> GetVTMonthlyTeachingPlansByCriteria(SearchVTMonthlyTeachingPlanModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.VTMonthlyTeachingPlanViewModels.FromSql<VTMonthlyTeachingPlanViewModel>("CALL GetVTMonthlyTeachingPlansByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}