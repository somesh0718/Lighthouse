using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.Controllers
{
    /// <summary>
    /// Expose all account WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountManager accountManager;

        /// <summary>
        /// Initializes the Account controller class.
        /// </summary>
        /// <param name="_accountManager"></param>
        public AccountController(IAccountManager _accountManager)
        {
            this.accountManager = _accountManager;
        }

        /// <summary>
        /// Get list of account data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetAccounts")]
        public async Task<ListResponse<AccountModel>> GetAccounts()
        {
            ListResponse<AccountModel> response = new ListResponse<AccountModel>();

            try
            {
                IQueryable<AccountModel> accountModels = await Task.Run(() =>
                {
                    return this.accountManager.GetAccounts();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = accountModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of Account with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetAccountsByCriteria")]
        public async Task<ListResponse<AccountViewModel>> GetAccountsByCriteria([FromBody] SearchAccountModel searchModel)
        {
            ListResponse<AccountViewModel> response = new ListResponse<AccountViewModel>();

            try
            {
                var accountModels = await Task.Run(() =>
                {
                    return this.accountManager.GetAccountsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = accountModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of account data by name
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetAccountsByName")]
        public async Task<ListResponse<AccountModel>> GetAccountsByName([FromQuery] string accountName)
        {
            ListResponse<AccountModel> response = new ListResponse<AccountModel>();

            try
            {
                var accountModels = await Task.Run(() =>
                {
                    return this.accountManager.GetAccountsByName(accountName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = accountModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get account data by Id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetAccountById")]
        public async Task<SingularResponse<AccountModel>> GetAccountById([FromBody] DataRequest accountRequest)
        {
            SingularResponse<AccountModel> response = new SingularResponse<AccountModel>();

            try
            {
                var accountModel = await Task.Run(() =>
                {
                    return this.accountManager.GetAccountById(Guid.Parse(accountRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = accountModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new account
        /// </summary>
        /// <param name="accountRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateAccount"), Route("CreateOrUpdateAccountDetails")]
        public async Task<SingularResponse<string>> CreateAccount([FromBody] AccountRequest accountRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //accountRequest.RequestType = RequestType.New;
                    return this.accountManager.SaveOrUpdateAccountDetails(accountRequest);
                });

                if (response.Errors.Count == 0)
                {
                    response.Messages.Add(Constants.CreatedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Update account by Id
        /// </summary>
        /// <param name="accountRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateAccount")]
        public async Task<SingularResponse<string>> UpdateAccount([FromBody] AccountRequest accountRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    accountRequest.RequestType = RequestType.Edit;
                    return this.accountManager.SaveOrUpdateAccountDetails(accountRequest);
                });

                if (response.Errors.Count == 0)
                {
                    response.Messages.Add(Constants.UpdatedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Delete account by Id
        /// </summary>
        /// <param name="accountRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteAccountById")]
        public async Task<SingularResponse<bool>> DeleteAccountById([FromBody] DeleteRequest<Guid> accountRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.accountManager.DeleteById(accountRequest.DataId);
                });

                if (response.Result)
                {
                    response.Messages.Add(Constants.DeletedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Login by User Id in Igmite Application
        /// </summary>
        /// <param name="syllabusRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("LoginByUserId")]
        public async Task<SingularResponse<LoginResponce>> LoginByUserId([FromBody] LoginRequest loginRequest)
        {
            SingularResponse<LoginResponce> loginResults = new SingularResponse<LoginResponce>();

            try
            {
                loginResults = await Task.Run(() =>
                {
                    var response = this.accountManager.GetAccountByUserId(loginRequest);

                    // Authentication successful so generate jwt token
                    if (response.Success)
                    {
                        response.Result.AuthToken = AuthHelper.GenerateJWTToken(response.Result);
                        response.Result.Password = string.Empty;
                        response.Result.IsMobile = loginRequest.IsMobile;

                        this.accountManager.UpdateUserAuthTokenAsync(response.Result, "Login");
                    }

                    return response;
                });

                loginResults.Messages.Add(Constants.GetDataMessage);
            }
            catch (Exception ex)
            {
                loginResults.Errors.Add(ex.Message);
                loginResults.Success = false;
            }

            return loginResults;
        }

        /// <summary>
        /// Logout by User Id
        /// </summary>
        /// <param name="logoutRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("LogoutByUserId")]
        public async Task<SingularResponse<string>> LogoutByUserId([FromBody] LogoutRequest logoutRequest)
        {
            SingularResponse<string> loginResults = new SingularResponse<string>();

            try
            {
                LoginResponce logoutResponce = new LoginResponce();
                logoutResponce.LoginUniqueId = logoutRequest.LoginUniqueId;
                logoutResponce.AuthToken = logoutRequest.AuthToken;

                await Task.Run(() =>
                {
                    return this.accountManager.UpdateUserAuthTokenAsync(logoutResponce, "Logout");
                });

                loginResults.Messages.Add(Constants.GetDataMessage);
            }
            catch (Exception ex)
            {
                loginResults.Errors.Add(ex.Message);
                loginResults.Success = false;
            }

            return loginResults;
        }

        /// <summary>
        /// Change password for user
        /// </summary>
        /// <param name="changePasswordRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("ChangePassword")]
        public async Task<SingularResponse<LoginResponce>> ChangePassword([FromBody] ChangePasswordModel changePasswordRequest)
        {
            SingularResponse<LoginResponce> loginResults = new SingularResponse<LoginResponce>();

            try
            {
                loginResults = await Task.Run(() =>
                {
                    return this.accountManager.ChangePasswordByUserId(changePasswordRequest);
                });

                // Authentication successful so generate jwt token
                if (loginResults.Success)
                {
                    loginResults.Result.AuthToken = AuthHelper.GenerateJWTToken(loginResults.Result);
                    loginResults.Result.Password = string.Empty;
                }

                loginResults.Messages.Add(Constants.GetDataMessage);
            }
            catch (Exception ex)
            {
                loginResults.Errors.Add(ex.Message);
                loginResults.Success = false;
            }

            return loginResults;
        }

        /// <summary>
        /// Change user login Id
        /// </summary>
        /// <param name="changeLoginRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("ChangeUserLoginId")]
        public async Task<SingularResponse<string>> ChangeUserLoginId([FromBody] ChangeLoginModel changeLoginRequest)
        {
            SingularResponse<string> loginResults = new SingularResponse<string>();

            try
            {
                var isChangedLoginId = await Task.Run(() =>
                {
                    return this.accountManager.ChangeUserLoginId(changeLoginRequest);
                });

                if (isChangedLoginId)
                {
                    loginResults.Result = "Changed user LoginId successfully";
                }

                loginResults.Messages.Add(Constants.GetDataMessage);
            }
            catch (Exception ex)
            {
                loginResults.Errors.Add(ex.Message);
                loginResults.Success = false;
            }

            return loginResults;
        }

        /// <summary>
        /// User's Transactions by User Id in Igmite Application
        /// </summary>
        /// <param name="LoginRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("GetUserTransactionsById")]
        public async Task<ListResponse<RoleTransactionResponce>> GetUserTransactionsById([FromBody] LoginRequest loginRequest)
        {
            ListResponse<RoleTransactionResponce> loginResults = new ListResponse<RoleTransactionResponce>();

            try
            {
                loginResults.Results = await Task.Run(() =>
                {
                    return this.accountManager.GetUserTransactionsById(loginRequest);
                });

                loginResults.Messages.Add(Constants.GetDataMessage);
            }
            catch (Exception ex)
            {
                loginResults.Errors.Add(ex.Message);
                loginResults.Success = false;
            }

            return loginResults;
        }
    }
}