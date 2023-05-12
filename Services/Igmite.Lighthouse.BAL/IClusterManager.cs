using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL
{
    /// <summary>
    /// Manager of the Cluster entity
    /// </summary>
    public interface IClusterManager : IGenericManager<ClusterModel>
    {
        /// <summary>
        /// Get list of Clusters
        /// </summary>
        /// <returns></returns>
        IQueryable<ClusterModel> GetClusters();

        /// <summary>
        /// Get list of Clusters by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<ClusterModel> GetClustersByName(string clusterName);

        /// <summary>
        /// Get Cluster by ClusterId
        /// </summary>
        /// <param name="clusterId"></param>
        /// <returns></returns>
        ClusterModel GetClusterById(Guid clusterId);

        /// <summary>
        /// Get Cluster by ClusterId using async
        /// </summary>
        /// <param name="clusterId"></param>
        /// <returns></returns>
        Task<ClusterModel> GetClusterByIdAsync(Guid clusterId);

        /// <summary>
        /// Insert/Update Cluster entity
        /// </summary>
        /// <param name="clusterModel"></param>
        /// <returns></returns>
        SingularResponse<string> SaveOrUpdateClusterDetails(ClusterModel clusterModel);

        /// <summary>
        /// Delete a record by ClusterId
        /// </summary>
        /// <param name="clusterId"></param>
        /// <returns></returns>
        bool DeleteById(Guid clusterId);

        /// <summary>
        /// Check duplicate Cluster by Name
        /// </summary>
        /// <param name="clusterModel"></param>
        /// <returns></returns>
        bool CheckClusterExistByName(ClusterModel clusterModel);

        /// <summary>
        /// List of Cluster with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<ClusterViewModel> GetClustersByCriteria(SearchClusterModel searchModel);
    }
}