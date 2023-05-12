using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.Controllers
{
    /// <summary>
    /// Expose all country WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class CountryController : BaseController
    {
        private readonly ICountryManager countryManager;

        /// <summary>
        /// Initializes the Country controller class.
        /// </summary>
        /// <param name="_countryManager"></param>
        public CountryController(ICountryManager _countryManager)
        {
            this.countryManager = _countryManager;
        }

        /// <summary>
        /// Get list of country data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetCountries")]
        public async Task<ListResponse<CountryModel>> GetCountries()
        {
            ListResponse<CountryModel> response = new ListResponse<CountryModel>();

            try
            {
                IQueryable<CountryModel> countryModels = await Task.Run(() =>
                {
                    return this.countryManager.GetCountries();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = countryModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of Country with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetCountriesByCriteria")]
        public async Task<ListResponse<CountryViewModel>> GetCountriesByCriteria([FromBody] SearchCountryModel searchModel)
        {
            ListResponse<CountryViewModel> response = new ListResponse<CountryViewModel>();

            try
            {
                var countryModels = await Task.Run(() =>
                {
                    return this.countryManager.GetCountriesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = countryModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of country data by name
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetCountriesByName")]
        public async Task<ListResponse<CountryModel>> GetCountriesByName([FromQuery] string countryName)
        {
            ListResponse<CountryModel> response = new ListResponse<CountryModel>();

            try
            {
                var countryModels = await Task.Run(() =>
                {
                    return this.countryManager.GetCountriesByName(countryName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = countryModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get country data by Id
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        [HttpPost, Route("GetCountryById")]
        public async Task<SingularResponse<CountryModel>> GetCountryById([FromBody] DataRequest countryRequest)
        {
            SingularResponse<CountryModel> response = new SingularResponse<CountryModel>();

            try
            {
                var countryModel = await Task.Run(() =>
                {
                    return this.countryManager.GetCountryById(countryRequest.DataId);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = countryModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new country
        /// </summary>
        /// <param name="countryRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateCountry"), Route("CreateOrUpdateCountryDetails")]
        public async Task<SingularResponse<string>> CreateCountry([FromBody] CountryRequest countryRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //countryRequest.RequestType = RequestType.New;
                    return this.countryManager.SaveOrUpdateCountryDetails(countryRequest);
                });

                if (response.Errors.Count == 0)
                {
                    response.Messages.Add(Constants.CreatedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Update country by Id
        /// </summary>
        /// <param name="countryRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateCountry")]
        public async Task<SingularResponse<string>> UpdateCountry([FromBody] CountryRequest countryRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    countryRequest.RequestType = RequestType.Edit;
                    return this.countryManager.SaveOrUpdateCountryDetails(countryRequest);
                });

                if (response.Errors.Count == 0)
                {
                    response.Messages.Add(Constants.UpdatedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Delete country by Id
        /// </summary>
        /// <param name="countryRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteCountryById")]
        public async Task<SingularResponse<bool>> DeleteCountryById([FromBody] DeleteRequest<string> countryRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.countryManager.DeleteById(countryRequest.DataId);
                });

                if (response.Result)
                {
                    response.Messages.Add(Constants.DeletedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }
    }
}