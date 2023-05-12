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
    /// Expose all schoolClass WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class SchoolClassController : BaseController
    {
        private readonly ISchoolClassManager schoolClassManager;

        /// <summary>
        /// Initializes the SchoolClass controller class.
        /// </summary>
        /// <param name="_schoolClassManager"></param>
        public SchoolClassController(ISchoolClassManager _schoolClassManager)
        {
            this.schoolClassManager = _schoolClassManager;
        }

        /// <summary>
        /// Get list of schoolClass data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetSchoolClasses")]
        public async Task<ListResponse<SchoolClassModel>> GetSchoolClasses()
        {
            ListResponse<SchoolClassModel> response = new ListResponse<SchoolClassModel>();

            try
            {
                IQueryable<SchoolClassModel> schoolClassModels = await Task.Run(() =>
                {
                    return this.schoolClassManager.GetSchoolClasses();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = schoolClassModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of SchoolClass with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSchoolClassesByCriteria")]
        public async Task<ListResponse<SchoolClassViewModel>> GetSchoolClassesByCriteria([FromBody] SearchSchoolClassModel searchModel)
        {
            ListResponse<SchoolClassViewModel> response = new ListResponse<SchoolClassViewModel>();

            try
            {
                var schoolClassModels = await Task.Run(() =>
                {
                    return this.schoolClassManager.GetSchoolClassesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = schoolClassModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of schoolClass data by name
        /// </summary>
        /// <param name="schoolClassName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetSchoolClassesByName")]
        public async Task<ListResponse<SchoolClassModel>> GetSchoolClassesByName([FromQuery] string schoolClassName)
        {
            ListResponse<SchoolClassModel> response = new ListResponse<SchoolClassModel>();

            try
            {
                var schoolClassModels = await Task.Run(() =>
                {
                    return this.schoolClassManager.GetSchoolClassesByName(schoolClassName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = schoolClassModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get schoolClass data by Id
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSchoolClassById")]
        public async Task<SingularResponse<SchoolClassModel>> GetSchoolClassById([FromBody] DataRequest classRequest)
        {
            SingularResponse<SchoolClassModel> response = new SingularResponse<SchoolClassModel>();

            try
            {
                var schoolClassModel = await Task.Run(() =>
                {
                    return this.schoolClassManager.GetSchoolClassById(Guid.Parse(classRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = schoolClassModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new schoolClass
        /// </summary>
        /// <param name="schoolClassRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateSchoolClass"), Route("CreateOrUpdateSchoolClassDetails")]
        public async Task<SingularResponse<string>> CreateSchoolClass([FromBody] SchoolClassRequest schoolClassRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //schoolClassRequest.RequestType = RequestType.New;
                    return this.schoolClassManager.SaveOrUpdateSchoolClassDetails(schoolClassRequest);
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
        /// Update schoolClass by Id
        /// </summary>
        /// <param name="schoolClassRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateSchoolClass")]
        public async Task<SingularResponse<string>> UpdateSchoolClass([FromBody] SchoolClassRequest schoolClassRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    schoolClassRequest.RequestType = RequestType.Edit;
                    return this.schoolClassManager.SaveOrUpdateSchoolClassDetails(schoolClassRequest);
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
        /// Delete schoolClass by Id
        /// </summary>
        /// <param name="schoolClassRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteSchoolClassById")]
        public async Task<SingularResponse<bool>> DeleteSchoolClassById([FromBody] DeleteRequest<Guid> schoolClassRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.schoolClassManager.DeleteById(schoolClassRequest.DataId);
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