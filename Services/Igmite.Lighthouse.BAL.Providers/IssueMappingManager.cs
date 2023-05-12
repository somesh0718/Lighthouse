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
using System.Text;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Manager of the HeadMaster entity
    /// </summary>
    public class IssueMappingManager : GenericManager<IssueMappingModel>, IIssueMappingManager
    {
        private readonly IIssueMappingRepository issueMappingRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the IssueMapping manager.
        /// </summary>
        /// <param name="IssueMappingRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public IssueMappingManager(IIssueMappingRepository _IssueMappingRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.issueMappingRepository = _IssueMappingRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of IssueMappings
        /// </summary>
        /// <returns></returns>
        public IQueryable<IssueMappingModel> GetIssueMapping()
        {
            var IssueMappings = this.issueMappingRepository.GetIssueMapping();

            IList<IssueMappingModel> IssueMappingModels = new List<IssueMappingModel>();
            IssueMappings.ForEach((user) => IssueMappingModels.Add(user.ToModel()));

            return IssueMappingModels.AsQueryable();
        }

        /// <summary>
        /// Get list of IssueMappings by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<IssueMappingModel> GetIssueMappingByIssueId(string MainIssueId, string SubIssueId)
        {
            var IssueMappings = this.issueMappingRepository.GetIssueMappingByIssueId(MainIssueId, SubIssueId);

            IList<IssueMappingModel> IssueMappingModels = new List<IssueMappingModel>();
            IssueMappings.ForEach((user) => IssueMappingModels.Add(user.ToModel()));

            return IssueMappingModels.AsQueryable();
        }

        /// <summary>
        /// Get IssueMapping by IssueMappingId
        /// </summary>
        /// <param name="IssueMappingId"></param>
        /// <returns></returns>
        public IssueMappingModel GetIssueMappingById(Guid IssueMappingId)
        {
            IssueMapping issueMapping = this.issueMappingRepository.GetIssueMappingById(IssueMappingId);

            return (issueMapping != null) ? issueMapping.ToModel() : null;
        }

        /// <summary>
        /// Get IssueMapping by IssueMappingId using async
        /// </summary>
        /// <param name="IssueMappingId"></param>
        /// <returns></returns>
        public async Task<IssueMappingModel> GetIssueMappingByIdAsync(Guid IssueMappingId)
        {
            var IssueMapping = await this.issueMappingRepository.GetIssueMappingByIdAsync(IssueMappingId);

            return (IssueMapping != null) ? IssueMapping.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update IssueMapping entity
        /// </summary>
        /// <param name="IssueMappingModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateIssueMappingDetails(IssueMappingModel IssueMappingModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            IssueMapping IssueMapping = null;

            //Validate model data
            IssueMappingModel = IssueMappingModel.GetModelValidationErrors<IssueMappingModel>();

            if (IssueMappingModel.ErrorMessages.Count > 0)
            {
                response.Errors = IssueMappingModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (IssueMappingModel.RequestType == RequestType.Edit)
            {
                IssueMapping = this.issueMappingRepository.GetIssueMappingById(IssueMappingModel.IssueMappingId);
            }
            else
            {
                IssueMapping = new IssueMapping();
                IssueMappingModel.IssueMappingId = Guid.NewGuid();
            }

            if (response.Errors.Count == 0)
            {
                IssueMapping.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                IssueMapping = IssueMappingModel.FromModel(IssueMapping);

                //Save Or Update IssueMapping details
                bool isSaved = this.issueMappingRepository.SaveOrUpdateIssueMappingDetails(IssueMapping);

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
        /// Delete a record by IssueMappingId
        /// </summary>
        /// <param name="IssueMappingId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid IssueMappingId)
        {
            return this.issueMappingRepository.DeleteById(IssueMappingId);
        }

        /// <summary>
        /// Check duplicate IssueMapping by Name
        /// </summary>
        /// <param name="IssueMappingModel"></param>
        /// <returns></returns>
        public string CheckIssueMappingExistByName(IssueMappingModel IssueMappingModel)
        {
            return this.issueMappingRepository.CheckIssueMappingExistByName(IssueMappingModel);
        }

        /// <summary>
        /// List of IssueMapping with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IList<IssueMappingViewModel> GetIssueMappingByCriteria(SearchIssueMappingModel searchModel)
        {
            return this.issueMappingRepository.GetIssueMappingByCriteria(searchModel);
        }

        /// <summary>
        /// List of Issue by userId with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IList<IssueViewModel> GetIssueByCriteria(SearchIssueModel searchModel)
        {
            return this.issueMappingRepository.GetIssueByCriteria(searchModel);
        }
    }
}
