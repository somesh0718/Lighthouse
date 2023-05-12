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
    /// Manager of the CourseModule entity
    /// </summary>
    public class CourseModuleManager : GenericManager<CourseModuleModel>, ICourseModuleManager
    {
        private readonly ICourseModuleRepository courseModuleRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the courseModule manager.
        /// </summary>
        /// <param name="courseModuleRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public CourseModuleManager(ICourseModuleRepository _courseModuleRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.courseModuleRepository = _courseModuleRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of CourseModules
        /// </summary>
        /// <returns></returns>
        public IQueryable<CourseModuleModel> GetCourseModules()
        {
            var courseModules = this.courseModuleRepository.GetCourseModules();

            IList<CourseModuleModel> courseModuleModels = new List<CourseModuleModel>();
            courseModules.ForEach((user) => courseModuleModels.Add(user.ToModel()));

            return courseModuleModels.AsQueryable();
        }

        /// <summary>
        /// Get list of CourseModules by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<CourseModuleModel> GetCourseModulesByName(string name)
        {
            var courseModules = this.courseModuleRepository.GetCourseModulesByName(name);

            IList<CourseModuleModel> courseModuleModels = new List<CourseModuleModel>();
            courseModules.ForEach((user) => courseModuleModels.Add(user.ToModel()));

            return courseModuleModels.AsQueryable();
        }

        /// <summary>
        /// Get CourseModule by CourseModuleId
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        public CourseModuleModel GetCourseModuleById(Guid courseModuleId)
        {
            CourseModuleModel courseModuleModel = null;

            CourseModule courseModule = this.courseModuleRepository.GetCourseModuleById(courseModuleId);

            if (courseModule != null)
            {
                courseModuleModel = courseModule.ToModel();
                courseModuleModel.Sessions = this.courseModuleRepository.GetUnitSessionsById(courseModuleId);
            }

            return courseModuleModel;
        }

        /// <summary>
        /// Get CourseModule by CourseModuleId using async
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        public async Task<CourseModuleModel> GetCourseModuleByIdAsync(Guid courseModuleId)
        {
            var courseModule = await this.courseModuleRepository.GetCourseModuleByIdAsync(courseModuleId);

            return (courseModule != null) ? courseModule.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update CourseModule entity
        /// </summary>
        /// <param name="courseModuleModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateCourseModuleDetails(CourseModuleModel courseModuleModel, bool isBulkUpload = false)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            CourseModule courseModule = null;

            //Validate model data
            courseModuleModel = courseModuleModel.GetModelValidationErrors<CourseModuleModel>();

            if (courseModuleModel.ErrorMessages.Count > 0)
            {
                response.Errors = courseModuleModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (courseModuleModel.RequestType == RequestType.Edit)
            {
                courseModule = this.courseModuleRepository.GetCourseModuleById(courseModuleModel.CourseModuleId);
            }
            else
            {
                if (isBulkUpload)
                {
                    CourseModule courseModuleItem = this.courseModuleRepository.CheckCourseModuleExistByName(courseModuleModel);
                    if (courseModuleItem != null)
                    {
                        if (courseModuleItem.CourseUnitSessions.Count == courseModuleModel.Sessions.Count)
                        {
                            response.Errors.Add("Course module unit is already exist");
                            response.Result = "Failed";
                            response.Success = false;
                            return response;
                        }
                        else
                        {
                            courseModule = courseModuleItem;
                            courseModule.RequestType = RequestType.Edit;
                        }
                    }
                    else
                    {
                        courseModule = new CourseModule();
                        courseModuleModel.CourseModuleId = Guid.NewGuid();
                    }
                }
                else
                {
                    CourseModule courseModuleItem = this.courseModuleRepository.CheckCourseModuleExistByName(courseModuleModel);
                    if (courseModuleItem != null)
                    {
                        response.Errors.Add(Constants.ExistMessage);
                    }
                    else
                    {
                        courseModule = new CourseModule();
                    }
                }
            }

            if (response.Errors.Count == 0)
            {
                courseModule.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                courseModule = courseModuleModel.FromModel(courseModule);

                //Save Or Update courseModule details
                bool isSaved = this.courseModuleRepository.SaveOrUpdateCourseModuleDetails(courseModule, courseModuleModel.Sessions);

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
        /// Delete a record by CourseModuleId
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid courseModuleId)
        {
            return this.courseModuleRepository.DeleteById(courseModuleId);
        }

        /// <summary>
        /// Check duplicate CourseModule by Name
        /// </summary>
        /// <param name="courseModuleModel"></param>
        /// <returns></returns>
        public bool CheckCourseModuleExistByName(CourseModuleModel courseModuleModel)
        {
            CourseModule courseModule = this.courseModuleRepository.CheckCourseModuleExistByName(courseModuleModel);

            return (courseModule != null);
        }

        /// <summary>}
        /// List of CourseModule with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<CourseModuleViewModel> GetCourseModulesByCriteria(SearchCourseModuleModel searchModel)
        {
            return this.courseModuleRepository.GetCourseModulesByCriteria(searchModel);
        }
    }
}