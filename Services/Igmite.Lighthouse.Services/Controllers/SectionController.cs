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
    /// Expose all section WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class SectionController : BaseController
    {
        private readonly ISectionManager sectionManager;

        /// <summary>
        /// Initializes the Section controller class.
        /// </summary>
        /// <param name="_sectionManager"></param>
        public SectionController(ISectionManager _sectionManager)
        {
            this.sectionManager = _sectionManager;
        }

        /// <summary>
        /// Get list of section data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetSections")]
        public async Task<ListResponse<SectionModel>> GetSections()
        {
            ListResponse<SectionModel> response = new ListResponse<SectionModel>();

            try
            {
                IQueryable<SectionModel> sectionModels = await Task.Run(() =>
                {
                    return this.sectionManager.GetSections();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = sectionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of Section with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSectionsByCriteria")]
        public async Task<ListResponse<SectionViewModel>> GetSectionsByCriteria([FromBody] SearchSectionModel searchModel)
        {
            ListResponse<SectionViewModel> response = new ListResponse<SectionViewModel>();

            try
            {
                var sectionModels = await Task.Run(() =>
                {
                    return this.sectionManager.GetSectionsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = sectionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of section data by name
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetSectionsByName")]
        public async Task<ListResponse<SectionModel>> GetSectionsByName([FromQuery] string sectionName)
        {
            ListResponse<SectionModel> response = new ListResponse<SectionModel>();

            try
            {
                var sectionModels = await Task.Run(() =>
                {
                    return this.sectionManager.GetSectionsByName(sectionName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = sectionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get section data by Id
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSectionById")]
        public async Task<SingularResponse<SectionModel>> GetSectionById([FromBody] DataRequest sectionRequest)
        {
            SingularResponse<SectionModel> response = new SingularResponse<SectionModel>();

            try
            {
                var sectionModel = await Task.Run(() =>
                {
                    return this.sectionManager.GetSectionById(Guid.Parse(sectionRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = sectionModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new section
        /// </summary>
        /// <param name="sectionRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateSection"), Route("CreateOrUpdateSectionDetails")]
        public async Task<SingularResponse<string>> CreateSection([FromBody] SectionRequest sectionRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //sectionRequest.RequestType = RequestType.New;
                    return this.sectionManager.SaveOrUpdateSectionDetails(sectionRequest);
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
        /// Update section by Id
        /// </summary>
        /// <param name="sectionRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateSection")]
        public async Task<SingularResponse<string>> UpdateSection([FromBody] SectionRequest sectionRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    sectionRequest.RequestType = RequestType.Edit;
                    return this.sectionManager.SaveOrUpdateSectionDetails(sectionRequest);
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
        /// Delete section by Id
        /// </summary>
        /// <param name="sectionRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteSectionById")]
        public async Task<SingularResponse<bool>> DeleteSectionById([FromBody] DeleteRequest<Guid> sectionRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.sectionManager.DeleteById(sectionRequest.DataId);
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