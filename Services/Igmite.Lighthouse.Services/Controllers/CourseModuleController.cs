using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.Controllers
{
    /// <summary>
    /// Expose all courseModule WebAPI services
    /// </summary>
    [Route(ServiceConstants.ServiceName), ApiController]
    public class CourseModuleController : BaseController
    {
        private readonly ICourseModuleManager courseModuleManager;

        /// <summary>
        /// Initializes the CourseModule controller class.
        /// </summary>
        /// <param name="_courseModuleManager"></param>
        public CourseModuleController(ICourseModuleManager _courseModuleManager)
        {
            this.courseModuleManager = _courseModuleManager;
        }

        /// <summary>
        /// Get list of courseModule data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route(ServiceConstants.Transaction.CourseModule.GetAll)]
        public async Task<ListResponse<CourseModuleModel>> GetCourseModules()
        {
            ListResponse<CourseModuleModel> response = new ListResponse<CourseModuleModel>();

            try
            {
                IQueryable<CourseModuleModel> courseModuleModels = await Task.Run(() =>
                {
                    return this.courseModuleManager.GetCourseModules();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = courseModuleModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of CourseModule with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Transaction.CourseModule.GetByCriteria)]
        public async Task<ListResponse<CourseModuleViewModel>> GetCourseModulesByCriteria([FromBody] SearchCourseModuleModel searchModel)
        {
            ListResponse<CourseModuleViewModel> response = new ListResponse<CourseModuleViewModel>();

            try
            {
                var courseModuleModels = await Task.Run(() =>
                {
                    return this.courseModuleManager.GetCourseModulesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = courseModuleModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of courseModule data by name
        /// </summary>
        /// <param name="courseModuleName"></param>
        /// <returns></returns>
        [HttpGet, Route(ServiceConstants.Transaction.CourseModule.GetByName)]
        public async Task<ListResponse<CourseModuleModel>> GetCourseModulesByName([FromQuery] string courseModuleName)
        {
            ListResponse<CourseModuleModel> response = new ListResponse<CourseModuleModel>();

            try
            {
                var courseModuleModels = await Task.Run(() =>
                {
                    return this.courseModuleManager.GetCourseModulesByName(courseModuleName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = courseModuleModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get courseModule data by Id
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Transaction.CourseModule.GetById)]
        public async Task<SingularResponse<CourseModuleModel>> GetCourseModuleById([FromBody] DataRequest courseModuleRequest)
        {
            SingularResponse<CourseModuleModel> response = new SingularResponse<CourseModuleModel>();

            try
            {
                var courseModuleModel = await Task.Run(() =>
                {
                    return this.courseModuleManager.GetCourseModuleById(Guid.Parse(courseModuleRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = courseModuleModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new courseModule
        /// </summary>
        /// <param name="courseModuleRequest"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Transaction.CourseModule.Create), Route(ServiceConstants.Transaction.CourseModule.CreateOrUpdate)]
        public async Task<SingularResponse<string>> CreateCourseModule([FromBody] CourseModuleRequest courseModuleRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //courseModuleRequest.RequestType = RequestType.New;
                    return this.courseModuleManager.SaveOrUpdateCourseModuleDetails(courseModuleRequest);
                });

                if (response.Errors.Count == 0)
                {
                    response.Messages.Add(Constants.CreatedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Update courseModule by Id
        /// </summary>
        /// <param name="courseModuleRequest"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Transaction.CourseModule.Update)]
        public async Task<SingularResponse<string>> UpdateCourseModule([FromBody] CourseModuleRequest courseModuleRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    courseModuleRequest.RequestType = RequestType.Edit;
                    return this.courseModuleManager.SaveOrUpdateCourseModuleDetails(courseModuleRequest);
                });

                if (response.Errors.Count == 0)
                {
                    response.Messages.Add(Constants.UpdatedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Delete courseModule by Id
        /// </summary>
        /// <param name="courseModuleRequest"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Transaction.CourseModule.DeleteById)]
        public async Task<SingularResponse<bool>> DeleteCourseModuleById([FromBody] DeleteRequest<Guid> courseModuleRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.courseModuleManager.DeleteById(courseModuleRequest.DataId);
                });

                if (response.Result)
                {
                    response.Messages.Add(Constants.DeletedMessage);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }
    }
}