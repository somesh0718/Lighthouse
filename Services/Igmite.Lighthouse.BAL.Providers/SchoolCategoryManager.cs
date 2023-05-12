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
    /// Manager of the SchoolCategory entity
    /// </summary>
    public class SchoolCategoryManager : GenericManager<SchoolCategoryModel>, ISchoolCategoryManager
    {
        private readonly ISchoolCategoryRepository schoolCategoryRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the schoolCategory manager.
        /// </summary>
        /// <param name="schoolCategoryRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public SchoolCategoryManager(ISchoolCategoryRepository _schoolCategoryRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.schoolCategoryRepository = _schoolCategoryRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of SchoolCategories
        /// </summary>
        /// <returns></returns>
        public IQueryable<SchoolCategoryModel> GetSchoolCategories()
        {
            var schoolCategories = this.schoolCategoryRepository.GetSchoolCategories();

            IList<SchoolCategoryModel> schoolCategoryModels = new List<SchoolCategoryModel>();
            schoolCategories.ForEach((user) => schoolCategoryModels.Add(user.ToModel()));

            return schoolCategoryModels.AsQueryable();
        }

        /// <summary>
        /// Get list of SchoolCategories by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<SchoolCategoryModel> GetSchoolCategoriesByName(string schoolCategoryName)
        {
            var schoolCategories = this.schoolCategoryRepository.GetSchoolCategoriesByName(schoolCategoryName);

            IList<SchoolCategoryModel> schoolCategoryModels = new List<SchoolCategoryModel>();
            schoolCategories.ForEach((user) => schoolCategoryModels.Add(user.ToModel()));

            return schoolCategoryModels.AsQueryable();
        }

        /// <summary>
        /// Get SchoolCategory by SchoolCategoryId
        /// </summary>
        /// <param name="schoolCategoryId"></param>
        /// <returns></returns>
        public SchoolCategoryModel GetSchoolCategoryById(Guid schoolCategoryId)
        {
            SchoolCategory schoolCategory = this.schoolCategoryRepository.GetSchoolCategoryById(schoolCategoryId);

            return (schoolCategory != null) ? schoolCategory.ToModel() : null;
        }

        /// <summary>
        /// Get SchoolCategory by SchoolCategoryId using async
        /// </summary>
        /// <param name="schoolCategoryId"></param>
        /// <returns></returns>
        public async Task<SchoolCategoryModel> GetSchoolCategoryByIdAsync(Guid schoolCategoryId)
        {
            var schoolCategory = await this.schoolCategoryRepository.GetSchoolCategoryByIdAsync(schoolCategoryId);

            return (schoolCategory != null) ? schoolCategory.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update SchoolCategory entity
        /// </summary>
        /// <param name="schoolCategoryModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateSchoolCategoryDetails(SchoolCategoryModel schoolCategoryModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            SchoolCategory schoolCategory = null;

            //Validate model data
            schoolCategoryModel = schoolCategoryModel.GetModelValidationErrors<SchoolCategoryModel>();

            if (schoolCategoryModel.ErrorMessages.Count > 0)
            {
                response.Errors = schoolCategoryModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (schoolCategoryModel.RequestType == RequestType.Edit)
            {
                schoolCategory = this.schoolCategoryRepository.GetSchoolCategoryById(schoolCategoryModel.SchoolCategoryId);
            }
            else
            {
                schoolCategory = new SchoolCategory();
                schoolCategoryModel.SchoolCategoryId = Guid.NewGuid();
            }

            if (schoolCategoryModel.ErrorMessages.Count == 0 && (schoolCategoryModel.CategoryName.StringVal().ToLower() != schoolCategory.CategoryName.StringVal().ToLower()))
            {
                bool isSchoolCategoryExists = this.schoolCategoryRepository.CheckSchoolCategoryExistByName(schoolCategoryModel);

                if (isSchoolCategoryExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                schoolCategory.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                schoolCategory = schoolCategoryModel.FromModel(schoolCategory);

                //Save Or Update schoolCategory details
                bool isSaved = this.schoolCategoryRepository.SaveOrUpdateSchoolCategoryDetails(schoolCategory);

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
        /// Delete a record by SchoolCategoryId
        /// </summary>
        /// <param name="schoolCategoryId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid schoolCategoryId)
        {
            return this.schoolCategoryRepository.DeleteById(schoolCategoryId);
        }

        /// <summary>
        /// Check duplicate SchoolCategory by Name
        /// </summary>
        /// <param name="schoolCategoryModel"></param>
        /// <returns></returns>
        public bool CheckSchoolCategoryExistByName(SchoolCategoryModel schoolCategoryModel)
        {
            return this.schoolCategoryRepository.CheckSchoolCategoryExistByName(schoolCategoryModel);
        }

        /// <summary>}
        /// List of SchoolCategory with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<SchoolCategoryViewModel> GetSchoolCategoriesByCriteria(SearchSchoolCategoryModel searchModel)
        {
            return this.schoolCategoryRepository.GetSchoolCategoriesByCriteria(searchModel);
        }
    }
}