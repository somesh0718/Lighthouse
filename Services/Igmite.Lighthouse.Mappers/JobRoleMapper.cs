using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class JobRoleMapper
    {
        public static JobRoleModel ToModel(this JobRole jobRole)
        {
            if (jobRole == null)
                return null;

            JobRoleModel jobRoleModel = new JobRoleModel
            {
                JobRoleId = jobRole.JobRoleId,
                SectorId = jobRole.SectorId,
                JobRoleName = jobRole.JobRoleName,
                QPCode = jobRole.QPCode,
                DisplayOrder = jobRole.DisplayOrder,
                Remarks = jobRole.Remarks,
                CreatedBy = jobRole.CreatedBy,
                CreatedOn = jobRole.CreatedOn,
                UpdatedBy = jobRole.UpdatedBy,
                UpdatedOn = jobRole.UpdatedOn,
                IsActive = jobRole.IsActive
            };

            return jobRoleModel;
        }

        public static JobRole FromModel(this JobRoleModel jobRoleModel, JobRole jobRole)
        {
            jobRole.JobRoleId = jobRoleModel.JobRoleId;
            jobRole.SectorId = jobRoleModel.SectorId;
            jobRole.JobRoleName = jobRoleModel.JobRoleName;
            jobRole.QPCode = jobRoleModel.QPCode;
            jobRole.DisplayOrder = jobRoleModel.DisplayOrder;
            jobRole.Remarks = jobRoleModel.Remarks;
            jobRole.IsActive = jobRoleModel.IsActive;
            jobRole.RequestType = jobRoleModel.RequestType;
            jobRole.SetAuditValues(jobRoleModel.RequestType);

            return jobRole;
        }
    }
}