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
    /// Repository of the VCSchoolVisit entity
    /// </summary>
    public class VCSchoolVisitRepository : GenericRepository<VCSchoolVisit>, IVCSchoolVisitRepository
    {
        /// <summary>
        /// Get list of VCSchoolVisit
        /// </summary>
        /// <returns></returns>
        public IQueryable<VCSchoolVisit> GetVCSchoolVisits()
        {
            return this.Context.VCSchoolVisits.AsQueryable();
        }

        /// <summary>
        /// Get list of VCSchoolVisit by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VCSchoolVisit> GetVCSchoolVisitsByName(string name)
        {
            var vcSchoolVisits = (from v in this.Context.VCSchoolVisits
                                  select v).AsQueryable();

            return vcSchoolVisits;
        }

        /// <summary>
        /// Get VCSchoolVisit by VCSchoolVisitId
        /// </summary>
        /// <param name="vcSchoolVisitId"></param>
        /// <returns></returns>
        public VCSchoolVisit GetVCSchoolVisitById(Guid vcSchoolVisitId)
        {
            return this.Context.VCSchoolVisits.FirstOrDefault(v => v.VCSchoolVisitId == vcSchoolVisitId);
        }

        /// <summary>
        /// Get VCSchoolVisit by VCSchoolVisitId using async
        /// </summary>
        /// <param name="vcSchoolVisitId"></param>
        /// <returns></returns>
        public async Task<VCSchoolVisit> GetVCSchoolVisitByIdAsync(Guid vcSchoolVisitId)
        {
            var vcSchoolVisit = await (from v in this.Context.VCSchoolVisits
                                       where v.VCSchoolVisitId == vcSchoolVisitId
                                       select v).FirstOrDefaultAsync();

            return vcSchoolVisit;
        }

        /// <summary>
        /// Insert/Update VCSchoolVisit entity
        /// </summary>
        /// <param name="vcSchoolVisit"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVCSchoolVisitDetails(VCSchoolVisit vcSchoolVisit)
        {
            try
            {
                if (RequestType.New == vcSchoolVisit.RequestType)
                    Context.VCSchoolVisits.Add(vcSchoolVisit);
                else
                {
                    Context.Entry<VCSchoolVisit>(vcSchoolVisit).State = EntityState.Modified;
                }

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateVCSchoolVisitDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by VCSchoolVisitId
        /// </summary>
        /// <param name="vcSchoolVisitId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vcSchoolVisitId)
        {
            VCSchoolVisit vcSchoolVisit = this.Context.VCSchoolVisits.FirstOrDefault(v => v.VCSchoolVisitId == vcSchoolVisitId);

            if (vcSchoolVisit != null)
            {
                Context.Entry<VCSchoolVisit>(vcSchoolVisit).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VCSchoolVisit by Name
        /// </summary>
        /// <param name="vcSchoolVisitModel"></param>
        /// <returns></returns>
        public string CheckVCSchoolVisitExistByName(VCSchoolVisitModel schoolVisitModel)
        {
            string errorMessage = string.Empty;

            try
            {
                if (schoolVisitModel.RequestType == RequestType.New)
                {
                    var schoolVisit = this.Context.VCSchoolVisits.FirstOrDefault(v => v.VCId == schoolVisitModel.VCId && v.ReportDate == schoolVisitModel.ReportDate);

                    if (schoolVisit != null)
                    {
                        errorMessage = "VC School Visit data is already submitted";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > CheckVCSchoolVisitExistByName", ex);
            }

            return errorMessage;
        }

        /// <summary>}
        /// List of VCSchoolVisit with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VCSchoolVisitViewModel> GetVCSchoolVisitsByCriteria(SearchVCSchoolVisitModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[5];
            sqlParams[0] = new MySqlParameter { ParameterName = "userId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.UserTypeId };
            sqlParams[1] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[4] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.VCSchoolVisitViewModels.FromSql<VCSchoolVisitViewModel>("CALL GetVCSchoolVisitsByCriteria (@userId, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}