using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Igmite.Lighthouse.BAL.Providers
{
    public class DashboardManager : GenericManager<AccountModel>, IDashboardManager
    {
        private readonly IDashboardRepository dashboradRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the Dashboard manager.
        /// </summary>
        /// <param name="dashboradRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public DashboardManager(IDashboardRepository _dashboradRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.dashboradRepository = _dashboradRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get Lighthouse dashboard data with filter criteria
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dashboardRequest"></param>
        /// <returns></returns>
        public IList<T> GetLighthouseDashboards<T>(DashboardDataRequest dashboardRequest)
        {
            return this.dashboradRepository.GetLighthouseDashboards<T>(dashboardRequest);
        }
    }
}