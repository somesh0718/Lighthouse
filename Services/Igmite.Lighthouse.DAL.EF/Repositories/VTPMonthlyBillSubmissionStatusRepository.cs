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
    /// Repository of the VTPMonthlyBillSubmissionStatus entity
    /// </summary>
    public class VTPMonthlyBillSubmissionStatusRepository : GenericRepository<VTPMonthlyBillSubmissionStatus>, IVTPMonthlyBillSubmissionStatusRepository
    {
        /// <summary>
        /// Get list of VTPMonthlyBillSubmissionStatus
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTPMonthlyBillSubmissionStatus> GetVTPMonthlyBillSubmissionStatus()
        {
            return this.Context.VTPMonthlyBillSubmissionStatus.AsQueryable();
        }

        /// <summary>
        /// Get list of VTPMonthlyBillSubmissionStatus by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTPMonthlyBillSubmissionStatus> GetVTPMonthlyBillSubmissionStatusByName(string name)
        {
            var vtpMonthlyBillSubmissionStatus = (from v in this.Context.VTPMonthlyBillSubmissionStatus
                                                  select v).AsQueryable();

            return vtpMonthlyBillSubmissionStatus;
        }

        /// <summary>
        /// Get VTPMonthlyBillSubmissionStatus by VTPMonthlyBillSubmissionStatusId
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusId"></param>
        /// <returns></returns>
        public VTPMonthlyBillSubmissionStatus GetVTPMonthlyBillSubmissionStatusById(Guid vtpMonthlyBillSubmissionStatusId)
        {
            return this.Context.VTPMonthlyBillSubmissionStatus.FirstOrDefault(v => v.VTPMonthlyBillSubmissionStatusId == vtpMonthlyBillSubmissionStatusId);
        }

        /// <summary>
        /// Get VTPMonthlyBillSubmissionStatus by VTPMonthlyBillSubmissionStatusId using async
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusId"></param>
        /// <returns></returns>
        public async Task<VTPMonthlyBillSubmissionStatus> GetVTPMonthlyBillSubmissionStatusByIdAsync(Guid vtpMonthlyBillSubmissionStatusId)
        {
            var vtpMonthlyBillSubmissionStatus = await (from v in this.Context.VTPMonthlyBillSubmissionStatus
                                                        where v.VTPMonthlyBillSubmissionStatusId == vtpMonthlyBillSubmissionStatusId
                                                        select v).FirstOrDefaultAsync();

            return vtpMonthlyBillSubmissionStatus;
        }

        /// <summary>
        /// Insert/Update VTPMonthlyBillSubmissionStatus entity
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatus"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVTPMonthlyBillSubmissionStatusDetails(VTPMonthlyBillSubmissionStatus vtpMonthlyBillSubmissionStatus)
        {
            if (RequestType.New == vtpMonthlyBillSubmissionStatus.RequestType)
                Context.VTPMonthlyBillSubmissionStatus.Add(vtpMonthlyBillSubmissionStatus);
            else
            {
                Context.Entry<VTPMonthlyBillSubmissionStatus>(vtpMonthlyBillSubmissionStatus).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by VTPMonthlyBillSubmissionStatusId
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtpMonthlyBillSubmissionStatusId)
        {
            VTPMonthlyBillSubmissionStatus vtpMonthlyBillSubmissionStatus = this.Context.VTPMonthlyBillSubmissionStatus.FirstOrDefault(v => v.VTPMonthlyBillSubmissionStatusId == vtpMonthlyBillSubmissionStatusId);

            if (vtpMonthlyBillSubmissionStatus != null)
            {
                Context.Entry<VTPMonthlyBillSubmissionStatus>(vtpMonthlyBillSubmissionStatus).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VTPMonthlyBillSubmissionStatus by Name
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusModel"></param>
        /// <returns></returns>
        public bool CheckVTPMonthlyBillSubmissionStatusExistByName(VTPMonthlyBillSubmissionStatusModel vtpMonthlyBillSubmissionStatusModel)
        {
            VTPMonthlyBillSubmissionStatus vtpMonthlyBillSubmissionStatus = this.Context.VTPMonthlyBillSubmissionStatus.FirstOrDefault(v => v.VCId == vtpMonthlyBillSubmissionStatusModel.VCId && v.Month == vtpMonthlyBillSubmissionStatusModel.Month && v.DateSubmission == vtpMonthlyBillSubmissionStatusModel.DateSubmission);

            return vtpMonthlyBillSubmissionStatus != null;
        }

        /// <summary>}
        /// List of VTPMonthlyBillSubmissionStatus with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTPMonthlyBillSubmissionStatusViewModel> GetVTPMonthlyBillSubmissionStatusByCriteria(SearchVTPMonthlyBillSubmissionStatusModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.VTPMonthlyBillSubmissionStatusViewModels.FromSql<VTPMonthlyBillSubmissionStatusViewModel>("CALL GetVTPMonthlyBillSubmissionStatusByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}