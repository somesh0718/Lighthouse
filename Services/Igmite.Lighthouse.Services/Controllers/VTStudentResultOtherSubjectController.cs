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
    /// Expose all vtStudentResultOtherSubject WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VTStudentResultOtherSubjectController : BaseController
    {
        private readonly IVTStudentResultOtherSubjectManager vtStudentResultOtherSubjectManager;

        /// <summary>
        /// Initializes the VTStudentResultOtherSubject controller class.
        /// </summary>
        /// <param name="_vtStudentResultOtherSubjectManager"></param>
        public VTStudentResultOtherSubjectController(IVTStudentResultOtherSubjectManager _vtStudentResultOtherSubjectManager)
        {
            this.vtStudentResultOtherSubjectManager = _vtStudentResultOtherSubjectManager;
        }

        /// <summary>
        /// Get list of vtStudentResultOtherSubject data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVTStudentResultOtherSubjects")]
        public async Task<ListResponse<VTStudentResultOtherSubjectModel>> GetVTStudentResultOtherSubjects()
        {
            ListResponse<VTStudentResultOtherSubjectModel> response = new ListResponse<VTStudentResultOtherSubjectModel>();

            try
            {
                IQueryable<VTStudentResultOtherSubjectModel> vtStudentResultOtherSubjectModels = await Task.Run(() =>
                {
                    return this.vtStudentResultOtherSubjectManager.GetVTStudentResultOtherSubjects();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtStudentResultOtherSubjectModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VTStudentResultOtherSubject with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTStudentResultOtherSubjectsByCriteria")]
        public async Task<ListResponse<VTStudentResultOtherSubjectViewModel>> GetVTStudentResultOtherSubjectsByCriteria([FromBody] SearchVTStudentResultOtherSubjectModel searchModel)
        {
            ListResponse<VTStudentResultOtherSubjectViewModel> response = new ListResponse<VTStudentResultOtherSubjectViewModel>();

            try
            {
                var vtStudentResultOtherSubjectModels = await Task.Run(() =>
                {
                    return this.vtStudentResultOtherSubjectManager.GetVTStudentResultOtherSubjectsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtStudentResultOtherSubjectModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vtStudentResultOtherSubject data by name
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVTStudentResultOtherSubjectsByName")]
        public async Task<ListResponse<VTStudentResultOtherSubjectModel>> GetVTStudentResultOtherSubjectsByName([FromQuery] string vtStudentResultOtherSubjectName)
        {
            ListResponse<VTStudentResultOtherSubjectModel> response = new ListResponse<VTStudentResultOtherSubjectModel>();

            try
            {
                var vtStudentResultOtherSubjectModels = await Task.Run(() =>
                {
                    return this.vtStudentResultOtherSubjectManager.GetVTStudentResultOtherSubjectsByName(vtStudentResultOtherSubjectName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtStudentResultOtherSubjectModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vtStudentResultOtherSubject data by Id
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTStudentResultOtherSubjectById")]
        public async Task<SingularResponse<VTStudentResultOtherSubjectModel>> GetVTStudentResultOtherSubjectById([FromBody] DataRequest vtStudentResultOtherSubjectRequest)
        {
            SingularResponse<VTStudentResultOtherSubjectModel> response = new SingularResponse<VTStudentResultOtherSubjectModel>();

            try
            {
                var vtStudentResultOtherSubjectModel = await Task.Run(() =>
                {
                    return this.vtStudentResultOtherSubjectManager.GetVTStudentResultOtherSubjectById(Guid.Parse(vtStudentResultOtherSubjectRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vtStudentResultOtherSubjectModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vtStudentResultOtherSubject
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVTStudentResultOtherSubject"), Route("CreateOrUpdateVTStudentResultOtherSubjectDetails")]
        public async Task<SingularResponse<string>> CreateVTStudentResultOtherSubject([FromBody] VTStudentResultOtherSubjectRequest vtStudentResultOtherSubjectRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vtStudentResultOtherSubjectRequest.RequestType = RequestType.New;
                    return this.vtStudentResultOtherSubjectManager.SaveOrUpdateVTStudentResultOtherSubjectDetails(vtStudentResultOtherSubjectRequest);
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
        /// Update vtStudentResultOtherSubject by Id
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVTStudentResultOtherSubject")]
        public async Task<SingularResponse<string>> UpdateVTStudentResultOtherSubject([FromBody] VTStudentResultOtherSubjectRequest vtStudentResultOtherSubjectRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vtStudentResultOtherSubjectRequest.RequestType = RequestType.Edit;
                    return this.vtStudentResultOtherSubjectManager.SaveOrUpdateVTStudentResultOtherSubjectDetails(vtStudentResultOtherSubjectRequest);
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
        /// Delete vtStudentResultOtherSubject by Id
        /// </summary>
        /// <param name="vtStudentResultOtherSubjectRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVTStudentResultOtherSubjectById")]
        public async Task<SingularResponse<bool>> DeleteVTStudentResultOtherSubjectById([FromBody] DeleteRequest<Guid> vtStudentResultOtherSubjectRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vtStudentResultOtherSubjectManager.DeleteById(vtStudentResultOtherSubjectRequest.DataId);
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