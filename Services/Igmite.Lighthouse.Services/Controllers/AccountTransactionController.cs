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
    /// Expose all accountTransaction WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController, ApiExplorerSettings(IgnoreApi = false)]
    public class AccountTransactionController : BaseController
    {
        private readonly IAccountTransactionManager accountTransactionManager;

        /// <summary>
        /// Initializes the AccountTransaction controller class.
        /// </summary>
        /// <param name="_accountTransactionManager"></param>
        public AccountTransactionController(IAccountTransactionManager _accountTransactionManager)
        {
            this.accountTransactionManager = _accountTransactionManager;
        }

        /// <summary>
        /// Get list of accountTransaction data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetAccountTransactions")]
        public async Task<ListResponse<AccountTransactionModel>> GetAccountTransactions()
        {
            ListResponse<AccountTransactionModel> response = new ListResponse<AccountTransactionModel>();

            try
            {
                IQueryable<AccountTransactionModel> accountTransactionModels = await Task.Run(() =>
                {
                    return this.accountTransactionManager.GetAccountTransactions();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = accountTransactionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of AccountTransaction with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetAccountTransactionsByCriteria")]
        public async Task<ListResponse<AccountTransactionViewModel>> GetAccountTransactionsByCriteria([FromBody] SearchAccountTransactionModel searchModel)
        {
            ListResponse<AccountTransactionViewModel> response = new ListResponse<AccountTransactionViewModel>();

            try
            {
                var accountTransactionModels = await Task.Run(() =>
                {
                    return this.accountTransactionManager.GetAccountTransactionsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = accountTransactionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of accountTransaction data by name
        /// </summary>
        /// <param name="accountTransactionName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetAccountTransactionsByName")]
        public async Task<ListResponse<AccountTransactionModel>> GetAccountTransactionsByName([FromQuery] string accountTransactionName)
        {
            ListResponse<AccountTransactionModel> response = new ListResponse<AccountTransactionModel>();

            try
            {
                var accountTransactionModels = await Task.Run(() =>
                {
                    return this.accountTransactionManager.GetAccountTransactionsByName(accountTransactionName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = accountTransactionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get accountTransaction data by Id
        /// </summary>
        /// <param name="accountTransactionId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetAccountTransactionById")]
        public async Task<SingularResponse<AccountTransactionModel>> GetAccountTransactionById([FromBody] DataRequest accountTransactionRequest)
        {
            SingularResponse<AccountTransactionModel> response = new SingularResponse<AccountTransactionModel>();

            try
            {
                var accountTransactionModel = await Task.Run(() =>
                {
                    return this.accountTransactionManager.GetAccountTransactionById(Guid.Parse(accountTransactionRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = accountTransactionModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new accountTransaction
        /// </summary>
        /// <param name="accountTransactionRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateAccountTransaction"), Route("CreateOrUpdateAccountTransactionDetails")]
        public async Task<SingularResponse<string>> CreateAccountTransaction([FromBody] AccountTransactionRequest accountTransactionRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //accountTransactionRequest.RequestType = RequestType.New;
                    return this.accountTransactionManager.SaveOrUpdateAccountTransactionDetails(accountTransactionRequest);
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
        /// Update accountTransaction by Id
        /// </summary>
        /// <param name="accountTransactionRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateAccountTransaction")]
        public async Task<SingularResponse<string>> UpdateAccountTransaction([FromBody] AccountTransactionRequest accountTransactionRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    accountTransactionRequest.RequestType = RequestType.Edit;
                    return this.accountTransactionManager.SaveOrUpdateAccountTransactionDetails(accountTransactionRequest);
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
        /// Delete accountTransaction by Id
        /// </summary>
        /// <param name="accountTransactionRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteAccountTransactionById")]
        public async Task<SingularResponse<bool>> DeleteAccountTransactionById([FromBody] DeleteRequest<Guid> accountTransactionRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.accountTransactionManager.DeleteById(accountTransactionRequest.DataId);
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
    }
}