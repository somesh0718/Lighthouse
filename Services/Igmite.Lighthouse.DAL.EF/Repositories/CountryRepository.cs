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
    /// Repository of the Country entity
    /// </summary>
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        /// <summary>
        /// Get list of Country
        /// </summary>
        /// <returns></returns>
        public IQueryable<Country> GetCountries()
        {
            return this.Context.Countries.AsQueryable();
        }

        /// <summary>
        /// Get list of Country by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<Country> GetCountriesByName(string name)
        {
            var countries = (from c in this.Context.Countries
                         where c.CountryName.Contains(name)
                         select c).AsQueryable();

            return countries;
        }

        /// <summary>
        /// Get Country by CountryCode
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public Country GetCountryById(string countryCode)
        {
            return this.Context.Countries.FirstOrDefault(c => c.CountryCode == countryCode);
        }

        /// <summary>
        /// Get Country by CountryCode using async
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public async Task<Country> GetCountryByIdAsync(string countryCode)
        {
            var country = await (from c in this.Context.Countries
                              where c.CountryCode == countryCode
                              select c).FirstOrDefaultAsync();

            return country;
        }

        /// <summary>
        /// Insert/Update Country entity
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public bool SaveOrUpdateCountryDetails(Country country)
        {
            if (RequestType.New == country.RequestType)
                Context.Countries.Add(country);
            else
            {
                Context.Entry<Country>(country).State = EntityState.Modified;

            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by CountryCode
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public bool DeleteById(string countryCode)
        {
            Country country = this.Context.Countries.FirstOrDefault(c => c.CountryCode == countryCode);

            if (country != null)
            {
                Context.Entry<Country>(country).State = EntityState.Deleted;

                //IList<string> stateIds = country.States.Select(c => c.StateCode).ToList();
                //foreach (string stateCode in stateIds)
                //{
                //    State state = country.States.FirstOrDefault(c => c.StateCode == stateCode);
                //    Context.Entry<State>(state).State = EntityState.Deleted;
                //}

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate Country by Name
        /// </summary>
        /// <param name="countryModel"></param>
        /// <returns></returns>
        public bool CheckCountryExistByName(CountryModel countryModel)
        {
            Country country = this.Context.Countries.FirstOrDefault(c => c.CountryName == countryModel.CountryName);

            return country != null;
        }

        /// <summary>}
        /// List of Country with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<CountryViewModel> GetCountriesByCriteria(SearchCountryModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.CountryViewModels.FromSql<CountryViewModel>("CALL GetCountriesByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}
