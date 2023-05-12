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
    /// Manager of the VTStudentPlacementDetail entity
    /// </summary>
    public class VTStudentPlacementDetailManager : GenericManager<VTStudentPlacementDetailModel>, IVTStudentPlacementDetailManager
    {
        private readonly IVTStudentPlacementDetailRepository vtStudentPlacementDetailRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the vtStudentPlacementDetail manager.
        /// </summary>
        /// <param name="vtStudentPlacementDetailRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_commonRepository"></param>
        public VTStudentPlacementDetailManager(IVTStudentPlacementDetailRepository _vtStudentPlacementDetailRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository)
        {
            this.vtStudentPlacementDetailRepository = _vtStudentPlacementDetailRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
        }

        /// <summary>
        /// Get list of VTStudentPlacementDetails
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTStudentPlacementDetailModel> GetVTStudentPlacementDetails()
        {
            var vtStudentPlacementDetails = this.vtStudentPlacementDetailRepository.GetVTStudentPlacementDetails();

            IList<VTStudentPlacementDetailModel> vtStudentPlacementDetailModels = new List<VTStudentPlacementDetailModel>();
            vtStudentPlacementDetails.ForEach((user) => vtStudentPlacementDetailModels.Add(user.ToModel()));

            return vtStudentPlacementDetailModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VTStudentPlacementDetails by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTStudentPlacementDetailModel> GetVTStudentPlacementDetailsByName(string vtStudentPlacementDetailName)
        {
            var vtStudentPlacementDetails = this.vtStudentPlacementDetailRepository.GetVTStudentPlacementDetailsByName(vtStudentPlacementDetailName);

            IList<VTStudentPlacementDetailModel> vtStudentPlacementDetailModels = new List<VTStudentPlacementDetailModel>();
            vtStudentPlacementDetails.ForEach((user) => vtStudentPlacementDetailModels.Add(user.ToModel()));

            return vtStudentPlacementDetailModels.AsQueryable();
        }

        /// <summary>
        /// Get VTStudentPlacementDetail by VTStudentPlacementDetailId
        /// </summary>
        /// <param name="vtStudentPlacementDetailId"></param>
        /// <returns></returns>
        public VTStudentPlacementDetailModel GetVTStudentPlacementDetailById(Guid vtStudentPlacementDetailId)
        {
            VTStudentPlacementDetail vtStudentPlacementDetail = this.vtStudentPlacementDetailRepository.GetVTStudentPlacementDetailById(vtStudentPlacementDetailId);

            return (vtStudentPlacementDetail != null) ? vtStudentPlacementDetail.ToModel() : null;
        }

        /// <summary>
        /// Get VTStudentPlacementDetail by VTStudentPlacementDetailId using async
        /// </summary>
        /// <param name="vtStudentPlacementDetailId"></param>
        /// <returns></returns>
        public async Task<VTStudentPlacementDetailModel> GetVTStudentPlacementDetailByIdAsync(Guid vtStudentPlacementDetailId)
        {
            var vtStudentPlacementDetail = await this.vtStudentPlacementDetailRepository.GetVTStudentPlacementDetailByIdAsync(vtStudentPlacementDetailId);

            return (vtStudentPlacementDetail != null) ? vtStudentPlacementDetail.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VTStudentPlacementDetail entity
        /// </summary>
        /// <param name="vtStudentPlacementDetailModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVTStudentPlacementDetailDetails(VTStudentPlacementDetailModel vtStudentPlacementDetailModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            VTStudentPlacementDetail vtStudentPlacementDetail = null;

            //Validate model data
            vtStudentPlacementDetailModel = vtStudentPlacementDetailModel.GetModelValidationErrors<VTStudentPlacementDetailModel>();

            if (vtStudentPlacementDetailModel.ErrorMessages.Count > 0)
            {
                response.Errors = vtStudentPlacementDetailModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (vtStudentPlacementDetailModel.RequestType == RequestType.Edit)
            {
                vtStudentPlacementDetail = this.vtStudentPlacementDetailRepository.GetVTStudentPlacementDetailById(vtStudentPlacementDetailModel.VTStudentPlacementDetailId);
            }
            else
            {
                vtStudentPlacementDetail = new VTStudentPlacementDetail();
                vtStudentPlacementDetailModel.VTStudentPlacementDetailId = Guid.NewGuid();
            }

            if (vtStudentPlacementDetailModel.ErrorMessages.Count == 0 && !(Guid.Equals(vtStudentPlacementDetailModel.StudentId, vtStudentPlacementDetail.StudentId)))
            {
                bool isVTStudentPlacementDetailExists = this.vtStudentPlacementDetailRepository.CheckVTStudentPlacementDetailExistByName(vtStudentPlacementDetailModel);

                if (isVTStudentPlacementDetailExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                vtStudentPlacementDetail.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

                if (vtStudentPlacementDetailModel.RequestType == RequestType.New)
                {
                    VTClass vtClass = this.commonRepository.GetVTClassByUserId(vtStudentPlacementDetail.AuthUserId);
                    if (vtClass != null)
                        vtStudentPlacementDetailModel.VTClassId = vtClass.VTClassId;
                }

                vtStudentPlacementDetail = vtStudentPlacementDetailModel.FromModel(vtStudentPlacementDetail);

                //Save Or Update vtStudentPlacementDetail details
                bool isSaved = this.vtStudentPlacementDetailRepository.SaveOrUpdateVTStudentPlacementDetailDetails(vtStudentPlacementDetail);

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
        /// Delete a record by VTStudentPlacementDetailId
        /// </summary>
        /// <param name="vtStudentPlacementDetailId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtStudentPlacementDetailId)
        {
            return this.vtStudentPlacementDetailRepository.DeleteById(vtStudentPlacementDetailId);
        }

        /// <summary>
        /// Check duplicate VTStudentPlacementDetail by Name
        /// </summary>
        /// <param name="vtStudentPlacementDetailModel"></param>
        /// <returns></returns>
        public bool CheckVTStudentPlacementDetailExistByName(VTStudentPlacementDetailModel vtStudentPlacementDetailModel)
        {
            return this.vtStudentPlacementDetailRepository.CheckVTStudentPlacementDetailExistByName(vtStudentPlacementDetailModel);
        }

        /// <summary>}
        /// List of VTStudentPlacementDetail with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTStudentPlacementDetailViewModel> GetVTStudentPlacementDetailsByCriteria(SearchVTStudentPlacementDetailModel searchModel)
        {
            return this.vtStudentPlacementDetailRepository.GetVTStudentPlacementDetailsByCriteria(searchModel);
        }
    }
}