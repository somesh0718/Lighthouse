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
    /// Expose all cluster WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class ClusterController : BaseController
    {
        private readonly IClusterManager clusterManager;

        /// <summary>
        /// Initializes the Cluster controller class.
        /// </summary>
        /// <param name="_clusterManager"></param>
        public ClusterController(IClusterManager _clusterManager)
        {
            this.clusterManager = _clusterManager;
        }

        /// <summary>
        /// Get list of cluster data
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetClusters")]
        public async Task<ListResponse<ClusterModel>> GetClusters()
        {
            ListResponse<ClusterModel> response = new ListResponse<ClusterModel>();

            try
            {
                IQueryable<ClusterModel> clusterModels = await Task.Run(() =>
                {
                    return this.clusterManager.GetClusters();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = clusterModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of Cluster with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route("GetClustersByCriteria")]
        public async Task<ListResponse<ClusterViewModel>> GetClustersByCriteria([FromBody] SearchClusterModel searchModel)
        {
            ListResponse<ClusterViewModel> response = new ListResponse<ClusterViewModel>();

            try
            {
                var clusterModels = await Task.Run(() =>
                {
                    return this.clusterManager.GetClustersByCriteria(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = clusterModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of cluster data by name
        /// </summary>
        /// <param name="clusterName"></param>
        /// <returns></returns>
        [HttpGet, Route("GetClustersByName")]
        public async Task<ListResponse<ClusterModel>> GetClustersByName([FromQuery] string clusterName)
        {
            ListResponse<ClusterModel> response = new ListResponse<ClusterModel>();

            try
            {
                var clusterModels = await Task.Run(() =>
                {
                    return this.clusterManager.GetClustersByName(clusterName);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = clusterModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get cluster data by Id
        /// </summary>
        /// <param name="clusterId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetClusterById")]
        public async Task<SingularResponse<ClusterModel>> GetClusterById([FromBody] DataRequest clusterRequest)
        {
            SingularResponse<ClusterModel> response = new SingularResponse<ClusterModel>();

            try
            {
                var clusterModel = await Task.Run(() =>
                {
                    return this.clusterManager.GetClusterById(Guid.Parse(clusterRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = clusterModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Create new cluster
        /// </summary>
        /// <param name="clusterRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("CreateCluster"), Route("CreateOrUpdateClusterDetails")]
        public async Task<SingularResponse<string>> CreateCluster([FromBody] ClusterRequest clusterRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    //clusterRequest.RequestType = RequestType.New;
                    return this.clusterManager.SaveOrUpdateClusterDetails(clusterRequest);
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
        /// Update cluster by Id
        /// </summary>
        /// <param name="clusterRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateCluster")]
        public async Task<SingularResponse<string>> UpdateCluster([FromBody] ClusterRequest clusterRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            try
            {
                response = await Task.Run(() =>
                {
                    clusterRequest.RequestType = RequestType.Edit;
                    return this.clusterManager.SaveOrUpdateClusterDetails(clusterRequest);
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
        /// Delete cluster by Id
        /// </summary>
        /// <param name="clusterRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteClusterById")]
        public async Task<SingularResponse<bool>> DeleteClusterById([FromBody] DeleteRequest<string> clusterRequest)
        {
            SingularResponse<bool> response = new SingularResponse<bool>();
            try
            {
                response.Result = await Task.Run(() =>
                {
                    return this.clusterManager.DeleteById(Guid.Parse(clusterRequest.DataId));
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