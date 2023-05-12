using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the UserOTPDetail entity
    /// </summary>
    public interface IUserOTPDetailManager : IGenericManager<UserOTPDetailModel>
    {
        /// <summary>
        /// Get list of UserOTPDetails
        /// </summary>
        /// <returns></returns>
        IQueryable<UserOTPDetailModel> GetUserOTPDetails();

        /// <summary>
        /// Get list of UserOTPDetails by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<UserOTPDetailModel> GetUserOTPDetailsByName(string userOTPDetailName);

        /// <summary>
        /// Get UserOTPDetail by OTPId
        /// </summary>
        /// <param name="otpId"></param>
        /// <returns></returns>
        UserOTPDetailModel GetUserOTPDetailById(Guid otpId);

        /// <summary>
        /// Get UserOTPDetail by OTPId using async
        /// </summary>
        /// <param name="otpId"></param>
        /// <returns></returns>
        Task<UserOTPDetailModel> GetUserOTPDetailByIdAsync(Guid otpId);

        /// <summary>
        /// Insert/Update UserOTPDetail entity
        /// </summary>
        /// <param name="userOTPDetailModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateUserOTPDetailDetails(UserOTPDetailModel userOTPDetailModel);

        /// <summary>
        /// Delete a record by OTPId
        /// </summary>
        /// <param name="otpId"></param>
        /// <returns></returns>
        bool DeleteById(Guid otpId);

        /// <summary>
        /// Check duplicate UserOTPDetail by Name
        /// </summary>
        /// <param name="userOTPDetailModel"></param>
        /// <returns></returns>
        bool CheckUserOTPDetailExistByName(UserOTPDetailModel userOTPDetailModel);

        /// <summary>
        /// List of UserOTPDetail with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<UserOTPDetailViewModel> GetUserOTPDetailsByCriteria(SearchUserOTPDetailModel searchModel);
    }
}
