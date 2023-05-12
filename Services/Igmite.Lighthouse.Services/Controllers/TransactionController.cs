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
    /// Expose all transaction WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class TransactionController : BaseController
    {
        private readonly ITransactionManager transactionManager;

        /// <summary>
        /// Initializes the Transaction controller class.
        /// </summary>
        /// <param name="_transactionManager"></param>
        public TransactionController(ITransactionManager _transactionManager)
        {
            this.transactionManager = _transactionManager;
        }

        /// <summary>
        /// Get list of transaction data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetTransactions")]
        public async Task<ListResponse<TransactionModel>> GetTransactions()
        {
            ListResponse<TransactionModel> response = new ListResponse<TransactionModel>();

            try
            {
                IQueryable<TransactionModel> transactionModels = await Task.Run(() =>
                {
                    return this.transactionManager.GetTransactions();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = transactionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of Transaction with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetTransactionsByCriteria")]
        public async Task<ListResponse<TransactionViewModel>> GetTransactionsByCriteria([FromBody] SearchTransactionModel searchModel)
        {
            ListResponse<TransactionViewModel> response = new ListResponse<TransactionViewModel>();

            try
            {
                var transactionModels = await Task.Run(() =>
                {
                    return this.transactionManager.GetTransactionsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = transactionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of transaction data by name
        /// </summary>
        /// <param name="transactionName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetTransactionsByName")]
        public async Task<ListResponse<TransactionModel>> GetTransactionsByName([FromQuery] string transactionName)
        {
            ListResponse<TransactionModel> response = new ListResponse<TransactionModel>();

            try
            {
                var transactionModels = await Task.Run(() =>
                {
                    return this.transactionManager.GetTransactionsByName(transactionName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = transactionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get transaction data by Id
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetTransactionById")]
        public async Task<SingularResponse<TransactionModel>> GetTransactionById([FromBody] DataRequest transactionRequest)
        {
            SingularResponse<TransactionModel> response = new SingularResponse<TransactionModel>();

            try
            {
                var transactionModel = await Task.Run(() =>
                {
                    return this.transactionManager.GetTransactionById(Guid.Parse(transactionRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = transactionModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new transaction
        /// </summary>
        /// <param name="transactionRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateTransaction"), Route("CreateOrUpdateTransactionDetails")]
        public async Task<SingularResponse<string>> CreateTransaction([FromBody] TransactionRequest transactionRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //transactionRequest.RequestType = RequestType.New;
                    return this.transactionManager.SaveOrUpdateTransactionDetails(transactionRequest);
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
        /// Update transaction by Id
        /// </summary>
        /// <param name="transactionRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateTransaction")]
        public async Task<SingularResponse<string>> UpdateTransaction([FromBody] TransactionRequest transactionRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    transactionRequest.RequestType = RequestType.Edit;
                    return this.transactionManager.SaveOrUpdateTransactionDetails(transactionRequest);
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
        /// Delete transaction by Id
        /// </summary>
        /// <param name="transactionRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteTransactionById")]
        public async Task<SingularResponse<bool>> DeleteTransactionById([FromBody] DeleteRequest<Guid> transactionRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.transactionManager.DeleteById(transactionRequest.DataId);
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