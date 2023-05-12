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
    /// Expose all vocationalCoordinator WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VocationalCoordinatorController : BaseController
    {
        private readonly IVocationalCoordinatorManager vocationalCoordinatorManager;

        /// <summary>
        /// Initializes the VocationalCoordinator controller class.
        /// </summary>
        /// <param name="_vocationalCoordinatorManager"></param>
        public VocationalCoordinatorController(IVocationalCoordinatorManager _vocationalCoordinatorManager)
        {
            this.vocationalCoordinatorManager = _vocationalCoordinatorManager;
        }

        /// <summary>
        /// Get list of vocationalCoordinator data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVocationalCoordinators")]
        public async Task<ListResponse<VocationalCoordinatorModel>> GetVocationalCoordinators()
        {
            ListResponse<VocationalCoordinatorModel> response = new ListResponse<VocationalCoordinatorModel>();

            try
            {
                IQueryable<VocationalCoordinatorModel> vocationalCoordinatorModels = await Task.Run(() =>
                {
                    return this.vocationalCoordinatorManager.GetVocationalCoordinators();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vocationalCoordinatorModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VocationalCoordinator with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVocationalCoordinatorsByCriteria")]
        public async Task<ListResponse<VocationalCoordinatorViewModel>> GetVocationalCoordinatorsByCriteria([FromBody] SearchVocationalCoordinatorModel searchModel)
        {
            ListResponse<VocationalCoordinatorViewModel> response = new ListResponse<VocationalCoordinatorViewModel>();

            try
            {
                var vocationalCoordinatorModels = await Task.Run(() =>
                {
                    return this.vocationalCoordinatorManager.GetVocationalCoordinatorsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vocationalCoordinatorModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vocationalCoordinator data by name
        /// </summary>
        /// <param name="vocationalCoordinatorName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVocationalCoordinatorsByName")]
        public async Task<ListResponse<VocationalCoordinatorModel>> GetVocationalCoordinatorsByName([FromQuery] string vocationalCoordinatorName)
        {
            ListResponse<VocationalCoordinatorModel> response = new ListResponse<VocationalCoordinatorModel>();

            try
            {
                var vocationalCoordinatorModels = await Task.Run(() =>
                {
                    return this.vocationalCoordinatorManager.GetVocationalCoordinatorsByName(vocationalCoordinatorName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vocationalCoordinatorModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get Vocational Coordinator data by Id
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="VTPId"></param>
        /// <param name="VCId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVocationalCoordinatorById")]
        public async Task<SingularResponse<VocationalCoordinatorModel>> GetVocationalCoordinatorById([FromBody] DataRequest vcRequest)
        {
            SingularResponse<VocationalCoordinatorModel> response = new SingularResponse<VocationalCoordinatorModel>();

            try
            {
                var vocationalCoordinatorModel = await Task.Run(() =>
                {
                    return this.vocationalCoordinatorManager.GetVocationalCoordinatorById(vcRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vocationalCoordinatorModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vocationalCoordinator
        /// </summary>
        /// <param name="vocationalCoordinatorRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVocationalCoordinator"), Route("CreateOrUpdateVocationalCoordinatorDetails")]
        public async Task<SingularResponse<string>> CreateVocationalCoordinator([FromBody] VocationalCoordinatorRequest vocationalCoordinatorRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vocationalCoordinatorRequest.RequestType = RequestType.New;
                    return this.vocationalCoordinatorManager.SaveOrUpdateVocationalCoordinatorDetails(vocationalCoordinatorRequest);
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
        /// Update vocationalCoordinator by Id
        /// </summary>
        /// <param name="vocationalCoordinatorRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVocationalCoordinator")]
        public async Task<SingularResponse<string>> UpdateVocationalCoordinator([FromBody] VocationalCoordinatorRequest vocationalCoordinatorRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vocationalCoordinatorRequest.RequestType = RequestType.Edit;
                    return this.vocationalCoordinatorManager.SaveOrUpdateVocationalCoordinatorDetails(vocationalCoordinatorRequest);
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
        /// Delete vocationalCoordinator by Id
        /// </summary>
        /// <param name="vocationalCoordinatorRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVocationalCoordinatorById")]
        public async Task<SingularResponse<bool>> DeleteVocationalCoordinatorById([FromBody] DeleteRequest<Guid> vocationalCoordinatorRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vocationalCoordinatorManager.DeleteById(vocationalCoordinatorRequest.DataId);
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
        /// Get VC Schools By VTP And VC Id
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="VTPId"></param>
        /// <param name="VCId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVCSchoolsByVTPAndVCId")]
        public async Task<ListResponse<VCSchoolModel>> GetVCSchoolsByVTPAndVCId([FromBody] DataRequest schoolRequest)
        {
            ListResponse<VCSchoolModel> response = new ListResponse<VCSchoolModel>();
            try
            {
                var vcSchools = await Task.Run(() =>
                {
                    return this.vocationalCoordinatorManager.GetVCSchoolsByVTPAndVCId(schoolRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vcSchools;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Save VC Transfers data
        /// </summary>
        /// <param name="VCSchoolModels"></param>
        /// <returns></returns>
        [HttpPost, Route("SaveVCTransfers")]
        public async Task<SingularResponse<VCSchoolTransferModel>> SaveVCTransfers([FromBody] VCSchoolTransferModel vcSchoolModels)
        {
            SingularResponse<VCSchoolTransferModel> response = new SingularResponse<VCSchoolTransferModel>();
            try
            {
                var vcSchools = await Task.Run(() =>
                {
                    return this.vocationalCoordinatorManager.SaveVCTransfers(vcSchoolModels);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vcSchools;
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