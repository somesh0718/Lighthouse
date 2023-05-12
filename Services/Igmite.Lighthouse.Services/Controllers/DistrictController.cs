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
    /// Expose all district WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class DistrictController : BaseController
    {
        private readonly IDistrictManager districtManager;

        /// <summary>
        /// Initializes the District controller class.
        /// </summary>
        /// <param name="_districtManager"></param>
        public DistrictController(IDistrictManager _districtManager)
        {
            this.districtManager = _districtManager;
        }

        /// <summary>
        /// Get list of district data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetDistricts")]
        public async Task<ListResponse<DistrictModel>> GetDistricts()
        {
            ListResponse<DistrictModel> response = new ListResponse<DistrictModel>();

            try
            {
                IQueryable<DistrictModel> districtModels = await Task.Run(() =>
                {
                    return this.districtManager.GetDistricts();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = districtModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of District with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetDistrictsByCriteria")]
        public async Task<ListResponse<DistrictViewModel>> GetDistrictsByCriteria([FromBody] SearchDistrictModel searchModel)
        {
            ListResponse<DistrictViewModel> response = new ListResponse<DistrictViewModel>();

            try
            {
                var districtModels = await Task.Run(() =>
                {
                    return this.districtManager.GetDistrictsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = districtModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of District with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetDistrictsByCriteriaTmp")]
        public async Task<ListResponse<DistrictViewModel>> GetDistrictsByCriteriaTmp([FromBody] SearchDistrictModel searchModel)
        {
            ListResponse<DistrictViewModel> response = new ListResponse<DistrictViewModel>();

            try
            {
                var districtModels = await Task.Run(() =>
                {
                    searchModel.PageSize = 15;
                    return this.districtManager.GetDistrictsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = districtModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of district data by name
        /// </summary>
        /// <param name="districtName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetDistrictsByName")]
        public async Task<ListResponse<DistrictModel>> GetDistrictsByName([FromQuery] string districtName)
        {
            ListResponse<DistrictModel> response = new ListResponse<DistrictModel>();

            try
            {
                var districtModels = await Task.Run(() =>
                {
                    return this.districtManager.GetDistrictsByName(districtName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = districtModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get district data by Id
        /// </summary>
        /// <param name="districtCode"></param>
        /// <returns></returns>
        [HttpPost, Route("GetDistrictById")]
        public async Task<SingularResponse<DistrictModel>> GetDistrictById([FromBody] DataRequest districtRequest)
        {
            SingularResponse<DistrictModel> response = new SingularResponse<DistrictModel>();

            try
            {
                var districtModel = await Task.Run(() =>
                {
                    return this.districtManager.GetDistrictById(districtRequest.DataId);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = districtModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new district
        /// </summary>
        /// <param name="districtRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateDistrict"), Route("CreateOrUpdateDistrictDetails")]
        public async Task<SingularResponse<string>> CreateDistrict([FromBody] DistrictRequest districtRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //districtRequest.RequestType = RequestType.New;
                    return this.districtManager.SaveOrUpdateDistrictDetails(districtRequest);
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
        /// Update district by Id
        /// </summary>
        /// <param name="districtRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateDistrict")]
        public async Task<SingularResponse<string>> UpdateDistrict([FromBody] DistrictRequest districtRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    districtRequest.RequestType = RequestType.Edit;
                    return this.districtManager.SaveOrUpdateDistrictDetails(districtRequest);
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
        /// Delete district by Id
        /// </summary>
        /// <param name="districtRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteDistrictById")]
        public async Task<SingularResponse<bool>> DeleteDistrictById([FromBody] DeleteRequest<string> districtRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.districtManager.DeleteById(districtRequest.DataId);
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