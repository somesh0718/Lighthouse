using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the School entity
    /// </summary>
    public class SchoolRepository : GenericRepository<School>, ISchoolRepository
    {
        /// <summary>
        /// Get list of School
        /// </summary>
        /// <returns></returns>
        public IQueryable<School> GetSchools()
        {
            return this.Context.Schools.AsQueryable();
        }

        /// <summary>
        /// Get list of School by name
        /// </summary>
        /// <param name="schoolName"></param>
        /// <returns></returns>
        public IQueryable<School> GetSchoolsByName(string schoolName)
        {
            var schools = (from s in this.Context.Schools
                           where s.SchoolName.Contains(schoolName)
                           select s).AsQueryable();

            return schools;
        }

        /// <summary>
        /// Get list of School by schoolNames
        /// </summary>
        /// <param name="schoolNames"></param>
        /// <returns></returns>
        public IQueryable<School> GetSchoolsByNames(List<string> schoolNames)
        {
            var schools = (from s in this.Context.Schools
                           where schoolNames.Contains(s.SchoolName)
                           select s).AsQueryable();

            return schools;
        }

        /// <summary>
        /// Get list of School by UDISE Codes
        /// </summary>
        /// <param name="udiseCodes"></param>
        /// <returns></returns>
        public IQueryable<School> GetSchoolsByUDISECodes(List<string> udiseCodes)
        {
            var schools = (from s in this.Context.Schools
                           where udiseCodes.Contains(s.Udise)
                           select s).AsQueryable();

            return schools;
        }

        /// <summary>
        /// Get School by SchoolId
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public School GetSchoolById(Guid schoolId)
        {
            return this.Context.Schools.FirstOrDefault(s => s.SchoolId == schoolId);
        }

        /// <summary>
        /// Get School by SchoolId using async
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public async Task<School> GetSchoolByIdAsync(Guid schoolId)
        {
            var school = await (from s in this.Context.Schools
                                where s.SchoolId == schoolId
                                select s).FirstOrDefaultAsync();

            return school;
        }

        /// <summary>
        /// Insert/Update School entity
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        public bool SaveOrUpdateSchoolDetails(School school)
        {
            try
            {
                if (RequestType.New == school.RequestType)
                    Context.Schools.Add(school);
                else
                {
                    Context.Entry<School>(school).State = EntityState.Modified;
                }

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateSchoolDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by SchoolId
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid schoolId)
        {
            School school = this.Context.Schools.FirstOrDefault(s => s.SchoolId == schoolId);

            if (school != null)
            {
                Context.Entry<School>(school).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate School by Name
        /// </summary>
        /// <param name="schoolModel"></param>
        /// <returns></returns>
        public bool CheckSchoolExistByName(SchoolModel schoolModel)
        {
            School school = this.Context.Schools.FirstOrDefault(s => s.Udise == schoolModel.Udise);

            return school != null;
        }

        /// <summary>
        /// Check school can be inactive
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public bool CheckUserCanInactiveSchoolById(Guid schoolId)
        {
            AcademicYear academicYear = this.Context.AcademicYears.FirstOrDefault(y => y.IsCurrentAcademicYear == true);
            VTSchoolSector vtSchoolSector = this.Context.VTSchoolSectors.FirstOrDefault(s => s.AcademicYearId == academicYear.AcademicYearId && s.SchoolId == schoolId && s.IsActive == true);

            VTClass vtClass = this.Context.VTClasses.FirstOrDefault(s => s.SchoolId == schoolId && s.IsActive == true);

            return (vtSchoolSector == null && vtClass == null);
        }

        /// <summary>
        /// List of School with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IList<SchoolViewModel> GetSchoolsByCriteria(SearchSchoolModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[10];
            sqlParams[0] = new MySqlParameter { ParameterName = "divisionId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.DivisionId };
            sqlParams[1] = new MySqlParameter { ParameterName = "districtId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.DistrictId };
            sqlParams[2] = new MySqlParameter { ParameterName = "schoolCategoryId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.SchoolCategoryId };
            sqlParams[3] = new MySqlParameter { ParameterName = "schoolManagementId", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.SchoolManagementId };
            sqlParams[4] = new MySqlParameter { ParameterName = "isImplemented", MySqlDbType = MySqlDbType.Bool, Value = searchModel.IsImplemented };
            sqlParams[5] = new MySqlParameter { ParameterName = "status", MySqlDbType = MySqlDbType.Bool, Value = searchModel.Status };
            sqlParams[6] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name };
            sqlParams[7] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy };
            sqlParams[8] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageIndex };
            sqlParams[9] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.Int32, Value = searchModel.PageSize };

            return Context.SchoolViewModels.FromSql<SchoolViewModel>("CALL GetSchoolsByCriteria (@divisionId, @districtId, @schoolCategoryId, @schoolManagementId, @isImplemented, @status, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}