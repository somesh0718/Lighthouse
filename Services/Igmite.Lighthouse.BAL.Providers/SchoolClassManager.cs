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
    /// Manager of the SchoolClass entity
    /// </summary>
    public class SchoolClassManager : GenericManager<SchoolClassModel>, ISchoolClassManager
    {
        private readonly ISchoolClassRepository schoolClassRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the schoolClass manager.
        /// </summary>
        /// <param name="schoolClassRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public SchoolClassManager(ISchoolClassRepository _schoolClassRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.schoolClassRepository = _schoolClassRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of SchoolClasses
        /// </summary>
        /// <returns></returns>
        public IQueryable<SchoolClassModel> GetSchoolClasses()
        {
            var schoolClasses = this.schoolClassRepository.GetSchoolClasses();

            IList<SchoolClassModel> schoolClassModels = new List<SchoolClassModel>();
            schoolClasses.ForEach((user) => schoolClassModels.Add(user.ToModel()));

            return schoolClassModels.AsQueryable();
        }

        /// <summary>
        /// Get list of SchoolClasses by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<SchoolClassModel> GetSchoolClassesByName(string schoolClassName)
        {
            var schoolClasses = this.schoolClassRepository.GetSchoolClassesByName(schoolClassName);

            IList<SchoolClassModel> schoolClassModels = new List<SchoolClassModel>();
            schoolClasses.ForEach((user) => schoolClassModels.Add(user.ToModel()));

            return schoolClassModels.AsQueryable();
        }

        /// <summary>
        /// Get SchoolClass by ClassId
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public SchoolClassModel GetSchoolClassById(Guid classId)
        {
            SchoolClass schoolClass = this.schoolClassRepository.GetSchoolClassById(classId);

            return (schoolClass != null) ? schoolClass.ToModel() : null;
        }

        /// <summary>
        /// Get SchoolClass by ClassId using async
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public async Task<SchoolClassModel> GetSchoolClassByIdAsync(Guid classId)
        {
            var schoolClass = await this.schoolClassRepository.GetSchoolClassByIdAsync(classId);

            return (schoolClass != null) ? schoolClass.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update SchoolClass entity
        /// </summary>
        /// <param name="schoolClassModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateSchoolClassDetails(SchoolClassModel schoolClassModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            SchoolClass schoolClass = null;

            //Validate model data
            schoolClassModel = schoolClassModel.GetModelValidationErrors<SchoolClassModel>();

            if (schoolClassModel.ErrorMessages.Count > 0)
            {
                response.Errors = schoolClassModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (schoolClassModel.RequestType == RequestType.Edit)
            {
                schoolClass = this.schoolClassRepository.GetSchoolClassById(schoolClassModel.ClassId);
            }
            else
            {
                schoolClass = new SchoolClass();
                schoolClassModel.ClassId = Guid.NewGuid();
            }

            if (schoolClassModel.ErrorMessages.Count == 0 && (schoolClassModel.Name.StringVal().ToLower() != schoolClass.Name.StringVal().ToLower()))
            {
                bool isSchoolClassExists = this.schoolClassRepository.CheckSchoolClassExistByName(schoolClassModel);

                if (isSchoolClassExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                schoolClass.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                schoolClass = schoolClassModel.FromModel(schoolClass);

                //Save Or Update schoolClass details
                bool isSaved = this.schoolClassRepository.SaveOrUpdateSchoolClassDetails(schoolClass);

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
        /// Delete a record by ClassId
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid classId)
        {
            return this.schoolClassRepository.DeleteById(classId);
        }

        /// <summary>
        /// Check duplicate SchoolClass by Name
        /// </summary>
        /// <param name="schoolClassModel"></param>
        /// <returns></returns>
        public bool CheckSchoolClassExistByName(SchoolClassModel schoolClassModel)
        {
            return this.schoolClassRepository.CheckSchoolClassExistByName(schoolClassModel);
        }

        /// <summary>}
        /// List of SchoolClass with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<SchoolClassViewModel> GetSchoolClassesByCriteria(SearchSchoolClassModel searchModel)
        {
            return this.schoolClassRepository.GetSchoolClassesByCriteria(searchModel);
        }
    }
}