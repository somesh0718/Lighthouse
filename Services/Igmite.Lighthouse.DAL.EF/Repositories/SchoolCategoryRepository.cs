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
    /// Repository of the SchoolCategory entity
    /// </summary>
    public class SchoolCategoryRepository : GenericRepository<SchoolCategory>, ISchoolCategoryRepository
    {
        /// <summary>
        /// Get list of SchoolCategory
        /// </summary>
        /// <returns></returns>
        public IQueryable<SchoolCategory> GetSchoolCategories()
        {
            return this.Context.SchoolCategories.AsQueryable();
        }

        /// <summary>
        /// Get list of SchoolCategory by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<SchoolCategory> GetSchoolCategoriesByName(string name)
        {
            var schoolCategories = (from s in this.Context.SchoolCategories
                         where s.CategoryName.Contains(name)
                         select s).AsQueryable();

            return schoolCategories;
        }

        /// <summary>
        /// Get SchoolCategory by SchoolCategoryId
        /// </summary>
        /// <param name="schoolCategoryId"></param>
        /// <returns></returns>
        public SchoolCategory GetSchoolCategoryById(Guid schoolCategoryId)
        {
            return this.Context.SchoolCategories.FirstOrDefault(s => s.SchoolCategoryId == schoolCategoryId);
        }

        /// <summary>
        /// Get SchoolCategory by SchoolCategoryId using async
        /// </summary>
        /// <param name="schoolCategoryId"></param>
        /// <returns></returns>
        public async Task<SchoolCategory> GetSchoolCategoryByIdAsync(Guid schoolCategoryId)
        {
            var schoolCategory = await (from s in this.Context.SchoolCategories
                              where s.SchoolCategoryId == schoolCategoryId
                              select s).FirstOrDefaultAsync();

            return schoolCategory;
        }

        /// <summary>
        /// Insert/Update SchoolCategory entity
        /// </summary>
        /// <param name="schoolCategory"></param>
        /// <returns></returns>
        public bool SaveOrUpdateSchoolCategoryDetails(SchoolCategory schoolCategory)
        {
            if (RequestType.New == schoolCategory.RequestType)
                Context.SchoolCategories.Add(schoolCategory);
            else
            {
                Context.Entry<SchoolCategory>(schoolCategory).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by SchoolCategoryId
        /// </summary>
        /// <param name="schoolCategoryId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid schoolCategoryId)
        {
            SchoolCategory schoolCategory = this.Context.SchoolCategories.FirstOrDefault(s => s.SchoolCategoryId == schoolCategoryId);

            if (schoolCategory != null)
            {
                Context.Entry<SchoolCategory>(schoolCategory).State = EntityState.Deleted;

                //IList<Guid> schoolIds = schoolCategory.Schools.Select(s => s.SchoolId).ToList();
                //foreach (Guid id in schoolIds)
                //{
                //    School school = schoolCategory.Schools.FirstOrDefault(s => s.SchoolId == id);
                //    Context.Entry<School>(school).State = EntityState.Deleted;
                //}

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate SchoolCategory by Name
        /// </summary>
        /// <param name="schoolCategoryModel"></param>
        /// <returns></returns>
        public bool CheckSchoolCategoryExistByName(SchoolCategoryModel schoolCategoryModel)
        {
            SchoolCategory schoolCategory = this.Context.SchoolCategories.FirstOrDefault(s => s.CategoryName == schoolCategoryModel.CategoryName);

            return schoolCategory != null;
        }

        /// <summary>}
        /// List of SchoolCategory with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<SchoolCategoryViewModel> GetSchoolCategoriesByCriteria(SearchSchoolCategoryModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.SchoolCategoryViewModels.FromSql<SchoolCategoryViewModel>("CALL GetSchoolCategoriesByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
