using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the SiteSubHeader entity
    /// </summary>
    public interface ISiteSubHeaderRepository : IGenericRepository<SiteSubHeader>
    {
        /// <summary>
        /// Get list of SiteSubHeader
        /// </summary>
        /// <returns></returns>
        IQueryable<SiteSubHeader> GetSiteSubHeaders();

        /// <summary>
        /// Get list of SiteSubHeader by siteSubHeaderName
        /// </summary>
        /// <param name="siteSubHeaderName"></param>
        /// <returns></returns>
        IQueryable<SiteSubHeader> GetSiteSubHeadersByName(string siteSubHeaderName);

        /// <summary>
        /// Get SiteSubHeader by SiteSubHeaderId
        /// </summary>
        /// <param name="siteSubHeaderId"></param>
        /// <returns></returns>
        SiteSubHeader GetSiteSubHeaderById(Guid siteSubHeaderId);

        /// <summary>
        /// Get SiteSubHeader by SiteSubHeaderId using async
        /// </summary>
        /// <param name="siteSubHeaderId"></param>
        /// <returns></returns>
        Task<SiteSubHeader> GetSiteSubHeaderByIdAsync(Guid siteSubHeaderId);

        /// <summary>
        /// Insert/Update SiteSubHeader entity
        /// </summary>
        /// <param name="siteSubHeader"></param>
        /// <returns></returns>
        bool SaveOrUpdateSiteSubHeaderDetails(SiteSubHeader siteSubHeader);

        /// <summary>
        /// Delete a record by SiteSubHeaderId
        /// </summary>
        /// <param name="siteSubHeaderId"></param>
        /// <returns></returns>
        bool DeleteById(Guid siteSubHeaderId);

        /// <summary>
        /// Check duplicate SiteSubHeader by Name
        /// </summary>
        /// <param name="siteSubHeaderModel"></param>
        /// <returns></returns>
        bool CheckSiteSubHeaderExistByName(SiteSubHeaderModel siteSubHeaderModel);

        /// <summary>
        /// List of SiteSubHeader with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<SiteSubHeaderViewModel> GetSiteSubHeadersByCriteria(SearchSiteSubHeaderModel searchModel);
    }
}
