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
    /// Manager of the Phase entity
    /// </summary>
    public class PhaseManager : GenericManager<PhaseModel>, IPhaseManager
    {
        private readonly IPhaseRepository phaseRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the phase manager.
        /// </summary>
        /// <param name="phaseRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public PhaseManager(IPhaseRepository _phaseRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.phaseRepository = _phaseRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of Phases
        /// </summary>
        /// <returns></returns>
        public IQueryable<PhaseModel> GetPhases()
        {
            var phases = this.phaseRepository.GetPhases();

            IList<PhaseModel> phaseModels = new List<PhaseModel>();
            phases.ForEach((user) => phaseModels.Add(user.ToModel()));

            return phaseModels.AsQueryable();
        }

        /// <summary>
        /// Get list of Phases by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<PhaseModel> GetPhasesByName(string phaseName)
        {
            var phases = this.phaseRepository.GetPhasesByName(phaseName);

            IList<PhaseModel> phaseModels = new List<PhaseModel>();
            phases.ForEach((user) => phaseModels.Add(user.ToModel()));

            return phaseModels.AsQueryable();
        }

        /// <summary>
        /// Get Phase by PhaseId
        /// </summary>
        /// <param name="phaseId"></param>
        /// <returns></returns>
        public PhaseModel GetPhaseById(Guid phaseId)
        {
            Phase phase = this.phaseRepository.GetPhaseById(phaseId);

            return (phase != null) ? phase.ToModel() : null;
        }

        /// <summary>
        /// Get Phase by PhaseId using async
        /// </summary>
        /// <param name="phaseId"></param>
        /// <returns></returns>
        public async Task<PhaseModel> GetPhaseByIdAsync(Guid phaseId)
        {
            var phase = await this.phaseRepository.GetPhaseByIdAsync(phaseId);

            return (phase != null) ? phase.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update Phase entity
        /// </summary>
        /// <param name="phaseModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdatePhaseDetails(PhaseModel phaseModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            Phase phase = null;

            //Validate model data
            phaseModel = phaseModel.GetModelValidationErrors<PhaseModel>();

            if (phaseModel.ErrorMessages.Count > 0)
            {
                response.Errors = phaseModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (phaseModel.RequestType == RequestType.Edit)
            {
                phase = this.phaseRepository.GetPhaseById(phaseModel.PhaseId);
            }
            else
            {
                phase = new Phase();
                phaseModel.PhaseId = Guid.NewGuid();
            }

            if (phaseModel.ErrorMessages.Count == 0 && (phaseModel.PhaseName.StringVal().ToLower() != phase.PhaseName.StringVal().ToLower()))
            {
                bool isPhaseExists = this.phaseRepository.CheckPhaseExistByName(phaseModel);

                if (isPhaseExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                phase.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                phase = phaseModel.FromModel(phase);

                //Save Or Update phase details
                bool isSaved = this.phaseRepository.SaveOrUpdatePhaseDetails(phase);

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
        /// Delete a record by PhaseId
        /// </summary>
        /// <param name="phaseId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid phaseId)
        {
            return this.phaseRepository.DeleteById(phaseId);
        }

        /// <summary>
        /// Check duplicate Phase by Name
        /// </summary>
        /// <param name="phaseModel"></param>
        /// <returns></returns>
        public bool CheckPhaseExistByName(PhaseModel phaseModel)
        {
            return this.phaseRepository.CheckPhaseExistByName(phaseModel);
        }

        /// <summary>}
        /// List of Phase with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<PhaseViewModel> GetPhasesByCriteria(SearchPhaseModel searchModel)
        {
            return this.phaseRepository.GetPhasesByCriteria(searchModel);
        }
    }
}