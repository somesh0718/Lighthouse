using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class ClusterMapper
    {
        public static ClusterModel ToModel(this Cluster cluster)
        {
            if (cluster == null)
                return null;

            ClusterModel clusterModel = new ClusterModel
            {
                ClusterId = cluster.ClusterId,
                DivisionId = cluster.DivisionId,
                DistrictId = cluster.DistrictId,
                BlockId = cluster.BlockId,
                ClusterName = cluster.ClusterName,
                Description = cluster.Description,
                CreatedBy = cluster.CreatedBy,
                CreatedOn = cluster.CreatedOn,
                UpdatedBy = cluster.UpdatedBy,
                UpdatedOn = cluster.UpdatedOn,
                IsActive = cluster.IsActive
            };

            return clusterModel;
        }

        public static Cluster FromModel(this ClusterModel clusterModel, Cluster cluster)
        {
            cluster.BlockId = clusterModel.BlockId;
            cluster.ClusterName = clusterModel.ClusterName;
            cluster.Description = clusterModel.Description;
            cluster.IsActive = clusterModel.IsActive;
            cluster.RequestType = clusterModel.RequestType;
            cluster.SetAuditValues(clusterModel.RequestType);

            return cluster;
        }
    }
}