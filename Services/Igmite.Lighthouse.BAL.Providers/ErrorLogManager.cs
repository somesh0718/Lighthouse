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
    /// Manager of the ErrorLog entity
    /// </summary>
    public class ErrorLogManager : GenericManager<ErrorLogModel>, IErrorLogManager
    {
        private readonly IErrorLogRepository errorLogRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the errorLog manager.
        /// </summary>
        /// <param name="errorLogRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public ErrorLogManager(IErrorLogRepository _errorLogRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.errorLogRepository = _errorLogRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of ErrorLogs
        /// </summary>
        /// <returns></returns>
        public IQueryable<ErrorLogModel> GetErrorLogs()
        {
            var errorLogs = this.errorLogRepository.GetErrorLogs();

            IList<ErrorLogModel> errorLogModels = new List<ErrorLogModel>();
            errorLogs.ForEach((user) => errorLogModels.Add(user.ToModel()));

            return errorLogModels.AsQueryable();
        }

        /// <summary>
        /// Get list of ErrorLogs by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<ErrorLogModel> GetErrorLogsByName(string errorLogName)
        {
            var errorLogs = this.errorLogRepository.GetErrorLogsByName(errorLogName);

            IList<ErrorLogModel> errorLogModels = new List<ErrorLogModel>();
            errorLogs.ForEach((user) => errorLogModels.Add(user.ToModel()));

            return errorLogModels.AsQueryable();
        }

        /// <summary>
        /// Get ErrorLog by ErrorLogId
        /// </summary>
        /// <param name="errorLogId"></param>
        /// <returns></returns>
        public ErrorLogModel GetErrorLogById(Guid errorLogId)
        {
            ErrorLog errorLog = this.errorLogRepository.GetErrorLogById(errorLogId);

            return (errorLog != null) ? errorLog.ToModel() : null;
        }

        /// <summary>
        /// Get ErrorLog by ErrorLogId using async
        /// </summary>
        /// <param name="errorLogId"></param>
        /// <returns></returns>
        public async Task<ErrorLogModel> GetErrorLogByIdAsync(Guid errorLogId)
        {
            var errorLog = await this.errorLogRepository.GetErrorLogByIdAsync(errorLogId);

            return (errorLog != null) ? errorLog.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update ErrorLog entity
        /// </summary>
        /// <param name="errorLogModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateErrorLogDetails(ErrorLogModel errorLogModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            ErrorLog errorLog = null;

            //Validate model data
            errorLogModel = errorLogModel.GetModelValidationErrors<ErrorLogModel>();

            if (errorLogModel.ErrorMessages.Count > 0)
            {
                response.Errors = errorLogModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (errorLogModel.RequestType == RequestType.Edit)
            {
                errorLog = this.errorLogRepository.GetErrorLogById(errorLogModel.ErrorLogId);
            }
            else
            {
                errorLog = new ErrorLog();
                errorLogModel.ErrorLogId = Guid.NewGuid();
            }

            if (errorLogModel.ErrorMessages.Count == 0 && (errorLogModel.ModuleName.StringVal().ToLower() != errorLog.ModuleName.StringVal().ToLower()))
            {
                bool isErrorLogExists = this.errorLogRepository.CheckErrorLogExistByName(errorLogModel);

                if (isErrorLogExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                errorLog.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                errorLog = errorLogModel.FromModel(errorLog);

                //Save Or Update errorLog details
                bool isSaved = this.errorLogRepository.SaveOrUpdateErrorLogDetails(errorLog);

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
        /// Delete a record by ErrorLogId
        /// </summary>
        /// <param name="errorLogId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid errorLogId)
        {
            return this.errorLogRepository.DeleteById(errorLogId);
        }

        /// <summary>
        /// Check duplicate ErrorLog by Name
        /// </summary>
        /// <param name="errorLogModel"></param>
        /// <returns></returns>
        public bool CheckErrorLogExistByName(ErrorLogModel errorLogModel)
        {
            return this.errorLogRepository.CheckErrorLogExistByName(errorLogModel);
        }

        /// <summary>}
        /// List of ErrorLog with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<ErrorLogViewModel> GetErrorLogsByCriteria(SearchErrorLogModel searchModel)
        {
            return this.errorLogRepository.GetErrorLogsByCriteria(searchModel);
        }
    }
}