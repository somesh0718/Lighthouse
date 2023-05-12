using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the UserOTPDetail entity
    /// </summary>
    public interface IUserOTPDetailRepository : IGenericRepository<UserOTPDetail>
    {
        /// <summary>
        /// Get list of UserOTPDetail
        /// </summary>
        /// <returns></returns>
        IQueryable<UserOTPDetail> GetUserOTPDetails();

        /// <summary>
        /// Get list of UserOTPDetail by userOTPDetailName
        /// </summary>
        /// <param name="userOTPDetailName"></param>
        /// <returns></returns>
        IQueryable<UserOTPDetail> GetUserOTPDetailsByName(string userOTPDetailName);

        /// <summary>
        /// Get UserOTPDetail by OTPId
        /// </summary>
        /// <param name="otpId"></param>
        /// <returns></returns>
        UserOTPDetail GetUserOTPDetailById(Guid otpId);

        /// <summary>
        /// Get UserOTPDetail by OTPId using async
        /// </summary>
        /// <param name="otpId"></param>
        /// <returns></returns>
        Task<UserOTPDetail> GetUserOTPDetailByIdAsync(Guid otpId);

        /// <summary>
        /// Insert/Update UserOTPDetail entity
        /// </summary>
        /// <param name="userOTPDetail"></param>
        /// <returns></returns>
        bool SaveOrUpdateUserOTPDetailDetails(UserOTPDetail userOTPDetail);

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
