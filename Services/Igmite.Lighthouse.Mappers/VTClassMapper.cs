using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class VTClassMapper
    {
        public static VTClassModel ToModel(this VTClass vtClass)
        {
            if (vtClass == null)
                return null;

            VTClassModel vtClassModel = new VTClassModel
            {
                VTClassId = vtClass.VTClassId,
                AcademicYearId = vtClass.AcademicYearId,
                VTId = vtClass.VTId,
                SchoolId = vtClass.SchoolId,
                ClassId = vtClass.ClassId,
                SectionId = vtClass.SectionId,
                CreatedBy = vtClass.CreatedBy,
                CreatedOn = vtClass.CreatedOn,
                UpdatedBy = vtClass.UpdatedBy,
                UpdatedOn = vtClass.UpdatedOn,
                IsActive = vtClass.IsActive
            };

            return vtClassModel;
        }

        public static VTClass FromModel(this VTClassModel vtClassModel, VTClass vtClass)
        {
            vtClass.VTClassId = vtClassModel.VTClassId;
            vtClass.AcademicYearId = vtClassModel.AcademicYearId;
            vtClass.VTId = vtClassModel.VTId;
            vtClass.ClassId = vtClassModel.ClassId;
            vtClass.SectionId = null;
            vtClass.IsActive = vtClassModel.IsActive;
            vtClass.RequestType = vtClassModel.RequestType;
            vtClass.SetAuditValues(vtClassModel.RequestType);

            return vtClass;
        }
    }
}