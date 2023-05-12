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
    /// Expose all sectorJobRole WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class SectorJobRoleController : BaseController
    {
        private readonly ISectorJobRoleManager sectorJobRoleManager;

        /// <summary>
        /// Initializes the SectorJobRole controller class.
        /// </summary>
        /// <param name="_sectorJobRoleManager"></param>
        public SectorJobRoleController(ISectorJobRoleManager _sectorJobRoleManager)
        {
            this.sectorJobRoleManager = _sectorJobRoleManager;
        }

        /// <summary>
        /// Get list of sectorJobRole data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetSectorJobRoles")]
        public async Task<ListResponse<SectorJobRoleModel>> GetSectorJobRoles()
        {
            ListResponse<SectorJobRoleModel> response = new ListResponse<SectorJobRoleModel>();

            try
            {
                IQueryable<SectorJobRoleModel> sectorJobRoleModels = await Task.Run(() =>
                {
                    return this.sectorJobRoleManager.GetSectorJobRoles();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = sectorJobRoleModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of SectorJobRole with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSectorJobRolesByCriteria")]
        public async Task<ListResponse<SectorJobRoleViewModel>> GetSectorJobRolesByCriteria([FromBody] SearchSectorJobRoleModel searchModel)
        {
            ListResponse<SectorJobRoleViewModel> response = new ListResponse<SectorJobRoleViewModel>();

            try
            {
                var sectorJobRoleModels = await Task.Run(() =>
                {
                    return this.sectorJobRoleManager.GetSectorJobRolesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = sectorJobRoleModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of sectorJobRole data by name
        /// </summary>
        /// <param name="sectorJobRoleName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetSectorJobRolesByName")]
        public async Task<ListResponse<SectorJobRoleModel>> GetSectorJobRolesByName([FromQuery] string sectorJobRoleName)
        {
            ListResponse<SectorJobRoleModel> response = new ListResponse<SectorJobRoleModel>();

            try
            {
                var sectorJobRoleModels = await Task.Run(() =>
                {
                    return this.sectorJobRoleManager.GetSectorJobRolesByName(sectorJobRoleName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = sectorJobRoleModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get sectorJobRole data by Id
        /// </summary>
        /// <param name="sectorJobRoleId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSectorJobRoleById")]
        public async Task<SingularResponse<SectorJobRoleModel>> GetSectorJobRoleById([FromBody] DataRequest sectorJobRoleRequest)
        {
            SingularResponse<SectorJobRoleModel> response = new SingularResponse<SectorJobRoleModel>();

            try
            {
                var sectorJobRoleModel = await Task.Run(() =>
                {
                    return this.sectorJobRoleManager.GetSectorJobRoleById(Guid.Parse(sectorJobRoleRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = sectorJobRoleModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new sectorJobRole
        /// </summary>
        /// <param name="sectorJobRoleRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateSectorJobRole"), Route("CreateOrUpdateSectorJobRoleDetails")]
        public async Task<SingularResponse<string>> CreateSectorJobRole([FromBody] SectorJobRoleRequest sectorJobRoleRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //sectorJobRoleRequest.RequestType = RequestType.New;
                    return this.sectorJobRoleManager.SaveOrUpdateSectorJobRoleDetails(sectorJobRoleRequest);
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
        /// Update sectorJobRole by Id
        /// </summary>
        /// <param name="sectorJobRoleRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateSectorJobRole")]
        public async Task<SingularResponse<string>> UpdateSectorJobRole([FromBody] SectorJobRoleRequest sectorJobRoleRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    sectorJobRoleRequest.RequestType = RequestType.Edit;
                    return this.sectorJobRoleManager.SaveOrUpdateSectorJobRoleDetails(sectorJobRoleRequest);
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
        /// Delete sectorJobRole by Id
        /// </summary>
        /// <param name="sectorJobRoleRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteSectorJobRoleById")]
        public async Task<SingularResponse<bool>> DeleteSectorJobRoleById([FromBody] DeleteRequest<Guid> sectorJobRoleRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.sectorJobRoleManager.DeleteById(sectorJobRoleRequest.DataId);
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