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
    /// Repository of the SiteHeader entity
    /// </summary>
    public class SiteHeaderRepository : GenericRepository<SiteHeader>, ISiteHeaderRepository
    {
        /// <summary>
        /// Get list of SiteHeader
        /// </summary>
        /// <returns></returns>
        public IQueryable<SiteHeader> GetSiteHeaders()
        {
            return this.Context.SiteHeaders.AsQueryable();
        }

        /// <summary>
        /// Get list of SiteHeader by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<SiteHeader> GetSiteHeadersByName(string name)
        {
            var siteHeaders = (from s in this.Context.SiteHeaders
                         where s.ShortName.Contains(name)
                         select s).AsQueryable();

            return siteHeaders;
        }

        /// <summary>
        /// Get SiteHeader by SiteHeaderId
        /// </summary>
        /// <param name="siteHeaderId"></param>
        /// <returns></returns>
        public SiteHeader GetSiteHeaderById(Guid siteHeaderId)
        {
            return this.Context.SiteHeaders.FirstOrDefault(s => s.SiteHeaderId == siteHeaderId);
        }

        /// <summary>
        /// Get SiteHeader by SiteHeaderId using async
        /// </summary>
        /// <param name="siteHeaderId"></param>
        /// <returns></returns>
        public async Task<SiteHeader> GetSiteHeaderByIdAsync(Guid siteHeaderId)
        {
            var siteHeader = await (from s in this.Context.SiteHeaders
                              where s.SiteHeaderId == siteHeaderId
                              select s).FirstOrDefaultAsync();

            return siteHeader;
        }

        /// <summary>
        /// Insert/Update SiteHeader entity
        /// </summary>
        /// <param name="siteHeader"></param>
        /// <returns></returns>
        public bool SaveOrUpdateSiteHeaderDetails(SiteHeader siteHeader)
        {
            if (RequestType.New == siteHeader.RequestType)
                Context.SiteHeaders.Add(siteHeader);
            else
            {
                Context.Entry<SiteHeader>(siteHeader).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by SiteHeaderId
        /// </summary>
        /// <param name="siteHeaderId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid siteHeaderId)
        {
            SiteHeader siteHeader = this.Context.SiteHeaders.FirstOrDefault(s => s.SiteHeaderId == siteHeaderId);

            if (siteHeader != null)
            {
                Context.Entry<SiteHeader>(siteHeader).State = EntityState.Deleted;

                //IList<Guid> subHeaderIds = siteHeader.SubHeaders.Select(s => s.SiteSubHeaderId).ToList();
                //foreach (Guid subId in subHeaderIds)
                //{
                //    SiteSubHeader subHeader = siteHeader.SubHeaders.FirstOrDefault(s => s.SiteSubHeaderId == subId);
                //    Context.Entry<SiteSubHeader>(subHeader).State = EntityState.Deleted;
                //}

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate SiteHeader by Name
        /// </summary>
        /// <param name="siteHeaderModel"></param>
        /// <returns></returns>
        public bool CheckSiteHeaderExistByName(SiteHeaderModel siteHeaderModel)
        {
            SiteHeader siteHeader = this.Context.SiteHeaders.FirstOrDefault(s => s.ShortName == siteHeaderModel.ShortName);

            return siteHeader != null;
        }

        /// <summary>}
        /// List of SiteHeader with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<SiteHeaderViewModel> GetSiteHeadersByCriteria(SearchSiteHeaderModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.SiteHeaderViewModels.FromSql<SiteHeaderViewModel>("CALL GetSiteHeadersByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
