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
    /// Expose all school WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class SchoolController : BaseController
    {
        private readonly ISchoolManager schoolManager;

        /// <summary>
        /// Initializes the School controller class.
        /// </summary>
        /// <param name="_schoolManager"></param>
        public SchoolController(ISchoolManager _schoolManager)
        {
            this.schoolManager = _schoolManager;
        }

        /// <summary>
        /// Get list of school data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetSchools")]
        public async Task<ListResponse<SchoolModel>> GetSchools()
        {
            ListResponse<SchoolModel> response = new ListResponse<SchoolModel>();

            try
            {
                IQueryable<SchoolModel> schoolModels = await Task.Run(() =>
                {
                    return this.schoolManager.GetSchools();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = schoolModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of School with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSchoolsByCriteria")]
        public async Task<ListResponse<SchoolViewModel>> GetSchoolsByCriteria([FromBody] SearchSchoolModel searchModel)
        {
            ListResponse<SchoolViewModel> response = new ListResponse<SchoolViewModel>();

            try
            {
                var schoolModels = await Task.Run(() =>
                {
                    return this.schoolManager.GetSchoolsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = schoolModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of school data by name
        /// </summary>
        /// <param name="schoolName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetSchoolsByName")]
        public async Task<ListResponse<SchoolModel>> GetSchoolsByName([FromQuery] string schoolName)
        {
            ListResponse<SchoolModel> response = new ListResponse<SchoolModel>();

            try
            {
                var schoolModels = await Task.Run(() =>
                {
                    return this.schoolManager.GetSchoolsByName(schoolName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = schoolModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get school data by Id
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSchoolById")]
        public async Task<SingularResponse<SchoolModel>> GetSchoolById([FromBody] DataRequest schoolRequest)
        {
            SingularResponse<SchoolModel> response = new SingularResponse<SchoolModel>();

            try
            {
                var schoolModel = await Task.Run(() =>
                {
                    return this.schoolManager.GetSchoolById(Guid.Parse(schoolRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = schoolModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new school
        /// </summary>
        /// <param name="schoolRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateSchool"), Route("CreateOrUpdateSchoolDetails")]
        public async Task<SingularResponse<string>> CreateSchool([FromBody] SchoolRequest schoolRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //schoolRequest.RequestType = RequestType.New;
                    return this.schoolManager.SaveOrUpdateSchoolDetails(schoolRequest);
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
        /// Update school by Id
        /// </summary>
        /// <param name="schoolRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateSchool")]
        public async Task<SingularResponse<string>> UpdateSchool([FromBody] SchoolRequest schoolRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    schoolRequest.RequestType = RequestType.Edit;
                    return this.schoolManager.SaveOrUpdateSchoolDetails(schoolRequest);
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
        /// Delete school by Id
        /// </summary>
        /// <param name="schoolRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteSchoolById")]
        public async Task<SingularResponse<bool>> DeleteSchoolById([FromBody] DeleteRequest<Guid> schoolRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.schoolManager.DeleteById(schoolRequest.DataId);
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