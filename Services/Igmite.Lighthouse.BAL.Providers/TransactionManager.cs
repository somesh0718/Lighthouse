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
    /// Manager of the Transaction entity
    /// </summary>
    public class TransactionManager : GenericManager<TransactionModel>, ITransactionManager
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the transaction manager.
        /// </summary>
        /// <param name="transactionRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public TransactionManager(ITransactionRepository _transactionRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.transactionRepository = _transactionRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of Transactions
        /// </summary>
        /// <returns></returns>
        public IQueryable<TransactionModel> GetTransactions()
        {
            var transactions = this.transactionRepository.GetTransactions();

            IList<TransactionModel> transactionModels = new List<TransactionModel>();
            transactions.ForEach((user) => transactionModels.Add(user.ToModel()));

            return transactionModels.AsQueryable();
        }

        /// <summary>
        /// Get list of Transactions by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<TransactionModel> GetTransactionsByName(string transactionName)
        {
            var transactions = this.transactionRepository.GetTransactionsByName(transactionName);

            IList<TransactionModel> transactionModels = new List<TransactionModel>();
            transactions.ForEach((user) => transactionModels.Add(user.ToModel()));

            return transactionModels.AsQueryable();
        }

        /// <summary>
        /// Get Transaction by TransactionId
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public TransactionModel GetTransactionById(Guid transactionId)
        {
            Transaction transaction = this.transactionRepository.GetTransactionById(transactionId);

            return (transaction != null) ? transaction.ToModel() : null;
        }

        /// <summary>
        /// Get Transaction by Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public TransactionModel GetTransactionByCode(string code)
        {
            Transaction transaction = this.transactionRepository.GetTransactionByCode(code);

            return (transaction != null) ? transaction.ToModel() : null;
        }

        /// <summary>
        /// Get Transaction by TransactionId using async
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public async Task<TransactionModel> GetTransactionByIdAsync(Guid transactionId)
        {
            var transaction = await this.transactionRepository.GetTransactionByIdAsync(transactionId);

            return (transaction != null) ? transaction.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update Transaction entity
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateTransactionDetails(TransactionModel transactionModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            Transaction transaction = null;

            //Validate model data
            transactionModel = transactionModel.GetModelValidationErrors<TransactionModel>();

            if (transactionModel.ErrorMessages.Count > 0)
            {
                response.Errors = transactionModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (transactionModel.RequestType == RequestType.Edit)
            {
                transaction = this.transactionRepository.GetTransactionById(transactionModel.TransactionId);
            }
            else
            {
                transaction = new Transaction();
                transactionModel.TransactionId = Guid.NewGuid();
            }

            if (transactionModel.ErrorMessages.Count == 0 && (transactionModel.Code.StringVal().ToLower() != transaction.Code.StringVal().ToLower()))
            {
                bool isTransactionExists = this.transactionRepository.CheckTransactionExistByName(transactionModel);

                if (isTransactionExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                transaction.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                transaction = transactionModel.FromModel(transaction);

                //Save Or Update transaction details
                bool isSaved = this.transactionRepository.SaveOrUpdateTransactionDetails(transaction);

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
        /// Delete a record by TransactionId
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid transactionId)
        {
            return this.transactionRepository.DeleteById(transactionId);
        }

        /// <summary>
        /// Check duplicate Transaction by Name
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        public bool CheckTransactionExistByName(TransactionModel transactionModel)
        {
            return this.transactionRepository.CheckTransactionExistByName(transactionModel);
        }

        /// <summary>}
        /// List of Transaction with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<TransactionViewModel> GetTransactionsByCriteria(SearchTransactionModel searchModel)
        {
            return this.transactionRepository.GetTransactionsByCriteria(searchModel);
        }
    }
}