using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.Controllers
{
    /// <summary>
    /// Expose all studentsForExitForm WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class StudentsForExitFormController : BaseController
    {
        private readonly IStudentsForExitFormManager studentsForExitFormManager;

        /// <summary>
        /// Initializes the StudentsForExitForm controller class.
        /// </summary>
        /// <param name="_studentsForExitFormManager"></param>
        public StudentsForExitFormController(IStudentsForExitFormManager _studentsForExitFormManager)
        {
            this.studentsForExitFormManager = _studentsForExitFormManager;
        }

        /// <summary>
        /// Get list of studentsForExitForm data
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetStudentsForExitForm")]
        public async Task<ListResponse<StudentsForExitSurveyViewModel>> GetStudentsForExitForm([FromBody] ExitSurveyRequestModel exitSurveyRequestModel)
        {
            ListResponse<StudentsForExitSurveyViewModel> response = new ListResponse<StudentsForExitSurveyViewModel>();

            try
            {
                IList<StudentsForExitSurveyViewModel> studentsForExitFormModels = await Task.Run(() =>
                {
                    return this.studentsForExitFormManager.GetStudentsForExitForm(exitSurveyRequestModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = studentsForExitFormModels;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpPost, Route("GetExitSurveyReport")]
        public async Task<ListResponse<ExitSurveyReportModel>> GetExitSurveyReport([FromBody] ExitSurveyRequestModel exitSurveyRequestModel)
        {
            ListResponse<ExitSurveyReportModel> response = new ListResponse<ExitSurveyReportModel>();

            try
            {
                IQueryable<ExitSurveyReportModel> exitSurveyDetailModels = await Task.Run(() =>
                {
                    return this.studentsForExitFormManager.GetExitSurveyReport(exitSurveyRequestModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = exitSurveyDetailModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of studentsForExitForm data by name
        /// </summary>
        /// <param name="studentsForExitFormName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetStudentsForExitFormByName")]
        public async Task<ListResponse<StudentsForExitFormModel>> GetStudentsForExitFormByName([FromQuery] string studentsName)
        {
            ListResponse<StudentsForExitFormModel> response = new ListResponse<StudentsForExitFormModel>();

            try
            {
                var studentsForExitFormModels = await Task.Run(() =>
                {
                    return this.studentsForExitFormManager.GetStudentsForExitFormByName(studentsName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = studentsForExitFormModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get studentsForExitForm data by Id
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetStudentsForExitFormById")]
        public async Task<SingularResponse<StudentsForExitFormModel>> GetStudentsForExitFormById([FromBody] StudentsForExitFormModel studentRequest)
        {
            SingularResponse<StudentsForExitFormModel> response = new SingularResponse<StudentsForExitFormModel>();

            try
            {
                var studentsForExitFormModel = await Task.Run(() =>
                {
                    return this.studentsForExitFormManager.GetStudentsForExitFormById(studentRequest.ExitStudentId);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = studentsForExitFormModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new studentsForExitForm
        /// </summary>
        /// <param name="studentsForExitFormRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateStudentsForExitForm")]
        public async Task<SingularResponse<string>> CreateStudentsForExitForm([FromBody] StudentsForExitFormModel studentsForExitFormRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //studentsForExitFormRequest.RequestType = RequestType.New;
                    return this.studentsForExitFormManager.SaveOrUpdateStudentsForExitFormDetails(studentsForExitFormRequest);
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
        /// Update studentsForExitForm by Id
        /// </summary>
        /// <param name="studentsForExitFormRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateStudentsForExitForm")]
        public async Task<SingularResponse<string>> UpdateStudentsForExitForm([FromBody] StudentsForExitFormModel studentsForExitFormRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    studentsForExitFormRequest.RequestType = RequestType.Edit;
                    return this.studentsForExitFormManager.SaveOrUpdateStudentsForExitFormDetails(studentsForExitFormRequest);
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
        /// Delete studentsForExitForm by Id
        /// </summary>
        /// <param name="studentsForExitFormRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteStudentsForExitFormById")]
        public async Task<SingularResponse<bool>> DeleteStudentsForExitFormById([FromBody] StudentsForExitFormModel studentsForExitFormRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.studentsForExitFormManager.DeleteById(studentsForExitFormRequest.ExitStudentId);
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