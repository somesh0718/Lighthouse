using Igmite.Lighthouse.Models;
using System.Collections.Generic;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Lighthouse Dashboard services
    /// </summary>
    public interface IDashboardManager : IGenericManager<AccountModel>
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