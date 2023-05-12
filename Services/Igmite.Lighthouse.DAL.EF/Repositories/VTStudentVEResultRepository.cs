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
    /// Repository of the VTStudentVEResult entity
    /// </summary>
    public class VTStudentVEResultRepository : GenericRepository<VTStudentVEResult>, IVTStudentVEResultRepository
    {
        /// <summary>
        /// Get list of VTStudentVEResult
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTStudentVEResult> GetVTStudentVEResults()
        {
            return this.Context.VTStudentVEResults.AsQueryable();
        }

        /// <summary>
        /// Get list of VTStudentVEResult by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTStudentVEResult> GetVTStudentVEResultsByName(string name)
        {
            var vtStudentVEResults = (from v in this.Context.VTStudentVEResults
                                      select v).AsQueryable();

            return vtStudentVEResults;
        }

        /// <summary>
        /// Get VTStudentVEResult by VTStudentVEResultId
        /// </summary>
        /// <param name="vtStudentVEResultId"></param>
        /// <returns></returns>
        public VTStudentVEResult GetVTStudentVEResultById(Guid vtStudentVEResultId)
        {
            return this.Context.VTStudentVEResults.FirstOrDefault(v => v.VTStudentVEResultId == vtStudentVEResultId);
        }

        /// <summary>
        /// Get VTStudentVEResult by VTStudentVEResultId using async
        /// </summary>
        /// <param name="vtStudentVEResultId"></param>
        /// <returns></returns>
        public async Task<VTStudentVEResult> GetVTStudentVEResultByIdAsync(Guid vtStudentVEResultId)
        {
            var vtStudentVEResult = await (from v in this.Context.VTStudentVEResults
                                           where v.VTStudentVEResultId == vtStudentVEResultId
                                           select v).FirstOrDefaultAsync();

            return vtStudentVEResult;
        }

        /// <summary>
        /// Insert/Update VTStudentVEResult entity
        /// </summary>
        /// <param name="vtStudentVEResult"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVTStudentVEResultDetails(VTStudentVEResult vtStudentVEResult)
        {
            if (RequestType.New == vtStudentVEResult.RequestType)
                Context.VTStudentVEResults.Add(vtStudentVEResult);
            else
            {
                Context.Entry<VTStudentVEResult>(vtStudentVEResult).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by VTStudentVEResultId
        /// </summary>
        /// <param name="vtStudentVEResultId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtStudentVEResultId)
        {
            VTStudentVEResult vtStudentVEResult = this.Context.VTStudentVEResults.FirstOrDefault(v => v.VTStudentVEResultId == vtStudentVEResultId);

            if (vtStudentVEResult != null)
            {
                Context.Entry<VTStudentVEResult>(vtStudentVEResult).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VTStudentVEResult by Name
        /// </summary>
        /// <param name="vtStudentVEResultModel"></param>
        /// <returns></returns>
        public bool CheckVTStudentVEResultExistByName(VTStudentVEResultModel vtStudentVEResultModel)
        {
            VTStudentVEResult vtStudentVEResult = this.Context.VTStudentVEResults.FirstOrDefault(v => v.StudentId == vtStudentVEResultModel.StudentId && v.DateIssuence == vtStudentVEResultModel.DateIssuence);

            return vtStudentVEResult != null;
        }

        /// <summary>}
        /// List of VTStudentVEResult with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTStudentVEResultViewModel> GetVTStudentVEResultsByCriteria(SearchVTStudentVEResultModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.VTStudentVEResultViewModels.FromSql<VTStudentVEResultViewModel>("CALL GetVTStudentVEResultsByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}