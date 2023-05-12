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
    /// Expose all termsCondition WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController, ApiExplorerSettings(IgnoreApi = true)]
    public class TermsConditionController : BaseController
    {
        private readonly ITermsConditionManager termsConditionManager;

        /// <summary>
        /// Initializes the TermsCondition controller class.
        /// </summary>
        /// <param name="_termsConditionManager"></param>
        public TermsConditionController(ITermsConditionManager _termsConditionManager)
        {
            this.termsConditionManager = _termsConditionManager;
        }

        /// <summary>
        /// Get list of termsCondition data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetTermsConditions")]
        public async Task<ListResponse<TermsConditionModel>> GetTermsConditions()
        {
            ListResponse<TermsConditionModel> response = new ListResponse<TermsConditionModel>();

            try
            {
                IQueryable<TermsConditionModel> termsConditionModels = await Task.Run(() =>
                {
                    return this.termsConditionManager.GetTermsConditions();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = termsConditionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of TermsCondition with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetTermsConditionsByCriteria")]
        public async Task<ListResponse<TermsConditionViewModel>> GetTermsConditionsByCriteria([FromBody] SearchTermsConditionModel searchModel)
        {
            ListResponse<TermsConditionViewModel> response = new ListResponse<TermsConditionViewModel>();

            try
            {
                var termsConditionModels = await Task.Run(() =>
                {
                    return this.termsConditionManager.GetTermsConditionsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = termsConditionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of termsCondition data by name
        /// </summary>
        /// <param name="termsConditionName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetTermsConditionsByName")]
        public async Task<ListResponse<TermsConditionModel>> GetTermsConditionsByName([FromQuery] string termsConditionName)
        {
            ListResponse<TermsConditionModel> response = new ListResponse<TermsConditionModel>();

            try
            {
                var termsConditionModels = await Task.Run(() =>
                {
                    return this.termsConditionManager.GetTermsConditionsByName(termsConditionName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = termsConditionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get termsCondition data by Id
        /// </summary>
        /// <param name="termsConditionId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetTermsConditionById")]
        public async Task<SingularResponse<TermsConditionModel>> GetTermsConditionById([FromBody] DataRequest termsConditionRequest)
        {
            SingularResponse<TermsConditionModel> response = new SingularResponse<TermsConditionModel>();

            try
            {
                var termsConditionModel = await Task.Run(() =>
                {
                    return this.termsConditionManager.GetTermsConditionById(Guid.Parse(termsConditionRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = termsConditionModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new termsCondition
        /// </summary>
        /// <param name="termsConditionRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateTermsCondition"), Route("CreateOrUpdateTermsConditionDetails")]
        public async Task<SingularResponse<string>> CreateTermsCondition([FromBody] TermsConditionRequest termsConditionRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //termsConditionRequest.RequestType = RequestType.New;
                    return this.termsConditionManager.SaveOrUpdateTermsConditionDetails(termsConditionRequest);
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
        /// Update termsCondition by Id
        /// </summary>
        /// <param name="termsConditionRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateTermsCondition")]
        public async Task<SingularResponse<string>> UpdateTermsCondition([FromBody] TermsConditionRequest termsConditionRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    termsConditionRequest.RequestType = RequestType.Edit;
                    return this.termsConditionManager.SaveOrUpdateTermsConditionDetails(termsConditionRequest);
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
        /// Delete termsCondition by Id
        /// </summary>
        /// <param name="termsConditionRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteTermsConditionById")]
        public async Task<SingularResponse<bool>> DeleteTermsConditionById([FromBody] DeleteRequest<Guid> termsConditionRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.termsConditionManager.DeleteById(termsConditionRequest.DataId);
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