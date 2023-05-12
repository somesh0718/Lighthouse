using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the SiteSubHeader entity
    /// </summary>
    public class SiteSubHeaderRepository : GenericRepository<SiteSubHeader>, ISiteSubHeaderRepository
    {
        /// <summary>
        /// Get list of SiteSubHeader
        /// </summary>
        /// <returns></returns>
        public IQueryable<SiteSubHeader> GetSiteSubHeaders()
        {
            return this.Context.SiteSubHeaders.AsQueryable();
        }

        /// <summary>
        /// Get list of SiteSubHeader by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<SiteSubHeader> GetSiteSubHeadersByName(string name)
        {
            var siteSubHeaders = (from s in this.Context.SiteSubHeaders
                         select s).AsQueryable();

            return siteSubHeaders;
        }

        /// <summary>
        /// Get SiteSubHeader by SiteSubHeaderId
        /// </summary>
        /// <param name="siteSubHeaderId"></param>
        /// <returns></returns>
        public SiteSubHeader GetSiteSubHeaderById(Guid siteSubHeaderId)
        {
            return this.Context.SiteSubHeaders.FirstOrDefault(s => s.SiteSubHeaderId == siteSubHeaderId);
        }

        /// <summary>
        /// Get SiteSubHeader by SiteSubHeaderId using async
        /// </summary>
        /// <param name="siteSubHeaderId"></param>
        /// <returns></returns>
        public async Task<SiteSubHeader> GetSiteSubHeaderByIdAsync(Guid siteSubHeaderId)
        {
            var siteSubHeader = await (from s in this.Context.SiteSubHeaders
                              where s.SiteSubHeaderId == siteSubHeaderId
                              select s).FirstOrDefaultAsync();

            return siteSubHeader;
        }

        /// <summary>
        /// Insert/Update SiteSubHeader entity
        /// </summary>
        /// <param name="siteSubHeader"></param>
        /// <returns></returns>
        public bool SaveOrUpdateSiteSubHeaderDetails(SiteSubHeader siteSubHeader)
        {
            if (RequestType.New == siteSubHeader.RequestType)
                Context.SiteSubHeaders.Add(siteSubHeader);
            else
            {
                Context.Entry<SiteSubHeader>(siteSubHeader).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by SiteSubHeaderId
        /// </summary>
        /// <param name="siteSubHeaderId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid siteSubHeaderId)
        {
            SiteSubHeader siteSubHeader = this.Context.SiteSubHeaders.FirstOrDefault(s => s.SiteSubHeaderId == siteSubHeaderId);

            if (siteSubHeader != null)
            {
                Context.Entry<SiteSubHeader>(siteSubHeader).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate SiteSubHeader by Name
        /// </summary>
        /// <param name="siteSubHeaderModel"></param>
        /// <returns></returns>
        public bool CheckSiteSubHeaderExistByName(SiteSubHeaderModel siteSubHeaderModel)
        {
            SiteSubHeader siteSubHeader = this.Context.SiteSubHeaders.FirstOrDefault(s => s.SiteHeaderId == siteSubHeaderModel.SiteHeaderId && s.TransactionId == siteSubHeaderModel.TransactionId);

            return siteSubHeader != null;
        }

        /// <summary>}
        /// List of SiteSubHeader with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<SiteSubHeaderViewModel> GetSiteSubHeadersByCriteria(SearchSiteSubHeaderModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.SiteSubHeaderViewModels.FromSql<SiteSubHeaderViewModel>("CALL GetSiteSubHeadersByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
