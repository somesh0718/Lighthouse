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
    /// Manager of the School entity
    /// </summary>
    public class SchoolManager : GenericManager<SchoolModel>, ISchoolManager
    {
        private readonly ISchoolRepository schoolRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the school manager.
        /// </summary>
        /// <param name="schoolRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public SchoolManager(ISchoolRepository _schoolRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.schoolRepository = _schoolRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of Schools
        /// </summary>
        /// <returns></returns>
        public IQueryable<SchoolModel> GetSchools()
        {
            var schools = this.schoolRepository.GetSchools();

            IList<SchoolModel> schoolModels = new List<SchoolModel>();
            schools.ForEach((user) => schoolModels.Add(user.ToModel()));

            return schoolModels.AsQueryable();
        }

        /// <summary>
        /// Get list of Schools by name
        /// </summary>
        /// <param name="schoolName"></param>
        /// <returns></returns>
        public IQueryable<SchoolModel> GetSchoolsByName(string schoolName)
        {
            var schools = this.schoolRepository.GetSchoolsByName(schoolName);

            IList<SchoolModel> schoolModels = new List<SchoolModel>();
            schools.ForEach((user) => schoolModels.Add(user.ToModel()));

            return schoolModels.AsQueryable();
        }

        /// <summary>
        /// Get list of School by schoolNames
        /// </summary>
        /// <param name="schoolNames"></param>
        /// <returns></returns>
        public IQueryable<SchoolModel> GetSchoolsByNames(List<string> schoolNames)
        {
            var schools = this.schoolRepository.GetSchoolsByNames(schoolNames);

            IList<SchoolModel> schoolModels = new List<SchoolModel>();
            schools.ForEach((user) => schoolModels.Add(user.ToModel()));

            return schoolModels.AsQueryable();
        }

        /// <summary>
        /// Get list of School by UDISE Codes
        /// </summary>
        /// <param name="udiseCodes"></param>
        /// <returns></returns>
        public IQueryable<SchoolModel> GetSchoolsByUDISECodes(List<string> udiseCodes)
        {
            var schools = this.schoolRepository.GetSchoolsByUDISECodes(udiseCodes);

            IList<SchoolModel> schoolModels = new List<SchoolModel>();
            schools.ForEach((user) => schoolModels.Add(user.ToModel()));

            return schoolModels.AsQueryable();
        }

        /// <summary>
        /// Get School by SchoolId
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public SchoolModel GetSchoolById(Guid schoolId)
        {
            School school = this.schoolRepository.GetSchoolById(schoolId);

            return (school != null) ? school.ToModel() : null;
        }

        /// <summary>
        /// Get School by SchoolId using async
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public async Task<SchoolModel> GetSchoolByIdAsync(Guid schoolId)
        {
            var school = await this.schoolRepository.GetSchoolByIdAsync(schoolId);

            return (school != null) ? school.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update School entity
        /// </summary>
        /// <param name="schoolModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateSchoolDetails(SchoolModel schoolModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                School school = null;

                //Validate model data
                schoolModel = schoolModel.GetModelValidationErrors<SchoolModel>();

                if (schoolModel.ErrorMessages.Count > 0)
                {
                    response.Errors = schoolModel.ErrorMessages;
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }

                if (schoolModel.RequestType == RequestType.Edit)
                {
                    school = this.schoolRepository.GetSchoolById(schoolModel.SchoolId);
                }
                else
                {
                    school = new School();
                    schoolModel.SchoolId = Guid.NewGuid();
                }

                if (schoolModel.ErrorMessages.Count == 0 && !(string.Equals(schoolModel.Udise.ToLower(), school.Udise.StringVal().ToLower())))
                {
                    bool isSchoolExists = this.schoolRepository.CheckSchoolExistByName(schoolModel);

                    if (isSchoolExists)
                    {
                        response.Errors.Add(Constants.ExistMessage);
                    }
                }

                if (schoolModel.RequestType == RequestType.Edit && school.IsActive && !schoolModel.IsActive)
                {
                    bool canInactiveSchool = this.schoolRepository.CheckUserCanInactiveSchoolById(school.SchoolId);

                    if (!canInactiveSchool)
                        response.Errors.Add("Current School cannot be inactive until all dependencies on that data have been removed or inactive");
                }

                if (response.Errors.Count == 0)
                {
                    school.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                    school = schoolModel.FromModel(school);

                    //Save Or Update school details
                    bool isSaved = this.schoolRepository.SaveOrUpdateSchoolDetails(school);

                    response.Result = isSaved ? "Success" : "Failed";
                }
                else
                {
                    response.Result = "Failed";
                    response.Success = false;
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("BAL > SaveOrUpdateSchoolDetails", ex);
            }

            return response;
        }

        /// <summary>
        /// Delete a record by SchoolId
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid schoolId)
        {
            return this.schoolRepository.DeleteById(schoolId);
        }

        /// <summary>
        /// Check duplicate School by Name
        /// </summary>
        /// <param name="schoolModel"></param>
        /// <returns></returns>
        public bool CheckSchoolExistByName(SchoolModel schoolModel)
        {
            return this.schoolRepository.CheckSchoolExistByName(schoolModel);
        }

        /// <summary>}
        /// List of School with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<SchoolViewModel> GetSchoolsByCriteria(SearchSchoolModel searchModel)
        {
            return this.schoolRepository.GetSchoolsByCriteria(searchModel);
        }
    }
}