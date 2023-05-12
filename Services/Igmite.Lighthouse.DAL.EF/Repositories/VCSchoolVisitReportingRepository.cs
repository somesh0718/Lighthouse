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
    /// Repository of the VCSchoolVisitReporting entity
    /// </summary>
    public class VCSchoolVisitReportingRepository : GenericRepository<VCSchoolVisitReporting>, IVCSchoolVisitReportingRepository
    {
        /// <summary>
        /// Get list of VCSchoolVisitReporting
        /// </summary>
        /// <returns></returns>
        public IQueryable<VCSchoolVisitReporting> GetVCSchoolVisitReporting()
        {
            return this.Context.VCSchoolVisitReporting.AsQueryable();
        }

        /// <summary>
        /// Get list of VCSchoolVisitReporting by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VCSchoolVisitReporting> GetVCSchoolVisitReportingByName(string name)
        {
            var vcSchoolVisitReporting = (from s in this.Context.VCSchoolVisitReporting
                                          where s.CompanyName.Contains(name)
                                          select s).AsQueryable();

            return vcSchoolVisitReporting;
        }

        /// <summary>
        /// Get VCSchoolVisitReporting by VCSchoolVisitReportingId
        /// </summary>
        /// <param name="vcSchoolVisitReportingId"></param>
        /// <returns></returns>
        public VCSchoolVisitReporting GetVCSchoolVisitReportingById(Guid vcSchoolVisitReportingId)
        {
            return this.Context.VCSchoolVisitReporting.FirstOrDefault(s => s.VCSchoolVisitReportingId == vcSchoolVisitReportingId);
        }

        /// <summary>
        /// Get VCSchoolVisitReporting by VCSchoolVisitReportingId using async
        /// </summary>
        /// <param name="vcSchoolVisitReportingId"></param>
        /// <returns></returns>
        public async Task<VCSchoolVisitReporting> GetVCSchoolVisitReportingByIdAsync(Guid vcSchoolVisitReportingId)
        {
            var vcSchoolVisitReporting = await (from s in this.Context.VCSchoolVisitReporting
                                                where s.VCSchoolVisitReportingId == vcSchoolVisitReportingId
                                                select s).FirstOrDefaultAsync();

            return vcSchoolVisitReporting;
        }

        /// <summary>
        /// Insert/Update VCSchoolVisitReporting entity
        /// </summary>
        /// <param name="vcSchoolVisitReporting"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVCSchoolVisitReportingDetails(VCSchoolVisitReporting vcSchoolVisitReporting)
        {
            if (RequestType.New == vcSchoolVisitReporting.RequestType)
                Context.VCSchoolVisitReporting.Add(vcSchoolVisitReporting);
            else
            {
                Context.Entry<VCSchoolVisitReporting>(vcSchoolVisitReporting).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by VCSchoolVisitReportingId
        /// </summary>
        /// <param name="vcSchoolVisitReportingId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vcSchoolVisitReportingId)
        {
            VCSchoolVisitReporting vcSchoolVisitReporting = this.Context.VCSchoolVisitReporting.FirstOrDefault(s => s.VCSchoolVisitReportingId == vcSchoolVisitReportingId);

            if (vcSchoolVisitReporting != null)
            {
                Context.Entry<VCSchoolVisitReporting>(vcSchoolVisitReporting).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VCSchoolVisitReporting by Name
        /// </summary>
        /// <param name="vcSchoolVisitReportingModel"></param>
        /// <returns></returns>
        public bool CheckVCSchoolVisitReportingExistByName(VCSchoolVisitReportingModel vcSchoolVisitReportingModel)
        {
            VCSchoolVisitReporting vcSchoolVisitReporting = this.Context.VCSchoolVisitReporting.FirstOrDefault(s => s.VCId == vcSchoolVisitReportingModel.VCId && s.VTId == vcSchoolVisitReportingModel.VTId && s.SchoolId == vcSchoolVisitReportingModel.SchoolId && s.VisitDate.Value.Date == vcSchoolVisitReportingModel.VisitDate.Value.Date);

            return vcSchoolVisitReporting != null;
        }

        /// <summary>}
        /// List of VCSchoolVisitReporting with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VCSchoolVisitReportingViewModel> GetVCSchoolVisitReportingByCriteria(SearchVCSchoolVisitReportingModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.UserTypeId };
            sqlParams[1] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[4] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.VCSchoolVisitReportingViewModels.FromSql<VCSchoolVisitReportingViewModel>("CALL GetVCSchoolVisitReportingByCriteria (@userId, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}