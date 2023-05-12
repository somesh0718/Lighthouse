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
    /// Repository of the VTStudentAssessment entity
    /// </summary>
    public class VTStudentAssessmentRepository : GenericRepository<VTStudentAssessment>, IVTStudentAssessmentRepository
    {
        /// <summary>
        /// Get list of VTStudentAssessment
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTStudentAssessment> GetVTStudentAssessments()
        {
            return this.Context.VTStudentAssessments.AsQueryable();
        }

        /// <summary>
        /// Get list of VTStudentAssessment by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTStudentAssessment> GetVTStudentAssessmentsByName(string name)
        {
            var vtStudentAssessments = (from v in this.Context.VTStudentAssessments
                         where v.StudentName.Contains(name)
                         select v).AsQueryable();

            return vtStudentAssessments;
        }

        /// <summary>
        /// Get VTStudentAssessment by VTStudentAssessmentId
        /// </summary>
        /// <param name="vtStudentAssessmentId"></param>
        /// <returns></returns>
        public VTStudentAssessment GetVTStudentAssessmentById(Guid vtStudentAssessmentId)
        {
            return this.Context.VTStudentAssessments.FirstOrDefault(v => v.VTStudentAssessmentId == vtStudentAssessmentId);
        }

        /// <summary>
        /// Get VTStudentAssessment by VTStudentAssessmentId using async
        /// </summary>
        /// <param name="vtStudentAssessmentId"></param>
        /// <returns></returns>
        public async Task<VTStudentAssessment> GetVTStudentAssessmentByIdAsync(Guid vtStudentAssessmentId)
        {
            var vtStudentAssessment = await (from v in this.Context.VTStudentAssessments
                              where v.VTStudentAssessmentId == vtStudentAssessmentId
                              select v).FirstOrDefaultAsync();

            return vtStudentAssessment;
        }

        /// <summary>
        /// Insert/Update VTStudentAssessment entity
        /// </summary>
        /// <param name="vtStudentAssessment"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVTStudentAssessmentDetails(VTStudentAssessment vtStudentAssessment)
        {
            if (RequestType.New == vtStudentAssessment.RequestType)
                Context.VTStudentAssessments.Add(vtStudentAssessment);
            else
            {
                Context.Entry<VTStudentAssessment>(vtStudentAssessment).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by VTStudentAssessmentId
        /// </summary>
        /// <param name="vtStudentAssessmentId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtStudentAssessmentId)
        {
            VTStudentAssessment vtStudentAssessment = this.Context.VTStudentAssessments.FirstOrDefault(v => v.VTStudentAssessmentId == vtStudentAssessmentId);

            if (vtStudentAssessment != null)
            {
                Context.Entry<VTStudentAssessment>(vtStudentAssessment).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VTStudentAssessment by Name
        /// </summary>
        /// <param name="vtStudentAssessmentModel"></param>
        /// <returns></returns>
        public bool CheckVTStudentAssessmentExistByName(VTStudentAssessmentModel vtStudentAssessmentModel)
        {
            VTStudentAssessment vtStudentAssessment = this.Context.VTStudentAssessments.FirstOrDefault(v => v.TestimonialType == vtStudentAssessmentModel.TestimonialType && v.StudentName == vtStudentAssessmentModel.StudentName);

            return vtStudentAssessment != null;
        }

        /// <summary>}
        /// List of VTStudentAssessment with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTStudentAssessmentViewModel> GetVTStudentAssessmentsByCriteria(SearchVTStudentAssessmentModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.VTStudentAssessmentViewModels.FromSql<VTStudentAssessmentViewModel>("CALL GetVTStudentAssessmentsByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
