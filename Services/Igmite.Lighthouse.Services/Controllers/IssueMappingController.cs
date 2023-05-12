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
    /// Expose all IssueMapping WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class IssueMappingController : BaseController
    {
        private readonly IIssueMappingManager IssueMappingManager;

        /// <summary>
        /// Initializes the IssueMapping controller class.
        /// </summary>
        /// <param name="_IssueMappingManager"></param>
        public IssueMappingController(IIssueMappingManager _IssueMappingManager)
        {
            this.IssueMappingManager = _IssueMappingManager;
        }

        /// <summary>
        /// Get list of IssueMapping data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetIssueMappings")]
        public async Task<ListResponse<IssueMappingModel>> GetIssueMappings()
        {
            ListResponse<IssueMappingModel> response = new ListResponse<IssueMappingModel>();

            try
            {
                IQueryable<IssueMappingModel> IssueMappingModels = await Task.Run(() =>
                {
                    return this.IssueMappingManager.GetIssueMapping();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = IssueMappingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of IssueMapping with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetIssueMappingsByCriteria")]
        public async Task<ListResponse<IssueMappingViewModel>> GetIssueMappingsByCriteria([FromBody] SearchIssueMappingModel searchModel)
        {
            ListResponse<IssueMappingViewModel> response = new ListResponse<IssueMappingViewModel>();

            try
            {
                var IssueMappingModels = await Task.Run(() =>
                {
                    return this.IssueMappingManager.GetIssueMappingByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = IssueMappingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of IssueMapping data by IssueId
        /// </summary>
        /// <param name="IssueMappingName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetIssueMappingByIssueId")]
        public async Task<ListResponse<IssueMappingModel>> GetIssueMappingByIssueId([FromQuery] string MainIssueId, string SubIssueId)
        {
            ListResponse<IssueMappingModel> response = new ListResponse<IssueMappingModel>();

            try
            {
                var IssueMappingModels = await Task.Run(() =>
                {
                    return this.IssueMappingManager.GetIssueMappingByIssueId(MainIssueId, SubIssueId);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = IssueMappingModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get IssueMapping data by Id
        /// </summary>
        /// <param name="IssueMappingId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetIssueMappingById")]
        public async Task<SingularResponse<IssueMappingModel>> GetIssueMappingById([FromBody] DataRequest issueMappingRequest)
        {
            SingularResponse<IssueMappingModel> response = new SingularResponse<IssueMappingModel>();

            try
            {
                var IssueMappingModel = await Task.Run(() =>
                {
                    return this.IssueMappingManager.GetIssueMappingById(Guid.Parse(issueMappingRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = IssueMappingModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new IssueMapping
        /// </summary>
        /// <param name="IssueMappingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateIssueMapping"), Route("CreateOrUpdateIssueMappingDetails")]
        public async Task<SingularResponse<string>> CreateIssueMapping([FromBody] IssueMappingRequest IssueMappingRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //IssueMappingRequest.RequestType = RequestType.New;
                    return this.IssueMappingManager.SaveOrUpdateIssueMappingDetails(IssueMappingRequest);
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
        /// Update IssueMapping by Id
        /// </summary>
        /// <param name="IssueMappingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateIssueMapping")]
        public async Task<SingularResponse<string>> UpdateIssueMapping([FromBody] IssueMappingRequest IssueMappingRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    IssueMappingRequest.RequestType = RequestType.Edit;
                    return this.IssueMappingManager.SaveOrUpdateIssueMappingDetails(IssueMappingRequest);
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
        /// Delete IssueMapping by Id
        /// </summary>
        /// <param name="IssueMappingRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteIssueMappingById")]
        public async Task<SingularResponse<bool>> DeleteIssueMappingById([FromBody] DeleteRequest<Guid> IssueMappingRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.IssueMappingManager.DeleteById(IssueMappingRequest.DataId);
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
        /// List of Issue by userId with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetIssueByCriteria")]
        public async Task<ListResponse<IssueViewModel>> GetIssueByCriteria([FromBody] SearchIssueModel searchModel)
        {
            ListResponse<IssueViewModel> response = new ListResponse<IssueViewModel>();

            try
            {
                var IssueMappingModels = await Task.Run(() =>
                {
                    return this.IssueMappingManager.GetIssueByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = IssueMappingModels.ToList();
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