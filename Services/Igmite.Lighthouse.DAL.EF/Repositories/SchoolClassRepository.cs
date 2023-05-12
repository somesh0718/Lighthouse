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
    /// Repository of the SchoolClass entity
    /// </summary>
    public class SchoolClassRepository : GenericRepository<SchoolClass>, ISchoolClassRepository
    {
        /// <summary>
        /// Get list of SchoolClass
        /// </summary>
        /// <returns></returns>
        public IQueryable<SchoolClass> GetSchoolClasses()
        {
            return this.Context.SchoolClasses.AsQueryable();
        }

        /// <summary>
        /// Get list of SchoolClass by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<SchoolClass> GetSchoolClassesByName(string name)
        {
            var schoolClasses = (from s in this.Context.SchoolClasses
                         where s.Name.Contains(name)
                         select s).AsQueryable();

            return schoolClasses;
        }

        /// <summary>
        /// Get SchoolClass by ClassId
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public SchoolClass GetSchoolClassById(Guid classId)
        {
            return this.Context.SchoolClasses.FirstOrDefault(s => s.ClassId == classId);
        }

        /// <summary>
        /// Get SchoolClass by ClassId using async
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public async Task<SchoolClass> GetSchoolClassByIdAsync(Guid classId)
        {
            var schoolClass = await (from s in this.Context.SchoolClasses
                              where s.ClassId == classId
                              select s).FirstOrDefaultAsync();

            return schoolClass;
        }

        /// <summary>
        /// Insert/Update SchoolClass entity
        /// </summary>
        /// <param name="schoolClass"></param>
        /// <returns></returns>
        public bool SaveOrUpdateSchoolClassDetails(SchoolClass schoolClass)
        {
            if (RequestType.New == schoolClass.RequestType)
                Context.SchoolClasses.Add(schoolClass);
            else
            {
                Context.Entry<SchoolClass>(schoolClass).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by ClassId
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid classId)
        {
            SchoolClass schoolClass = this.Context.SchoolClasses.FirstOrDefault(s => s.ClassId == classId);

            if (schoolClass != null)
            {
                Context.Entry<SchoolClass>(schoolClass).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate SchoolClass by Name
        /// </summary>
        /// <param name="schoolClassModel"></param>
        /// <returns></returns>
        public bool CheckSchoolClassExistByName(SchoolClassModel schoolClassModel)
        {
            SchoolClass schoolClass = this.Context.SchoolClasses.FirstOrDefault(s => s.Name == schoolClassModel.Name);

            return schoolClass != null;
        }

        /// <summary>}
        /// List of SchoolClass with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<SchoolClassViewModel> GetSchoolClassesByCriteria(SearchSchoolClassModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.SchoolClassViewModels.FromSql<SchoolClassViewModel>("CALL GetSchoolClassesByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
