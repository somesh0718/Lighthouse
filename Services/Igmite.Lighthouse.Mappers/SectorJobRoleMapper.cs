using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class SectorJobRoleMapper
    {
        public static SectorJobRoleModel ToModel(this SectorJobRole sectorJobRole)
        {
            if (sectorJobRole == null)
                return null;

            SectorJobRoleModel sectorJobRoleModel = new SectorJobRoleModel
            {
                SectorJobRoleId = sectorJobRole.SectorJobRoleId,
                SectorId = sectorJobRole.SectorId,
                JobRoleId = sectorJobRole.JobRoleId,
                QPCode = sectorJobRole.QPCode,
                Remarks = sectorJobRole.Remarks,
                CreatedBy = sectorJobRole.CreatedBy,
                CreatedOn = sectorJobRole.CreatedOn,
                UpdatedBy = sectorJobRole.UpdatedBy,
                UpdatedOn = sectorJobRole.UpdatedOn,
                IsActive = sectorJobRole.IsActive
            };

            //sectorJobRole.VTPSectors.ForEach((vtpSector) => sectorJobRoleModel.VTPSectorModels.Add(vtpSector.ToModel()));

            return sectorJobRoleModel;
        }

        public static SectorJobRole FromModel(this SectorJobRoleModel sectorJobRoleModel, SectorJobRole sectorJobRole)
        {
            sectorJobRole.SectorJobRoleId = sectorJobRoleModel.SectorJobRoleId;
            sectorJobRole.SectorId = sectorJobRoleModel.SectorId;
            sectorJobRole.JobRoleId = sectorJobRoleModel.JobRoleId;
            sectorJobRole.QPCode = sectorJobRoleModel.QPCode;
            sectorJobRole.Remarks = sectorJobRoleModel.Remarks;
            sectorJobRole.IsActive = sectorJobRoleModel.IsActive;
            sectorJobRole.RequestType = sectorJobRoleModel.RequestType;
            sectorJobRole.SetAuditValues(sectorJobRoleModel.RequestType);

            //// Handling multiple sectorJobRole vtpSectors
            //foreach (var vtpSectorModel in sectorJobRoleModel.VTPSectorModels)
            //{
            //    VTPSector vtpSector = sectorJobRole.VTPSectors.FirstOrDefault(f => f.VTPSectorId == vtpSectorModel.VTPSectorId);
            //    if (vtpSector == null || sectorJobRoleModel.RequestType == RequestType.New)
            //    {
            //        vtpSector = new VTPSector();
            //        vtpSector.VTPSectorId = Guid.NewGuid();
            //        vtpSector.SectorJobRoleId = sectorJobRole.SectorJobRoleId;
            //    }
            //    vtpSector = vtpSectorModel.FromModel(vtpSector);
            //    vtpSector.SetAuditValues(sectorJobRoleModel.RequestType);

            //    sectorJobRole.VTPSectors.Add(vtpSector);
            //}

            return sectorJobRole;
        }
    }
}