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
    /// Manager of the VTStudentAssessment entity
    /// </summary>
    public class VTStudentAssessmentManager : GenericManager<VTStudentAssessmentModel>, IVTStudentAssessmentManager
    {
        private readonly IVTStudentAssessmentRepository vtStudentAssessmentRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the vtStudentAssessment manager.
        /// </summary>
        /// <param name="vtStudentAssessmentRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_commonRepository"></param>
        public VTStudentAssessmentManager(IVTStudentAssessmentRepository _vtStudentAssessmentRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository)
        {
            this.vtStudentAssessmentRepository = _vtStudentAssessmentRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
        }

        /// <summary>
        /// Get list of VTStudentAssessments
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTStudentAssessmentModel> GetVTStudentAssessments()
        {
            var vtStudentAssessments = this.vtStudentAssessmentRepository.GetVTStudentAssessments();

            IList<VTStudentAssessmentModel> vtStudentAssessmentModels = new List<VTStudentAssessmentModel>();
            vtStudentAssessments.ForEach((user) => vtStudentAssessmentModels.Add(user.ToModel()));

            return vtStudentAssessmentModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VTStudentAssessments by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTStudentAssessmentModel> GetVTStudentAssessmentsByName(string vtStudentAssessmentName)
        {
            var vtStudentAssessments = this.vtStudentAssessmentRepository.GetVTStudentAssessmentsByName(vtStudentAssessmentName);

            IList<VTStudentAssessmentModel> vtStudentAssessmentModels = new List<VTStudentAssessmentModel>();
            vtStudentAssessments.ForEach((user) => vtStudentAssessmentModels.Add(user.ToModel()));

            return vtStudentAssessmentModels.AsQueryable();
        }

        /// <summary>
        /// Get VTStudentAssessment by VTStudentAssessmentId
        /// </summary>
        /// <param name="vtStudentAssessmentId"></param>
        /// <returns></returns>
        public VTStudentAssessmentModel GetVTStudentAssessmentById(Guid vtStudentAssessmentId)
        {
            VTStudentAssessment vtStudentAssessment = this.vtStudentAssessmentRepository.GetVTStudentAssessmentById(vtStudentAssessmentId);

            return (vtStudentAssessment != null) ? vtStudentAssessment.ToModel() : null;
        }

        /// <summary>
        /// Get VTStudentAssessment by VTStudentAssessmentId using async
        /// </summary>
        /// <param name="vtStudentAssessmentId"></param>
        /// <returns></returns>
        public async Task<VTStudentAssessmentModel> GetVTStudentAssessmentByIdAsync(Guid vtStudentAssessmentId)
        {
            var vtStudentAssessment = await this.vtStudentAssessmentRepository.GetVTStudentAssessmentByIdAsync(vtStudentAssessmentId);

            return (vtStudentAssessment != null) ? vtStudentAssessment.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VTStudentAssessment entity
        /// </summary>
        /// <param name="vtStudentAssessmentModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVTStudentAssessmentDetails(VTStudentAssessmentModel vtStudentAssessmentModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            VTStudentAssessment vtStudentAssessment = null;

            //Validate model data
            vtStudentAssessmentModel = vtStudentAssessmentModel.GetModelValidationErrors<VTStudentAssessmentModel>();

            if (vtStudentAssessmentModel.ErrorMessages.Count > 0)
            {
                response.Errors = vtStudentAssessmentModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (vtStudentAssessmentModel.RequestType == RequestType.Edit)
            {
                vtStudentAssessment = this.vtStudentAssessmentRepository.GetVTStudentAssessmentById(vtStudentAssessmentModel.VTStudentAssessmentId);
            }
            else
            {
                vtStudentAssessment = new VTStudentAssessment();
                vtStudentAssessmentModel.VTStudentAssessmentId = Guid.NewGuid();
            }

            if (vtStudentAssessmentModel.ErrorMessages.Count == 0 && !(string.Equals(vtStudentAssessmentModel.TestimonialType.ToLower(), vtStudentAssessment.TestimonialType.StringVal().ToLower()) && string.Equals(vtStudentAssessmentModel.StudentName.ToLower(), vtStudentAssessment.StudentName.StringVal().ToLower())))
            {
                bool isVTStudentAssessmentExists = this.vtStudentAssessmentRepository.CheckVTStudentAssessmentExistByName(vtStudentAssessmentModel);

                if (isVTStudentAssessmentExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                vtStudentAssessment.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

                if (vtStudentAssessmentModel.RequestType == RequestType.New)
                {
                    VTClass vtClass = this.commonRepository.GetVTClassByUserId(vtStudentAssessment.AuthUserId);
                    if (vtClass != null)
                        vtStudentAssessmentModel.VTClassId = vtClass.VTClassId;
                }

                vtStudentAssessment = vtStudentAssessmentModel.FromModel(vtStudentAssessment);

                //Save Or Update vtStudentAssessment details
                bool isSaved = this.vtStudentAssessmentRepository.SaveOrUpdateVTStudentAssessmentDetails(vtStudentAssessment);

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
        /// Delete a record by VTStudentAssessmentId
        /// </summary>
        /// <param name="vtStudentAssessmentId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtStudentAssessmentId)
        {
            return this.vtStudentAssessmentRepository.DeleteById(vtStudentAssessmentId);
        }

        /// <summary>
        /// Check duplicate VTStudentAssessment by Name
        /// </summary>
        /// <param name="vtStudentAssessmentModel"></param>
        /// <returns></returns>
        public bool CheckVTStudentAssessmentExistByName(VTStudentAssessmentModel vtStudentAssessmentModel)
        {
            return this.vtStudentAssessmentRepository.CheckVTStudentAssessmentExistByName(vtStudentAssessmentModel);
        }

        /// <summary>}
        /// List of VTStudentAssessment with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTStudentAssessmentViewModel> GetVTStudentAssessmentsByCriteria(SearchVTStudentAssessmentModel searchModel)
        {
            return this.vtStudentAssessmentRepository.GetVTStudentAssessmentsByCriteria(searchModel);
        }
    }
}