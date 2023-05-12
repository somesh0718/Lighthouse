using Igmite.Lighthouse.BAL.Validations;
using Igmite.Lighthouse.DAL;
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
    /// Manager of the AccountTransaction entity
    /// </summary>
    public class AccountTransactionManager : GenericManager<AccountTransactionModel>, IAccountTransactionManager
    {
        private readonly IAccountTransactionRepository accountTransactionRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the accountTransaction manager.
        /// </summary>
        /// <param name="accountTransactionRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public AccountTransactionManager(IAccountTransactionRepository _accountTransactionRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.accountTransactionRepository = _accountTransactionRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of AccountTransactions
        /// </summary>
        /// <returns></returns>
        public IQueryable<AccountTransactionModel> GetAccountTransactions()
        {
            var accountTransactions = this.accountTransactionRepository.GetAccountTransactions();

            IList<AccountTransactionModel> accountTransactionModels = new List<AccountTransactionModel>();
            accountTransactions.ForEach((user) => accountTransactionModels.Add(user.ToModel()));

            return accountTransactionModels.AsQueryable();
        }

        /// <summary>
        /// Get list of AccountTransactions by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<AccountTransactionModel> GetAccountTransactionsByName(string accountTransactionName)
        {
            var accountTransactions = this.accountTransactionRepository.GetAccountTransactionsByName(accountTransactionName);

            IList<AccountTransactionModel> accountTransactionModels = new List<AccountTransactionModel>();
            accountTransactions.ForEach((user) => accountTransactionModels.Add(user.ToModel()));

            return accountTransactionModels.AsQueryable();
        }

        /// <summary>
        /// Get AccountTransaction by AccountTransactionId
        /// </summary>
        /// <param name="accountTransactionId"></param>
        /// <returns></returns>
        public AccountTransactionModel GetAccountTransactionById(Guid accountTransactionId)
        {
            AccountTransactionModel accountTransactionModel = null;

            AccountTransaction accountTransaction = this.accountTransactionRepository.GetAccountTransactionById(accountTransactionId);

            if (accountTransaction != null)
            {
                accountTransactionModel = accountTransaction.ToModel();

                accountTransactionModel.RoleId = this.accountTransactionRepository.GetAccountRoleIdByUser(accountTransaction.AccountTransactionId);
            }

            return accountTransactionModel;
        }

        /// <summary>
        /// Get AccountTransaction by AccountTransactionId using async
        /// </summary>
        /// <param name="accountTransactionId"></param>
        /// <returns></returns>
        public async Task<AccountTransactionModel> GetAccountTransactionByIdAsync(Guid accountTransactionId)
        {
            var accountTransaction = await this.accountTransactionRepository.GetAccountTransactionByIdAsync(accountTransactionId);

            return (accountTransaction != null) ? accountTransaction.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update AccountTransaction entity
        /// </summary>
        /// <param name="accountTransactionModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateAccountTransactionDetails(AccountTransactionModel accountTransactionModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            AccountTransaction accountTransaction = null;

            //Validate model data
            accountTransactionModel = accountTransactionModel.GetModelValidationErrors<AccountTransactionModel>();

            if (accountTransactionModel.ErrorMessages.Count > 0)
            {
                response.Errors = accountTransactionModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (accountTransactionModel.RequestType == RequestType.Edit)
            {
                accountTransaction = this.accountTransactionRepository.GetAccountTransactionById(accountTransactionModel.AccountTransactionId);
            }
            else
            {
                accountTransaction = new AccountTransaction();
                accountTransactionModel.AccountTransactionId = Guid.NewGuid();
            }

            if (accountTransactionModel.ErrorMessages.Count == 0 && !(Guid.Equals(accountTransactionModel.AccountId, accountTransaction.AccountId) && Guid.Equals(accountTransactionModel.TransactionId, accountTransaction.TransactionId)))
            {
                bool isAccountTransactionExists = this.accountTransactionRepository.CheckAccountTransactionExistByName(accountTransactionModel);

                if (isAccountTransactionExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                accountTransaction.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                accountTransaction = accountTransactionModel.FromModel(accountTransaction);

                //Save Or Update accountTransaction details
                bool isSaved = this.accountTransactionRepository.SaveOrUpdateAccountTransactionDetails(accountTransaction);

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
        /// Delete a record by AccountTransactionId
        /// </summary>
        /// <param name="accountTransactionId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid accountTransactionId)
        {
            return this.accountTransactionRepository.DeleteById(accountTransactionId);
        }

        /// <summary>
        /// Check duplicate AccountTransaction by Name
        /// </summary>
        /// <param name="accountTransactionModel"></param>
        /// <returns></returns>
        public bool CheckAccountTransactionExistByName(AccountTransactionModel accountTransactionModel)
        {
            return this.accountTransactionRepository.CheckAccountTransactionExistByName(accountTransactionModel);
        }

        /// <summary>}
        /// List of AccountTransaction with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<AccountTransactionViewModel> GetAccountTransactionsByCriteria(SearchAccountTransactionModel searchModel)
        {
            return this.accountTransactionRepository.GetAccountTransactionsByCriteria(searchModel);
        }
    }
}