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
    /// Repository of the VCIssueReporting entity
    /// </summary>
    public class VCIssueReportingRepository : GenericRepository<VCIssueReporting>, IVCIssueReportingRepository
    {
        /// <summary>
        /// Get list of VCIssueReporting
        /// </summary>
        /// <returns></returns>
        public IQueryable<VCIssueReporting> GetVCIssueReportings()
        {
            return this.Context.VCIssueReportings.AsQueryable();
        }

        /// <summary>
        /// Get list of VCIssueReporting by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VCIssueReporting> GetVCIssueReportingsByName(string name)
        {
            var vcIssueReportings = (from v in this.Context.VCIssueReportings
                                     select v).AsQueryable();

            return vcIssueReportings;
        }

        /// <summary>
        /// Get VCIssueReporting by VCIssueReportingId
        /// </summary>
        /// <param name="vcIssueReportingId"></param>
        /// <returns></returns>
        public VCIssueReporting GetVCIssueReportingById(Guid vcIssueReportingId)
        {
            return this.Context.VCIssueReportings.FirstOrDefault(v => v.VCIssueReportingId == vcIssueReportingId);
        }

        /// <summary>
        /// Get VCIssueReporting by VCIssueReportingId using async
        /// </summary>
        /// <param name="vcIssueReportingId"></param>
        /// <returns></returns>
        public async Task<VCIssueReporting> GetVCIssueReportingByIdAsync(Guid vcIssueReportingId)
        {
            var vcIssueReporting = await (from v in this.Context.VCIssueReportings
                                          where v.VCIssueReportingId == vcIssueReportingId
                                          select v).FirstOrDefaultAsync();

            return vcIssueReporting;
        }

        /// <summary>
        /// Insert/Update VCIssueReporting entity
        /// </summary>
        /// <param name="vcIssueReporting"></param>
        /// <param name="issueApprovalHistory"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVCIssueReportingDetails(VCIssueReporting vcIssueReporting, IssueApprovalHistory issueApprovalHistory = null)
        {
            try
            {
                if (RequestType.New == vcIssueReporting.RequestType)
                {
                    Context.VCIssueReportings.Add(vcIssueReporting);
                }
                else
                {
                    Context.Entry<VCIssueReporting>(vcIssueReporting).State = EntityState.Modified;
                }

                if (issueApprovalHistory != null)
                {
                    Context.IssueApprovalHistories.Add(issueApprovalHistory);
                }

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateVCIssueReportingDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by VCIssueReportingId
        /// </summary>
        /// <param name="vcIssueReportingId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vcIssueReportingId)
        {
            VCIssueReporting vcIssueReporting = this.Context.VCIssueReportings.FirstOrDefault(v => v.VCIssueReportingId == vcIssueReportingId);

            if (vcIssueReporting != null)
            {
                Context.Entry<VCIssueReporting>(vcIssueReporting).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VCIssueReporting by Name
        /// </summary>
        /// <param name="vcIssueReportingModel"></param>
        /// <returns></returns>
        public List<string> CheckVCIssueReportingExistByName(VCIssueReportingModel vcIssueReportingModel)
        {
            var errorMessageList = new List<string>();

            try
            {
                var issueReporting = this.Context.VCIssueReportings.Where(v => v.VCId == vcIssueReportingModel.VCId && v.IssueReportDate.Date == vcIssueReportingModel.IssueReportDate.Date).ToList();

                if (vcIssueReportingModel.RequestType == RequestType.New && issueReporting.Count > 0)
                {
                    var vcIssueItem = issueReporting.FirstOrDefault(x => x.MainIssue == vcIssueReportingModel.MainIssue && x.SubIssue == vcIssueReportingModel.SubIssue);

                    if (vcIssueItem != null)
                    {
                        errorMessageList.Add("Selected main & sub issue is already submitted");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > CheckVCIssueReportingExistByName", ex);
            }

            return errorMessageList;
        }

        /// <summary>}
        /// List of VCIssueReporting with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VCIssueReportingViewModel> GetVCIssueReportingsByCriteria(SearchVCIssueReportingModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.UserTypeId };
            sqlParams[1] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[4] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.VCIssueReportingViewModels.FromSql<VCIssueReportingViewModel>("CALL GetVCIssueReportingsByCriteria (@userId, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}