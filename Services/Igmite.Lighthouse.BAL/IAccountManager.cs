using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the Account entity
    /// </summary>
    public interface IAccountManager : IGenericManager<AccountModel>
    {
        /// <summary>
        /// Get list of Accounts
        /// </summary>
        /// <returns></returns>
        IQueryable<AccountModel> GetAccounts();

        /// <summary>
        /// Get list of Accounts by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<AccountModel> GetAccountsByName(string accountName);

        /// <summary>
        /// Get Account by AccountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        AccountModel GetAccountById(Guid accountId);

        /// <summary>
        /// User Login by Id
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        AccountModel GetAccountByLoginId(string loginId);

        /// <summary>
        /// Get Account by AccountId using async
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<AccountModel> GetAccountByIdAsync(Guid accountId);

        /// <summary>
        /// Insert/Update Account entity
        /// </summary>
        /// <param name="accountModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateAccountDetails(AccountModel accountModel);

        /// <summary>
        /// Delete a record by AccountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        bool DeleteById(Guid accountId);

        /// <summary>
        /// Check duplicate Account by Name
        /// </summary>
        /// <param name="accountModel"></param>
        /// <returns></returns>
        List<string> CheckAccountExistByName(AccountModel accountModel);

        /// <summary>
        /// List of Account with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<AccountViewModel> GetAccountsByCriteria(SearchAccountModel searchModel);

        /// <summary>
        /// Validate login by User Id in Igmite Application
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        SingularResponse<LoginResponce> GetAccountByUserId(LoginRequest loginRequest);

        /// <summary>
        /// Change Password by User Id
        /// </summary>
        /// <param name="changePasswordRequest"></param>
        /// <returns></returns>
        SingularResponse<LoginResponce> ChangePasswordByUserId(ChangePasswordModel changePasswordRequest);

        /// <summary>
        /// Forgot Password by User Id
        /// </summary>
        /// <param name="forgotPasswordRequest"></param>
        /// <returns></returns>
        Task<SingularResponse<string>> ForgotPasswordByUserId(ForgotPasswordRequest forgotPasswordRequest);

        /// <summary>
        /// Reset Password by User Id
        /// </summary>
        /// <param name="resetPasswordRequest"></param>
        /// <returns></returns>
        Task<SingularResponse<string>> ResetPasswordByUserId(ResetPasswordRequest resetPasswordRequest);

        /// <summary>
        /// Logout by User Id
        /// </summary>
        /// <param name="logoutRequest"></param>
        /// <returns></returns>
        SingularResponse<string> LogoutByUserId(LogoutRequest logoutRequest);

        /// <summary>}
        /// User's Transactions by User Id in Igmite Application
        /// </summary>}
        /// <param name="loginRequest"></param>}
        /// <returns></returns>}
        IList<RoleTransactionResponce> GetUserTransactionsById(LoginRequest loginRequest);

        /// <summary>
        /// Update AuthToken Async after user logged-in application
        /// </summary>
        /// <param name="loginResponce"></param>
        /// <param name="loginType"></param>
        /// <returns></returns>
        Task<int> UpdateUserAuthTokenAsync(LoginResponce loginResponce, string loginType);

        /// <summary>}
        /// Change User LoginId by Admin
        /// </summary>}
        /// <param name="changeLoginModel"></param>}
        /// <returns></returns>}
        bool ChangeUserLoginId(ChangeLoginModel changeLoginModel);
    }
}