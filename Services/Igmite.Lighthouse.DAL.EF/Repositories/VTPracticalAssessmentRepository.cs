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
    /// Repository of the VTPracticalAssessment entity
    /// </summary>
    public class VTPracticalAssessmentRepository : GenericRepository<VTPracticalAssessment>, IVTPracticalAssessmentRepository
    {
        /// <summary>
        /// Get list of VTPracticalAssessment
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTPracticalAssessment> GetVTPracticalAssessments()
        {
            return this.Context.VTPracticalAssessments.AsQueryable();
        }

        /// <summary>
        /// Get list of VTPracticalAssessment by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTPracticalAssessment> GetVTPracticalAssessmentsByName(string name)
        {
            var vtPracticalAssessments = (from v in this.Context.VTPracticalAssessments
                         select v).AsQueryable();

            return vtPracticalAssessments;
        }

        /// <summary>
        /// Get VTPracticalAssessment by VTPracticalAssessmentId
        /// </summary>
        /// <param name="vtPracticalAssessmentId"></param>
        /// <returns></returns>
        public VTPracticalAssessment GetVTPracticalAssessmentById(Guid vtPracticalAssessmentId)
        {
            return this.Context.VTPracticalAssessments.FirstOrDefault(v => v.VTPracticalAssessmentId == vtPracticalAssessmentId);
        }

        /// <summary>
        /// Get VTPracticalAssessment by VTPracticalAssessmentId using async
        /// </summary>
        /// <param name="vtPracticalAssessmentId"></param>
        /// <returns></returns>
        public async Task<VTPracticalAssessment> GetVTPracticalAssessmentByIdAsync(Guid vtPracticalAssessmentId)
        {
            var vtPracticalAssessment = await (from v in this.Context.VTPracticalAssessments
                              where v.VTPracticalAssessmentId == vtPracticalAssessmentId
                              select v).FirstOrDefaultAsync();

            return vtPracticalAssessment;
        }

        /// <summary>
        /// Insert/Update VTPracticalAssessment entity
        /// </summary>
        /// <param name="vtPracticalAssessment"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVTPracticalAssessmentDetails(VTPracticalAssessment vtPracticalAssessment)
        {
            if (RequestType.New == vtPracticalAssessment.RequestType)
                Context.VTPracticalAssessments.Add(vtPracticalAssessment);
            else
            {
                Context.Entry<VTPracticalAssessment>(vtPracticalAssessment).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by VTPracticalAssessmentId
        /// </summary>
        /// <param name="vtPracticalAssessmentId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtPracticalAssessmentId)
        {
            VTPracticalAssessment vtPracticalAssessment = this.Context.VTPracticalAssessments.FirstOrDefault(v => v.VTPracticalAssessmentId == vtPracticalAssessmentId);

            if (vtPracticalAssessment != null)
            {
                Context.Entry<VTPracticalAssessment>(vtPracticalAssessment).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VTPracticalAssessment by Name
        /// </summary>
        /// <param name="vtPracticalAssessmentModel"></param>
        /// <returns></returns>
        public bool CheckVTPracticalAssessmentExistByName(VTPracticalAssessmentModel vtPracticalAssessmentModel)
        {
            VTPracticalAssessment vtPracticalAssessment = this.Context.VTPracticalAssessments.FirstOrDefault(v => v.GirlsPresent == vtPracticalAssessmentModel.GirlsPresent);

            return vtPracticalAssessment != null;
        }

        /// <summary>}
        /// List of VTPracticalAssessment with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTPracticalAssessmentViewModel> GetVTPracticalAssessmentsByCriteria(SearchVTPracticalAssessmentModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.VTPracticalAssessmentViewModels.FromSql<VTPracticalAssessmentViewModel>("CALL GetVTPracticalAssessmentsByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
