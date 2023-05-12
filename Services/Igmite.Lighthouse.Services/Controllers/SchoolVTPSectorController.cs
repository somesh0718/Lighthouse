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
    /// Expose all schoolVTPSector WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class SchoolVTPSectorController : BaseController
    {
        private readonly ISchoolVTPSectorManager schoolVTPSectorManager;

        /// <summary>
        /// Initializes the SchoolVTPSector controller class.
        /// </summary>
        /// <param name="_schoolVTPSectorManager"></param>
        public SchoolVTPSectorController(ISchoolVTPSectorManager _schoolVTPSectorManager)
        {
            this.schoolVTPSectorManager = _schoolVTPSectorManager;
        }

        /// <summary>
        /// Get list of schoolVTPSector data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetSchoolVTPSectors")]
        public async Task<ListResponse<SchoolVTPSectorModel>> GetSchoolVTPSectors()
        {
            ListResponse<SchoolVTPSectorModel> response = new ListResponse<SchoolVTPSectorModel>();

            try
            {
                IQueryable<SchoolVTPSectorModel> schoolVTPSectorModels = await Task.Run(() =>
                {
                    return this.schoolVTPSectorManager.GetSchoolVTPSectors();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = schoolVTPSectorModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of SchoolVTPSector with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSchoolVTPSectorsByCriteria")]
        public async Task<ListResponse<SchoolVTPSectorViewModel>> GetSchoolVTPSectorsByCriteria([FromBody] SearchSchoolVTPSectorModel searchModel)
        {
            ListResponse<SchoolVTPSectorViewModel> response = new ListResponse<SchoolVTPSectorViewModel>();

            try
            {
                var schoolVTPSectorModels = await Task.Run(() =>
                {
                    return this.schoolVTPSectorManager.GetSchoolVTPSectorsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = schoolVTPSectorModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of schoolVTPSector data by name
        /// </summary>
        /// <param name="schoolVTPSectorName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetSchoolVTPSectorsByName")]
        public async Task<ListResponse<SchoolVTPSectorModel>> GetSchoolVTPSectorsByName([FromQuery] string schoolVTPSectorName)
        {
            ListResponse<SchoolVTPSectorModel> response = new ListResponse<SchoolVTPSectorModel>();

            try
            {
                var schoolVTPSectorModels = await Task.Run(() =>
                {
                    return this.schoolVTPSectorManager.GetSchoolVTPSectorsByName(schoolVTPSectorName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = schoolVTPSectorModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get schoolVTPSector data by Id
        /// </summary>
        /// <param name="schoolVTPSectorId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSchoolVTPSectorById")]
        public async Task<SingularResponse<SchoolVTPSectorModel>> GetSchoolVTPSectorById([FromBody] DataRequest schoolVTPSectorRequest)
        {
            SingularResponse<SchoolVTPSectorModel> response = new SingularResponse<SchoolVTPSectorModel>();

            try
            {
                var schoolVTPSectorModel = await Task.Run(() =>
                {
                    return this.schoolVTPSectorManager.GetSchoolVTPSectorById(Guid.Parse(schoolVTPSectorRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = schoolVTPSectorModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new schoolVTPSector
        /// </summary>
        /// <param name="schoolVTPSectorRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateSchoolVTPSector"), Route("CreateOrUpdateSchoolVTPSectorDetails")]
        public async Task<SingularResponse<string>> CreateSchoolVTPSector([FromBody] SchoolVTPSectorRequest schoolVTPSectorRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //schoolVTPSectorRequest.RequestType = RequestType.New;
                    return this.schoolVTPSectorManager.SaveOrUpdateSchoolVTPSectorDetails(schoolVTPSectorRequest);
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
        /// Update schoolVTPSector by Id
        /// </summary>
        /// <param name="schoolVTPSectorRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateSchoolVTPSector")]
        public async Task<SingularResponse<string>> UpdateSchoolVTPSector([FromBody] SchoolVTPSectorRequest schoolVTPSectorRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    schoolVTPSectorRequest.RequestType = RequestType.Edit;
                    return this.schoolVTPSectorManager.SaveOrUpdateSchoolVTPSectorDetails(schoolVTPSectorRequest);
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
        /// Delete schoolVTPSector by Id
        /// </summary>
        /// <param name="schoolVTPSectorRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteSchoolVTPSectorById")]
        public async Task<SingularResponse<bool>> DeleteSchoolVTPSectorById([FromBody] DeleteRequest<Guid> schoolVTPSectorRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.schoolVTPSectorManager.DeleteById(schoolVTPSectorRequest.DataId);
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