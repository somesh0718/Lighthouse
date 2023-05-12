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
    /// Expose all vocationalTrainer WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VocationalTrainerController : BaseController
    {
        private readonly IVocationalTrainerManager vocationalTrainerManager;

        /// <summary>
        /// Initializes the VocationalTrainer controller class.
        /// </summary>
        /// <param name="_vocationalTrainerManager"></param>
        public VocationalTrainerController(IVocationalTrainerManager _vocationalTrainerManager)
        {
            this.vocationalTrainerManager = _vocationalTrainerManager;
        }

        /// <summary>
        /// Get list of vocationalTrainer data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVocationalTrainers")]
        public async Task<ListResponse<VocationalTrainerModel>> GetVocationalTrainers()
        {
            ListResponse<VocationalTrainerModel> response = new ListResponse<VocationalTrainerModel>();

            try
            {
                IQueryable<VocationalTrainerModel> vocationalTrainerModels = await Task.Run(() =>
                {
                    return this.vocationalTrainerManager.GetVocationalTrainers();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vocationalTrainerModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VocationalTrainer with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVocationalTrainersByCriteria")]
        public async Task<ListResponse<VocationalTrainerViewModel>> GetVocationalTrainersByCriteria([FromBody] SearchVocationalTrainerModel searchModel)
        {
            ListResponse<VocationalTrainerViewModel> response = new ListResponse<VocationalTrainerViewModel>();

            try
            {
                var vocationalTrainerModels = await Task.Run(() =>
                {
                    return this.vocationalTrainerManager.GetVocationalTrainersByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vocationalTrainerModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vocationalTrainer data by name
        /// </summary>
        /// <param name="vocationalTrainerName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVocationalTrainersByName")]
        public async Task<ListResponse<VocationalTrainerModel>> GetVocationalTrainersByName([FromQuery] string vocationalTrainerName)
        {
            ListResponse<VocationalTrainerModel> response = new ListResponse<VocationalTrainerModel>();

            try
            {
                var vocationalTrainerModels = await Task.Run(() =>
                {
                    return this.vocationalTrainerManager.GetVocationalTrainersByName(vocationalTrainerName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vocationalTrainerModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vocationalTrainer data by Id
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="VTPId"></param>
        /// <param name="VCId"></param>
        /// <param name="VTId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVocationalTrainerById")]
        public async Task<SingularResponse<VocationalTrainerModel>> GetVocationalTrainerById([FromBody] DataRequest vtRequest)
        {
            SingularResponse<VocationalTrainerModel> response = new SingularResponse<VocationalTrainerModel>();

            try
            {
                var vocationalTrainerModel = await Task.Run(() =>
                {
                    return this.vocationalTrainerManager.GetVocationalTrainerById(vtRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vocationalTrainerModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vocationalTrainer
        /// </summary>
        /// <param name="vocationalTrainerRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVocationalTrainer"), Route("CreateOrUpdateVocationalTrainerDetails")]
        public async Task<SingularResponse<string>> CreateVocationalTrainer([FromBody] VocationalTrainerRequest vocationalTrainerRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vocationalTrainerRequest.RequestType = RequestType.New;
                    return this.vocationalTrainerManager.SaveOrUpdateVocationalTrainerDetails(vocationalTrainerRequest);
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
        /// Update vocationalTrainer by Id
        /// </summary>
        /// <param name="vocationalTrainerRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVocationalTrainer")]
        public async Task<SingularResponse<string>> UpdateVocationalTrainer([FromBody] VocationalTrainerRequest vocationalTrainerRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vocationalTrainerRequest.RequestType = RequestType.Edit;
                    return this.vocationalTrainerManager.SaveOrUpdateVocationalTrainerDetails(vocationalTrainerRequest);
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
        /// Delete vocationalTrainer by Id
        /// </summary>
        /// <param name="vocationalTrainerRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVocationalTrainerById")]
        public async Task<SingularResponse<bool>> DeleteVocationalTrainerById([FromBody] DeleteRequest<Guid> vocationalTrainerRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vocationalTrainerManager.DeleteById(vocationalTrainerRequest.DataId);
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
        /// Transfer a VT to another School
        /// </summary>
        /// <param name="VTTransferRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("VTTransfer")]
        public async Task<SingularResponse<string>> VTTransfer([FromBody] VTTransferRequest vtTransferRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    return this.vocationalTrainerManager.VTTransfer(vtTransferRequest);
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
        /// Transfer a VT to another School
        /// </summary>
        /// <param name="VTTransferRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSchoolStudentsByVTId")]
        public async Task<ListResponse<SchoolStudentModel>> GetSchoolStudentsByVTId([FromBody] DataRequest studentRequest)
        {
            ListResponse<SchoolStudentModel> response = new ListResponse<SchoolStudentModel>();
            try
            {
                var schoolStudents = await Task.Run(() =>
                 {
                     return this.vocationalTrainerManager.GetSchoolStudentsByVTId(Guid.Parse(studentRequest.DataId), Guid.Parse(studentRequest.DataId1));
                 });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = schoolStudents;
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