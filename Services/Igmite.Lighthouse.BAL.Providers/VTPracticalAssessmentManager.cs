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
    /// Manager of the VTPracticalAssessment entity
    /// </summary>
    public class VTPracticalAssessmentManager : GenericManager<VTPracticalAssessmentModel>, IVTPracticalAssessmentManager
    {
        private readonly IVTPracticalAssessmentRepository vtPracticalAssessmentRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the vtPracticalAssessment manager.
        /// </summary>
        /// <param name="vtPracticalAssessmentRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public VTPracticalAssessmentManager(IVTPracticalAssessmentRepository _vtPracticalAssessmentRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.vtPracticalAssessmentRepository = _vtPracticalAssessmentRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of VTPracticalAssessments
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTPracticalAssessmentModel> GetVTPracticalAssessments()
        {
            var vtPracticalAssessments = this.vtPracticalAssessmentRepository.GetVTPracticalAssessments();

            IList<VTPracticalAssessmentModel> vtPracticalAssessmentModels = new List<VTPracticalAssessmentModel>();
            vtPracticalAssessments.ForEach((user) => vtPracticalAssessmentModels.Add(user.ToModel()));

            return vtPracticalAssessmentModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VTPracticalAssessments by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTPracticalAssessmentModel> GetVTPracticalAssessmentsByName(string vtPracticalAssessmentName)
        {
            var vtPracticalAssessments = this.vtPracticalAssessmentRepository.GetVTPracticalAssessmentsByName(vtPracticalAssessmentName);

            IList<VTPracticalAssessmentModel> vtPracticalAssessmentModels = new List<VTPracticalAssessmentModel>();
            vtPracticalAssessments.ForEach((user) => vtPracticalAssessmentModels.Add(user.ToModel()));

            return vtPracticalAssessmentModels.AsQueryable();
        }

        /// <summary>
        /// Get VTPracticalAssessment by VTPracticalAssessmentId
        /// </summary>
        /// <param name="vtPracticalAssessmentId"></param>
        /// <returns></returns>
        public VTPracticalAssessmentModel GetVTPracticalAssessmentById(Guid vtPracticalAssessmentId)
        {
            VTPracticalAssessment vtPracticalAssessment = this.vtPracticalAssessmentRepository.GetVTPracticalAssessmentById(vtPracticalAssessmentId);

            return (vtPracticalAssessment != null) ? vtPracticalAssessment.ToModel() : null;
        }

        /// <summary>
        /// Get VTPracticalAssessment by VTPracticalAssessmentId using async
        /// </summary>
        /// <param name="vtPracticalAssessmentId"></param>
        /// <returns></returns>
        public async Task<VTPracticalAssessmentModel> GetVTPracticalAssessmentByIdAsync(Guid vtPracticalAssessmentId)
        {
            var vtPracticalAssessment = await this.vtPracticalAssessmentRepository.GetVTPracticalAssessmentByIdAsync(vtPracticalAssessmentId);

            return (vtPracticalAssessment != null) ? vtPracticalAssessment.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VTPracticalAssessment entity
        /// </summary>
        /// <param name="vtPracticalAssessmentModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVTPracticalAssessmentDetails(VTPracticalAssessmentModel vtPracticalAssessmentModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            VTPracticalAssessment vtPracticalAssessment = null;

            //Validate model data
            vtPracticalAssessmentModel = vtPracticalAssessmentModel.GetModelValidationErrors<VTPracticalAssessmentModel>();

            if (vtPracticalAssessmentModel.ErrorMessages.Count > 0)
            {
                response.Errors = vtPracticalAssessmentModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (vtPracticalAssessmentModel.RequestType == RequestType.Edit)
            {
                vtPracticalAssessment = this.vtPracticalAssessmentRepository.GetVTPracticalAssessmentById(vtPracticalAssessmentModel.VTPracticalAssessmentId);
            }
            else
            {
                vtPracticalAssessment = new VTPracticalAssessment();
                vtPracticalAssessmentModel.VTPracticalAssessmentId = Guid.NewGuid();
            }

            if (vtPracticalAssessmentModel.ErrorMessages.Count == 0 && (vtPracticalAssessmentModel.GirlsPresent.StringVal().ToLower() != vtPracticalAssessment.GirlsPresent.StringVal().ToLower()))
            {
                bool isVTPracticalAssessmentExists = this.vtPracticalAssessmentRepository.CheckVTPracticalAssessmentExistByName(vtPracticalAssessmentModel);

                if (isVTPracticalAssessmentExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                vtPracticalAssessment.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                vtPracticalAssessment = vtPracticalAssessmentModel.FromModel(vtPracticalAssessment);

                //Save Or Update vtPracticalAssessment details
                bool isSaved = this.vtPracticalAssessmentRepository.SaveOrUpdateVTPracticalAssessmentDetails(vtPracticalAssessment);

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
        /// Delete a record by VTPracticalAssessmentId
        /// </summary>
        /// <param name="vtPracticalAssessmentId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtPracticalAssessmentId)
        {
            return this.vtPracticalAssessmentRepository.DeleteById(vtPracticalAssessmentId);
        }

        /// <summary>
        /// Check duplicate VTPracticalAssessment by Name
        /// </summary>
        /// <param name="vtPracticalAssessmentModel"></param>
        /// <returns></returns>
        public bool CheckVTPracticalAssessmentExistByName(VTPracticalAssessmentModel vtPracticalAssessmentModel)
        {
            return this.vtPracticalAssessmentRepository.CheckVTPracticalAssessmentExistByName(vtPracticalAssessmentModel);
        }

        /// <summary>}
        /// List of VTPracticalAssessment with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTPracticalAssessmentViewModel> GetVTPracticalAssessmentsByCriteria(SearchVTPracticalAssessmentModel searchModel)
        {
            return this.vtPracticalAssessmentRepository.GetVTPracticalAssessmentsByCriteria(searchModel);
        }
    }
}