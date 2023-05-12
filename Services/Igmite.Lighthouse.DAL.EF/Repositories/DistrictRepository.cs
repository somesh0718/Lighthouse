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
    /// Repository of the District entity
    /// </summary>
    public class DistrictRepository : GenericRepository<District>, IDistrictRepository
    {
        /// <summary>
        /// Get list of District
        /// </summary>
        /// <returns></returns>
        public IQueryable<District> GetDistricts()
        {
            return this.Context.Districts.AsQueryable();
        }

        /// <summary>
        /// Get list of District by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<District> GetDistrictsByName(string name)
        {
            var districts = (from d in this.Context.Districts
                         where d.DistrictName.Contains(name)
                         select d).AsQueryable();

            return districts;
        }

        /// <summary>
        /// Get District by DistrictCode
        /// </summary>
        /// <param name="districtCode"></param>
        /// <returns></returns>
        public District GetDistrictById(string districtCode)
        {
            return this.Context.Districts.FirstOrDefault(d => d.DistrictCode == districtCode);
        }

        /// <summary>
        /// Get District by DistrictCode using async
        /// </summary>
        /// <param name="districtCode"></param>
        /// <returns></returns>
        public async Task<District> GetDistrictByIdAsync(string districtCode)
        {
            var district = await (from d in this.Context.Districts
                              where d.DistrictCode == districtCode
                              select d).FirstOrDefaultAsync();

            return district;
        }

        /// <summary>
        /// Insert/Update District entity
        /// </summary>
        /// <param name="district"></param>
        /// <returns></returns>
        public bool SaveOrUpdateDistrictDetails(District district)
        {
            if (RequestType.New == district.RequestType)
                Context.Districts.Add(district);
            else
            {
                Context.Entry<District>(district).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by DistrictCode
        /// </summary>
        /// <param name="districtCode"></param>
        /// <returns></returns>
        public bool DeleteById(string districtCode)
        {
            District district = this.Context.Districts.FirstOrDefault(d => d.DistrictCode == districtCode);

            if (district != null)
            {
                Context.Entry<District>(district).State = EntityState.Deleted;

                //IList<Guid> schoolIds = district.Schools.Select(d => d.SchoolId).ToList();
                //foreach (Guid schoolId in schoolIds)
                //{
                //    School school = district.Schools.FirstOrDefault(d => d.SchoolId == schoolId);
                //    Context.Entry<School>(school).State = EntityState.Deleted;
                //}

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate District by Name
        /// </summary>
        /// <param name="districtModel"></param>
        /// <returns></returns>
        public bool CheckDistrictExistByName(DistrictModel districtModel)
        {
            District district = this.Context.Districts.FirstOrDefault(d => d.DistrictName == districtModel.DistrictName);

            return district != null;
        }

        /// <summary>}
        /// List of District with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<DistrictViewModel> GetDistrictsByCriteria(SearchDistrictModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.DistrictViewModels.FromSql<DistrictViewModel>("CALL GetDistrictsByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
