using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the Country entity
    /// </summary>
    public interface ICountryManager : IGenericManager<CountryModel>
    {
        /// <summary>
        /// Get list of Countries
        /// </summary>
        /// <returns></returns>
        IQueryable<CountryModel> GetCountries();

        /// <summary>
        /// Get list of Countries by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<CountryModel> GetCountriesByName(string countryName);

        /// <summary>
        /// Get Country by CountryCode
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        CountryModel GetCountryById(string countryCode);

        /// <summary>
        /// Get Country by CountryCode using async
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        Task<CountryModel> GetCountryByIdAsync(string countryCode);

        /// <summary>
        /// Insert/Update Country entity
        /// </summary>
        /// <param name="countryModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateCountryDetails(CountryModel countryModel);

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
