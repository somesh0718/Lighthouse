using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the SiteHeader entity
    /// </summary>
    public interface ISiteHeaderManager : IGenericManager<SiteHeaderModel>
    {
        /// <summary>
        /// Get list of SiteHeaders
        /// </summary>
        /// <returns></returns>
        IQueryable<SiteHeaderModel> GetSiteHeaders();

        /// <summary>
        /// Get list of SiteHeaders by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<SiteHeaderModel> GetSiteHeadersByName(string siteHeaderName);

        /// <summary>
        /// Get SiteHeader by SiteHeaderId
        /// </summary>
        /// <param name="siteHeaderId"></param>
        /// <returns></returns>
        SiteHeaderModel GetSiteHeaderById(Guid siteHeaderId);

        /// <summary>
        /// Get SiteHeader by SiteHeaderId using async
        /// </summary>
        /// <param name="siteHeaderId"></param>
        /// <returns></returns>
        Task<SiteHeaderModel> GetSiteHeaderByIdAsync(Guid siteHeaderId);

        /// <summary>
        /// Insert/Update SiteHeader entity
        /// </summary>
        /// <param name="siteHeaderModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateSiteHeaderDetails(SiteHeaderModel siteHeaderModel);

        /// <summary>
        /// Delete a record by SiteHeaderId
        /// </summary>
        /// <param name="siteHeaderId"></param>
        /// <returns></returns>
        bool DeleteById(Guid siteHeaderId);

        /// <summary>
        /// Check duplicate SiteHeader by Name
        /// </summary>
        /// <param name="siteHeaderModel"></param>
        /// <returns></returns>
        bool CheckSiteHeaderExistByName(SiteHeaderModel siteHeaderModel);

        /// <summary>
        /// List of SiteHeader with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<SiteHeaderViewModel> GetSiteHeadersByCriteria(SearchSiteHeaderModel searchModel);
    }
}
