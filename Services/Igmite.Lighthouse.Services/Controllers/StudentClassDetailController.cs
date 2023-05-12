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
    /// Expose all studentClassDetail WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class StudentClassDetailController : BaseController
    {
        private readonly IStudentClassDetailManager studentClassDetailManager;

        /// <summary>
        /// Initializes the StudentClassDetail controller class.
        /// </summary>
        /// <param name="_studentClassDetailManager"></param>
        public StudentClassDetailController(IStudentClassDetailManager _studentClassDetailManager)
        {
            this.studentClassDetailManager = _studentClassDetailManager;
        }

        /// <summary>
        /// Get list of studentClassDetail data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetStudentClassDetails")]
        public async Task<ListResponse<StudentClassDetailModel>> GetStudentClassDetails()
        {
            ListResponse<StudentClassDetailModel> response = new ListResponse<StudentClassDetailModel>();

            try
            {
                IQueryable<StudentClassDetailModel> studentClassDetailModels = await Task.Run(() =>
                {
                    return this.studentClassDetailManager.GetStudentClassDetails();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = studentClassDetailModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of StudentClassDetail with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetStudentClassDetailsByCriteria")]
        public async Task<ListResponse<StudentClassDetailViewModel>> GetStudentClassDetailsByCriteria([FromBody] SearchStudentClassDetailModel searchModel)
        {
            ListResponse<StudentClassDetailViewModel> response = new ListResponse<StudentClassDetailViewModel>();

            try
            {
                var studentClassDetailModels = await Task.Run(() =>
                {
                    return this.studentClassDetailManager.GetStudentClassDetailsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = studentClassDetailModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of studentClassDetail data by name
        /// </summary>
        /// <param name="studentClassDetailName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetStudentClassDetailsByName")]
        public async Task<ListResponse<StudentClassDetailModel>> GetStudentClassDetailsByName([FromQuery] string studentClassDetailName)
        {
            ListResponse<StudentClassDetailModel> response = new ListResponse<StudentClassDetailModel>();

            try
            {
                var studentClassDetailModels = await Task.Run(() =>
                {
                    return this.studentClassDetailManager.GetStudentClassDetailsByName(studentClassDetailName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = studentClassDetailModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get studentClassDetail data by Id
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetStudentClassDetailById")]
        public async Task<SingularResponse<StudentClassDetailModel>> GetStudentClassDetailById([FromBody] DataRequest studentRequest)
        {
            SingularResponse<StudentClassDetailModel> response = new SingularResponse<StudentClassDetailModel>();

            try
            {
                var studentClassDetailModel = await Task.Run(() =>
                {
                    return this.studentClassDetailManager.GetStudentClassDetailById(Guid.Parse(studentRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = studentClassDetailModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new studentClassDetail
        /// </summary>
        /// <param name="studentClassDetailRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateStudentClassDetail"), Route("CreateOrUpdateStudentClassDetailDetails")]
        public async Task<SingularResponse<string>> CreateStudentClassDetail([FromBody] StudentClassDetailRequest studentClassDetailRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //studentClassDetailRequest.RequestType = RequestType.New;
                    return this.studentClassDetailManager.SaveOrUpdateStudentClassDetailDetails(studentClassDetailRequest);
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
        /// Update studentClassDetail by Id
        /// </summary>
        /// <param name="studentClassDetailRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateStudentClassDetail")]
        public async Task<SingularResponse<string>> UpdateStudentClassDetail([FromBody] StudentClassDetailRequest studentClassDetailRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    studentClassDetailRequest.RequestType = RequestType.Edit;
                    return this.studentClassDetailManager.SaveOrUpdateStudentClassDetailDetails(studentClassDetailRequest);
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
        /// Delete studentClassDetail by Id
        /// </summary>
        /// <param name="studentClassDetailRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteStudentClassDetailById")]
        public async Task<SingularResponse<bool>> DeleteStudentClassDetailById([FromBody] DeleteRequest<Guid> studentClassDetailRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.studentClassDetailManager.DeleteById(studentClassDetailRequest.DataId);
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

        /// <summary>
        /// List of VocationalEducationAssessment with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVocationalEducationAssessmentBySchoolAndVTId")]
        public async Task<SingularResponse<VocationalEducationAssessmentModel>> GetVocationalEducationAssessmentBySchoolAndVTId([FromBody] SearchStudentClassDetailModel searchModel)
        {

            SingularResponse<VocationalEducationAssessmentModel> response = new SingularResponse<VocationalEducationAssessmentModel>();

            try
            {
                var studentClassDetailModel = await Task.Run(() =>
                {
                    return this.studentClassDetailManager.GetVocationalEducationAssessmentBySchoolAndVTId(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = studentClassDetailModel;
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