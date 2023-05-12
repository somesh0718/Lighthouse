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
    /// Expose all academicYear WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class AcademicYearController : BaseController
    {
        private readonly IAcademicYearManager academicYearManager;

        /// <summary>
        /// Initializes the AcademicYear controller class.
        /// </summary>
        /// <param name="_academicYearManager"></param>
        public AcademicYearController(IAcademicYearManager _academicYearManager)
        {
            this.academicYearManager = _academicYearManager;
        }

        /// <summary>
        /// Get list of academicYear data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetAcademicYears")]
        public async Task<ListResponse<AcademicYearModel>> GetAcademicYears()
        {
            ListResponse<AcademicYearModel> response = new ListResponse<AcademicYearModel>();

            try
            {
                IQueryable<AcademicYearModel> academicYearModels = await Task.Run(() =>
                {
                    return this.academicYearManager.GetAcademicYears();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = academicYearModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of AcademicYear with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetAcademicYearsByCriteria")]
        public async Task<ListResponse<AcademicYearViewModel>> GetAcademicYearsByCriteria([FromBody] SearchAcademicYearModel searchModel)
        {
            ListResponse<AcademicYearViewModel> response = new ListResponse<AcademicYearViewModel>();

            try
            {
                var academicYearModels = await Task.Run(() =>
                {
                    return this.academicYearManager.GetAcademicYearsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = academicYearModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of academicYear data by name
        /// </summary>
        /// <param name="academicYearName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetAcademicYearsByName")]
        public async Task<ListResponse<AcademicYearModel>> GetAcademicYearsByName([FromQuery] string academicYearName)
        {
            ListResponse<AcademicYearModel> response = new ListResponse<AcademicYearModel>();

            try
            {
                var academicYearModels = await Task.Run(() =>
                {
                    return this.academicYearManager.GetAcademicYearsByName(academicYearName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = academicYearModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get academicYear data by Id
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetAcademicYearById")]
        public async Task<SingularResponse<AcademicYearModel>> GetAcademicYearById([FromBody] DataRequest academicYearRequest)
        {
            SingularResponse<AcademicYearModel> response = new SingularResponse<AcademicYearModel>();

            try
            {
                var academicYearModel = await Task.Run(() =>
                {
                    return this.academicYearManager.GetAcademicYearById(Guid.Parse(academicYearRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = academicYearModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new academicYear
        /// </summary>
        /// <param name="academicYearRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateAcademicYear"), Route("CreateOrUpdateAcademicYearDetails")]
        public async Task<SingularResponse<string>> CreateAcademicYear([FromBody] AcademicYearRequest academicYearRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //academicYearRequest.RequestType = RequestType.New;
                    return this.academicYearManager.SaveOrUpdateAcademicYearDetails(academicYearRequest);
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
        /// Update academicYear by Id
        /// </summary>
        /// <param name="academicYearRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateAcademicYear")]
        public async Task<SingularResponse<string>> UpdateAcademicYear([FromBody] AcademicYearRequest academicYearRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    academicYearRequest.RequestType = RequestType.Edit;
                    return this.academicYearManager.SaveOrUpdateAcademicYearDetails(academicYearRequest);
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
        /// Delete academicYear by Id
        /// </summary>
        /// <param name="academicYearRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteAcademicYearById")]
        public async Task<SingularResponse<bool>> DeleteAcademicYearById([FromBody] DeleteRequest<Guid> academicYearRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.academicYearManager.DeleteById(academicYearRequest.DataId);
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