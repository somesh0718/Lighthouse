using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// Repository of the Cluster entity
    /// </summary>
    public class ClusterRepository : GenericRepository<Cluster>, IClusterRepository
    {
        /// <summary>
        /// Get list of Cluster
        /// </summary>
        /// <returns></returns>
        public IQueryable<Cluster> GetClusters()
        {
            return this.Context.Clusters.AsQueryable();
        }

        /// <summary>
        /// Get list of Cluster by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<Cluster> GetClustersByName(string name)
        {
            var clusters = (from d in this.Context.Clusters
                            where d.ClusterName.Contains(name)
                            select d).AsQueryable();

            return clusters;
        }

        /// <summary>
        /// Get Cluster by clusterId
        /// </summary>
        /// <param name="clusterId"></param>
        /// <returns></returns>
        public Cluster GetClusterById(Guid clusterId)
        {
            var cluster = (from c in this.Context.Clusters
                           join b in this.Context.Blocks on c.BlockId equals b.BlockId
                           join d in this.Context.Districts on b.DistrictId equals d.DistrictCode
                           where c.ClusterId == clusterId
                           select new Cluster
                           {
                               ClusterId = c.ClusterId,
                               DivisionId = d.DivisionId,
                               DistrictId = b.DistrictId,
                               BlockId = c.BlockId,
                               ClusterName = c.ClusterName,
                               Description = c.Description,
                               CreatedBy = c.CreatedBy,
                               CreatedOn = c.CreatedOn,
                               UpdatedBy = c.UpdatedBy,
                               UpdatedOn = c.UpdatedOn,
                               IsActive = c.IsActive
                           }).FirstOrDefault();

            return cluster;
        }

        /// <summary>
        /// Get Cluster by clusterId using async
        /// </summary>
        /// <param name="clusterId"></param>
        /// <returns></returns>
        public async Task<Cluster> GetClusterByIdAsync(Guid clusterId)
        {
            var cluster = await (from d in this.Context.Clusters
                                 where d.ClusterId == clusterId
                                 select d).FirstOrDefaultAsync();

            return cluster;
        }

        /// <summary>
        /// Insert/Update Cluster entity
        /// </summary>
        /// <param name="cluster"></param>
        /// <returns></returns>
        public bool SaveOrUpdateClusterDetails(Cluster cluster)
        {
            if (RequestType.New == cluster.RequestType)
                Context.Clusters.Add(cluster);
            else
            {
                Context.Entry<Cluster>(cluster).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a record by ClusterCode
        /// </summary>
        /// <param name="clusterCode"></param>
        /// <returns></returns>
        public bool DeleteById(Guid clusterId)
        {
            Cluster cluster = this.Context.Clusters.FirstOrDefault(d => d.ClusterId == clusterId);

            if (cluster != null)
            {
                Context.Entry<Cluster>(cluster).State = EntityState.Deleted;

                Context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check duplicate Cluster by Name
        /// </summary>
        /// <param name="clusterModel"></param>
        /// <returns></returns>
        public bool CheckClusterExistByName(ClusterModel clusterModel)
        {
            Cluster cluster = this.Context.Clusters.FirstOrDefault(d => d.ClusterName == clusterModel.ClusterName);

            return cluster != null;
        }

        /// <summary>}
        /// List of Cluster with filter criteria}
        /// </summary>}
        /// <param name="searchModel"></param>}
        /// <returns></returns>}
        public IList<ClusterViewModel> GetClustersByCriteria(SearchClusterModel searchModel)
        {
            MySqlParameter[] sqlParams = new MySqlParameter[4];
            sqlParams[0] = new MySqlParameter { ParameterName = "name", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.Name.StringVal() };
            sqlParams[1] = new MySqlParameter { ParameterName = "charBy", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.CharBy.StringVal() };
            sqlParams[2] = new MySqlParameter { ParameterName = "pageIndex", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageIndex.StringVal() };
            sqlParams[3] = new MySqlParameter { ParameterName = "pageSize", MySqlDbType = MySqlDbType.VarChar, Value = searchModel.PageSize.StringVal() };

            return Context.ClusterViewModels.FromSql<ClusterViewModel>("CALL GetClustersByCriteria (@name, @charBy, @pageIndex, @pageSize)", sqlParams).ToList();
        }
    }
}