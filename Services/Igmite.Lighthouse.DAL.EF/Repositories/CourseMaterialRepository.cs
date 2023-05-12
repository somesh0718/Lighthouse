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
    /// Repository of the CourseMaterial entity
    /// </summary>
    public class CourseMaterialRepository : GenericRepository<CourseMaterial>, ICourseMaterialRepository
    {
        /// <summary>
        /// Get list of CourseMaterial
        /// </summary>
        /// <returns></returns>
        public IQueryable<CourseMaterial> GetCourseMaterials()
        {
            return this.Context.CourseMaterials.AsQueryable();
        }

        /// <summary>
        /// Get list of CourseMaterial by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<CourseMaterial> GetCourseMaterialsByName(string name)
        {
            var courseMaterials = (from c in this.Context.CourseMaterials
                                   select c).AsQueryable();

            return courseMaterials;
        }

        /// <summary>
        /// Get CourseMaterial by CourseMaterialId
        /// </summary>
        /// <param name="courseMaterialId"></param>
        /// <returns></returns>
        public CourseMaterial GetCourseMaterialById(Guid courseMaterialId)
        {
            return this.Context.CourseMaterials.FirstOrDefault(c => c.CourseMaterialId == courseMaterialId);
        }

        /// <summary>
        /// Get CourseMaterial by CourseMaterialId using async
        /// </summary>
        /// <param name="courseMaterialId"></param>
        /// <returns></returns>
        public async Task<CourseMaterial> GetCourseMaterialByIdAsync(Guid courseMaterialId)
        {
            var courseMaterial = await (from c in this.Context.CourseMaterials
                                        where c.CourseMaterialId == courseMaterialId
                                        select c).FirstOrDefaultAsync();

            return courseMaterial;
        }

        /// <summary>
        /// Insert/Update CourseMaterial entity
        /// </summary>
        /// <param name="courseMaterial"></param>
        /// <returns></returns>
        public bool SaveOrUpdateCourseMaterialDetails(CourseMaterial courseMaterial)
        {
            try
            {
                if (RequestType.New == courseMaterial.RequestType)
                    Context.CourseMaterials.Add(courseMaterial);
                else
                {
                    Context.Entry<CourseMaterial>(courseMaterial).State = EntityState.Modified;
                }

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL > SaveOrUpdateCourseMaterialDetails", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a record by CourseMaterialId
        /// </summary>
        /// <param name="courseMaterialId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid courseMaterialId)
        {
            CourseMaterial courseMaterial = this.Context.CourseMaterials.FirstOrDefault(c => c.CourseMaterialId == courseMaterialId);

            if (courseMaterial != null)
            {
                Context.Entry<CourseMaterial>(courseMaterial).State = EntityState.Deleted;
                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate CourseMaterial by Name
        /// </summary>
        /// <param name="courseMaterialModel"></param>
        /// <returns></returns>
        public bool CheckCourseMaterialExistByName(CourseMaterialModel courseMaterialModel)
        {
            CourseMaterial courseMaterial = this.Context.CourseMaterials.FirstOrDefault(c => c.AcademicYearId == courseMaterialModel.AcademicYearId && c.ClassId == courseMaterialModel.ClassId && c.VTId == courseMaterialModel.VTId);

            return courseMaterial != null;
        }

        /// <summary>}
        /// List of CourseMaterial with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<CourseMaterialViewModel> GetCourseMaterialsByCriteria(SearchCourseMaterialModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[10];

            sqlParams[0] = new MySqlParameter { ParameterName = "academicYearId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.AcademicYearId };
            sqlParams[1] = new MySqlParameter { ParameterName = "vtpId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTPId };
            sqlParams[2] = new MySqlParameter { ParameterName = "vcId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VCId };
            sqlParams[3] = new MySqlParameter { ParameterName = "vtId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.VTId };
            sqlParams[4] = new MySqlParameter { ParameterName = "hmId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.HMId };
            sqlParams[5] = new MySqlParameter { ParameterName = "schoolId", MySqlDbType = MySqlDbType.Guid, Value = searchModel.SchoolId };
            sqlParams[6] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[7] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[8] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[9] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.CourseMaterialViewModels.FromSql<CourseMaterialViewModel>("CALL GetCourseMaterialsByCriteria (@academicYearId, @vtpId, @vcId, @vtId, @hmId, @schoolId, @name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}