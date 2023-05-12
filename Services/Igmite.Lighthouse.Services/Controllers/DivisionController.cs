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
    /// Expose all division WebAPI services
    /// </summary>
    [Route(ServiceConstants.ServiceName), ApiController]
    public class DivisionController : BaseController
    {
        private readonly IDivisionManager divisionManager;

        /// <summary>
        /// Initializes the Division controller class.
        /// </summary>
        /// <param name="_divisionManager"></param>
        public DivisionController(IDivisionManager _divisionManager)
        {
            this.divisionManager = _divisionManager;
        }

        /// <summary>
        /// Get list of division data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route(ServiceConstants.Transaction.Division.GetAll)]
        public async Task<ListResponse<DivisionModel>> GetDivisions()
        {
            ListResponse<DivisionModel> response = new ListResponse<DivisionModel>();

            try
            {
                IQueryable<DivisionModel> divisionModels = await Task.Run(() =>
                {
                    return this.divisionManager.GetDivisions();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = divisionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of Division with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Transaction.Division.GetByCriteria)]
        public async Task<ListResponse<DivisionViewModel>> GetDivisionsByCriteria([FromBody] SearchDivisionModel searchModel)
        {
            ListResponse<DivisionViewModel> response = new ListResponse<DivisionViewModel>();

            try
            {
                var divisionModels = await Task.Run(() =>
                {
                    return this.divisionManager.GetDivisionsByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = divisionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of division data by name
        /// </summary>
        /// <param name="divisionName"></param>
        /// <returns></returns>
        [HttpGet, Route(ServiceConstants.Transaction.Division.GetByName)]
        public async Task<ListResponse<DivisionModel>> GetDivisionsByName([FromQuery] string divisionName)
        {
            ListResponse<DivisionModel> response = new ListResponse<DivisionModel>();

            try
            {
                var divisionModels = await Task.Run(() =>
                {
                    return this.divisionManager.GetDivisionsByName(divisionName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = divisionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get division data by Id
        /// </summary>
        /// <param name="divisionId"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Transaction.Division.GetById)]
        public async Task<SingularResponse<DivisionModel>> GetDivisionById([FromBody] DataRequest divisionRequest)
        {
            SingularResponse<DivisionModel> response = new SingularResponse<DivisionModel>();

            try
            {
                var divisionModel = await Task.Run(() =>
                {
                    return this.divisionManager.GetDivisionById(Guid.Parse(divisionRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = divisionModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new division
        /// </summary>
        /// <param name="divisionRequest"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Transaction.Division.Create), Route(ServiceConstants.Transaction.Division.CreateOrUpdate)]
        public async Task<SingularResponse<string>> CreateDivision([FromBody] DivisionRequest divisionRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //divisionRequest.RequestType = RequestType.New;
                    return this.divisionManager.SaveOrUpdateDivisionDetails(divisionRequest);
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
        /// Update division by Id
        /// </summary>
        /// <param name="divisionRequest"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Transaction.Division.Update)]
        public async Task<SingularResponse<string>> UpdateDivision([FromBody] DivisionRequest divisionRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    divisionRequest.RequestType = RequestType.Edit;
                    return this.divisionManager.SaveOrUpdateDivisionDetails(divisionRequest);
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
        /// Delete division by Id
        /// </summary>
        /// <param name="divisionRequest"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Transaction.Division.DeleteById)]
        public async Task<SingularResponse<bool>> DeleteDivisionById([FromBody] DeleteRequest<Guid> divisionRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.divisionManager.DeleteById(divisionRequest.DataId);
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