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
    /// Expose all schoolVEIncharge WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class SchoolVEInchargeController : BaseController
    {
        private readonly ISchoolVEInchargeManager schoolVEInchargeManager;

        /// <summary>
        /// Initializes the SchoolVEIncharge controller class.
        /// </summary>
        /// <param name="_schoolVEInchargeManager"></param>
        public SchoolVEInchargeController(ISchoolVEInchargeManager _schoolVEInchargeManager)
        {
            this.schoolVEInchargeManager = _schoolVEInchargeManager;
        }

        /// <summary>
        /// Get list of schoolVEIncharge data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetSchoolVEIncharges")]
        public async Task<ListResponse<SchoolVEInchargeModel>> GetSchoolVEIncharges()
        {
            ListResponse<SchoolVEInchargeModel> response = new ListResponse<SchoolVEInchargeModel>();

            try
            {
                IQueryable<SchoolVEInchargeModel> schoolVEInchargeModels = await Task.Run(() =>
                {
                    return this.schoolVEInchargeManager.GetSchoolVEIncharges();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = schoolVEInchargeModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of SchoolVEIncharge with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSchoolVEInchargesByCriteria")]
        public async Task<ListResponse<SchoolVEInchargeViewModel>> GetSchoolVEInchargesByCriteria([FromBody] SearchSchoolVEInchargeModel searchModel)
        {
            ListResponse<SchoolVEInchargeViewModel> response = new ListResponse<SchoolVEInchargeViewModel>();

            try
            {
                var schoolVEInchargeModels = await Task.Run(() =>
                {
                    return this.schoolVEInchargeManager.GetSchoolVEInchargesByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = schoolVEInchargeModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of schoolVEIncharge data by name
        /// </summary>
        /// <param name="schoolVEInchargeName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetSchoolVEInchargesByName")]
        public async Task<ListResponse<SchoolVEInchargeModel>> GetSchoolVEInchargesByName([FromQuery] string schoolVEInchargeName)
        {
            ListResponse<SchoolVEInchargeModel> response = new ListResponse<SchoolVEInchargeModel>();

            try
            {
                var schoolVEInchargeModels = await Task.Run(() =>
                {
                    return this.schoolVEInchargeManager.GetSchoolVEInchargesByName(schoolVEInchargeName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = schoolVEInchargeModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get schoolVEIncharge data by Id
        /// </summary>
        /// <param name="schoolVEInchargeId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSchoolVEInchargeById")]
        public async Task<SingularResponse<SchoolVEInchargeModel>> GetSchoolVEInchargeById([FromBody] DataRequest schoolVEInchargeRequest)
        {
            SingularResponse<SchoolVEInchargeModel> response = new SingularResponse<SchoolVEInchargeModel>();

            try
            {
                var schoolVEInchargeModel = await Task.Run(() =>
                {
                    return this.schoolVEInchargeManager.GetSchoolVEInchargeById(Guid.Parse(schoolVEInchargeRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = schoolVEInchargeModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new schoolVEIncharge
        /// </summary>
        /// <param name="schoolVEInchargeRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateSchoolVEIncharge"), Route("CreateOrUpdateSchoolVEInchargeDetails")]
        public async Task<SingularResponse<string>> CreateSchoolVEIncharge([FromBody] SchoolVEInchargeRequest schoolVEInchargeRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //schoolVEInchargeRequest.RequestType = RequestType.New;
                    return this.schoolVEInchargeManager.SaveOrUpdateSchoolVEInchargeDetails(schoolVEInchargeRequest);
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
        /// Update schoolVEIncharge by Id
        /// </summary>
        /// <param name="schoolVEInchargeRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateSchoolVEIncharge")]
        public async Task<SingularResponse<string>> UpdateSchoolVEIncharge([FromBody] SchoolVEInchargeRequest schoolVEInchargeRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    schoolVEInchargeRequest.RequestType = RequestType.Edit;
                    return this.schoolVEInchargeManager.SaveOrUpdateSchoolVEInchargeDetails(schoolVEInchargeRequest);
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
        /// Delete schoolVEIncharge by Id
        /// </summary>
        /// <param name="schoolVEInchargeRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteSchoolVEInchargeById")]
        public async Task<SingularResponse<bool>> DeleteSchoolVEInchargeById([FromBody] DeleteRequest<Guid> schoolVEInchargeRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.schoolVEInchargeManager.DeleteById(schoolVEInchargeRequest.DataId);
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