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
    /// Manager of the RoleTransaction entity
    /// </summary>
    public class RoleTransactionManager : GenericManager<RoleTransactionModel>, IRoleTransactionManager
    {
        private readonly IRoleTransactionRepository roleTransactionRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the roleTransaction manager.
        /// </summary>
        /// <param name="roleTransactionRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public RoleTransactionManager(IRoleTransactionRepository _roleTransactionRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.roleTransactionRepository = _roleTransactionRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of RoleTransactions
        /// </summary>
        /// <returns></returns>
        public IQueryable<RoleTransactionModel> GetRoleTransactions()
        {
            var roleTransactions = this.roleTransactionRepository.GetRoleTransactions();

            IList<RoleTransactionModel> roleTransactionModels = new List<RoleTransactionModel>();
            roleTransactions.ForEach((user) => roleTransactionModels.Add(user.ToModel()));

            return roleTransactionModels.AsQueryable();
        }

        /// <summary>
        /// Get list of RoleTransactions by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<RoleTransactionModel> GetRoleTransactionsByName(string roleTransactionName)
        {
            var roleTransactions = this.roleTransactionRepository.GetRoleTransactionsByName(roleTransactionName);

            IList<RoleTransactionModel> roleTransactionModels = new List<RoleTransactionModel>();
            roleTransactions.ForEach((user) => roleTransactionModels.Add(user.ToModel()));

            return roleTransactionModels.AsQueryable();
        }

        /// <summary>
        /// Get RoleTransaction by RoleTransactionId
        /// </summary>
        /// <param name="roleTransactionId"></param>
        /// <returns></returns>
        public RoleTransactionModel GetRoleTransactionById(Guid roleTransactionId)
        {
            RoleTransaction roleTransaction = this.roleTransactionRepository.GetRoleTransactionById(roleTransactionId);

            return (roleTransaction != null) ? roleTransaction.ToModel() : null;
        }

        /// <summary>
        /// Get RoleTransaction by RoleTransactionId using async
        /// </summary>
        /// <param name="roleTransactionId"></param>
        /// <returns></returns>
        public async Task<RoleTransactionModel> GetRoleTransactionByIdAsync(Guid roleTransactionId)
        {
            var roleTransaction = await this.roleTransactionRepository.GetRoleTransactionByIdAsync(roleTransactionId);

            return (roleTransaction != null) ? roleTransaction.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update RoleTransaction entity
        /// </summary>
        /// <param name="roleTransactionModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateRoleTransactionDetails(RoleTransactionModel roleTransactionModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            RoleTransaction roleTransaction = null;

            //Validate model data
            roleTransactionModel = roleTransactionModel.GetModelValidationErrors<RoleTransactionModel>();

            if (roleTransactionModel.ErrorMessages.Count > 0)
            {
                response.Errors = roleTransactionModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (roleTransactionModel.RequestType == RequestType.Edit)
            {
                roleTransaction = this.roleTransactionRepository.GetRoleTransactionById(roleTransactionModel.RoleTransactionId);
            }
            else
            {
                roleTransaction = new RoleTransaction();
                roleTransactionModel.RoleTransactionId = Guid.NewGuid();
            }

            if (roleTransactionModel.ErrorMessages.Count == 0 && !(Guid.Equals(roleTransactionModel.RoleId, roleTransaction.RoleId) && Guid.Equals(roleTransactionModel.TransactionId, roleTransaction.TransactionId)))
            {
                bool isRoleTransactionExists = this.roleTransactionRepository.CheckRoleTransactionExistByName(roleTransactionModel);

                if (isRoleTransactionExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                roleTransaction.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                roleTransaction = roleTransactionModel.FromModel(roleTransaction);

                //Save Or Update roleTransaction details
                bool isSaved = this.roleTransactionRepository.SaveOrUpdateRoleTransactionDetails(roleTransaction);

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
        /// Delete a record by RoleTransactionId
        /// </summary>
        /// <param name="roleTransactionId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid roleTransactionId)
        {
            return this.roleTransactionRepository.DeleteById(roleTransactionId);
        }

        /// <summary>
        /// Check duplicate RoleTransaction by Name
        /// </summary>
        /// <param name="roleTransactionModel"></param>
        /// <returns></returns>
        public bool CheckRoleTransactionExistByName(RoleTransactionModel roleTransactionModel)
        {
            return this.roleTransactionRepository.CheckRoleTransactionExistByName(roleTransactionModel);
        }

        /// <summary>}
        /// List of RoleTransaction with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<RoleTransactionViewModel> GetRoleTransactionsByCriteria(SearchRoleTransactionModel searchModel)
        {
            return this.roleTransactionRepository.GetRoleTransactionsByCriteria(searchModel);
        }
    }
}