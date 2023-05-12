using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class VTPSectorMapper
    {
        public static VTPSectorModel ToModel(this VTPSector vtpSector)
        {
            if (vtpSector == null)
                return null;

            VTPSectorModel vtpSectorModel = new VTPSectorModel
            {
                VTPSectorId = vtpSector.VTPSectorId,
                AcademicYearId = vtpSector.AcademicYearId,
                VTPId = vtpSector.VTPId,
                SectorId = vtpSector.SectorId,
                Remarks = vtpSector.Remarks,
                CreatedBy = vtpSector.CreatedBy,
                CreatedOn = vtpSector.CreatedOn,
                UpdatedBy = vtpSector.UpdatedBy,
                UpdatedOn = vtpSector.UpdatedOn,
                IsActive = vtpSector.IsActive
            };

            return vtpSectorModel;
        }

        public static VTPSector FromModel(this VTPSectorModel vtpSectorModel, VTPSector vtpSector)
        {
            vtpSector.VTPSectorId = vtpSectorModel.VTPSectorId;
            vtpSector.AcademicYearId = vtpSectorModel.AcademicYearId;
            vtpSector.VTPId = vtpSectorModel.VTPId;
            vtpSector.SectorId = vtpSectorModel.SectorId;
            vtpSector.Remarks = vtpSectorModel.Remarks;
            vtpSector.IsActive = vtpSectorModel.IsActive;
            vtpSector.RequestType = vtpSectorModel.RequestType;
            vtpSector.SetAuditValues(vtpSectorModel.RequestType);

            return vtpSector;
        }
    }
}