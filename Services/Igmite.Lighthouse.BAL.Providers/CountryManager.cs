using Igmite.Lighthouse.BAL.Validations;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Mappers;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Manager of the Country entity
    /// </summary>
    public class CountryManager : GenericManager<CountryModel>, ICountryManager
    {
        private readonly ICountryRepository countryRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the country manager.
        /// </summary>
        /// <param name="countryRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public CountryManager(ICountryRepository _countryRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.countryRepository = _countryRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of Countries
        /// </summary>
        /// <returns></returns>
        public IQueryable<CountryModel> GetCountries()
        {
            var countries = this.countryRepository.GetCountries();

            IList<CountryModel> countryModels = new List<CountryModel>();
            countries.ForEach((user) => countryModels.Add(user.ToModel()));

            return countryModels.AsQueryable();
        }

        /// <summary>
        /// Get list of Countries by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<CountryModel> GetCountriesByName(string countryName)
        {
            var countries = this.countryRepository.GetCountriesByName(countryName);

            IList<CountryModel> countryModels = new List<CountryModel>();
            countries.ForEach((user) => countryModels.Add(user.ToModel()));

            return countryModels.AsQueryable();
        }

        /// <summary>
        /// Get Country by CountryCode
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public CountryModel GetCountryById(string countryCode)
        {
            Country country = this.countryRepository.GetCountryById(countryCode);

            return (country != null) ? country.ToModel() : null;
        }

        /// <summary>
        /// Get Country by CountryCode using async
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public async Task<CountryModel> GetCountryByIdAsync(string countryCode)
        {
            var country = await this.countryRepository.GetCountryByIdAsync(countryCode);

            return (country != null) ? country.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update Country entity
        /// </summary>
        /// <param name="countryModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateCountryDetails(CountryModel countryModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            Country country = null;

            //Validate model data
            countryModel = countryModel.GetModelValidationErrors<CountryModel>();

            if (countryModel.ErrorMessages.Count > 0)
            {
                response.Errors = countryModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (countryModel.RequestType == RequestType.Edit)
            {
                country = this.countryRepository.GetCountryById(countryModel.CountryCode);

                //country.States.ForEach((oldStateItem) =>
                //{
                //    var valStateItem = countryModel.StateModels.FirstOrDefault(a => a.StateCode == oldStateItem.StateCode);
                //    if (valStateItem == null)
                //    {
                //        country.Deleted//s.Add(oldStateCodeItem.State);
                //    }
                //});
            }
            else
            {
                country = new Country();
            }

            if (countryModel.ErrorMessages.Count == 0 && (countryModel.CountryName.StringVal().ToLower() != country.CountryName.StringVal().ToLower()))
            {
                bool isCountryExists = this.countryRepository.CheckCountryExistByName(countryModel);

                if (isCountryExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                country.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                country = countryModel.FromModel(country);

                //Save Or Update country details
                bool isSaved = this.countryRepository.SaveOrUpdateCountryDetails(country);

                response.Result = isSaved ? "Success" : "Failed";
            }
            else
            {
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            return response;
        }

        /// <summary>
        /// Delete a record by CountryCode
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public bool DeleteById(string countryCode)
        {
            return this.countryRepository.DeleteById(countryCode);
        }

        /// <summary>
        /// Check duplicate Country by Name
        /// </summary>
        /// <param name="countryModel"></param>
        /// <returns></returns>
        public bool CheckCountryExistByName(CountryModel countryModel)
        {
            return this.countryRepository.CheckCountryExistByName(countryModel);
        }

        /// <summary>}
        /// List of Country with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<CountryViewModel> GetCountriesByCriteria(SearchCountryModel searchModel)
        {
            return this.countryRepository.GetCountriesByCriteria(searchModel);
        }
    }
}