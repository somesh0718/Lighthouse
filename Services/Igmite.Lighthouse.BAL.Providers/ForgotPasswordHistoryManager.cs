using Igmite.Lighthouse.BAL.Validations;
using Igmite.Lighthouse.Cryptography;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Mappers;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Manager of the ForgotPasswordHistory entity
    /// </summary>
    public class ForgotPasswordHistoryManager : GenericManager<ForgotPasswordHistoryModel>, IForgotPasswordHistoryManager
    {
        private readonly IForgotPasswordHistoryRepository forgotPasswordHistoryRepository;

        /// <summary>
        /// Initializes the forgotPasswordHistory manager.
        /// </summary>
        /// <param name="forgotPasswordHistoryRepository"></param>
        public ForgotPasswordHistoryManager(IForgotPasswordHistoryRepository _forgotPasswordHistoryRepository)
        {
            this.forgotPasswordHistoryRepository = _forgotPasswordHistoryRepository;
        }

        /// <summary>
        /// Get list of ForgotPasswordHistories
        /// </summary>
        /// <returns></returns>
        public IQueryable<ForgotPasswordHistoryModel> GetForgotPasswordHistories()
        {
            var forgotPasswordHistories = this.forgotPasswordHistoryRepository.GetForgotPasswordHistories();

            IList<ForgotPasswordHistoryModel> forgotPasswordHistoryModels = new List<ForgotPasswordHistoryModel>();
            forgotPasswordHistories.ForEach((user) => forgotPasswordHistoryModels.Add(user.ToModel()));

            return forgotPasswordHistoryModels.AsQueryable();
        }

        /// <summary>
        /// Get list of ForgotPasswordHistories by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<ForgotPasswordHistoryModel> GetForgotPasswordHistoriesByName(string forgotPasswordHistoryName)
        {
            var forgotPasswordHistories = this.forgotPasswordHistoryRepository.GetForgotPasswordHistoriesByName(forgotPasswordHistoryName);

            IList<ForgotPasswordHistoryModel> forgotPasswordHistoryModels = new List<ForgotPasswordHistoryModel>();
            forgotPasswordHistories.ForEach((user) => forgotPasswordHistoryModels.Add(user.ToModel()));

            return forgotPasswordHistoryModels.AsQueryable();
        }

        /// <summary>
        /// Get ForgotPasswordHistory by ForgotPasswordId
        /// </summary>
        /// <param name="forgotPasswordId"></param>
        /// <returns></returns>
        public ForgotPasswordHistoryModel GetForgotPasswordHistoryById(Guid forgotPasswordId)
        {
            ForgotPasswordHistory forgotPasswordHistory = this.forgotPasswordHistoryRepository.GetForgotPasswordHistoryById(forgotPasswordId);

            return (forgotPasswordHistory != null) ? forgotPasswordHistory.ToModel() : null;
        }

        /// <summary>
        /// Get ForgotPasswordHistory by ForgotPasswordId using async
        /// </summary>
        /// <param name="forgotPasswordId"></param>
        /// <returns></returns>
        public async Task<ForgotPasswordHistoryModel> GetForgotPasswordHistoryByIdAsync(Guid forgotPasswordId)
        {
            var forgotPasswordHistory = await this.forgotPasswordHistoryRepository.GetForgotPasswordHistoryByIdAsync(forgotPasswordId);

            return (forgotPasswordHistory != null) ? forgotPasswordHistory.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update ForgotPasswordHistory entity
        /// </summary>
        /// <param name="forgotPasswordHistoryModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateForgotPasswordHistoryDetails(ForgotPasswordHistoryModel forgotPasswordHistoryModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            ForgotPasswordHistory forgotPasswordHistory = null;

            //Validate model data
            forgotPasswordHistoryModel = forgotPasswordHistoryModel.GetModelValidationErrors<ForgotPasswordHistoryModel>();

            if (forgotPasswordHistoryModel.ErrorMessages.Count > 0)
            {
                response.Errors = forgotPasswordHistoryModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (forgotPasswordHistoryModel.RequestType == RequestType.Edit)
            {
                forgotPasswordHistory = this.forgotPasswordHistoryRepository.GetForgotPasswordHistoryById(forgotPasswordHistoryModel.ForgotPasswordId);
            }
            else
            {
                forgotPasswordHistory = new ForgotPasswordHistory();
                forgotPasswordHistoryModel.ForgotPasswordId = Guid.NewGuid();
            }

            if (forgotPasswordHistoryModel.ErrorMessages.Count == 0 && (forgotPasswordHistoryModel.RequestDate.StringVal().ToLower() != forgotPasswordHistory.RequestDate.StringVal().ToLower()))
            {
                bool isForgotPasswordHistoryExists = this.forgotPasswordHistoryRepository.CheckForgotPasswordHistoryExistByName(forgotPasswordHistoryModel);

                if (isForgotPasswordHistoryExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                forgotPasswordHistory = forgotPasswordHistoryModel.FromModel(forgotPasswordHistory);

                //Save Or Update forgotPasswordHistory details
                bool isSaved = this.forgotPasswordHistoryRepository.SaveOrUpdateForgotPasswordHistoryDetails(forgotPasswordHistory);

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
        /// Delete a record by ForgotPasswordId
        /// </summary>
        /// <param name="forgotPasswordId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid forgotPasswordId)
        {
            return this.forgotPasswordHistoryRepository.DeleteById(forgotPasswordId);
        }

        /// <summary>
        /// Check duplicate ForgotPasswordHistory by Name
        /// </summary>
        /// <param name="forgotPasswordHistoryModel"></param>
        /// <returns></returns>
        public bool CheckForgotPasswordHistoryExistByName(ForgotPasswordHistoryModel forgotPasswordHistoryModel)
        {
            return this.forgotPasswordHistoryRepository.CheckForgotPasswordHistoryExistByName(forgotPasswordHistoryModel);
        }

        /// <summary>}
        /// List of ForgotPasswordHistory with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<ForgotPasswordHistoryViewModel> GetForgotPasswordHistoriesByCriteria(SearchForgotPasswordHistoryModel searchModel)
        {
            return this.forgotPasswordHistoryRepository.GetForgotPasswordHistoriesByCriteria(searchModel);
        }
    }
}
