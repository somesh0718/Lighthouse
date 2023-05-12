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
    /// Manager of the VTStatusOfInductionInserviceTraining entity
    /// </summary>
    public class VTStatusOfInductionInserviceTrainingManager : GenericManager<VTStatusOfInductionInserviceTrainingModel>, IVTStatusOfInductionInserviceTrainingManager
    {
        private readonly IVTStatusOfInductionInserviceTrainingRepository vtStatusOfInductionInserviceTrainingRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the vtStatusOfInductionInserviceTraining manager.
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        /// <param name="_commonRepository"></param>
        public VTStatusOfInductionInserviceTrainingManager(IVTStatusOfInductionInserviceTrainingRepository _vtStatusOfInductionInserviceTrainingRepository, IHttpContextAccessor _httpContextAccessor, ICommonRepository _commonRepository)
        {
            this.vtStatusOfInductionInserviceTrainingRepository = _vtStatusOfInductionInserviceTrainingRepository;
            this.httpContextAccessor = _httpContextAccessor;
            this.commonRepository = _commonRepository;
        }

        /// <summary>
        /// Get list of VTStatusOfInductionInserviceTrainings
        /// </summary>
        /// <returns></returns>
        public IQueryable<VTStatusOfInductionInserviceTrainingModel> GetVTStatusOfInductionInserviceTrainings()
        {
            var vtStatusOfInductionInserviceTrainings = this.vtStatusOfInductionInserviceTrainingRepository.GetVTStatusOfInductionInserviceTrainings();

            IList<VTStatusOfInductionInserviceTrainingModel> vtStatusOfInductionInserviceTrainingModels = new List<VTStatusOfInductionInserviceTrainingModel>();
            vtStatusOfInductionInserviceTrainings.ForEach((user) => vtStatusOfInductionInserviceTrainingModels.Add(user.ToModel()));

            return vtStatusOfInductionInserviceTrainingModels.AsQueryable();
        }

        /// <summary>
        /// Get list of VTStatusOfInductionInserviceTrainings by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<VTStatusOfInductionInserviceTrainingModel> GetVTStatusOfInductionInserviceTrainingsByName(string vtStatusOfInductionInserviceTrainingName)
        {
            var vtStatusOfInductionInserviceTrainings = this.vtStatusOfInductionInserviceTrainingRepository.GetVTStatusOfInductionInserviceTrainingsByName(vtStatusOfInductionInserviceTrainingName);

            IList<VTStatusOfInductionInserviceTrainingModel> vtStatusOfInductionInserviceTrainingModels = new List<VTStatusOfInductionInserviceTrainingModel>();
            vtStatusOfInductionInserviceTrainings.ForEach((user) => vtStatusOfInductionInserviceTrainingModels.Add(user.ToModel()));

            return vtStatusOfInductionInserviceTrainingModels.AsQueryable();
        }

        /// <summary>
        /// Get VTStatusOfInductionInserviceTraining by VTStatusOfInductionInserviceTrainingId
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingId"></param>
        /// <returns></returns>
        public VTStatusOfInductionInserviceTrainingModel GetVTStatusOfInductionInserviceTrainingById(Guid vtStatusOfInductionInserviceTrainingId)
        {
            VTStatusOfInductionInserviceTraining vtStatusOfInductionInserviceTraining = this.vtStatusOfInductionInserviceTrainingRepository.GetVTStatusOfInductionInserviceTrainingById(vtStatusOfInductionInserviceTrainingId);

            return (vtStatusOfInductionInserviceTraining != null) ? vtStatusOfInductionInserviceTraining.ToModel() : null;
        }

        /// <summary>
        /// Get VTStatusOfInductionInserviceTraining by VTStatusOfInductionInserviceTrainingId using async
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingId"></param>
        /// <returns></returns>
        public async Task<VTStatusOfInductionInserviceTrainingModel> GetVTStatusOfInductionInserviceTrainingByIdAsync(Guid vtStatusOfInductionInserviceTrainingId)
        {
            var vtStatusOfInductionInserviceTraining = await this.vtStatusOfInductionInserviceTrainingRepository.GetVTStatusOfInductionInserviceTrainingByIdAsync(vtStatusOfInductionInserviceTrainingId);

            return (vtStatusOfInductionInserviceTraining != null) ? vtStatusOfInductionInserviceTraining.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update VTStatusOfInductionInserviceTraining entity
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateVTStatusOfInductionInserviceTrainingDetails(VTStatusOfInductionInserviceTrainingModel vtStatusOfInductionInserviceTrainingModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            VTStatusOfInductionInserviceTraining vtStatusOfInductionInserviceTraining = null;

            //Validate model data
            vtStatusOfInductionInserviceTrainingModel = vtStatusOfInductionInserviceTrainingModel.GetModelValidationErrors<VTStatusOfInductionInserviceTrainingModel>();

            if (vtStatusOfInductionInserviceTrainingModel.ErrorMessages.Count > 0)
            {
                response.Errors = vtStatusOfInductionInserviceTrainingModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (vtStatusOfInductionInserviceTrainingModel.RequestType == RequestType.Edit)
            {
                vtStatusOfInductionInserviceTraining = this.vtStatusOfInductionInserviceTrainingRepository.GetVTStatusOfInductionInserviceTrainingById(vtStatusOfInductionInserviceTrainingModel.VTStatusOfInductionInserviceTrainingId);
            }
            else
            {
                vtStatusOfInductionInserviceTraining = new VTStatusOfInductionInserviceTraining();
                vtStatusOfInductionInserviceTrainingModel.VTStatusOfInductionInserviceTrainingId = Guid.NewGuid();
            }

            if (vtStatusOfInductionInserviceTrainingModel.ErrorMessages.Count == 0 && !(string.Equals(vtStatusOfInductionInserviceTrainingModel.IndustryTrainingStatus.ToLower(), vtStatusOfInductionInserviceTraining.IndustryTrainingStatus.StringVal().ToLower())))
            {
                bool isVTStatusOfInductionInserviceTrainingExists = this.vtStatusOfInductionInserviceTrainingRepository.CheckVTStatusOfInductionInserviceTrainingExistByName(vtStatusOfInductionInserviceTrainingModel);

                if (isVTStatusOfInductionInserviceTrainingExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                vtStatusOfInductionInserviceTraining.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);

                if (vtStatusOfInductionInserviceTrainingModel.RequestType == RequestType.New)
                {
                    VTSchoolSector vtSchoolSector = this.commonRepository.GetVTSchoolSectorsByVTId(vtStatusOfInductionInserviceTrainingModel.VTId);
                    if (vtSchoolSector != null)
                        vtStatusOfInductionInserviceTrainingModel.VTSchoolSectorId = vtSchoolSector.VTSchoolSectorId;
                }

                vtStatusOfInductionInserviceTraining = vtStatusOfInductionInserviceTrainingModel.FromModel(vtStatusOfInductionInserviceTraining);

                //Save Or Update vtStatusOfInductionInserviceTraining details
                bool isSaved = this.vtStatusOfInductionInserviceTrainingRepository.SaveOrUpdateVTStatusOfInductionInserviceTrainingDetails(vtStatusOfInductionInserviceTraining);

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
        /// Delete a record by VTStatusOfInductionInserviceTrainingId
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid vtStatusOfInductionInserviceTrainingId)
        {
            return this.vtStatusOfInductionInserviceTrainingRepository.DeleteById(vtStatusOfInductionInserviceTrainingId);
        }

        /// <summary>
        /// Check duplicate VTStatusOfInductionInserviceTraining by Name
        /// </summary>
        /// <param name="vtStatusOfInductionInserviceTrainingModel"></param>
        /// <returns></returns>
        public bool CheckVTStatusOfInductionInserviceTrainingExistByName(VTStatusOfInductionInserviceTrainingModel vtStatusOfInductionInserviceTrainingModel)
        {
            return this.vtStatusOfInductionInserviceTrainingRepository.CheckVTStatusOfInductionInserviceTrainingExistByName(vtStatusOfInductionInserviceTrainingModel);
        }

        /// <summary>}
        /// List of VTStatusOfInductionInserviceTraining with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<VTStatusOfInductionInserviceTrainingViewModel> GetVTStatusOfInductionInserviceTrainingsByCriteria(SearchVTStatusOfInductionInserviceTrainingModel searchModel)
        {
            return this.vtStatusOfInductionInserviceTrainingRepository.GetVTStatusOfInductionInserviceTrainingsByCriteria(searchModel);
        }
    }
}