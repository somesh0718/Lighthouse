using Igmite.Lighthouse.BAL.Validations;
using Igmite.Lighthouse.Cryptography;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.EmailServices;
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
    /// Manager of the Account entity
    /// </summary>
    public class AccountManager : GenericManager<AccountModel>, IAccountManager
    {
        private readonly IAccountRepository accountRepository;
        private readonly IEmailSender emailSender;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the account manager.
        /// </summary>
        /// <param name="accountRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public AccountManager(IAccountRepository _accountRepository, IEmailSender _emailSender, IHttpContextAccessor _httpContextAccessor)
        {
            this.accountRepository = _accountRepository;
            this.emailSender = _emailSender;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of Accounts
        /// </summary>
        /// <returns></returns>
        public IQueryable<AccountModel> GetAccounts()
        {
            var accounts = this.accountRepository.GetAccounts();

            IList<AccountModel> accountModels = new List<AccountModel>();
            accounts.ForEach((user) => accountModels.Add(user.ToModel()));

            return accountModels.AsQueryable();
        }

        /// <summary>
        /// Get list of Accounts by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<AccountModel> GetAccountsByName(string accountName)
        {
            var accounts = this.accountRepository.GetAccountsByName(accountName);

            IList<AccountModel> accountModels = new List<AccountModel>();
            accounts.ForEach((user) => accountModels.Add(user.ToModel()));

            return accountModels.AsQueryable();
        }

        /// <summary>
        /// Get Account by AccountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public AccountModel GetAccountById(Guid accountId)
        {
            AccountModel accountModel = null;
            Account account = this.accountRepository.GetAccountById(accountId);

            if (account != null)
            {
                accountModel = account.ToModel();

                Role roleItem = this.accountRepository.GetRoleByAccountId(account.AccountId);
                if (roleItem != null)
                {
                    accountModel.RoleId = roleItem.RoleId;
                    accountModel.RoleCode = roleItem.Code;
                }

                accountModel.WorkLocationModels = this.accountRepository.GetAccountWorkLocationsByCriteria(account.AccountId);
            }

            return accountModel;
        }

        /// <summary>
        /// User Login by Id
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public AccountModel GetAccountByLoginId(string loginId)
        {
            Account account = this.accountRepository.GetAccountByLoginId(loginId);

            return (account != null) ? account.ToModel() : null;
        }

        /// <summary>
        /// Get Account by AccountId using async
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<AccountModel> GetAccountByIdAsync(Guid accountId)
        {
            var account = await this.accountRepository.GetAccountByIdAsync(accountId);

            return (account != null) ? account.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update Account entity
        /// </summary>
        /// <param name="accountModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateAccountDetails(AccountModel accountModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            Account account = null;

            try
            {
                //Validate model data
                accountModel = accountModel.GetModelValidationErrors<AccountModel>();

                if (accountModel.ErrorMessages.Count > 0)
                {
                    response.Errors = accountModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (accountModel.RequestType == RequestType.Edit)
                {
                    account = this.accountRepository.GetAccountById(accountModel.AccountId);
                }
                else
                {
                    account = new Account();
                    account.AccountId = Guid.NewGuid();
                    account.Password = CryptographyManager.Encrypt(accountModel.Password, true);
                    account.UserId = accountModel.LoginId.Substring(0, accountModel.LoginId.IndexOf("@"));
                }

                if (accountModel.ErrorMessages.Count == 0)
                {
                    var errorMessages = this.accountRepository.CheckAccountExistByName(accountModel);

                    if (errorMessages.Count > 0)
                    {
                        response.Errors.Add(string.Join(",", errorMessages));
                    }
                }

                if (response.Errors.Count == 0)
                {
                    account.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                    account = accountModel.FromModel(account);

                    string userId = accountModel.LoginId.Substring(0, accountModel.LoginId.IndexOf("@"));
                    account.UserId = this.accountRepository.GetUniqueUserId(userId);
                    account.PasswordUpdateDate = Constants.GetCurrentDateTime;
                    account.PasswordExpiredOn = Constants.GetCurrentDateTime.AddMonths(6);

                    Role roleItem = this.accountRepository.GetRoleByRoleId(accountModel.RoleId);
                    if (roleItem != null)
                    {
                        account.AccountType = roleItem.Name;
                        account.Designation = roleItem.Name;
                    }

                    // Get list of User Work Locations from Database
                    IList<AccountWorkLocation> workLocations = this.accountRepository.GetAccountWorkLocationsById(account.AccountId);

                    var workLocation = accountModel.WorkLocationModels.FirstOrDefault();
                    if (workLocation != null)
                    {
                        account.StateId = workLocation.StateCode;
                        account.DivisionId = workLocation.DivisionId;
                    }

                    // Add/Update User Work Location from Models
                    accountModel.WorkLocationModels.ForEach(workLocationModel =>
                    {
                        if (workLocationModel.RequestType == RequestType.New)
                        {
                            var workLocationItem = workLocations.FirstOrDefault(awl => awl.DivisionId == workLocationModel.DivisionId && awl.DistrictId == workLocationModel.DistrictId && awl.BlockId == workLocationModel.BlockId && awl.ClusterId == workLocationModel.ClusterId);

                            if (workLocationItem == null)
                            {
                                // Add new User Work Location
                                workLocations.Add(new AccountWorkLocation
                                {
                                    AccountWorkLocationId = Guid.NewGuid(),
                                    AccountId = account.AccountId,
                                    StateCode = workLocationModel.StateCode,
                                    DivisionId = workLocationModel.DivisionId,
                                    DistrictId = workLocationModel.DistrictId,
                                    BlockId = workLocationModel.BlockId,
                                    ClusterId = workLocationModel.ClusterId,
                                    CreatedBy = account.AuthUserId,
                                    CreatedOn = Constants.GetCurrentDateTime,
                                    UpdatedBy = account.AuthUserId,
                                    UpdatedOn = Constants.GetCurrentDateTime,
                                    IsActive = workLocationModel.IsActive,
                                    RequestType = RequestType.New
                                });
                            }
                        }
                        else
                        {
                            var workLocationItem = workLocations.FirstOrDefault(awl => awl.AccountWorkLocationId == workLocationModel.AccountWorkLocationId);

                            // Update new User Work Location
                            if (workLocationItem != null)
                            {
                                workLocationItem.DivisionId = workLocationModel.DivisionId;
                                workLocationItem.DistrictId = workLocationModel.DistrictId;
                                workLocationItem.BlockId = workLocationModel.BlockId;
                                workLocationItem.ClusterId = workLocationModel.ClusterId;
                                workLocationItem.UpdatedBy = account.AuthUserId;
                                workLocationItem.UpdatedOn = Constants.GetCurrentDateTime;
                                workLocationItem.IsActive = workLocationModel.IsActive;
                                workLocationItem.RequestType = RequestType.Edit;
                            }
                        }
                    });

                    // Delete User Work Location from Database if any User Work Location removed from Models
                    workLocations.ForEach(workLocationItem =>
                    {
                        var workLocationModel = accountModel.WorkLocationModels.FirstOrDefault(awl => awl.DivisionId == workLocationItem.DivisionId && awl.DistrictId == workLocationItem.DistrictId && awl.BlockId == workLocationItem.BlockId && awl.ClusterId == workLocationItem.ClusterId);

                        if (workLocationModel == null)
                            account.DeletedWorkLocationIds.Add(workLocationItem.AccountWorkLocationId);
                    });

                    account.AccountWorkLocations = workLocations;

                    //Save Or Update account details
                    bool isSaved = this.accountRepository.SaveOrUpdateAccountDetails(account);

                    if (isSaved && accountModel.RequestType == RequestType.New)
                    {
                        AccountRole accountRole = new AccountRole();
                        accountRole.AuthUserId = account.AuthUserId;

                        accountRole.AccountRoleId = Guid.NewGuid();
                        accountRole.AccountId = account.AccountId;
                        accountRole.RoleId = accountModel.RoleId;
                        accountRole.RequestType = RequestType.New;
                        accountRole.IsActive = account.IsActive;

                        accountRole.SetAuditValues(accountModel.RequestType);

                        this.accountRepository.SaveOrUpdateAccountRoleDetails(accountRole);
                    }
                    else
                    {
                        AccountRole accountRole = this.accountRepository.GetAccountRoleById(account.AccountId);

                        accountRole.AuthUserId = account.AuthUserId;
                        accountRole.RoleId = accountModel.RoleId;
                        accountRole.RequestType = RequestType.Edit;
                        accountRole.SetAuditValues(accountModel.RequestType);
                        accountRole.IsActive = account.IsActive;

                        this.accountRepository.SaveOrUpdateAccountRoleDetails(accountRole);
                    }

                    response.Result = isSaved ? "Success" : "Failed";
                }
                else
                {
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("BAL > SaveOrUpdateAccountDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by AccountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid accountId)
        {
            return this.accountRepository.DeleteById(accountId);
        }

        /// <summary>
        /// Check duplicate Account by Name
        /// </summary>
        /// <param name="accountModel"></param>
        /// <returns></returns>
        public List<string> CheckAccountExistByName(AccountModel accountModel)
        {
            return this.accountRepository.CheckAccountExistByName(accountModel);
        }

        /// <summary>}
        /// List of Account with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<AccountViewModel> GetAccountsByCriteria(SearchAccountModel searchModel)
        {
            return this.accountRepository.GetAccountsByCriteria(searchModel);
        }

        /// <summary>
        /// Validate login by User Id in Igmite Application
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public SingularResponse<LoginResponce> GetAccountByUserId(LoginRequest loginRequest)
        {
            var results = new SingularResponse<LoginResponce>();

            LoginResponce loginResponce = this.accountRepository.ValidateUserLogin(loginRequest);

            if (loginResponce == null)
            {
                results.Errors.Add("Invalid UserId or Password");
                results.Success = false;
            }
            else
            {
                string encryptPassword = CryptographyManager.Encrypt(loginRequest.Password, true);
                if (!string.Equals(loginResponce.Password, encryptPassword))
                {
                    results.Errors.Add("Invalid UserId or Password");
                    results.Success = false;
                }
                else
                {
                    results.Result = loginResponce;
                }
            }

            //string authToken = string.Format("{0}#!${1}#!${2}", loginResponce.LoginId, EntityConstants.GetCurrentDateTime, System.Guid.NewGuid().ToString("N"));
            //loginResponce.AuthToken = CryptographyManager.Encrypt(authToken, true);

            return results;
        }

        /// <summary>
        /// Change Password by User Id
        /// </summary>
        /// <param name="changePasswordRequest"></param>
        /// <returns></returns>
        public SingularResponse<LoginResponce> ChangePasswordByUserId(ChangePasswordModel changePasswordRequest)
        {
            SingularResponse<LoginResponce> results = new SingularResponse<LoginResponce>();

            try
            {
                LoginRequest loginRequest = new LoginRequest();
                loginRequest.UserId = changePasswordRequest.UserId;

                LoginResponce loginResponce = this.accountRepository.ValidateUserLogin(loginRequest);

                if (loginResponce != null)
                {
                    string encryptPassword = CryptographyManager.Encrypt(changePasswordRequest.Password, true);
                    if (!string.Equals(loginResponce.Password, encryptPassword))
                    {
                        results.Errors.Add("Invalid UserId or Password");
                        results.Success = false;

                        return results;
                    }

                    if (!string.Equals(changePasswordRequest.ConfirmPassword, changePasswordRequest.NewPassword))
                    {
                        results.Errors.Add("Confirm password not matching with new passsword");
                        results.Success = false;

                        return results;
                    }

                    Account account = this.accountRepository.GetAccountByLoginId(changePasswordRequest.UserId);

                    if (account != null)
                    {
                        account.Password = CryptographyManager.Encrypt(changePasswordRequest.NewPassword, true);
                        account.IsPasswordReset = false;
                        account.UpdatedOn = Constants.GetCurrentDateTime;
                        account.PasswordUpdateDate = Constants.GetCurrentDateTime;
                        account.PasswordExpiredOn = Constants.GetCurrentDateTime.AddMonths(6);
                        account.RequestType = RequestType.Edit;

                        this.accountRepository.SaveOrUpdateAccountDetails(account);
                    }

                    results.Result = loginResponce;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("BAL > ChangePasswordByUserId", ex);
            }

            return results;
        }

        /// <summary>
        /// Forgot Password by User Id
        /// </summary>
        /// <param name="forgotPasswordRequest"></param>
        /// <returns></returns>
        public async Task<SingularResponse<string>> ForgotPasswordByUserId(ForgotPasswordRequest forgotPasswordRequest)
        {
            SingularResponse<string> results = new SingularResponse<string>();

            try
            {
                Account account = this.accountRepository.GetAccountByLoginId(forgotPasswordRequest.UserId);

                if (account != null)
                {
                    //string resetToken = account.AccountId.ToString();
                    //var key = "E546C8DF278CD5931069B522E695D4F2";

                    // Encrypt the string to an array of bytes.
                    //string encrypted = CryptographyManager.EncryptString(resetToken, key);

                    // Decrypt the bytes to a string.
                    //string roundtrip = CryptographyManager.DecryptString(encrypted, key);

                    account.IsPasswordReset = true;
                    account.PasswordResetToken = forgotPasswordRequest.ResetToken;
                    account.TokenExpiredOn = Constants.GetCurrentDateTime.AddHours(4);
                    account.UpdatedOn = Constants.GetCurrentDateTime;
                    account.RequestType = RequestType.Edit;

                    this.accountRepository.SaveOrUpdateAccountDetails(account);

                    string toEmailId = Constants.IsDeveloperMode ? Constants.TestToEmail : forgotPasswordRequest.UserId;

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.AppendFormat("<p>Hello {0},</p>\n", account.UserName);
                    sb.AppendFormat("<p>Follow this link to reset your Lighthouse {0} password for your {1} account.</p>\n", Constants.StateCode, forgotPasswordRequest.UserId);
                    //sb.AppendFormat("<p><a href=\"{0}/reset-password?accessToken={1}\">Reset Password</a></p>\n", forgotPasswordRequest.WebsiteUrl, forgotPasswordRequest.ResetToken);
                    sb.AppendFormat("<p><a href=\"{0}\">Reset Password</a></p>\n", forgotPasswordRequest.ResetUrl, forgotPasswordRequest.ResetToken);

                    sb.AppendLine("<p>If you didn't ask to reset your password, you can ignore this email.</p>");
                    sb.AppendLine("<p>Thanks,</p>");
                    sb.AppendLine("<p>Your Lighthouse Team</p>");

                    Message message = new Message(new string[] { toEmailId }, "Reset your password for Lighthouse " + Constants.StateCode, sb.ToString(), null);

                    await this.emailSender.SendEmailAsync(message);

                    results.Result = "Sent reset password link to registered email";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("BAL > ForgotPasswordByUserId", ex);
            }

            return results;
        }

        /// <summary>
        /// Reset Password by User Id
        /// </summary>
        /// <param name="resetPasswordRequest"></param>
        /// <returns></returns>
        public async Task<SingularResponse<string>> ResetPasswordByUserId(ResetPasswordRequest resetPasswordRequest)
        {
            SingularResponse<string> results = new SingularResponse<string>();

            try
            {
                if (!string.Equals(resetPasswordRequest.ConfirmPassword, resetPasswordRequest.NewPassword))
                {
                    results.Errors.Add("Confirm password not matching with new passsword");
                    results.Success = false;

                    return results;
                }

                Account account = this.accountRepository.GetAccountByLoginId(resetPasswordRequest.UserId);

                if (account != null)
                {
                    string newPassword = CryptographyManager.Encrypt(resetPasswordRequest.NewPassword, true);
                    if (string.Equals(account.Password, newPassword))
                    {
                        results.Errors.Add("Old Password and new password cannot be same");
                        results.Success = false;

                        return results;
                    }

                    account.Password = CryptographyManager.Encrypt(resetPasswordRequest.NewPassword, true);
                    account.IsPasswordReset = false;
                    account.UpdatedOn = Constants.GetCurrentDateTime;
                    account.PasswordUpdateDate = Constants.GetCurrentDateTime;
                    account.PasswordExpiredOn = Constants.GetCurrentDateTime.AddMonths(6);
                    account.RequestType = RequestType.Edit;

                    this.accountRepository.SaveOrUpdateAccountDetails(account);

                    string toEmailId = Constants.IsDeveloperMode ? Constants.TestToEmail : resetPasswordRequest.UserId;
                    Message message = new Message(new string[] { toEmailId }, "Reset Password", "Password change successfully. New Password is " + resetPasswordRequest.NewPassword, null);

                    await this.emailSender.SendEmailAsync(message);

                    results.Result = "Password change successfully";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("BAL > ResetPasswordByUserId", ex);
            }

            return results;
        }

        /// <summary>
        /// Logout by User Id
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public SingularResponse<string> LogoutByUserId(LogoutRequest logoutRequest)
        {
            SingularResponse<string> results = new SingularResponse<string>();

            try
            {
                Account account = this.accountRepository.GetAccountByLoginId(logoutRequest.LoginUniqueId);

                if (account != null)
                {
                    if (string.Equals(account.AuthToken, logoutRequest.AuthToken))
                    {
                        results.Errors.Add("Invalid authentication for Logout");
                        results.Success = false;

                        return results;
                    }

                    account.UpdatedOn = Constants.GetCurrentDateTime;
                    account.LastLoginDate = Constants.GetCurrentDateTime;
                    account.TokenExpiredOn = Constants.GetCurrentDateTime;
                    account.RequestType = RequestType.Edit;

                    //LogoutHistory logoutHistory = new LogoutHistory
                    //{
                    //    LogoutHistoryId = Guid.NewGuid(),
                    //    UserId = account.AccountId,
                    //    LoginDateTime = account.LastLoginDate.Value,
                    //    LogoutDateTime = Constants.GetCurrentDateTime,
                    //    AuthToken = logoutRequest.AuthToken
                    //};

                    //this.accountRepository.SaveOrUpdateLogoutHistoryDetails(logoutHistory, account);

                    results.Result = "Logout successfully from Lighthouse Application";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("BAL > LogoutByUserId", ex);
            }

            return results;
        }

        /// <summary>}
        /// User's Transactions by User Id in Igmite Application
        /// </summary>}
        /// <param name="loginRequest"></param>}
        /// <returns></returns>}
        public IList<RoleTransactionResponce> GetUserTransactionsById(LoginRequest loginRequest)
        {
            var roleTransactions = this.accountRepository.GetRoleTransactionsById(loginRequest);
            var userTransactions = this.accountRepository.GetUserTransactionsById(loginRequest);

            foreach (var userTransactionItem in userTransactions)
            {
                var roleTransactionItem = roleTransactions.FirstOrDefault(t => t.TransactionId == userTransactionItem.TransactionId);

                if (roleTransactionItem != null)
                {
                    roleTransactionItem.Rights = userTransactionItem.Rights;
                    roleTransactionItem.IsAdd = userTransactionItem.IsAdd;
                    roleTransactionItem.IsEdit = userTransactionItem.IsEdit;
                    roleTransactionItem.IsDelete = userTransactionItem.IsDelete;
                    roleTransactionItem.IsView = userTransactionItem.IsView;
                    roleTransactionItem.IsExport = userTransactionItem.IsExport;
                    roleTransactionItem.ListView = userTransactionItem.ListView;
                    roleTransactionItem.BasicView = userTransactionItem.BasicView;
                    roleTransactionItem.DetailView = userTransactionItem.DetailView;
                    roleTransactionItem.IsPublic = userTransactionItem.IsPublic;
                }
                else
                {
                    roleTransactions.Add(new RoleTransactionResponce
                    {
                        SrNo = userTransactionItem.SrNo,
                        HeaderName = userTransactionItem.HeaderName,
                        HeaderOrder = userTransactionItem.HeaderOrder,
                        TransactionOrder = userTransactionItem.TransactionOrder,
                        IsHeaderMenu = userTransactionItem.IsHeaderMenu,
                        TransactionId = userTransactionItem.TransactionId,
                        Code = userTransactionItem.Code,
                        Name = userTransactionItem.Name,
                        PageTitle = userTransactionItem.PageTitle,
                        PageDescription = userTransactionItem.PageDescription,
                        TransactionIcon = userTransactionItem.TransactionIcon,
                        UrlAction = userTransactionItem.UrlAction,
                        UrlController = userTransactionItem.UrlController,
                        UrlPara = userTransactionItem.UrlPara,
                        Rights = userTransactionItem.Rights,
                        IsAdd = userTransactionItem.IsAdd,
                        IsEdit = userTransactionItem.IsEdit,
                        IsDelete = userTransactionItem.IsDelete,
                        IsView = userTransactionItem.IsView,
                        IsExport = userTransactionItem.IsExport,
                        ListView = userTransactionItem.ListView,
                        BasicView = userTransactionItem.BasicView,
                        DetailView = userTransactionItem.DetailView,
                        IsPublic = userTransactionItem.IsPublic,
                        RouteUrl = userTransactionItem.RouteUrl
                    });
                }
            }

            return roleTransactions.OrderBy(t => t.HeaderOrder).ThenBy(w => w.TransactionOrder).ToList();
        }

        /// <summary>
        /// Update AuthToken Async after user logged-in application
        /// </summary>
        /// <param name="loginResponce"></param>
        /// <param name="loginType"></param>
        /// <returns></returns>
        public async Task<int> UpdateUserAuthTokenAsync(LoginResponce loginResponce, string loginType)
        {
            return await this.accountRepository.UpdateUserAuthTokenAsync(loginResponce, loginType);
        }

        /// <summary>}
        /// Change User LoginId by Admin
        /// </summary>}
        /// <param name="changeLoginModel"></param>}
        /// <returns></returns>}
        public bool ChangeUserLoginId(ChangeLoginModel changeLoginModel)
        {
            return this.accountRepository.ChangeUserLoginId(changeLoginModel);
        }
    }
}