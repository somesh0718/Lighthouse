using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class SchoolVTPSectorMapper
    {
        public static SchoolVTPSectorModel ToModel(this SchoolVTPSector schoolVTPSector)
        {
            if (schoolVTPSector == null)
                return null;

            SchoolVTPSectorModel schoolVTPSectorModel = new SchoolVTPSectorModel
            {
                SchoolVTPSectorId = schoolVTPSector.SchoolVTPSectorId,
                AcademicYearId = schoolVTPSector.AcademicYearId,
                SectorId = schoolVTPSector.SectorId,
                VTPId = schoolVTPSector.VTPId,
                SchoolId = schoolVTPSector.SchoolId,
                Remarks = schoolVTPSector.Remarks,
                CreatedBy = schoolVTPSector.CreatedBy,
                CreatedOn = schoolVTPSector.CreatedOn,
                UpdatedBy = schoolVTPSector.UpdatedBy,
                UpdatedOn = schoolVTPSector.UpdatedOn,
                IsActive = schoolVTPSector.IsActive
            };

            return schoolVTPSectorModel;
        }

        public static SchoolVTPSector FromModel(this SchoolVTPSectorModel schoolVTPSectorModel, SchoolVTPSector schoolVTPSector)
        {
            schoolVTPSector.SchoolVTPSectorId = schoolVTPSectorModel.SchoolVTPSectorId;
            schoolVTPSector.AcademicYearId = schoolVTPSectorModel.AcademicYearId;
            schoolVTPSector.SectorId = schoolVTPSectorModel.SectorId;
            schoolVTPSector.VTPId = schoolVTPSectorModel.VTPId;
            schoolVTPSector.SchoolId = schoolVTPSectorModel.SchoolId;
            schoolVTPSector.Remarks = schoolVTPSectorModel.Remarks;
            schoolVTPSector.IsActive = schoolVTPSectorModel.IsActive;
            schoolVTPSector.RequestType = schoolVTPSectorModel.RequestType;
            schoolVTPSector.SetAuditValues(schoolVTPSectorModel.RequestType);

            return schoolVTPSector;
        }
    }
}