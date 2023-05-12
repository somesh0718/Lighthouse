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
    /// Repository of the HMIssueReporting entity
    /// </summary>
    public class HMIssueReportingRepository : GenericRepository<HMIssueReporting>, IHMIssueReportingRepository
    {
        /// <summary>
        /// Get list of HMIssueReporting
        /// </summary>
        /// <returns></returns>
        public IQueryable<HMIssueReporting> GetHMIssueReportings()
        {
            return this.Context.HMIssueReportings.AsQueryable();
        }

        /// <summary>
        /// Get list of HMIssueReporting by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<HMIssueReporting> GetHMIssueReportingsByName(string name)
        {
            var hmIssueReportings = (from h in this.Context.HMIssueReportings
                                     select h).AsQueryable();

            return hmIssueReportings;
        }

        /// <summary>
        /// Get HMIssueReporting by HMIssueReportingId
        /// </summary>
        /// <param name="hmIssueReportingId"></param>
        /// <returns></returns>
        public HMIssueReporting GetHMIssueReportingById(Guid hmIssueReportingId)
        {
            return this.Context.HMIssueReportings.FirstOrDefault(h => h.HMIssueReportingId == hmIssueReportingId);
        }

        /// <summary>
        /// Get HMIssueReporting by HMIssueReportingId using async
        /// </summary>
        /// <param name="hmIssueReportingId"></param>
        /// <returns></returns>
        public async Task<HMIssueReporting> GetHMIssueReportingByIdAsync(Guid hmIssueReportingId)
        {
            var hmIssueReporting = await (from h in this.Context.HMIssueReportings
                                          where h.HMIssueReportingId == hmIssueReportingId
                                          select h).FirstOrDefaultAsync();

            return hmIssueReporting;
        }

        /// <summary>
        /// Insert/Update HMIssueReporting entity
        /// </summary>
        /// <param name="hmIssueReporting"></param>
        /// <param name="issueApprovalHistory"></param>
        /// <returns></returns>
        public bool SaveOrUpdateHMIssueReportingDetails(HMIssueReporting hmIssueReporting, IssueApprovalHistory issueApprovalHistory = null)
        {
            try
            {
                if (RequestType.New == hmIssueReporting.RequestType)
                {
                    Context.HMIssueReportings.Add(hmIssueReporting);
                }
                else
                {
                    Context.Entry<HMIssueReporting>(hmIssueReporting).State = EntityState.Modified;
                }

                if (issueApprovalHistory != null)
                {
                    Context.IssueApprovalHistories.Add(issueApprovalHistory);
                }

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateHMIssueReportingDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by HMIssueReportingId
        /// </summary>
        /// <param name="hmIssueReportingId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid hmIssueReportingId)
        {
            HMIssueReporting hmIssueReporting = this.Context.HMIssueReportings.FirstOrDefault(h => h.HMIssueReportingId == hmIssueReportingId);

            if (hmIssueReporting != null)
            {
                Context.Entry<HMIssueReporting>(hmIssueReporting).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate HMIssueReporting by Name
        /// </summary>
        /// <param name="hmIssueReportingModel"></param>
        /// <returns></returns>
        public List<string> CheckHMIssueReportingExistByName(HMIssueReportingModel hmIssueReportingModel)
        {
            var errorMessageList = new List<string>();

            try
            {
                var issueReporting = this.Context.HMIssueReportings.Where(v => v.HMId == hmIssueReportingModel.HMId && v.IssueReportDate.Date == hmIssueReportingModel.IssueReportDate.Date).ToList();

                if (hmIssueReportingModel.RequestType == RequestType.New && issueReporting.Count > 0)
                {
                    var hmIssueItem = issueReporting.FirstOrDefault(x => x.MainIssue == hmIssueReportingModel.MainIssue && x.SubIssue == hmIssueReportingModel.SubIssue);

                    if (hmIssueItem != null)
                    {
                        errorMessageList.Add("Selected main & sub issue is already submitted");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > CheckHMIssueReportingExistByName", ex);
            }

            return errorMessageList;
        }

        /// <summary>}
        /// List of HMIssueReporting with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<HMIssueReportingViewModel> GetHMIssueReportingsByCriteria(SearchHMIssueReportingModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.UserTypeId };
            sqlParams[1] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[4] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.HMIssueReportingViewModels.FromSql<HMIssueReportingViewModel>("CALL GetHMIssueReportingsByCriteria (@userId, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}