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
    /// Expose all schoolCategory WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class SchoolCategoryController : BaseController
    {
        private readonly ISchoolCategoryManager schoolCategoryManager;

        /// <summary>
        /// Initializes the SchoolCategory controller class.
        /// </summary>
        /// <param name="_schoolCategoryManager"></param>
        public SchoolCategoryController(ISchoolCategoryManager _schoolCategoryManager)
        {
            this.schoolCategoryManager = _schoolCategoryManager;
        }

        /// <summary>
        /// Get list of schoolCategory data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetSchoolCategories")]
        public async Task<ListResponse<SchoolCategoryModel>> GetSchoolCategories()
        {
            ListResponse<SchoolCategoryModel> response = new ListResponse<SchoolCategoryModel>();

            try
            {
                IQueryable<SchoolCategoryModel> schoolCategoryModels = await Task.Run(() =>
                {
                    return this.schoolCategoryManager.GetSchoolCategories();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = schoolCategoryModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of SchoolCategory with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSchoolCategoriesByCriteria")]
        public async Task<ListResponse<SchoolCategoryViewModel>> GetSchoolCategoriesByCriteria([FromBody] SearchSchoolCategoryModel searchModel)
        {
            ListResponse<SchoolCategoryViewModel> response = new ListResponse<SchoolCategoryViewModel>();

            try
            {
                var schoolCategoryModels = await Task.Run(() =>
                {
                    return this.schoolCategoryManager.GetSchoolCategoriesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = schoolCategoryModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of schoolCategory data by name
        /// </summary>
        /// <param name="schoolCategoryName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetSchoolCategoriesByName")]
        public async Task<ListResponse<SchoolCategoryModel>> GetSchoolCategoriesByName([FromQuery] string schoolCategoryName)
        {
            ListResponse<SchoolCategoryModel> response = new ListResponse<SchoolCategoryModel>();

            try
            {
                var schoolCategoryModels = await Task.Run(() =>
                {
                    return this.schoolCategoryManager.GetSchoolCategoriesByName(schoolCategoryName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = schoolCategoryModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get schoolCategory data by Id
        /// </summary>
        /// <param name="schoolCategoryId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSchoolCategoryById")]
        public async Task<SingularResponse<SchoolCategoryModel>> GetSchoolCategoryById([FromBody] DataRequest schoolCategoryRequest)
        {
            SingularResponse<SchoolCategoryModel> response = new SingularResponse<SchoolCategoryModel>();

            try
            {
                var schoolCategoryModel = await Task.Run(() =>
                {
                    return this.schoolCategoryManager.GetSchoolCategoryById(Guid.Parse(schoolCategoryRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = schoolCategoryModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new schoolCategory
        /// </summary>
        /// <param name="schoolCategoryRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateSchoolCategory"), Route("CreateOrUpdateSchoolCategoryDetails")]
        public async Task<SingularResponse<string>> CreateSchoolCategory([FromBody] SchoolCategoryRequest schoolCategoryRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //schoolCategoryRequest.RequestType = RequestType.New;
                    return this.schoolCategoryManager.SaveOrUpdateSchoolCategoryDetails(schoolCategoryRequest);
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
        /// Update schoolCategory by Id
        /// </summary>
        /// <param name="schoolCategoryRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateSchoolCategory")]
        public async Task<SingularResponse<string>> UpdateSchoolCategory([FromBody] SchoolCategoryRequest schoolCategoryRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    schoolCategoryRequest.RequestType = RequestType.Edit;
                    return this.schoolCategoryManager.SaveOrUpdateSchoolCategoryDetails(schoolCategoryRequest);
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
        /// Delete schoolCategory by Id
        /// </summary>
        /// <param name="schoolCategoryRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteSchoolCategoryById")]
        public async Task<SingularResponse<bool>> DeleteSchoolCategoryById([FromBody] DeleteRequest<Guid> schoolCategoryRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.schoolCategoryManager.DeleteById(schoolCategoryRequest.DataId);
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