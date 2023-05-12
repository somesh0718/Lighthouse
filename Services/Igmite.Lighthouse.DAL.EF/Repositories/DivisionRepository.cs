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
    /// Repository of the Division entity
    /// </summary>
    public class DivisionRepository : GenericRepository<Division>, IDivisionRepository
    {
        /// <summary>
        /// Get list of Division
        /// </summary>
        /// <returns></returns>
        public IQueryable<Division> GetDivisions()
        {
            return this.Context.Divisions.AsQueryable();
        }

        /// <summary>
        /// Get list of Division by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<Division> GetDivisionsByName(string name)
        {
            var divisions = (from d in this.Context.Divisions
                         where d.DivisionName.Contains(name)
                         select d).AsQueryable();

            return divisions;
        }

        /// <summary>
        /// Get Division by DivisionId
        /// </summary>
        /// <param name="divisionId"></param>
        /// <returns></returns>
        public Division GetDivisionById(Guid divisionId)
        {
            return this.Context.Divisions.FirstOrDefault(d => d.DivisionId == divisionId);
        }

        /// <summary>
        /// Get Division by DivisionId using async
        /// </summary>
        /// <param name="divisionId"></param>
        /// <returns></returns>
        public async Task<Division> GetDivisionByIdAsync(Guid divisionId)
        {
            var division = await (from d in this.Context.Divisions
                              where d.DivisionId == divisionId
                              select d).FirstOrDefaultAsync();

            return division;
        }

        /// <summary>
        /// Insert/Update Division entity
        /// </summary>
        /// <param name="division"></param>
        /// <returns></returns>
        public bool SaveOrUpdateDivisionDetails(Division division)
        {
            if (RequestType.New == division.RequestType)
                Context.Divisions.Add(division);
            else
            {
                Context.Entry<Division>(division).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by DivisionId
        /// </summary>
        /// <param name="divisionId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid divisionId)
        {
            Division division = this.Context.Divisions.FirstOrDefault(d => d.DivisionId == divisionId);

            if (division != null)
            {
                Context.Entry<Division>(division).State = EntityState.Deleted;

                //IList<Guid> schoolIds = division.Schools.Select(d => d.SchoolId).ToList();
                //foreach (Guid schoolId in schoolIds)
                //{
                //    School school = division.Schools.FirstOrDefault(d => d.SchoolId == schoolId);
                //    Context.Entry<School>(school).State = EntityState.Deleted;
                //}

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate Division by Name
        /// </summary>
        /// <param name="divisionModel"></param>
        /// <returns></returns>
        public bool CheckDivisionExistByName(DivisionModel divisionModel)
        {
            Division division = this.Context.Divisions.FirstOrDefault(d => d.DivisionName == divisionModel.DivisionName);

            return division != null;
        }

        /// <summary>}
        /// List of Division with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<DivisionViewModel> GetDivisionsByCriteria(SearchDivisionModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.DivisionViewModels.FromSql<DivisionViewModel>("CALL GetDivisionsByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
