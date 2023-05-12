using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System.Collections.Generic;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Lighthouse Dashboard services
    /// </summary>
    public interface IDashboardRepository : IGenericRepository<Account>
    {
        /// <summary>
        /// Get Lighthouse dashboard data with filter criteria
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dashboardRequest"></param>
        /// <returns></returns>
        IList<T> GetLighthouseDashboards<T>(DashboardDataRequest dashboardRequest);
    }
}