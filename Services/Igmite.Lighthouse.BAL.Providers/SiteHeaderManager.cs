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
    /// Manager of the SiteHeader entity
    /// </summary>
    public class SiteHeaderManager : GenericManager<SiteHeaderModel>, ISiteHeaderManager
    {
        private readonly ISiteHeaderRepository siteHeaderRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the siteHeader manager.
        /// </summary>
        /// <param name="siteHeaderRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public SiteHeaderManager(ISiteHeaderRepository _siteHeaderRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.siteHeaderRepository = _siteHeaderRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of SiteHeaders
        /// </summary>
        /// <returns></returns>
        public IQueryable<SiteHeaderModel> GetSiteHeaders()
        {
            var siteHeaders = this.siteHeaderRepository.GetSiteHeaders();

            IList<SiteHeaderModel> siteHeaderModels = new List<SiteHeaderModel>();
            siteHeaders.ForEach((user) => siteHeaderModels.Add(user.ToModel()));

            return siteHeaderModels.AsQueryable();
        }

        /// <summary>
        /// Get list of SiteHeaders by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<SiteHeaderModel> GetSiteHeadersByName(string siteHeaderName)
        {
            var siteHeaders = this.siteHeaderRepository.GetSiteHeadersByName(siteHeaderName);

            IList<SiteHeaderModel> siteHeaderModels = new List<SiteHeaderModel>();
            siteHeaders.ForEach((user) => siteHeaderModels.Add(user.ToModel()));

            return siteHeaderModels.AsQueryable();
        }

        /// <summary>
        /// Get SiteHeader by SiteHeaderId
        /// </summary>
        /// <param name="siteHeaderId"></param>
        /// <returns></returns>
        public SiteHeaderModel GetSiteHeaderById(Guid siteHeaderId)
        {
            SiteHeader siteHeader = this.siteHeaderRepository.GetSiteHeaderById(siteHeaderId);

            return (siteHeader != null) ? siteHeader.ToModel() : null;
        }

        /// <summary>
        /// Get SiteHeader by SiteHeaderId using async
        /// </summary>
        /// <param name="siteHeaderId"></param>
        /// <returns></returns>
        public async Task<SiteHeaderModel> GetSiteHeaderByIdAsync(Guid siteHeaderId)
        {
            var siteHeader = await this.siteHeaderRepository.GetSiteHeaderByIdAsync(siteHeaderId);

            return (siteHeader != null) ? siteHeader.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update SiteHeader entity
        /// </summary>
        /// <param name="siteHeaderModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateSiteHeaderDetails(SiteHeaderModel siteHeaderModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            SiteHeader siteHeader = null;

            //Validate model data
            siteHeaderModel = siteHeaderModel.GetModelValidationErrors<SiteHeaderModel>();

            if (siteHeaderModel.ErrorMessages.Count > 0)
            {
                response.Errors = siteHeaderModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (siteHeaderModel.RequestType == RequestType.Edit)
            {
                siteHeader = this.siteHeaderRepository.GetSiteHeaderById(siteHeaderModel.SiteHeaderId);

                //siteHeader.SubHeaders.ForEach((oldSubHeaderItem) =>
                //{
                //    var valSubHeaderItem = siteHeaderModel.SubHeaderModels.FirstOrDefault(a => a.SiteSubHeaderId == oldSubHeaderItem.SiteSubHeaderId);
                //    if (valSubHeaderItem == null)
                //    {
                //        siteHeader.Deleted//s.Add(oldSubIdItem.SubHeader);
                //    }
                //});
            }
            else
            {
                siteHeader = new SiteHeader();
                siteHeaderModel.SiteHeaderId = Guid.NewGuid();
            }

            if (siteHeaderModel.ErrorMessages.Count == 0 && (siteHeaderModel.ShortName.StringVal().ToLower() != siteHeader.ShortName.StringVal().ToLower()))
            {
                bool isSiteHeaderExists = this.siteHeaderRepository.CheckSiteHeaderExistByName(siteHeaderModel);

                if (isSiteHeaderExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                siteHeader.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                siteHeader = siteHeaderModel.FromModel(siteHeader);

                //Save Or Update siteHeader details
                bool isSaved = this.siteHeaderRepository.SaveOrUpdateSiteHeaderDetails(siteHeader);

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
        /// Delete a record by SiteHeaderId
        /// </summary>
        /// <param name="siteHeaderId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid siteHeaderId)
        {
            return this.siteHeaderRepository.DeleteById(siteHeaderId);
        }

        /// <summary>
        /// Check duplicate SiteHeader by Name
        /// </summary>
        /// <param name="siteHeaderModel"></param>
        /// <returns></returns>
        public bool CheckSiteHeaderExistByName(SiteHeaderModel siteHeaderModel)
        {
            return this.siteHeaderRepository.CheckSiteHeaderExistByName(siteHeaderModel);
        }

        /// <summary>}
        /// List of SiteHeader with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<SiteHeaderViewModel> GetSiteHeadersByCriteria(SearchSiteHeaderModel searchModel)
        {
            return this.siteHeaderRepository.GetSiteHeadersByCriteria(searchModel);
        }
    }
}