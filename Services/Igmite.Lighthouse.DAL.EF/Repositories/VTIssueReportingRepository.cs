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
    /// Repository of the VTIssueReporting entity
    /// </summary>
    public class VTIssueReportingRepository : GenericRepository<VTIssueReporting>, IVTIssueReportingRepository
    {
        /// <summary>
        /// Get list of VTIssueReporting
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTIssueReporting> GetVTIssueReportings()
        {
            return this.Context.VTIssueReportings.AsQueryable();
        }

        /// <summary>
        /// Get list of VTIssueReporting by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTIssueReporting> GetVTIssueReportingsByName(string name)
        {
            var vtIssueReportings = (from v in this.Context.VTIssueReportings
                                     select v).AsQueryable();

            return vtIssueReportings;
        }

        /// <summary>
        /// Get VTIssueReporting by VTIssueReportingId
        /// </summary>
        /// <param name="vtIssueReportingId"></param>
        /// <returns></returns>
        public VTIssueReporting GetVTIssueReportingById(Guid vtIssueReportingId)
        {
            return this.Context.VTIssueReportings.FirstOrDefault(v => v.VTIssueReportingId == vtIssueReportingId);
        }

        /// <summary>
        /// Get VTIssueReporting by VTIssueReportingId using async
        /// </summary>
        /// <param name="vtIssueReportingId"></param>
        /// <returns></returns>
        public async Task<VTIssueReporting> GetVTIssueReportingByIdAsync(Guid vtIssueReportingId)
        {
            var vtIssueReporting = await (from v in this.Context.VTIssueReportings
                                          where v.VTIssueReportingId == vtIssueReportingId
                                          select v).FirstOrDefaultAsync();

            return vtIssueReporting;
        }

        /// <summary>
        /// Insert/Update VTIssueReporting entity
        /// </summary>
        /// <param name="vtIssueReporting"></param>
        /// <param name="issueApprovalHistory"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVTIssueReportingDetails(VTIssueReporting vtIssueReporting, IssueApprovalHistory issueApprovalHistory = null)
        {
            try
            {
                if (RequestType.New == vtIssueReporting.RequestType)
                {
                    Context.VTIssueReportings.Add(vtIssueReporting);
                }
                else
                {
                    Context.Entry<VTIssueReporting>(vtIssueReporting).State = EntityState.Modified;
                }

                if (issueApprovalHistory != null)
                {
                    Context.IssueApprovalHistories.Add(issueApprovalHistory);
                }

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateVTIssueReportingDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by VTIssueReportingId
        /// </summary>
        /// <param name="vtIssueReportingId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtIssueReportingId)
        {
            VTIssueReporting vtIssueReporting = this.Context.VTIssueReportings.FirstOrDefault(v => v.VTIssueReportingId == vtIssueReportingId);

            if (vtIssueReporting != null)
            {
                Context.Entry<VTIssueReporting>(vtIssueReporting).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VTIssueReporting by Name
        /// </summary>
        /// <param name="vtIssueReportingModel"></param>
        /// <returns></returns>
        public List<string> CheckVTIssueReportingExistByName(VTIssueReportingModel vtIssueReportingModel)
        {
            var errorMessageList = new List<string>();
            try
            {
                var issueReporting = this.Context.VTIssueReportings.Where(v => v.VTId == vtIssueReportingModel.VTId && v.IssueReportDate.Date == vtIssueReportingModel.IssueReportDate.Date).ToList();

                if (vtIssueReportingModel.RequestType == RequestType.New && issueReporting.Count > 0)
                {
                    var vtIssueItem = issueReporting.FirstOrDefault(x => x.MainIssue == vtIssueReportingModel.MainIssue && x.SubIssue == vtIssueReportingModel.SubIssue);

                    if (vtIssueItem != null)
                    {
                        errorMessageList.Add("Selected main & sub issue is already submitted");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > CheckVTIssueReportingExistByName", ex);
            }

            return errorMessageList;
        }

        /// <summary>}
        /// List of VTIssueReporting with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTIssueReportingViewModel> GetVTIssueReportingsByCriteria(SearchVTIssueReportingModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.UserTypeId };
            sqlParams[1] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[4] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.VTIssueReportingViewModels.FromSql<VTIssueReportingViewModel>("CALL GetVTIssueReportingsByCriteria (@userId, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}