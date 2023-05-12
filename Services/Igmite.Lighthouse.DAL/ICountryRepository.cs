using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the Country entity
    /// </summary>
    public interface ICountryRepository : IGenericRepository<Country>
    {
        /// <summary>
        /// Get list of Country
        /// </summary>
        /// <returns></returns>
        IQueryable<Country> GetCountries();

        /// <summary>
        /// Get list of Country by countryName
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        IQueryable<Country> GetCountriesByName(string countryName);

        /// <summary>
        /// Get Country by CountryCode
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        Country GetCountryById(string countryCode);

        /// <summary>
        /// Get Country by CountryCode using async
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        Task<Country> GetCountryByIdAsync(string countryCode);

        /// <summary>
        /// Insert/Update Country entity
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        bool SaveOrUpdateCountryDetails(Country country);

        /// <summary>
        /// Delete a record by CountryCode
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        bool DeleteById(string countryCode);

        /// <summary>
        /// Check duplicate Country by Name
        /// </summary>
        /// <param name="countryModel"></param>
        /// <returns></returns>
        bool CheckCountryExistByName(CountryModel countryModel);

        /// <summary>
        /// List of Country with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<CountryViewModel> GetCountriesByCriteria(SearchCountryModel searchModel);
    }
}
