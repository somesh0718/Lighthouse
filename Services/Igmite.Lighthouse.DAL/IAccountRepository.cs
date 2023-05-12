using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the Account entity
    /// </summary>
    public interface IAccountRepository : IGenericRepository<Account>
    {
        /// <summary>
        /// Get list of Account
        /// </summary>
        /// <returns></returns>
        IQueryable<Account> GetAccounts();

        /// <summary>
        /// Get list of Account by accountName
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        IQueryable<Account> GetAccountsByName(string accountName);

        /// <summary>
        /// Get Account by AccountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Account GetAccountById(Guid accountId);

        /// <summary>
        /// User Login by Id
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        Account GetAccountByLoginId(string loginId);

        /// <summary>
        /// Get Account by LoginId using async
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        Task<Account> GetAccountByLoginIdAsync(string loginId);

        /// <summary>
        /// Get Account by AccountId using async
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<Account> GetAccountByIdAsync(Guid accountId);

        /// <summary>
        /// Insert/Update Account entity
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool SaveOrUpdateAccountDetails(Account account);

        /// <summary>
        /// Insert/Update Account Async entity
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<bool> SaveOrUpdateAccountAsync(Account account);

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
        /// Get Role by Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Role GetRoleByCode(string code);

        /// <summary>
        /// Get Role by Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Role GetRoleByRoleId(Guid roleId);

        /// <summary>
        /// Get Role by Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Role GetRoleByAccountId(Guid accountId);

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
        LoginResponce ValidateUserLogin(LoginRequest loginRequest);

        /// <summary>
        /// Get user transaction details with rights
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        LoginResponce GetUserWithTransactionsById(LoginRequest loginRequest);

        /// <summary>
        /// Get Role Transactions By User Id
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        IList<RoleTransactionResponce> GetRoleTransactionsById(LoginRequest loginRequest);

        /// <summary>
        /// Get User Transactions By User Id
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        IList<UserTransactionResponce> GetUserTransactionsById(LoginRequest loginRequest);

        /// <summary>
        /// Get AccountRole by Id
        /// </summary>
        /// <param name="accountRoleId"></param>
        /// <returns></returns>
        AccountRole GetAccountRoleById(Guid accountId);

        /// <summary>
        /// Insert/Update Account Role entity
        /// </summary>
        /// <param name="accountRole"></param>
        /// <returns></returns>
        bool SaveOrUpdateAccountRoleDetails(AccountRole accountRole);

        /// <summary>
        /// Get Unique User Id by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetUniqueUserId(string userId);

        /// <summary>
        /// Insert/Update LogoutHistory entity
        /// </summary>
        /// <param name="logoutHistory"></param>
        /// <returns></returns>
        bool SaveOrUpdateLogoutHistoryDetails(LogoutHistory logoutHistory, Account account);

        /// <summary>}
        /// Change User LoginId by Admin
        /// </summary>}
        /// <param name="changeLoginModel"></param>}
        /// <returns></returns>}
        bool ChangeUserLoginId(ChangeLoginModel changeLoginModel);

        /// <summary>
        /// Update AuthToken Async after user logged-in application
        /// </summary>
        /// <param name="loginResponce"></param>
        /// <param name="loginType"></param>
        /// <returns></returns>
        Task<int> UpdateUserAuthTokenAsync(LoginResponce loginResponce, string loginType);

        /// <summary>
        /// Get Account Work Locations By User Id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        List<AccountWorkLocationModel> GetAccountWorkLocationsByCriteria(Guid accountId);

        /// <summary>
        /// Get Account Work Locations By User Id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        List<AccountWorkLocation> GetAccountWorkLocationsById(Guid accountId);
    }
}