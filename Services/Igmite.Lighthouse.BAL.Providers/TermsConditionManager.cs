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
    /// Manager of the TermsCondition entity
    /// </summary>
    public class TermsConditionManager : GenericManager<TermsConditionModel>, ITermsConditionManager
    {
        private readonly ITermsConditionRepository termsConditionRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the termsCondition manager.
        /// </summary>
        /// <param name="termsConditionRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public TermsConditionManager(ITermsConditionRepository _termsConditionRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.termsConditionRepository = _termsConditionRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of TermsConditions
        /// </summary>
        /// <returns></returns>
        public IQueryable<TermsConditionModel> GetTermsConditions()
        {
            var termsConditions = this.termsConditionRepository.GetTermsConditions();

            IList<TermsConditionModel> termsConditionModels = new List<TermsConditionModel>();
            termsConditions.ForEach((user) => termsConditionModels.Add(user.ToModel()));

            return termsConditionModels.AsQueryable();
        }

        /// <summary>
        /// Get list of TermsConditions by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<TermsConditionModel> GetTermsConditionsByName(string termsConditionName)
        {
            var termsConditions = this.termsConditionRepository.GetTermsConditionsByName(termsConditionName);

            IList<TermsConditionModel> termsConditionModels = new List<TermsConditionModel>();
            termsConditions.ForEach((user) => termsConditionModels.Add(user.ToModel()));

            return termsConditionModels.AsQueryable();
        }

        /// <summary>
        /// Get TermsCondition by TermsConditionId
        /// </summary>
        /// <param name="termsConditionId"></param>
        /// <returns></returns>
        public TermsConditionModel GetTermsConditionById(Guid termsConditionId)
        {
            TermsCondition termsCondition = this.termsConditionRepository.GetTermsConditionById(termsConditionId);

            return (termsCondition != null) ? termsCondition.ToModel() : null;
        }

        /// <summary>
        /// Get TermsCondition by TermsConditionId using async
        /// </summary>
        /// <param name="termsConditionId"></param>
        /// <returns></returns>
        public async Task<TermsConditionModel> GetTermsConditionByIdAsync(Guid termsConditionId)
        {
            var termsCondition = await this.termsConditionRepository.GetTermsConditionByIdAsync(termsConditionId);

            return (termsCondition != null) ? termsCondition.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update TermsCondition entity
        /// </summary>
        /// <param name="termsConditionModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateTermsConditionDetails(TermsConditionModel termsConditionModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            TermsCondition termsCondition = null;

            //Validate model data
            termsConditionModel = termsConditionModel.GetModelValidationErrors<TermsConditionModel>();

            if (termsConditionModel.ErrorMessages.Count > 0)
            {
                response.Errors = termsConditionModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (termsConditionModel.RequestType == RequestType.Edit)
            {
                termsCondition = this.termsConditionRepository.GetTermsConditionById(termsConditionModel.TermsConditionId);

                //termsCondition.AccountUserTerms.ForEach((oldAccountUserTermItem) =>
                //{
                //    var valAccountUserTermItem = termsConditionModel.AccountUserTermModels.FirstOrDefault(a => a.AccountTermsId == oldAccountUserTermItem.AccountTermsId);
                //    if (valAccountUserTermItem == null)
                //    {
                //        termsCondition.Deleted//s.Add(oldAccountIdItem.AccountUserTerm);
                //    }
                //});

                //termsCondition.UserAcceptances.ForEach((oldUserAcceptanceItem) =>
                //{
                //    var valUserAcceptanceItem = termsConditionModel.UserAcceptanceModels.FirstOrDefault(a => a.UserAcceptanceId == oldUserAcceptanceItem.UserAcceptanceId);
                //    if (valUserAcceptanceItem == null)
                //    {
                //        termsCondition.Deleted//s.Add(oldUserAcceptanceIdItem.UserAcceptance);
                //    }
                //});
            }
            else
            {
                termsCondition = new TermsCondition();
                termsConditionModel.TermsConditionId = Guid.NewGuid();
            }

            if (termsConditionModel.ErrorMessages.Count == 0 && (termsConditionModel.Name.StringVal().ToLower() != termsCondition.Name.StringVal().ToLower()))
            {
                bool isTermsConditionExists = this.termsConditionRepository.CheckTermsConditionExistByName(termsConditionModel);

                if (isTermsConditionExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                termsCondition.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                termsCondition = termsConditionModel.FromModel(termsCondition);

                //Save Or Update termsCondition details
                bool isSaved = this.termsConditionRepository.SaveOrUpdateTermsConditionDetails(termsCondition);

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
        /// Delete a record by TermsConditionId
        /// </summary>
        /// <param name="termsConditionId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid termsConditionId)
        {
            return this.termsConditionRepository.DeleteById(termsConditionId);
        }

        /// <summary>
        /// Check duplicate TermsCondition by Name
        /// </summary>
        /// <param name="termsConditionModel"></param>
        /// <returns></returns>
        public bool CheckTermsConditionExistByName(TermsConditionModel termsConditionModel)
        {
            return this.termsConditionRepository.CheckTermsConditionExistByName(termsConditionModel);
        }

        /// <summary>}
        /// List of TermsCondition with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<TermsConditionViewModel> GetTermsConditionsByCriteria(SearchTermsConditionModel searchModel)
        {
            return this.termsConditionRepository.GetTermsConditionsByCriteria(searchModel);
        }
    }
}