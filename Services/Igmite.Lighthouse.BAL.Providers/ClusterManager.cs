using Igmite.Lighthouse.BAL.Validations;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Mappers;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Manager of the Cluster entity
    /// </summary>
    public class ClusterManager : GenericManager<ClusterModel>, IClusterManager
    {
        private readonly IClusterRepository clusterRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes the cluster manager.
        /// </summary>
        /// <param name="clusterRepository"></param>
        /// <param name="_httpContextAccessor"></param>
        public ClusterManager(IClusterRepository _clusterRepository, IHttpContextAccessor _httpContextAccessor)
        {
            this.clusterRepository = _clusterRepository;
            this.httpContextAccessor = _httpContextAccessor;
        }

        /// <summary>
        /// Get list of Clusters
        /// </summary>
        /// <returns></returns>
        public IQueryable<ClusterModel> GetClusters()
        {
            var clusters = this.clusterRepository.GetClusters();

            IList<ClusterModel> clusterModels = new List<ClusterModel>();
            clusters.ForEach((user) => clusterModels.Add(user.ToModel()));

            return clusterModels.AsQueryable();
        }

        /// <summary>
        /// Get list of Clusters by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<ClusterModel> GetClustersByName(string clusterName)
        {
            var clusters = this.clusterRepository.GetClustersByName(clusterName);

            IList<ClusterModel> clusterModels = new List<ClusterModel>();
            clusters.ForEach((user) => clusterModels.Add(user.ToModel()));

            return clusterModels.AsQueryable();
        }

        /// <summary>
        /// Get Cluster by ClusterId
        /// </summary>
        /// <param name="clusterId"></param>
        /// <returns></returns>
        public ClusterModel GetClusterById(Guid clusterId)
        {
            Cluster cluster = this.clusterRepository.GetClusterById(clusterId);

            return (cluster != null) ? cluster.ToModel() : null;
        }

        /// <summary>
        /// Get Cluster by ClusterId using async
        /// </summary>
        /// <param name="clusterId"></param>
        /// <returns></returns>
        public async Task<ClusterModel> GetClusterByIdAsync(Guid clusterId)
        {
            var cluster = await this.clusterRepository.GetClusterByIdAsync(clusterId);

            return (cluster != null) ? cluster.ToModel() : null;
        }

        /// <summary>
        /// Insert/Update Cluster entity
        /// </summary>
        /// <param name="clusterModel"></param>
        /// <returns></returns>
        public SingularResponse<string> SaveOrUpdateClusterDetails(ClusterModel clusterModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            Cluster cluster = null;

            //Validate model data
            clusterModel = clusterModel.GetModelValidationErrors<ClusterModel>();

            if (clusterModel.ErrorMessages.Count > 0)
            {
                response.Errors = clusterModel.ErrorMessages;
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            if (clusterModel.RequestType == RequestType.Edit)
            {
                cluster = this.clusterRepository.GetClusterById(clusterModel.ClusterId);
            }
            else
            {
                cluster = new Cluster();
                cluster.ClusterId = Guid.NewGuid();
            }

            if (clusterModel.ErrorMessages.Count == 0 && (clusterModel.ClusterName.StringVal().ToLower() != cluster.ClusterName.StringVal().ToLower()))
            {
                bool isClusterExists = this.clusterRepository.CheckClusterExistByName(clusterModel);

                if (isClusterExists)
                {
                    response.Errors.Add(Constants.ExistMessage);
                }
            }

            if (response.Errors.Count == 0)
            {
                cluster.AuthUserId = Convert.ToString(this.httpContextAccessor.HttpContext.Items[Constants.AuthUserId]);
                cluster = clusterModel.FromModel(cluster);

                //Save Or Update cluster details
                bool isSaved = this.clusterRepository.SaveOrUpdateClusterDetails(cluster);

                response.Result = isSaved ? "Success" : "Failed";
            }
            else
            {
                response.Result = "Failed";
                response.Success = false;
                return response;
            }

            return response;
        }

        /// <summary>
        /// Delete a record by ClusterId
        /// </summary>
        /// <param name="clusterId"></param>
        /// <returns></returns>
        public bool DeleteById(Guid clusterId)
        {
            return this.clusterRepository.DeleteById(clusterId);
        }

        /// <summary>
        /// Check duplicate Cluster by Name
        /// </summary>
        /// <param name="clusterModel"></param>
        /// <returns></returns>
        public bool CheckClusterExistByName(ClusterModel clusterModel)
        {
            return this.clusterRepository.CheckClusterExistByName(clusterModel);
        }

        /// <summary>}
        /// List of Cluster with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<ClusterViewModel> GetClustersByCriteria(SearchClusterModel searchModel)
        {
            return this.clusterRepository.GetClustersByCriteria(searchModel);
        }
    }
}