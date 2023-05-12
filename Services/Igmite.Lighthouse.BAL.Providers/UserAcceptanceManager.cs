using Igmite.Lighthouse.BAL.Validations;
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
    /// Manager of the UserAcceptance entity
    /// </summary>
    public class UserAcceptanceManager : GenericManager<UserAcceptanceModel>, IUserAcceptanceManager
    {
        private readonly IUserAcceptanceRepository userAcceptanceRepository;

        /// <summary>
        /// Initializes the userAcceptance manager.
        /// </summary>
        /// <param name="userAcceptanceRepository"></param>
        public UserAcceptanceManager(IUserAcceptanceRepository _userAcceptanceRepository)
        {
            this.userAcceptanceRepository = _userAcceptanceRepository;
        }

        /// <summary>
        /// Get list of UserAcceptances
        /// </summary>
        /// <returns></returns>
        public IQueryable<UserAcceptanceModel> GetUserAcceptances()
        {
            var userAcceptances = this.userAcceptanceRepository.GetUserAcceptances();

            IList<UserAcceptanceModel> userAcceptanceModels = new List<UserAcceptanceModel>();
            userAcceptances.ForEach((user) => userAcceptanceModels.Add(user.ToModel()));

            return userAcceptanceModels.AsQueryable();
        }

        /// <summary>
        /// Get list of UserAcceptances by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<UserAcceptanceModel> GetUserAcceptancesByName(string userAcceptanceName)
        {
            var userAcceptances = this.userAcceptanceRepository.GetUserAcceptancesByName(userAcceptanceName);

            IList<UserAcceptanceModel> userAcceptanceModels = new List<UserAcceptanceModel>();
            userAcceptances.ForEach((user) => userAcceptanceModels.Add(user.ToModel()));

            return userAcceptanceModels.AsQueryable();
        }

        /// <summary>
        /// Get UserAcceptance by UserAcceptanceId
        /// </summary>
        /// <param name="userAcceptanceId"></param>
        /// <returns></returns>
        public UserAcceptanceModel GetUserAcceptanceById(Guid userAcceptanceId)
        {
            UserAcceptance userAcceptance = this.userAcceptanceRepository.GetUserAcceptanceById(userAcceptanceId);

            return (userAcceptance != null) ? userAcceptance.ToModel() : null;
        }

        /// <summary>
        /// Get UserAcceptance by UserAcceptanceId using async
        /// </summary>
        /// <param name="userAcceptanceId"></param>
        /// <returns></returns>
        public async Task<UserAcceptanceModel> GetUserAcceptanceByIdAsync(Guid userAcceptanceId)
        {
            var userAcceptance = await this.userAcceptanceRepository.GetUserAcceptanceByIdAsync(userAcceptanceId);

            return (userAcceptance != null) ? userAcceptance.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update UserAcceptance entity
        /// </summary>
        /// <param name="userAcceptanceModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateUserAcceptanceDetails(UserAcceptanceModel userAcceptanceModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            UserAcceptance userAcceptance = null;

            //Validate model data
            userAcceptanceModel = userAcceptanceModel.GetModelValidationErrors<UserAcceptanceModel>();

            if (userAcceptanceModel.ErrorMessages.Count > 0)
            {
                response.Errors = userAcceptanceModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (userAcceptanceModel.RequestType == RequestType.Edit)
            {
                userAcceptance = this.userAcceptanceRepository.GetUserAcceptanceById(userAcceptanceModel.UserAcceptanceId);
            }
            else
            {
                userAcceptance = new UserAcceptance();
                userAcceptanceModel.UserAcceptanceId = Guid.NewGuid();
            }

            if (userAcceptanceModel.ErrorMessages.Count == 0 && (userAcceptanceModel.CreatedOn.StringVal().ToLower() != userAcceptance.CreatedOn.StringVal().ToLower()))
            {
                bool isUserAcceptanceExists = this.userAcceptanceRepository.CheckUserAcceptanceExistByName(userAcceptanceModel);

                if (isUserAcceptanceExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                userAcceptance = userAcceptanceModel.FromModel(userAcceptance);

                //Save Or Update userAcceptance details
                bool isSaved = this.userAcceptanceRepository.SaveOrUpdateUserAcceptanceDetails(userAcceptance);

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
        /// Delete a record by UserAcceptanceId
        /// </summary>
        /// <param name="userAcceptanceId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid userAcceptanceId)
        {
            return this.userAcceptanceRepository.DeleteById(userAcceptanceId);
        }

        /// <summary>
        /// Check duplicate UserAcceptance by Name
        /// </summary>
        /// <param name="userAcceptanceModel"></param>
        /// <returns></returns>
        public bool CheckUserAcceptanceExistByName(UserAcceptanceModel userAcceptanceModel)
        {
            return this.userAcceptanceRepository.CheckUserAcceptanceExistByName(userAcceptanceModel);
        }
    }
}