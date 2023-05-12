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
    /// Manager of the SiteSubHeader entity
    /// </summary>
    public class SiteSubHeaderManager : GenericManager<SiteSubHeaderModel>, ISiteSubHeaderManager
    {
        private readonly ISiteSubHeaderRepository siteSubHeaderRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the siteSubHeader manager.
        /// </summary>
        /// <param name="siteSubHeaderRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public SiteSubHeaderManager(ISiteSubHeaderRepository _siteSubHeaderRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.siteSubHeaderRepository = _siteSubHeaderRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of SiteSubHeaders
        /// </summary>
        /// <returns></returns>
        public IQueryable<SiteSubHeaderModel> GetSiteSubHeaders()
        {
            var siteSubHeaders = this.siteSubHeaderRepository.GetSiteSubHeaders();

            IList<SiteSubHeaderModel> siteSubHeaderModels = new List<SiteSubHeaderModel>();
            siteSubHeaders.ForEach((user) => siteSubHeaderModels.Add(user.ToModel()));

            return siteSubHeaderModels.AsQueryable();
        }

        /// <summary>
        /// Get list of SiteSubHeaders by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<SiteSubHeaderModel> GetSiteSubHeadersByName(string siteSubHeaderName)
        {
            var siteSubHeaders = this.siteSubHeaderRepository.GetSiteSubHeadersByName(siteSubHeaderName);

            IList<SiteSubHeaderModel> siteSubHeaderModels = new List<SiteSubHeaderModel>();
            siteSubHeaders.ForEach((user) => siteSubHeaderModels.Add(user.ToModel()));

            return siteSubHeaderModels.AsQueryable();
        }

        /// <summary>
        /// Get SiteSubHeader by SiteSubHeaderId
        /// </summary>
        /// <param name="siteSubHeaderId"></param>
        /// <returns></returns>
        public SiteSubHeaderModel GetSiteSubHeaderById(Guid siteSubHeaderId)
        {
            SiteSubHeader siteSubHeader = this.siteSubHeaderRepository.GetSiteSubHeaderById(siteSubHeaderId);

            return (siteSubHeader != null) ? siteSubHeader.ToModel() : null;
        }

        /// <summary>
        /// Get SiteSubHeader by SiteSubHeaderId using async
        /// </summary>
        /// <param name="siteSubHeaderId"></param>
        /// <returns></returns>
        public async Task<SiteSubHeaderModel> GetSiteSubHeaderByIdAsync(Guid siteSubHeaderId)
        {
            var siteSubHeader = await this.siteSubHeaderRepository.GetSiteSubHeaderByIdAsync(siteSubHeaderId);

            return (siteSubHeader != null) ? siteSubHeader.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update SiteSubHeader entity
        /// </summary>
        /// <param name="siteSubHeaderModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateSiteSubHeaderDetails(SiteSubHeaderModel siteSubHeaderModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            SiteSubHeader siteSubHeader = null;

            //Validate model data
            siteSubHeaderModel = siteSubHeaderModel.GetModelValidationErrors<SiteSubHeaderModel>();

            if (siteSubHeaderModel.ErrorMessages.Count > 0)
            {
                response.Errors = siteSubHeaderModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (siteSubHeaderModel.RequestType == RequestType.Edit)
            {
                siteSubHeader = this.siteSubHeaderRepository.GetSiteSubHeaderById(siteSubHeaderModel.SiteSubHeaderId);
            }
            else
            {
                siteSubHeader = new SiteSubHeader();
                siteSubHeaderModel.SiteSubHeaderId = Guid.NewGuid();
            }

            if (siteSubHeaderModel.ErrorMessages.Count == 0 && (siteSubHeaderModel.SiteHeaderId != siteSubHeader.SiteHeaderId && siteSubHeaderModel.TransactionId != siteSubHeader.TransactionId))
            {
                bool isSiteSubHeaderExists = this.siteSubHeaderRepository.CheckSiteSubHeaderExistByName(siteSubHeaderModel);

                if (isSiteSubHeaderExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                siteSubHeader.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                siteSubHeader = siteSubHeaderModel.FromModel(siteSubHeader);

                //Save Or Update siteSubHeader details
                bool isSaved = this.siteSubHeaderRepository.SaveOrUpdateSiteSubHeaderDetails(siteSubHeader);

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
        /// Delete a record by SiteSubHeaderId
        /// </summary>
        /// <param name="siteSubHeaderId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid siteSubHeaderId)
        {
            return this.siteSubHeaderRepository.DeleteById(siteSubHeaderId);
        }

        /// <summary>
        /// Check duplicate SiteSubHeader by Name
        /// </summary>
        /// <param name="siteSubHeaderModel"></param>
        /// <returns></returns>
        public bool CheckSiteSubHeaderExistByName(SiteSubHeaderModel siteSubHeaderModel)
        {
            return this.siteSubHeaderRepository.CheckSiteSubHeaderExistByName(siteSubHeaderModel);
        }

        /// <summary>}
        /// List of SiteSubHeader with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<SiteSubHeaderViewModel> GetSiteSubHeadersByCriteria(SearchSiteSubHeaderModel searchModel)
        {
            return this.siteSubHeaderRepository.GetSiteSubHeadersByCriteria(searchModel);
        }
    }
}