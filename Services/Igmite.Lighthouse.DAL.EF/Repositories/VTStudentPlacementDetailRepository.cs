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
    /// Repository of the VTStudentPlacementDetail entity
    /// </summary>
    public class VTStudentPlacementDetailRepository : GenericRepository<VTStudentPlacementDetail>, IVTStudentPlacementDetailRepository
    {
        /// <summary>
        /// Get list of VTStudentPlacementDetail
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTStudentPlacementDetail> GetVTStudentPlacementDetails()
        {
            return this.Context.VTStudentPlacementDetails.AsQueryable();
        }

        /// <summary>
        /// Get list of VTStudentPlacementDetail by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTStudentPlacementDetail> GetVTStudentPlacementDetailsByName(string name)
        {
            var vtStudentPlacementDetails = (from v in this.Context.VTStudentPlacementDetails
                                             select v).AsQueryable();

            return vtStudentPlacementDetails;
        }

        /// <summary>
        /// Get VTStudentPlacementDetail by VTStudentPlacementDetailId
        /// </summary>
        /// <param name="vtStudentPlacementDetailId"></param>
        /// <returns></returns>
        public VTStudentPlacementDetail GetVTStudentPlacementDetailById(Guid vtStudentPlacementDetailId)
        {
            return this.Context.VTStudentPlacementDetails.FirstOrDefault(v => v.VTStudentPlacementDetailId == vtStudentPlacementDetailId);
        }

        /// <summary>
        /// Get VTStudentPlacementDetail by VTStudentPlacementDetailId using async
        /// </summary>
        /// <param name="vtStudentPlacementDetailId"></param>
        /// <returns></returns>
        public async Task<VTStudentPlacementDetail> GetVTStudentPlacementDetailByIdAsync(Guid vtStudentPlacementDetailId)
        {
            var vtStudentPlacementDetail = await (from v in this.Context.VTStudentPlacementDetails
                                                  where v.VTStudentPlacementDetailId == vtStudentPlacementDetailId
                                                  select v).FirstOrDefaultAsync();

            return vtStudentPlacementDetail;
        }

        /// <summary>
        /// Insert/Update VTStudentPlacementDetail entity
        /// </summary>
        /// <param name="vtStudentPlacementDetail"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVTStudentPlacementDetailDetails(VTStudentPlacementDetail vtStudentPlacementDetail)
        {
            if (RequestType.New == vtStudentPlacementDetail.RequestType)
                Context.VTStudentPlacementDetails.Add(vtStudentPlacementDetail);
            else
            {
                Context.Entry<VTStudentPlacementDetail>(vtStudentPlacementDetail).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by VTStudentPlacementDetailId
        /// </summary>
        /// <param name="vtStudentPlacementDetailId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtStudentPlacementDetailId)
        {
            VTStudentPlacementDetail vtStudentPlacementDetail = this.Context.VTStudentPlacementDetails.FirstOrDefault(v => v.VTStudentPlacementDetailId == vtStudentPlacementDetailId);

            if (vtStudentPlacementDetail != null)
            {
                Context.Entry<VTStudentPlacementDetail>(vtStudentPlacementDetail).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VTStudentPlacementDetail by Name
        /// </summary>
        /// <param name="vtStudentPlacementDetailModel"></param>
        /// <returns></returns>
        public bool CheckVTStudentPlacementDetailExistByName(VTStudentPlacementDetailModel vtStudentPlacementDetailModel)
        {
            VTStudentPlacementDetail vtStudentPlacementDetail = this.Context.VTStudentPlacementDetails.FirstOrDefault(v => v.StudentId == vtStudentPlacementDetailModel.StudentId);

            return vtStudentPlacementDetail != null;
        }

        /// <summary>}
        /// List of VTStudentPlacementDetail with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTStudentPlacementDetailViewModel> GetVTStudentPlacementDetailsByCriteria(SearchVTStudentPlacementDetailModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.VTStudentPlacementDetailViewModels.FromSql<VTStudentPlacementDetailViewModel>("CALL GetVTStudentPlacementDetailsByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}