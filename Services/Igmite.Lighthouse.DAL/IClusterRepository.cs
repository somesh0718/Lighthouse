using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL
{
    /// <summary>
    /// Repository of the Cluster entity
    /// </summary>
    public interface IClusterRepository : IGenericRepository<Cluster>
    {
        /// <summary>
        /// Get list of Cluster
        /// </summary>
        /// <returns></returns>
        IQueryable<Cluster> GetClusters();

        /// <summary>
        /// Get list of Cluster by clusterName
        /// </summary>
        /// <param name="clusterName"></param>
        /// <returns></returns>
        IQueryable<Cluster> GetClustersByName(string clusterName);

        /// <summary>
        /// Get Cluster by clusterId
        /// </summary>
        /// <param name="clusterId"></param>
        /// <returns></returns>
        Cluster GetClusterById(Guid clusterId);

        /// <summary>
        /// Get Cluster by clusterId using async
        /// </summary>
        /// <param name="clusterId"></param>
        /// <returns></returns>
        Task<Cluster> GetClusterByIdAsync(Guid clusterId);

        /// <summary>
        /// Insert/Update Cluster entity
        /// </summary>
        /// <param name="cluster"></param>
        /// <returns></returns>
        bool SaveOrUpdateClusterDetails(Cluster cluster);

        /// <summary>
        /// Delete a record by clusterId
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