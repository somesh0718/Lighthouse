using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class VTPSectorJobRoleMapper
    {
        public static VTPSectorJobRoleModel ToModel(this VTPSectorJobRole vtpSectorJobRole)
        {
            if (vtpSectorJobRole == null)
                return null;

            VTPSectorJobRoleModel vtpSectorJobRoleModel = new VTPSectorJobRoleModel
            {
                VTPSectorJobRoleId = vtpSectorJobRole.VTPSectorJobRoleId,
                VTPId = vtpSectorJobRole.VTPId,
                SectorId = vtpSectorJobRole.SectorId,
                JobRoleId = vtpSectorJobRole.JobRoleId,
                VTPSectorJobRoleName = vtpSectorJobRole.VTPSectorJobRoleName,
                CreatedBy = vtpSectorJobRole.CreatedBy,
                CreatedOn = vtpSectorJobRole.CreatedOn,
                UpdatedBy = vtpSectorJobRole.UpdatedBy,
                UpdatedOn = vtpSectorJobRole.UpdatedOn,
                IsActive = vtpSectorJobRole.IsActive
            };

            //vtpSectorJobRole.SchoolVTPSectors.ForEach((schoolVTPSector) => vtpSectorJobRoleModel.SchoolVTPSectorModels.Add(schoolVTPSector.ToModel()));

            return vtpSectorJobRoleModel;
        }

        public static VTPSectorJobRole FromModel(this VTPSectorJobRoleModel vtpSectorJobRoleModel, VTPSectorJobRole vtpSectorJobRole)
        {
            vtpSectorJobRole.VTPSectorJobRoleId = vtpSectorJobRoleModel.VTPSectorJobRoleId;
            vtpSectorJobRole.VTPId = vtpSectorJobRoleModel.VTPId;
            vtpSectorJobRole.SectorId = vtpSectorJobRoleModel.SectorId;
            vtpSectorJobRole.JobRoleId = vtpSectorJobRoleModel.JobRoleId;
            vtpSectorJobRole.VTPSectorJobRoleName = vtpSectorJobRoleModel.VTPSectorJobRoleName;
            vtpSectorJobRole.IsActive = vtpSectorJobRoleModel.IsActive;
            vtpSectorJobRole.RequestType = vtpSectorJobRoleModel.RequestType;
            vtpSectorJobRole.SetAuditValues(vtpSectorJobRoleModel.RequestType);

            //// Handling multiple vtpSectorJobRole vtpSectors
            //foreach (var vtpSectorModel in vtpSectorJobRoleModel.SchoolVTPSectorModels)
            //{
            //    SchoolVTPSector vtpSector = vtpSectorJobRole.SchoolVTPSectors.FirstOrDefault(f => f.SchoolVTPSectorId == vtpSectorModel.SchoolVTPSectorId);
            //    if (vtpSector == null || vtpSectorJobRoleModel.RequestType == RequestType.New)
            //    {
            //        vtpSector = new SchoolVTPSector();
            //        vtpSector.SchoolVTPSectorId = Guid.NewGuid();
            //        vtpSector.VTPSectorJobRoleId = vtpSectorJobRole.VTPSectorJobRoleId;
            //    }
            //    vtpSector = vtpSectorModel.FromModel(vtpSector);
            //    vtpSector.SetAuditValues(vtpSectorJobRoleModel.RequestType);

            //    vtpSectorJobRole.SchoolVTPSectors.Add(vtpSector);
            //}

            return vtpSectorJobRole;
        }
    }
}