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
    /// Expose all siteHeader WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController, ApiExplorerSettings(IgnoreApi = true)]
    public class SiteHeaderController : BaseController
    {
        private readonly ISiteHeaderManager siteHeaderManager;

        /// <summary>
        /// Initializes the SiteHeader controller class.
        /// </summary>
        /// <param name="_siteHeaderManager"></param>
        public SiteHeaderController(ISiteHeaderManager _siteHeaderManager)
        {
            this.siteHeaderManager = _siteHeaderManager;
        }

        /// <summary>
        /// Get list of siteHeader data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetSiteHeaders")]
        public async Task<ListResponse<SiteHeaderModel>> GetSiteHeaders()
        {
            ListResponse<SiteHeaderModel> response = new ListResponse<SiteHeaderModel>();

            try
            {
                IQueryable<SiteHeaderModel> siteHeaderModels = await Task.Run(() =>
                {
                    return this.siteHeaderManager.GetSiteHeaders();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = siteHeaderModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of SiteHeader with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSiteHeadersByCriteria")]
        public async Task<ListResponse<SiteHeaderViewModel>> GetSiteHeadersByCriteria([FromBody] SearchSiteHeaderModel searchModel)
        {
            ListResponse<SiteHeaderViewModel> response = new ListResponse<SiteHeaderViewModel>();

            try
            {
                var siteHeaderModels = await Task.Run(() =>
                {
                    return this.siteHeaderManager.GetSiteHeadersByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = siteHeaderModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of siteHeader data by name
        /// </summary>
        /// <param name="siteHeaderName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetSiteHeadersByName")]
        public async Task<ListResponse<SiteHeaderModel>> GetSiteHeadersByName([FromQuery] string siteHeaderName)
        {
            ListResponse<SiteHeaderModel> response = new ListResponse<SiteHeaderModel>();

            try
            {
                var siteHeaderModels = await Task.Run(() =>
                {
                    return this.siteHeaderManager.GetSiteHeadersByName(siteHeaderName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = siteHeaderModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get siteHeader data by Id
        /// </summary>
        /// <param name="siteHeaderId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSiteHeaderById")]
        public async Task<SingularResponse<SiteHeaderModel>> GetSiteHeaderById([FromBody] DataRequest siteHeaderRequest)
        {
            SingularResponse<SiteHeaderModel> response = new SingularResponse<SiteHeaderModel>();

            try
            {
                var siteHeaderModel = await Task.Run(() =>
                {
                    return this.siteHeaderManager.GetSiteHeaderById(Guid.Parse(siteHeaderRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = siteHeaderModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new siteHeader
        /// </summary>
        /// <param name="siteHeaderRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateSiteHeader"), Route("CreateOrUpdateSiteHeaderDetails")]
        public async Task<SingularResponse<string>> CreateSiteHeader([FromBody] SiteHeaderRequest siteHeaderRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //siteHeaderRequest.RequestType = RequestType.New;
                    return this.siteHeaderManager.SaveOrUpdateSiteHeaderDetails(siteHeaderRequest);
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
        /// Update siteHeader by Id
        /// </summary>
        /// <param name="siteHeaderRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateSiteHeader")]
        public async Task<SingularResponse<string>> UpdateSiteHeader([FromBody] SiteHeaderRequest siteHeaderRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    siteHeaderRequest.RequestType = RequestType.Edit;
                    return this.siteHeaderManager.SaveOrUpdateSiteHeaderDetails(siteHeaderRequest);
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
        /// Delete siteHeader by Id
        /// </summary>
        /// <param name="siteHeaderRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteSiteHeaderById")]
        public async Task<SingularResponse<bool>> DeleteSiteHeaderById([FromBody] DeleteRequest<Guid> siteHeaderRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.siteHeaderManager.DeleteById(siteHeaderRequest.DataId);
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