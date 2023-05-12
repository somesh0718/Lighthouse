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
    /// Expose all studentClass WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class StudentClassController : BaseController
    {
        private readonly IStudentClassManager studentClassManager;

        /// <summary>
        /// Initializes the StudentClass controller class.
        /// </summary>
        /// <param name="_studentClassManager"></param>
        public StudentClassController(IStudentClassManager _studentClassManager)
        {
            this.studentClassManager = _studentClassManager;
        }

        /// <summary>
        /// Get list of studentClass data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetStudentClasses")]
        public async Task<ListResponse<StudentClassModel>> GetStudentClasses()
        {
            ListResponse<StudentClassModel> response = new ListResponse<StudentClassModel>();

            try
            {
                IQueryable<StudentClassModel> studentClassModels = await Task.Run(() =>
                {
                    return this.studentClassManager.GetStudentClasses();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = studentClassModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of StudentClass with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetStudentClassesByCriteria")]
        public async Task<ListResponse<StudentClassViewModel>> GetStudentClassesByCriteria([FromBody] SearchStudentClassModel searchModel)
        {
            ListResponse<StudentClassViewModel> response = new ListResponse<StudentClassViewModel>();

            try
            {
                var studentClassModels = await Task.Run(() =>
                {
                    return this.studentClassManager.GetStudentClassesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = studentClassModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of studentClass data by name
        /// </summary>
        /// <param name="studentClassName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetStudentClassesByName")]
        public async Task<ListResponse<StudentClassModel>> GetStudentClassesByName([FromQuery] string studentClassName)
        {
            ListResponse<StudentClassModel> response = new ListResponse<StudentClassModel>();

            try
            {
                var studentClassModels = await Task.Run(() =>
                {
                    return this.studentClassManager.GetStudentClassesByName(studentClassName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = studentClassModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get studentClass data by Id
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetStudentClassById")]
        public async Task<SingularResponse<StudentClassModel>> GetStudentClassById([FromBody] DataRequest studentRequest)
        {
            SingularResponse<StudentClassModel> response = new SingularResponse<StudentClassModel>();

            try
            {
                var studentClassModel = await Task.Run(() =>
                {
                    return this.studentClassManager.GetStudentClassById(Guid.Parse(studentRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = studentClassModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new studentClass
        /// </summary>
        /// <param name="studentClassRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateStudentClass"), Route("CreateOrUpdateStudentClassDetails")]
        public async Task<SingularResponse<string>> CreateStudentClass([FromBody] StudentClassRequest studentClassRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //studentClassRequest.RequestType = RequestType.New;
                    return this.studentClassManager.SaveOrUpdateStudentClassDetails(studentClassRequest);
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
        /// Update studentClass by Id
        /// </summary>
        /// <param name="studentClassRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateStudentClass")]
        public async Task<SingularResponse<string>> UpdateStudentClass([FromBody] StudentClassRequest studentClassRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    studentClassRequest.RequestType = RequestType.Edit;
                    return this.studentClassManager.SaveOrUpdateStudentClassDetails(studentClassRequest);
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
        /// Delete studentClass by Id
        /// </summary>
        /// <param name="studentClassRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteStudentClassById")]
        public async Task<SingularResponse<bool>> DeleteStudentClassById([FromBody] DeleteRequest<Guid> studentClassRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.studentClassManager.DeleteById(studentClassRequest.DataId);
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