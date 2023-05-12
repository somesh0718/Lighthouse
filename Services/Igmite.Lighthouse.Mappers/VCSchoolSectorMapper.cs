using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class VCSchoolSectorMapper
    {
        public static VCSchoolSectorModel ToModel(this VCSchoolSector vcSchoolSector)
        {
            if (vcSchoolSector == null)
                return null;

            VCSchoolSectorModel vcSchoolSectorModel = new VCSchoolSectorModel
            {
                VCSchoolSectorId = vcSchoolSector.VCSchoolSectorId,
                AcademicYearId = vcSchoolSector.AcademicYearId,
                VCId = vcSchoolSector.VCId,
                SchoolVTPSectorId = vcSchoolSector.SchoolVTPSectorId,
                DateOfAllocation = vcSchoolSector.DateOfAllocation,
                DateOfRemoval = vcSchoolSector.DateOfRemoval,
                CreatedBy = vcSchoolSector.CreatedBy,
                CreatedOn = vcSchoolSector.CreatedOn,
                UpdatedBy = vcSchoolSector.UpdatedBy,
                UpdatedOn = vcSchoolSector.UpdatedOn,
                IsActive = vcSchoolSector.IsActive
            };

            return vcSchoolSectorModel;
        }

        public static VCSchoolSector FromModel(this VCSchoolSectorModel vcSchoolSectorModel, VCSchoolSector vcSchoolSector)
        {
            vcSchoolSector.VCSchoolSectorId = vcSchoolSectorModel.VCSchoolSectorId;
            vcSchoolSector.AcademicYearId = vcSchoolSectorModel.AcademicYearId;
            vcSchoolSector.VCId = vcSchoolSectorModel.VCId;
            vcSchoolSector.SchoolVTPSectorId = vcSchoolSectorModel.SchoolVTPSectorId;
            vcSchoolSector.DateOfAllocation = vcSchoolSectorModel.DateOfAllocation;
            vcSchoolSector.DateOfRemoval = vcSchoolSectorModel.DateOfRemoval;
            vcSchoolSector.IsActive = vcSchoolSectorModel.IsActive;
            vcSchoolSector.RequestType = vcSchoolSectorModel.RequestType;
            vcSchoolSector.SetAuditValues(vcSchoolSectorModel.RequestType);

            return vcSchoolSector;
        }
    }
}