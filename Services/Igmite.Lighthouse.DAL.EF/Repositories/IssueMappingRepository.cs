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
    /// Repository of the IssueMapping entity
    /// </summary>
    public class IssueMappingRepository : GenericRepository<IssueMapping>, IIssueMappingRepository
    {
        /// <summary>
        /// Get list of IssueMapping
        /// </summary>
        /// <returns></returns>
        public IQueryable<IssueMapping> GetIssueMapping()
        {
            return this.Context.IssueMapping.AsQueryable();
        }

        /// <summary>
        /// Get list of IssueMapping by name
        /// </summary>
        /// <param name="messageText"></param>
        /// <returns></returns>
        public IQueryable<IssueMapping> GetIssueMappingByIssueId(string MainIssueId, string SubIssueId)
        {
            var IssueMapping = (from b in this.Context.IssueMapping
                                where b.MainIssueId == MainIssueId && b.SubIssueId == SubIssueId
                                select b).AsQueryable();

            return IssueMapping;
        }

        /// <summary>
        /// Get IssueMapping by IssueMappingId
        /// </summary>
        /// <param name="IssueMappingId"></param>
        /// <returns></returns>
        public IssueMapping GetIssueMappingById(Guid IssueMappingId)
        {
            return this.Context.IssueMapping.FirstOrDefault(h => h.IssueMappingId == IssueMappingId);
        }

        /// <summary>
        /// Get GetIssueMappingById by mainIssueId & subIssueId
        /// </summary>
        /// <param name="IssueMappingId"></param>
        /// <returns></returns>
        public SubIssue GetIssueMappingById(Guid mainIssueId, Guid subIssueId)
        {
            return this.Context.SubIssues.FirstOrDefault(h => h.MainIssueId == mainIssueId && h.SubIssueId == subIssueId);
        }

        /// <summary>
        /// Get IssueMapping by IssueMappingId using async
        /// </summary>
        /// <param name="IssueMappingId"></param>
        /// <returns></returns>
        public async Task<IssueMapping> GetIssueMappingByIdAsync(Guid IssueMappingId)
        {
            var IssueMapping = await (from h in this.Context.IssueMapping
                                      where h.IssueMappingId == IssueMappingId
                                      select h).FirstOrDefaultAsync();

            return IssueMapping;
        }

        /// <summary>
        /// Insert/Update IssueMapping entity
        /// </summary>
        /// <param name="IssueMapping"></param>
        /// <returns></returns>
        public bool SaveOrUpdateIssueMappingDetails(IssueMapping IssueMapping)
        {
            if (RequestType.New == IssueMapping.RequestType)
                Context.IssueMapping.Add(IssueMapping);
            else
            {
                Context.Entry<IssueMapping>(IssueMapping).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by IssueMappingId
        /// </summary>
        /// <param name="IssueMappingId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid IssueMappingId)
        {
            IssueMapping IssueMapping = this.Context.IssueMapping.FirstOrDefault(h => h.IssueMappingId == IssueMappingId);

            if (IssueMapping != null)
            {
                Context.Entry<IssueMapping>(IssueMapping).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate IssueMapping by Name
        /// </summary>
        /// <param name="IssueMappingModel"></param>
        /// <returns></returns>
        public string CheckIssueMappingExistByName(IssueMappingModel IssueMappingModel)
        {
            string errorMessage = string.Empty;

            if (IssueMappingModel.RequestType == RequestType.New)
            {
                IssueMapping IssueMapping = this.Context.IssueMapping.FirstOrDefault(h => h.IsActive == true && h.MainIssueId == IssueMappingModel.MainIssueId && h.SubIssueId == IssueMappingModel.SubIssueId);

                if (IssueMapping != null)
                {
                    errorMessage = string.Format("Record already exists.");
                }
            }

            return errorMessage;
        }

        /// <summary>
        /// List of IssueMapping with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IList<IssueMappingViewModel> GetIssueMappingByCriteria(SearchIssueMappingModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.UserTypeId };
            sqlParams[1] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[4] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.IssueMappingViewModels.FromSql<IssueMappingViewModel>("CALL GetIssueMappingByCriteria (@userId, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }

        /// <summary>
        /// List of Issue by userId with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IList<IssueViewModel> GetIssueByCriteria(SearchIssueModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[6];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.UserId };
            sqlParams[1] = new MySqlParameter { ParameterName = "reportedBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.ReportedBy };
            sqlParams[2] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[4] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex.IntValOrZero() };
            sqlParams[5] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize.IntValOrZero() };

            return Context.IssueViewModels.FromSql<IssueViewModel>("CALL GetIssueByCriteria (@userId, @reportedBy, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}