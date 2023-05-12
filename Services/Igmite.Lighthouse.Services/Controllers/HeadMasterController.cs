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
    /// Expose all headMaster WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class HeadMasterController : BaseController
    {
        private readonly IHeadMasterManager headMasterManager;

        /// <summary>
        /// Initializes the HeadMaster controller class.
        /// </summary>
        /// <param name="_headMasterManager"></param>
        public HeadMasterController(IHeadMasterManager _headMasterManager)
        {
            this.headMasterManager = _headMasterManager;
        }

        /// <summary>
        /// Get list of headMaster data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetHeadMasters")]
        public async Task<ListResponse<HeadMasterModel>> GetHeadMasters()
        {
            ListResponse<HeadMasterModel> response = new ListResponse<HeadMasterModel>();

            try
            {
                IQueryable<HeadMasterModel> headMasterModels = await Task.Run(() =>
                {
                    return this.headMasterManager.GetHeadMasters();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = headMasterModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of HeadMaster with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetHeadMastersByCriteria")]
        public async Task<ListResponse<HeadMasterViewModel>> GetHeadMastersByCriteria([FromBody] SearchHeadMasterModel searchModel)
        {
            ListResponse<HeadMasterViewModel> response = new ListResponse<HeadMasterViewModel>();

            try
            {
                var headMasterModels = await Task.Run(() =>
                {
                    return this.headMasterManager.GetHeadMastersByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = headMasterModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of headMaster data by name
        /// </summary>
        /// <param name="headMasterName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetHeadMastersByName")]
        public async Task<ListResponse<HeadMasterModel>> GetHeadMastersByName([FromQuery] string headMasterName)
        {
            ListResponse<HeadMasterModel> response = new ListResponse<HeadMasterModel>();

            try
            {
                var headMasterModels = await Task.Run(() =>
                {
                    return this.headMasterManager.GetHeadMastersByName(headMasterName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = headMasterModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get headMaster data by Id
        /// </summary>
        /// <param name="AcademicYearId"></param>
        /// <param name="SchoolId"></param>
        /// <param name="HMId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetHeadMasterById")]
        public async Task<SingularResponse<HeadMasterModel>> GetHeadMasterById([FromBody] DataRequest hmRequest)
        {
            SingularResponse<HeadMasterModel> response = new SingularResponse<HeadMasterModel>();

            try
            {
                var headMasterModel = await Task.Run(() =>
                {
                    return this.headMasterManager.GetHeadMasterById(hmRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = headMasterModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new headMaster
        /// </summary>
        /// <param name="headMasterRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateHeadMaster"), Route("CreateOrUpdateHeadMasterDetails")]
        public async Task<SingularResponse<string>> CreateHeadMaster([FromBody] HeadMasterRequest headMasterRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //headMasterRequest.RequestType = RequestType.New;
                    return this.headMasterManager.SaveOrUpdateHeadMasterDetails(headMasterRequest);
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
        /// Update headMaster by Id
        /// </summary>
        /// <param name="headMasterRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateHeadMaster")]
        public async Task<SingularResponse<string>> UpdateHeadMaster([FromBody] HeadMasterRequest headMasterRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    headMasterRequest.RequestType = RequestType.Edit;
                    return this.headMasterManager.SaveOrUpdateHeadMasterDetails(headMasterRequest);
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
        /// Delete headMaster by Id
        /// </summary>
        /// <param name="headMasterRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteHeadMasterById")]
        public async Task<SingularResponse<bool>> DeleteHeadMasterById([FromBody] DeleteRequest<Guid> headMasterRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.headMasterManager.DeleteById(headMasterRequest.DataId);
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