using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the SiteSubHeader entity
    /// </summary>
    public interface ISiteSubHeaderManager : IGenericManager<SiteSubHeaderModel>
    {
        /// <summary>
        /// Get list of SiteSubHeaders
        /// </summary>
        /// <returns></returns>
        IQueryable<SiteSubHeaderModel> GetSiteSubHeaders();

        /// <summary>
        /// Get list of SiteSubHeaders by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<SiteSubHeaderModel> GetSiteSubHeadersByName(string siteSubHeaderName);

        /// <summary>
        /// Get SiteSubHeader by SiteSubHeaderId
        /// </summary>
        /// <param name="siteSubHeaderId"></param>
        /// <returns></returns>
        SiteSubHeaderModel GetSiteSubHeaderById(Guid siteSubHeaderId);

        /// <summary>
        /// Get SiteSubHeader by SiteSubHeaderId using async
        /// </summary>
        /// <param name="siteSubHeaderId"></param>
        /// <returns></returns>
        Task<SiteSubHeaderModel> GetSiteSubHeaderByIdAsync(Guid siteSubHeaderId);

        /// <summary>
        /// Insert/Update SiteSubHeader entity
        /// </summary>
        /// <param name="siteSubHeaderModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateSiteSubHeaderDetails(SiteSubHeaderModel siteSubHeaderModel);

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
