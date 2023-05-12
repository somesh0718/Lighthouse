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
    /// Expose all siteSubHeader WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController, ApiExplorerSettings(IgnoreApi = true)]
    public class SiteSubHeaderController : BaseController
    {
        private readonly ISiteSubHeaderManager siteSubHeaderManager;

        /// <summary>
        /// Initializes the SiteSubHeader controller class.
        /// </summary>
        /// <param name="_siteSubHeaderManager"></param>
        public SiteSubHeaderController(ISiteSubHeaderManager _siteSubHeaderManager)
        {
            this.siteSubHeaderManager = _siteSubHeaderManager;
        }

        /// <summary>
        /// Get list of siteSubHeader data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetSiteSubHeaders")]
        public async Task<ListResponse<SiteSubHeaderModel>> GetSiteSubHeaders()
        {
            ListResponse<SiteSubHeaderModel> response = new ListResponse<SiteSubHeaderModel>();

            try
            {
                IQueryable<SiteSubHeaderModel> siteSubHeaderModels = await Task.Run(() =>
                {
                    return this.siteSubHeaderManager.GetSiteSubHeaders();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = siteSubHeaderModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of SiteSubHeader with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSiteSubHeadersByCriteria")]
        public async Task<ListResponse<SiteSubHeaderViewModel>> GetSiteSubHeadersByCriteria([FromBody] SearchSiteSubHeaderModel searchModel)
        {
            ListResponse<SiteSubHeaderViewModel> response = new ListResponse<SiteSubHeaderViewModel>();

            try
            {
                var siteSubHeaderModels = await Task.Run(() =>
                {
                    return this.siteSubHeaderManager.GetSiteSubHeadersByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = siteSubHeaderModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of siteSubHeader data by name
        /// </summary>
        /// <param name="siteSubHeaderName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetSiteSubHeadersByName")]
        public async Task<ListResponse<SiteSubHeaderModel>> GetSiteSubHeadersByName([FromQuery] string siteSubHeaderName)
        {
            ListResponse<SiteSubHeaderModel> response = new ListResponse<SiteSubHeaderModel>();

            try
            {
                var siteSubHeaderModels = await Task.Run(() =>
                {
                    return this.siteSubHeaderManager.GetSiteSubHeadersByName(siteSubHeaderName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = siteSubHeaderModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get siteSubHeader data by Id
        /// </summary>
        /// <param name="siteSubHeaderId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSiteSubHeaderById")]
        public async Task<SingularResponse<SiteSubHeaderModel>> GetSiteSubHeaderById([FromBody] DataRequest siteSubHeaderRequest)
        {
            SingularResponse<SiteSubHeaderModel> response = new SingularResponse<SiteSubHeaderModel>();

            try
            {
                var siteSubHeaderModel = await Task.Run(() =>
                {
                    return this.siteSubHeaderManager.GetSiteSubHeaderById(Guid.Parse(siteSubHeaderRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = siteSubHeaderModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new siteSubHeader
        /// </summary>
        /// <param name="siteSubHeaderRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateSiteSubHeader"), Route("CreateOrUpdateSiteSubHeaderDetails")]
        public async Task<SingularResponse<string>> CreateSiteSubHeader([FromBody] SiteSubHeaderRequest siteSubHeaderRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //siteSubHeaderRequest.RequestType = RequestType.New;
                    return this.siteSubHeaderManager.SaveOrUpdateSiteSubHeaderDetails(siteSubHeaderRequest);
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
        /// Update siteSubHeader by Id
        /// </summary>
        /// <param name="siteSubHeaderRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateSiteSubHeader")]
        public async Task<SingularResponse<string>> UpdateSiteSubHeader([FromBody] SiteSubHeaderRequest siteSubHeaderRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    siteSubHeaderRequest.RequestType = RequestType.Edit;
                    return this.siteSubHeaderManager.SaveOrUpdateSiteSubHeaderDetails(siteSubHeaderRequest);
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
        /// Delete siteSubHeader by Id
        /// </summary>
        /// <param name="siteSubHeaderRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteSiteSubHeaderById")]
        public async Task<SingularResponse<bool>> DeleteSiteSubHeaderById([FromBody] DeleteRequest<Guid> siteSubHeaderRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.siteSubHeaderManager.DeleteById(siteSubHeaderRequest.DataId);
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