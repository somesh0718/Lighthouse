using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class CourseMaterialMapper
    {
        public static CourseMaterialModel ToModel(this CourseMaterial courseMaterial)
        {
            if (courseMaterial == null)
                return null;

            CourseMaterialModel courseMaterialModel = new CourseMaterialModel
            {
                CourseMaterialId = courseMaterial.CourseMaterialId,
                VTId = courseMaterial.VTId,
                AcademicYearId = courseMaterial.AcademicYearId,
                ClassId = courseMaterial.ClassId,
                ReceiptDate = courseMaterial.ReceiptDate,
                Details = courseMaterial.Details,
                CMStatus = courseMaterial.CMStatus,
                CreatedBy = courseMaterial.CreatedBy,
                CreatedOn = courseMaterial.CreatedOn,
                UpdatedBy = courseMaterial.UpdatedBy,
                UpdatedOn = courseMaterial.UpdatedOn,
                IsActive = courseMaterial.IsActive
            };

            return courseMaterialModel;
        }

        public static CourseMaterial FromModel(this CourseMaterialModel courseMaterialModel, CourseMaterial courseMaterial)
        {
            courseMaterial.VTId = courseMaterialModel.VTId;
            courseMaterial.AcademicYearId = courseMaterialModel.AcademicYearId;
            courseMaterial.ClassId = courseMaterialModel.ClassId;
            courseMaterial.ReceiptDate = courseMaterialModel.ReceiptDate;
            courseMaterial.Details = courseMaterialModel.Details;
            courseMaterial.CMStatus = courseMaterialModel.CMStatus;
            courseMaterial.IsActive = courseMaterialModel.IsActive;
            courseMaterial.RequestType = courseMaterialModel.RequestType;
            courseMaterial.SetAuditValues(courseMaterialModel.RequestType);

            return courseMaterial;
        }
    }
}