using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the SiteHeader entity
    /// </summary>
    public interface ISiteHeaderRepository : IGenericRepository<SiteHeader>
    {
        /// <summary>
        /// Get list of SiteHeader
        /// </summary>
        /// <returns></returns>
        IQueryable<SiteHeader> GetSiteHeaders();

        /// <summary>
        /// Get list of SiteHeader by siteHeaderName
        /// </summary>
        /// <param name="siteHeaderName"></param>
        /// <returns></returns>
        IQueryable<SiteHeader> GetSiteHeadersByName(string siteHeaderName);

        /// <summary>
        /// Get SiteHeader by SiteHeaderId
        /// </summary>
        /// <param name="siteHeaderId"></param>
        /// <returns></returns>
        SiteHeader GetSiteHeaderById(Guid siteHeaderId);

        /// <summary>
        /// Get SiteHeader by SiteHeaderId using async
        /// </summary>
        /// <param name="siteHeaderId"></param>
        /// <returns></returns>
        Task<SiteHeader> GetSiteHeaderByIdAsync(Guid siteHeaderId);

        /// <summary>
        /// Insert/Update SiteHeader entity
        /// </summary>
        /// <param name="siteHeader"></param>
        /// <returns></returns>
        bool SaveOrUpdateSiteHeaderDetails(SiteHeader siteHeader);

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
