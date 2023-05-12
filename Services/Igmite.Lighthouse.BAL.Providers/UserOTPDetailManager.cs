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
    /// Manager of the UserOTPDetail entity
    /// </summary>
    public class UserOTPDetailManager : GenericManager<UserOTPDetailModel>, IUserOTPDetailManager
    {
        private readonly IUserOTPDetailRepository userOTPDetailRepository;

        /// <summary>
        /// Initializes the userOTPDetail manager.
        /// </summary>
        /// <param name="userOTPDetailRepository"></param>
        public UserOTPDetailManager(IUserOTPDetailRepository _userOTPDetailRepository)
        {
            this.userOTPDetailRepository = _userOTPDetailRepository;
        }

        /// <summary>
        /// Get list of UserOTPDetails
        /// </summary>
        /// <returns></returns>
        public IQueryable<UserOTPDetailModel> GetUserOTPDetails()
        {
            var userOTPDetails = this.userOTPDetailRepository.GetUserOTPDetails();

            IList<UserOTPDetailModel> userOTPDetailModels = new List<UserOTPDetailModel>();
            userOTPDetails.ForEach((user) => userOTPDetailModels.Add(user.ToModel()));

            return userOTPDetailModels.AsQueryable();
        }

        /// <summary>
        /// Get list of UserOTPDetails by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<UserOTPDetailModel> GetUserOTPDetailsByName(string userOTPDetailName)
        {
            var userOTPDetails = this.userOTPDetailRepository.GetUserOTPDetailsByName(userOTPDetailName);

            IList<UserOTPDetailModel> userOTPDetailModels = new List<UserOTPDetailModel>();
            userOTPDetails.ForEach((user) => userOTPDetailModels.Add(user.ToModel()));

            return userOTPDetailModels.AsQueryable();
        }

        /// <summary>
        /// Get UserOTPDetail by OTPId
        /// </summary>
        /// <param name="otpId"></param>
        /// <returns></returns>
        public UserOTPDetailModel GetUserOTPDetailById(Guid otpId)
        {
            UserOTPDetail userOTPDetail = this.userOTPDetailRepository.GetUserOTPDetailById(otpId);

            return (userOTPDetail != null) ? userOTPDetail.ToModel() : null;
        }

        /// <summary>
        /// Get UserOTPDetail by OTPId using async
        /// </summary>
        /// <param name="otpId"></param>
        /// <returns></returns>
        public async Task<UserOTPDetailModel> GetUserOTPDetailByIdAsync(Guid otpId)
        {
            var userOTPDetail = await this.userOTPDetailRepository.GetUserOTPDetailByIdAsync(otpId);

            return (userOTPDetail != null) ? userOTPDetail.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update UserOTPDetail entity
        /// </summary>
        /// <param name="userOTPDetailModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateUserOTPDetailDetails(UserOTPDetailModel userOTPDetailModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            UserOTPDetail userOTPDetail = null;

            //Validate model data
            userOTPDetailModel = userOTPDetailModel.GetModelValidationErrors<UserOTPDetailModel>();

            if (userOTPDetailModel.ErrorMessages.Count > 0)
            {
                response.Errors = userOTPDetailModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (userOTPDetailModel.RequestType == RequestType.Edit)
            {
                userOTPDetail = this.userOTPDetailRepository.GetUserOTPDetailById(userOTPDetailModel.OTPId);

                //userOTPDetail.AccountOTPs.ForEach((oldAccountOTPItem) =>
                //{
                //    var valAccountOTPItem = userOTPDetailModel.AccountOTPModels.FirstOrDefault(a => a.AccountOTPId == oldAccountOTPItem.AccountOTPId);
                //    if (valAccountOTPItem == null)
                //    {
                //        userOTPDetail.Deleted//s.Add(oldAccountOTPIdItem.AccountOTP);
                //    }
                //});
            }
            else
            {
                userOTPDetail = new UserOTPDetail();
                userOTPDetailModel.OTPId = Guid.NewGuid();
            }

            if (userOTPDetailModel.ErrorMessages.Count == 0 && (userOTPDetailModel.IsRedeemed.StringVal().ToLower() != userOTPDetail.IsRedeemed.StringVal().ToLower()))
            {
                bool isUserOTPDetailExists = this.userOTPDetailRepository.CheckUserOTPDetailExistByName(userOTPDetailModel);

                if (isUserOTPDetailExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                userOTPDetail = userOTPDetailModel.FromModel(userOTPDetail);

                //Save Or Update userOTPDetail details
                bool isSaved = this.userOTPDetailRepository.SaveOrUpdateUserOTPDetailDetails(userOTPDetail);

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
        /// Delete a record by OTPId
        /// </summary>
        /// <param name="otpId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid otpId)
        {
            return this.userOTPDetailRepository.DeleteById(otpId);
        }

        /// <summary>
        /// Check duplicate UserOTPDetail by Name
        /// </summary>
        /// <param name="userOTPDetailModel"></param>
        /// <returns></returns>
        public bool CheckUserOTPDetailExistByName(UserOTPDetailModel userOTPDetailModel)
        {
            return this.userOTPDetailRepository.CheckUserOTPDetailExistByName(userOTPDetailModel);
        }

        /// <summary>}
        /// List of UserOTPDetail with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<UserOTPDetailViewModel> GetUserOTPDetailsByCriteria(SearchUserOTPDetailModel searchModel)
        {
            return this.userOTPDetailRepository.GetUserOTPDetailsByCriteria(searchModel);
        }
    }
}
