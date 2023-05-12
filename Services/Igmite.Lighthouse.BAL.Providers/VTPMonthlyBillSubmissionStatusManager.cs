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
    /// Manager of the VTPMonthlyBillSubmissionStatus entity
    /// </summary>
    public class VTPMonthlyBillSubmissionStatusManager : GenericManager<VTPMonthlyBillSubmissionStatusModel>, IVTPMonthlyBillSubmissionStatusManager
    {
        private readonly IVTPMonthlyBillSubmissionStatusRepository vtpMonthlyBillSubmissionStatusRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the vtpMonthlyBillSubmissionStatus manager.
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public VTPMonthlyBillSubmissionStatusManager(IVTPMonthlyBillSubmissionStatusRepository _vtpMonthlyBillSubmissionStatusRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.vtpMonthlyBillSubmissionStatusRepository = _vtpMonthlyBillSubmissionStatusRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of VTPMonthlyBillSubmissionStatus
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTPMonthlyBillSubmissionStatusModel> GetVTPMonthlyBillSubmissionStatus()
        {
            var vtpMonthlyBillSubmissionStatus = this.vtpMonthlyBillSubmissionStatusRepository.GetVTPMonthlyBillSubmissionStatus();

            IList<VTPMonthlyBillSubmissionStatusModel> vtpMonthlyBillSubmissionStatusModels = new List<VTPMonthlyBillSubmissionStatusModel>();
            vtpMonthlyBillSubmissionStatus.ForEach((user) => vtpMonthlyBillSubmissionStatusModels.Add(user.ToModel()));

            return vtpMonthlyBillSubmissionStatusModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VTPMonthlyBillSubmissionStatus by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTPMonthlyBillSubmissionStatusModel> GetVTPMonthlyBillSubmissionStatusByName(string vtpMonthlyBillSubmissionStatusName)
        {
            var vtpMonthlyBillSubmissionStatus = this.vtpMonthlyBillSubmissionStatusRepository.GetVTPMonthlyBillSubmissionStatusByName(vtpMonthlyBillSubmissionStatusName);

            IList<VTPMonthlyBillSubmissionStatusModel> vtpMonthlyBillSubmissionStatusModels = new List<VTPMonthlyBillSubmissionStatusModel>();
            vtpMonthlyBillSubmissionStatus.ForEach((user) => vtpMonthlyBillSubmissionStatusModels.Add(user.ToModel()));

            return vtpMonthlyBillSubmissionStatusModels.AsQueryable();
        }

        /// <summary>
        /// Get VTPMonthlyBillSubmissionStatus by VTPMonthlyBillSubmissionStatusId
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusId"></param>
        /// <returns></returns>
        public VTPMonthlyBillSubmissionStatusModel GetVTPMonthlyBillSubmissionStatusById(Guid vtpMonthlyBillSubmissionStatusId)
        {
            VTPMonthlyBillSubmissionStatus vtpMonthlyBillSubmissionStatus = this.vtpMonthlyBillSubmissionStatusRepository.GetVTPMonthlyBillSubmissionStatusById(vtpMonthlyBillSubmissionStatusId);

            return (vtpMonthlyBillSubmissionStatus != null) ? vtpMonthlyBillSubmissionStatus.ToModel() : null;
        }

        /// <summary>
        /// Get VTPMonthlyBillSubmissionStatus by VTPMonthlyBillSubmissionStatusId using async
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusId"></param>
        /// <returns></returns>
        public async Task<VTPMonthlyBillSubmissionStatusModel> GetVTPMonthlyBillSubmissionStatusByIdAsync(Guid vtpMonthlyBillSubmissionStatusId)
        {
            var vtpMonthlyBillSubmissionStatus = await this.vtpMonthlyBillSubmissionStatusRepository.GetVTPMonthlyBillSubmissionStatusByIdAsync(vtpMonthlyBillSubmissionStatusId);

            return (vtpMonthlyBillSubmissionStatus != null) ? vtpMonthlyBillSubmissionStatus.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VTPMonthlyBillSubmissionStatus entity
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVTPMonthlyBillSubmissionStatusDetails(VTPMonthlyBillSubmissionStatusModel vtpMonthlyBillSubmissionStatusModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            VTPMonthlyBillSubmissionStatus vtpMonthlyBillSubmissionStatus = null;

            //Validate model data
            vtpMonthlyBillSubmissionStatusModel = vtpMonthlyBillSubmissionStatusModel.GetModelValidationErrors<VTPMonthlyBillSubmissionStatusModel>();

            if (vtpMonthlyBillSubmissionStatusModel.ErrorMessages.Count > 0)
            {
                response.Errors = vtpMonthlyBillSubmissionStatusModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (vtpMonthlyBillSubmissionStatusModel.RequestType == RequestType.Edit)
            {
                vtpMonthlyBillSubmissionStatus = this.vtpMonthlyBillSubmissionStatusRepository.GetVTPMonthlyBillSubmissionStatusById(vtpMonthlyBillSubmissionStatusModel.VTPMonthlyBillSubmissionStatusId);
            }
            else
            {
                vtpMonthlyBillSubmissionStatus = new VTPMonthlyBillSubmissionStatus();
                vtpMonthlyBillSubmissionStatusModel.VTPMonthlyBillSubmissionStatusId = Guid.NewGuid();
            }

            if (vtpMonthlyBillSubmissionStatusModel.ErrorMessages.Count == 0 && !(Guid.Equals(vtpMonthlyBillSubmissionStatusModel.VCId, vtpMonthlyBillSubmissionStatus.VCId) && Int32.Equals(vtpMonthlyBillSubmissionStatusModel.Month, vtpMonthlyBillSubmissionStatus.Month) && DateTime.Equals(vtpMonthlyBillSubmissionStatusModel.DateSubmission, vtpMonthlyBillSubmissionStatus.DateSubmission)))
            {
                bool isVTPMonthlyBillSubmissionStatusExists = this.vtpMonthlyBillSubmissionStatusRepository.CheckVTPMonthlyBillSubmissionStatusExistByName(vtpMonthlyBillSubmissionStatusModel);

                if (isVTPMonthlyBillSubmissionStatusExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                vtpMonthlyBillSubmissionStatus.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                vtpMonthlyBillSubmissionStatus = vtpMonthlyBillSubmissionStatusModel.FromModel(vtpMonthlyBillSubmissionStatus);

                //Save Or Update vtpMonthlyBillSubmissionStatus details
                bool isSaved = this.vtpMonthlyBillSubmissionStatusRepository.SaveOrUpdateVTPMonthlyBillSubmissionStatusDetails(vtpMonthlyBillSubmissionStatus);

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
        /// Delete a record by VTPMonthlyBillSubmissionStatusId
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtpMonthlyBillSubmissionStatusId)
        {
            return this.vtpMonthlyBillSubmissionStatusRepository.DeleteById(vtpMonthlyBillSubmissionStatusId);
        }

        /// <summary>
        /// Check duplicate VTPMonthlyBillSubmissionStatus by Name
        /// </summary>
        /// <param name="vtpMonthlyBillSubmissionStatusModel"></param>
        /// <returns></returns>
        public bool CheckVTPMonthlyBillSubmissionStatusExistByName(VTPMonthlyBillSubmissionStatusModel vtpMonthlyBillSubmissionStatusModel)
        {
            return this.vtpMonthlyBillSubmissionStatusRepository.CheckVTPMonthlyBillSubmissionStatusExistByName(vtpMonthlyBillSubmissionStatusModel);
        }

        /// <summary>}
        /// List of VTPMonthlyBillSubmissionStatus with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTPMonthlyBillSubmissionStatusViewModel> GetVTPMonthlyBillSubmissionStatusByCriteria(SearchVTPMonthlyBillSubmissionStatusModel searchModel)
        {
            return this.vtpMonthlyBillSubmissionStatusRepository.GetVTPMonthlyBillSubmissionStatusByCriteria(searchModel);
        }
    }
}