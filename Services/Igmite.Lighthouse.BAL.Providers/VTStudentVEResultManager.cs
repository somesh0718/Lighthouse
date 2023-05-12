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
    /// Manager of the VTStudentVEResult entity
    /// </summary>
    public class VTStudentVEResultManager : GenericManager<VTStudentVEResultModel>, IVTStudentVEResultManager
    {
        private readonly IVTStudentVEResultRepository vtStudentVEResultRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the vtStudentVEResult manager.
        /// </summary>
        /// <param name="vtStudentVEResultRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_commonRepository"></param>
        public VTStudentVEResultManager(IVTStudentVEResultRepository _vtStudentVEResultRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository)
        {
            this.vtStudentVEResultRepository = _vtStudentVEResultRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
        }

        /// <summary>
        /// Get list of VTStudentVEResults
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTStudentVEResultModel> GetVTStudentVEResults()
        {
            var vtStudentVEResults = this.vtStudentVEResultRepository.GetVTStudentVEResults();

            IList<VTStudentVEResultModel> vtStudentVEResultModels = new List<VTStudentVEResultModel>();
            vtStudentVEResults.ForEach((user) => vtStudentVEResultModels.Add(user.ToModel()));

            return vtStudentVEResultModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VTStudentVEResults by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTStudentVEResultModel> GetVTStudentVEResultsByName(string vtStudentVEResultName)
        {
            var vtStudentVEResults = this.vtStudentVEResultRepository.GetVTStudentVEResultsByName(vtStudentVEResultName);

            IList<VTStudentVEResultModel> vtStudentVEResultModels = new List<VTStudentVEResultModel>();
            vtStudentVEResults.ForEach((user) => vtStudentVEResultModels.Add(user.ToModel()));

            return vtStudentVEResultModels.AsQueryable();
        }

        /// <summary>
        /// Get VTStudentVEResult by VTStudentVEResultId
        /// </summary>
        /// <param name="vtStudentVEResultId"></param>
        /// <returns></returns>
        public VTStudentVEResultModel GetVTStudentVEResultById(Guid vtStudentVEResultId)
        {
            VTStudentVEResult vtStudentVEResult = this.vtStudentVEResultRepository.GetVTStudentVEResultById(vtStudentVEResultId);

            return (vtStudentVEResult != null) ? vtStudentVEResult.ToModel() : null;
        }

        /// <summary>
        /// Get VTStudentVEResult by VTStudentVEResultId using async
        /// </summary>
        /// <param name="vtStudentVEResultId"></param>
        /// <returns></returns>
        public async Task<VTStudentVEResultModel> GetVTStudentVEResultByIdAsync(Guid vtStudentVEResultId)
        {
            var vtStudentVEResult = await this.vtStudentVEResultRepository.GetVTStudentVEResultByIdAsync(vtStudentVEResultId);

            return (vtStudentVEResult != null) ? vtStudentVEResult.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VTStudentVEResult entity
        /// </summary>
        /// <param name="vtStudentVEResultModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVTStudentVEResultDetails(VTStudentVEResultModel vtStudentVEResultModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            VTStudentVEResult vtStudentVEResult = null;

            //Validate model data
            vtStudentVEResultModel = vtStudentVEResultModel.GetModelValidationErrors<VTStudentVEResultModel>();

            if (vtStudentVEResultModel.ErrorMessages.Count > 0)
            {
                response.Errors = vtStudentVEResultModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (vtStudentVEResultModel.RequestType == RequestType.Edit)
            {
                vtStudentVEResult = this.vtStudentVEResultRepository.GetVTStudentVEResultById(vtStudentVEResultModel.VTStudentVEResultId);
            }
            else
            {
                vtStudentVEResult = new VTStudentVEResult();
                vtStudentVEResultModel.VTStudentVEResultId = Guid.NewGuid();
            }

            if (vtStudentVEResultModel.ErrorMessages.Count == 0 && !(Guid.Equals(vtStudentVEResultModel.StudentId, vtStudentVEResult.StudentId) && DateTime.Equals(vtStudentVEResultModel.DateIssuence, vtStudentVEResult.DateIssuence)))
            {
                bool isVTStudentVEResultExists = this.vtStudentVEResultRepository.CheckVTStudentVEResultExistByName(vtStudentVEResultModel);

                if (isVTStudentVEResultExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                vtStudentVEResult.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

                if (vtStudentVEResultModel.RequestType == RequestType.New)
                {
                    VTClass vtClass = this.commonRepository.GetVTClassByUserId(vtStudentVEResult.AuthUserId);
                    if (vtClass != null)
                        vtStudentVEResultModel.VTClassId = vtClass.VTClassId;
                }

                vtStudentVEResult = vtStudentVEResultModel.FromModel(vtStudentVEResult);

                //Save Or Update vtStudentVEResult details
                bool isSaved = this.vtStudentVEResultRepository.SaveOrUpdateVTStudentVEResultDetails(vtStudentVEResult);

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
        /// Delete a record by VTStudentVEResultId
        /// </summary>
        /// <param name="vtStudentVEResultId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtStudentVEResultId)
        {
            return this.vtStudentVEResultRepository.DeleteById(vtStudentVEResultId);
        }

        /// <summary>
        /// Check duplicate VTStudentVEResult by Name
        /// </summary>
        /// <param name="vtStudentVEResultModel"></param>
        /// <returns></returns>
        public bool CheckVTStudentVEResultExistByName(VTStudentVEResultModel vtStudentVEResultModel)
        {
            return this.vtStudentVEResultRepository.CheckVTStudentVEResultExistByName(vtStudentVEResultModel);
        }

        /// <summary>}
        /// List of VTStudentVEResult with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTStudentVEResultViewModel> GetVTStudentVEResultsByCriteria(SearchVTStudentVEResultModel searchModel)
        {
            return this.vtStudentVEResultRepository.GetVTStudentVEResultsByCriteria(searchModel);
        }
    }
}