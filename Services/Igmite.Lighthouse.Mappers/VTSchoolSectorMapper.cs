using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class VTSchoolSectorMapper
    {
        public static VTSchoolSectorModel ToModel(this VTSchoolSector vtSchoolSector)
        {
            if (vtSchoolSector == null)
                return null;

            VTSchoolSectorModel vtSchoolSectorModel = new VTSchoolSectorModel
            {
                VTSchoolSectorId = vtSchoolSector.VTSchoolSectorId,
                AcademicYearId = vtSchoolSector.AcademicYearId,
                VTId = vtSchoolSector.VTId,
                SchoolId = vtSchoolSector.SchoolId,
                SectorId = vtSchoolSector.SectorId,
                JobRoleId = vtSchoolSector.JobRoleId,
                DateOfAllocation = vtSchoolSector.DateOfAllocation,
                DateOfRemoval = vtSchoolSector.DateOfRemoval,
                CreatedBy = vtSchoolSector.CreatedBy,
                CreatedOn = vtSchoolSector.CreatedOn,
                UpdatedBy = vtSchoolSector.UpdatedBy,
                UpdatedOn = vtSchoolSector.UpdatedOn,
                IsActive = vtSchoolSector.IsActive
            };

            return vtSchoolSectorModel;
        }

        public static VTSchoolSector FromModel(this VTSchoolSectorModel vtSchoolSectorModel, VTSchoolSector vtSchoolSector)
        {
            vtSchoolSector.VTSchoolSectorId = vtSchoolSectorModel.VTSchoolSectorId;
            vtSchoolSector.AcademicYearId = vtSchoolSectorModel.AcademicYearId;
            vtSchoolSector.VTId = vtSchoolSectorModel.VTId;
            vtSchoolSector.SchoolId = vtSchoolSectorModel.SchoolId;
            vtSchoolSector.SectorId = vtSchoolSectorModel.SectorId;
            vtSchoolSector.JobRoleId = vtSchoolSectorModel.JobRoleId;
            vtSchoolSector.DateOfAllocation = vtSchoolSectorModel.DateOfAllocation;
            vtSchoolSector.DateOfRemoval = vtSchoolSectorModel.DateOfRemoval;
            vtSchoolSector.IsActive = vtSchoolSectorModel.IsActive;
            vtSchoolSector.RequestType = vtSchoolSectorModel.RequestType;
            vtSchoolSector.SetAuditValues(vtSchoolSectorModel.RequestType);

            return vtSchoolSector;
        }
    }
}