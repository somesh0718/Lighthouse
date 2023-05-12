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
    /// Manager of the Section entity
    /// </summary>
    public class SectionManager : GenericManager<SectionModel>, ISectionManager
    {
        private readonly ISectionRepository sectionRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the section manager.
        /// </summary>
        /// <param name="sectionRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public SectionManager(ISectionRepository _sectionRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.sectionRepository = _sectionRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of Sections
        /// </summary>
        /// <returns></returns>
        public IQueryable<SectionModel> GetSections()
        {
            var sections = this.sectionRepository.GetSections();

            IList<SectionModel> sectionModels = new List<SectionModel>();
            sections.ForEach((user) => sectionModels.Add(user.ToModel()));

            return sectionModels.AsQueryable();
        }

        /// <summary>
        /// Get list of Sections by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<SectionModel> GetSectionsByName(string sectionName)
        {
            var sections = this.sectionRepository.GetSectionsByName(sectionName);

            IList<SectionModel> sectionModels = new List<SectionModel>();
            sections.ForEach((user) => sectionModels.Add(user.ToModel()));

            return sectionModels.AsQueryable();
        }

        /// <summary>
        /// Get Section by SectionId
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public SectionModel GetSectionById(Guid sectionId)
        {
            Section section = this.sectionRepository.GetSectionById(sectionId);

            return (section != null) ? section.ToModel() : null;
        }

        /// <summary>
        /// Get Section by SectionId using async
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public async Task<SectionModel> GetSectionByIdAsync(Guid sectionId)
        {
            var section = await this.sectionRepository.GetSectionByIdAsync(sectionId);

            return (section != null) ? section.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update Section entity
        /// </summary>
        /// <param name="sectionModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateSectionDetails(SectionModel sectionModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            Section section = null;

            //Validate model data
            sectionModel = sectionModel.GetModelValidationErrors<SectionModel>();

            if (sectionModel.ErrorMessages.Count > 0)
            {
                response.Errors = sectionModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (sectionModel.RequestType == RequestType.Edit)
            {
                section = this.sectionRepository.GetSectionById(sectionModel.SectionId);
            }
            else
            {
                section = new Section();
                sectionModel.SectionId = Guid.NewGuid();
            }

            if (sectionModel.ErrorMessages.Count == 0 && (sectionModel.Name.StringVal().ToLower() != section.Name.StringVal().ToLower()))
            {
                bool isSectionExists = this.sectionRepository.CheckSectionExistByName(sectionModel);

                if (isSectionExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                section.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                section = sectionModel.FromModel(section);

                //Save Or Update section details
                bool isSaved = this.sectionRepository.SaveOrUpdateSectionDetails(section);

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
        /// Delete a record by SectionId
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid sectionId)
        {
            return this.sectionRepository.DeleteById(sectionId);
        }

        /// <summary>
        /// Check duplicate Section by Name
        /// </summary>
        /// <param name="sectionModel"></param>
        /// <returns></returns>
        public bool CheckSectionExistByName(SectionModel sectionModel)
        {
            return this.sectionRepository.CheckSectionExistByName(sectionModel);
        }

        /// <summary>}
        /// List of Section with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<SectionViewModel> GetSectionsByCriteria(SearchSectionModel searchModel)
        {
            return this.sectionRepository.GetSectionsByCriteria(searchModel);
        }
    }
}