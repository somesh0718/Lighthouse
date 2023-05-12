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
    /// Expose all courseMaterial WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class CourseMaterialController : BaseController
    {
        private readonly ICourseMaterialManager courseMaterialManager;

        /// <summary>
        /// Initializes the CourseMaterial controller class.
        /// </summary>
        /// <param name="_courseMaterialManager"></param>
        public CourseMaterialController(ICourseMaterialManager _courseMaterialManager)
        {
            this.courseMaterialManager = _courseMaterialManager;
        }

        /// <summary>
        /// Get list of courseMaterial data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetCourseMaterials")]
        public async Task<ListResponse<CourseMaterialModel>> GetCourseMaterials()
        {
            ListResponse<CourseMaterialModel> response = new ListResponse<CourseMaterialModel>();

            try
            {
                IQueryable<CourseMaterialModel> courseMaterialModels = await Task.Run(() =>
                {
                    return this.courseMaterialManager.GetCourseMaterials();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = courseMaterialModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of CourseMaterial with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetCourseMaterialsByCriteria")]
        public async Task<ListResponse<CourseMaterialViewModel>> GetCourseMaterialsByCriteria([FromBody] SearchCourseMaterialModel searchModel)
        {
            ListResponse<CourseMaterialViewModel> response = new ListResponse<CourseMaterialViewModel>();

            try
            {
                var courseMaterialModels = await Task.Run(() =>
                {
                    return this.courseMaterialManager.GetCourseMaterialsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = courseMaterialModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of courseMaterial data by name
        /// </summary>
        /// <param name="courseMaterialName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetCourseMaterialsByName")]
        public async Task<ListResponse<CourseMaterialModel>> GetCourseMaterialsByName([FromQuery] string courseMaterialName)
        {
            ListResponse<CourseMaterialModel> response = new ListResponse<CourseMaterialModel>();

            try
            {
                var courseMaterialModels = await Task.Run(() =>
                {
                    return this.courseMaterialManager.GetCourseMaterialsByName(courseMaterialName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = courseMaterialModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get courseMaterial data by Id
        /// </summary>
        /// <param name="courseMaterialId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetCourseMaterialById")]
        public async Task<SingularResponse<CourseMaterialModel>> GetCourseMaterialById([FromBody] DataRequest courseMaterialRequest)
        {
            SingularResponse<CourseMaterialModel> response = new SingularResponse<CourseMaterialModel>();

            try
            {
                var courseMaterialModel = await Task.Run(() =>
                {
                    return this.courseMaterialManager.GetCourseMaterialById(Guid.Parse(courseMaterialRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = courseMaterialModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new courseMaterial
        /// </summary>
        /// <param name="courseMaterialRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateCourseMaterial"), Route("CreateOrUpdateCourseMaterialDetails")]
        public async Task<SingularResponse<string>> CreateCourseMaterial([FromBody] CourseMaterialRequest courseMaterialRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //courseMaterialRequest.RequestType = RequestType.New;
                    return this.courseMaterialManager.SaveOrUpdateCourseMaterialDetails(courseMaterialRequest);
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
        /// Update courseMaterial by Id
        /// </summary>
        /// <param name="courseMaterialRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateCourseMaterial")]
        public async Task<SingularResponse<string>> UpdateCourseMaterial([FromBody] CourseMaterialRequest courseMaterialRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    courseMaterialRequest.RequestType = RequestType.Edit;
                    return this.courseMaterialManager.SaveOrUpdateCourseMaterialDetails(courseMaterialRequest);
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
        /// Delete courseMaterial by Id
        /// </summary>
        /// <param name="courseMaterialRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteCourseMaterialById")]
        public async Task<SingularResponse<bool>> DeleteCourseMaterialById([FromBody] DeleteRequest<Guid> courseMaterialRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.courseMaterialManager.DeleteById(courseMaterialRequest.DataId);
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