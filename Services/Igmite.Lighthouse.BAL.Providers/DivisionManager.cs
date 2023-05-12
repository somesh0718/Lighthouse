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
    /// Manager of the Division entity
    /// </summary>
    public class DivisionManager : GenericManager<DivisionModel>, IDivisionManager
    {
        private readonly IDivisionRepository divisionRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the division manager.
        /// </summary>
        /// <param name="divisionRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public DivisionManager(IDivisionRepository _divisionRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.divisionRepository = _divisionRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of Divisions
        /// </summary>
        /// <returns></returns>
        public IQueryable<DivisionModel> GetDivisions()
        {
            var divisions = this.divisionRepository.GetDivisions();

            IList<DivisionModel> divisionModels = new List<DivisionModel>();
            divisions.ForEach((user) => divisionModels.Add(user.ToModel()));

            return divisionModels.AsQueryable();
        }

        /// <summary>
        /// Get list of Divisions by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<DivisionModel> GetDivisionsByName(string divisionName)
        {
            var divisions = this.divisionRepository.GetDivisionsByName(divisionName);

            IList<DivisionModel> divisionModels = new List<DivisionModel>();
            divisions.ForEach((user) => divisionModels.Add(user.ToModel()));

            return divisionModels.AsQueryable();
        }

        /// <summary>
        /// Get Division by DivisionId
        /// </summary>
        /// <param name="divisionId"></param>
        /// <returns></returns>
        public DivisionModel GetDivisionById(Guid divisionId)
        {
            Division division = this.divisionRepository.GetDivisionById(divisionId);

            return (division != null) ? division.ToModel() : null;
        }

        /// <summary>
        /// Get Division by DivisionId using async
        /// </summary>
        /// <param name="divisionId"></param>
        /// <returns></returns>
        public async Task<DivisionModel> GetDivisionByIdAsync(Guid divisionId)
        {
            var division = await this.divisionRepository.GetDivisionByIdAsync(divisionId);

            return (division != null) ? division.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update Division entity
        /// </summary>
        /// <param name="divisionModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateDivisionDetails(DivisionModel divisionModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            Division division = null;

            //Validate model data
            divisionModel = divisionModel.GetModelValidationErrors<DivisionModel>();

            if (divisionModel.ErrorMessages.Count > 0)
            {
                response.Errors = divisionModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (divisionModel.RequestType == RequestType.Edit)
            {
                division = this.divisionRepository.GetDivisionById(divisionModel.DivisionId);
            }
            else
            {
                division = new Division();
                divisionModel.DivisionId = Guid.NewGuid();
            }

            if (divisionModel.ErrorMessages.Count == 0 && (divisionModel.DivisionName.StringVal().ToLower() != division.DivisionName.StringVal().ToLower()))
            {
                bool isDivisionExists = this.divisionRepository.CheckDivisionExistByName(divisionModel);

                if (isDivisionExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                division.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                division = divisionModel.FromModel(division);

                //Save Or Update division details
                bool isSaved = this.divisionRepository.SaveOrUpdateDivisionDetails(division);

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
        /// Delete a record by DivisionId
        /// </summary>
        /// <param name="divisionId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid divisionId)
        {
            return this.divisionRepository.DeleteById(divisionId);
        }

        /// <summary>
        /// Check duplicate Division by Name
        /// </summary>
        /// <param name="divisionModel"></param>
        /// <returns></returns>
        public bool CheckDivisionExistByName(DivisionModel divisionModel)
        {
            return this.divisionRepository.CheckDivisionExistByName(divisionModel);
        }

        /// <summary>}
        /// List of Division with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<DivisionViewModel> GetDivisionsByCriteria(SearchDivisionModel searchModel)
        {
            return this.divisionRepository.GetDivisionsByCriteria(searchModel);
        }
    }
}