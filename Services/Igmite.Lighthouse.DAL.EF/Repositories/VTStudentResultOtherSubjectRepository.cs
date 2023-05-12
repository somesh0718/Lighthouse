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
    /// Repository of the VTStudentResultOtherSubject entity
    /// </summary>
    public class VTStudentResultOtherSubjectRepository : GenericRepository<VTStudentResultOtherSubject>, IVTStudentResultOtherSubjectRepository
    {
        /// <summary>
        /// Get list of VTStudentResultOtherSubject
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTStudentResultOtherSubject> GetVTStudentResultOtherSubjects()
        {
            return this.Context.VTStudentResultOtherSubjects.AsQueryable();
        }

        /// <summary>
        /// Get list of VTStudentResultOtherSubject by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTStudentResultOtherSubject> GetVTStudentResultOtherSubjectsByName(string name)
        {
            var vtStudentResultOtherSubjects = (from v in this.Context.VTStudentResultOtherSubjects
                                                where v.SubjectName.Contains(name)
                                                select v).AsQueryable();

            return vtStudentResultOtherSubjects;
        }

        /// <summary>
        /// Get VTStudentResultOtherSubject by VTStudentResultOtherSubjectId
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectId"></param>
        /// <returns></returns>
        public VTStudentResultOtherSubject GetVTStudentResultOtherSubjectById(Guid vtStudentResultOtherSubjectId)
        {
            return this.Context.VTStudentResultOtherSubjects.FirstOrDefault(v => v.VTStudentResultOtherSubjectId == vtStudentResultOtherSubjectId);
        }

        /// <summary>
        /// Get VTStudentResultOtherSubject by VTStudentResultOtherSubjectId using async
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectId"></param>
        /// <returns></returns>
        public async Task<VTStudentResultOtherSubject> GetVTStudentResultOtherSubjectByIdAsync(Guid vtStudentResultOtherSubjectId)
        {
            var vtStudentResultOtherSubject = await (from v in this.Context.VTStudentResultOtherSubjects
                                                     where v.VTStudentResultOtherSubjectId == vtStudentResultOtherSubjectId
                                                     select v).FirstOrDefaultAsync();

            return vtStudentResultOtherSubject;
        }

        /// <summary>
        /// Insert/Update VTStudentResultOtherSubject entity
        /// </summary>
        /// <param name="vtStudentResultOtherSubject"></param>
        /// <returns></returns>
        public bool SaveOrUpdateVTStudentResultOtherSubjectDetails(VTStudentResultOtherSubject vtStudentResultOtherSubject)
        {
            if (RequestType.New == vtStudentResultOtherSubject.RequestType)
                Context.VTStudentResultOtherSubjects.Add(vtStudentResultOtherSubject);
            else
            {
                Context.Entry<VTStudentResultOtherSubject>(vtStudentResultOtherSubject).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by VTStudentResultOtherSubjectId
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtStudentResultOtherSubjectId)
        {
            VTStudentResultOtherSubject vtStudentResultOtherSubject = this.Context.VTStudentResultOtherSubjects.FirstOrDefault(v => v.VTStudentResultOtherSubjectId == vtStudentResultOtherSubjectId);

            if (vtStudentResultOtherSubject != null)
            {
                Context.Entry<VTStudentResultOtherSubject>(vtStudentResultOtherSubject).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate VTStudentResultOtherSubject by Name
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectModel"></param>
        /// <returns></returns>
        public bool CheckVTStudentResultOtherSubjectExistByName(VTStudentResultOtherSubjectModel vtStudentResultOtherSubjectModel)
        {
            VTStudentResultOtherSubject vtStudentResultOtherSubject = this.Context.VTStudentResultOtherSubjects.FirstOrDefault(v => v.StudentId == vtStudentResultOtherSubjectModel.StudentId && v.SubjectName == vtStudentResultOtherSubjectModel.SubjectName);

            return vtStudentResultOtherSubject != null;
        }

        /// <summary>}
        /// List of VTStudentResultOtherSubject with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTStudentResultOtherSubjectViewModel> GetVTStudentResultOtherSubjectsByCriteria(SearchVTStudentResultOtherSubjectModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.VTStudentResultOtherSubjectViewModels.FromSql<VTStudentResultOtherSubjectViewModel>("CALL GetVTStudentResultOtherSubjectsByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}