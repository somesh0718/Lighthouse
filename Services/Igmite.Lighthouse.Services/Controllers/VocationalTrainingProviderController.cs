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
    /// Expose all vocationalTrainingProvider WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class VocationalTrainingProviderController : BaseController
    {
        private readonly IVocationalTrainingProviderManager vocationalTrainingProviderManager;

        /// <summary>
        /// Initializes the VocationalTrainingProvider controller class.
        /// </summary>
        /// <param name="_vocationalTrainingProviderManager"></param>
        public VocationalTrainingProviderController(IVocationalTrainingProviderManager _vocationalTrainingProviderManager)
        {
            this.vocationalTrainingProviderManager = _vocationalTrainingProviderManager;
        }

        /// <summary>
        /// Get list of vocationalTrainingProvider data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetVocationalTrainingProviders")]
        public async Task<ListResponse<VocationalTrainingProviderModel>> GetVocationalTrainingProviders()
        {
            ListResponse<VocationalTrainingProviderModel> response = new ListResponse<VocationalTrainingProviderModel>();

            try
            {
                IQueryable<VocationalTrainingProviderModel> vocationalTrainingProviderModels = await Task.Run(() =>
                {
                    return this.vocationalTrainingProviderManager.GetVocationalTrainingProviders();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vocationalTrainingProviderModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of VocationalTrainingProvider with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVocationalTrainingProvidersByCriteria")]
        public async Task<ListResponse<VocationalTrainingProviderViewModel>> GetVocationalTrainingProvidersByCriteria([FromBody] SearchVocationalTrainingProviderModel searchModel)
        {
            ListResponse<VocationalTrainingProviderViewModel> response = new ListResponse<VocationalTrainingProviderViewModel>();

            try
            {
                var vocationalTrainingProviderModels = await Task.Run(() =>
                {
                    return this.vocationalTrainingProviderManager.GetVocationalTrainingProvidersByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vocationalTrainingProviderModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of vocationalTrainingProvider data by name
        /// </summary>
        /// <param name="vocationalTrainingProviderName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetVocationalTrainingProvidersByName")]
        public async Task<ListResponse<VocationalTrainingProviderModel>> GetVocationalTrainingProvidersByName([FromQuery] string vocationalTrainingProviderName)
        {
            ListResponse<VocationalTrainingProviderModel> response = new ListResponse<VocationalTrainingProviderModel>();

            try
            {
                var vocationalTrainingProviderModels = await Task.Run(() =>
                {
                    return this.vocationalTrainingProviderManager.GetVocationalTrainingProvidersByName(vocationalTrainingProviderName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vocationalTrainingProviderModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get vocationalTrainingProvider data by Id
        /// </summary>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVocationalTrainingProviderById")]
        public async Task<SingularResponse<VocationalTrainingProviderModel>> GetVocationalTrainingProviderById([FromBody] DataRequest vtpRequest)
        {
            SingularResponse<VocationalTrainingProviderModel> response = new SingularResponse<VocationalTrainingProviderModel>();

            try
            {
                var vocationalTrainingProviderModel = await Task.Run(() =>
                {
                    return this.vocationalTrainingProviderManager.GetVocationalTrainingProviderById(Guid.Parse(vtpRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = vocationalTrainingProviderModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new vocationalTrainingProvider
        /// </summary>
        /// <param name="vocationalTrainingProviderRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateVocationalTrainingProvider"), Route("CreateOrUpdateVocationalTrainingProviderDetails")]
        public async Task<SingularResponse<string>> CreateVocationalTrainingProvider([FromBody] VocationalTrainingProviderRequest vocationalTrainingProviderRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //vocationalTrainingProviderRequest.RequestType = RequestType.New;
                    return this.vocationalTrainingProviderManager.SaveOrUpdateVocationalTrainingProviderDetails(vocationalTrainingProviderRequest);
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
        /// Update vocationalTrainingProvider by Id
        /// </summary>
        /// <param name="vocationalTrainingProviderRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateVocationalTrainingProvider")]
        public async Task<SingularResponse<string>> UpdateVocationalTrainingProvider([FromBody] VocationalTrainingProviderRequest vocationalTrainingProviderRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    vocationalTrainingProviderRequest.RequestType = RequestType.Edit;
                    return this.vocationalTrainingProviderManager.SaveOrUpdateVocationalTrainingProviderDetails(vocationalTrainingProviderRequest);
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
        /// Delete vocationalTrainingProvider by Id
        /// </summary>
        /// <param name="vocationalTrainingProviderRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteVocationalTrainingProviderById")]
        public async Task<SingularResponse<bool>> DeleteVocationalTrainingProviderById([FromBody] DeleteRequest<Guid> vocationalTrainingProviderRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.vocationalTrainingProviderManager.DeleteById(vocationalTrainingProviderRequest.DataId);
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
        /// get school by VTPTransferRequest
        /// </summary>
        /// <param name="VTPTransferRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSchoolByVTPIdSectorId")]
        public async Task<ListResponse<VTPSchoolModel>> GetSchoolByVTPIdSectorId([FromBody] DataRequest schoolRequest)
        {
            ListResponse<VTPSchoolModel> response = new ListResponse<VTPSchoolModel>();
            try
            {
                var schoolStudents = await Task.Run(() =>
                {
                    return this.vocationalTrainingProviderManager.GetSchoolByVTPIdSectorId(Guid.Parse(schoolRequest.DataId), Guid.Parse(schoolRequest.DataId1), Guid.Parse(schoolRequest.DataId2));
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

        /// <summary>
        /// Save VTP Transfers data
        /// </summary>
        /// <param name="VTPSchoolModels"></param>
        /// <returns></returns>
        [HttpPost, Route("SaveVTPTransfers")]
        public async Task<SingularResponse<VTPSchoolTransferModel>> SaveVCTransfers([FromBody] VTPSchoolTransferModel vtpSchoolTransferRequest)
        {
            SingularResponse<VTPSchoolTransferModel> response = new SingularResponse<VTPSchoolTransferModel>();
            try
            {
                var vcSchools = await Task.Run(() =>
                {
                    return this.vocationalTrainingProviderManager.SaveVTPTransfers(vtpSchoolTransferRequest);
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