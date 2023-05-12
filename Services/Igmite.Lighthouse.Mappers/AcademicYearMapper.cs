using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class AcademicYearMapper
    {
        public static AcademicYearModel ToModel(this AcademicYear academicYear)
        {
            if (academicYear == null)
                return null;

            AcademicYearModel academicYearModel = new AcademicYearModel
            {
                AcademicYearId = academicYear.AcademicYearId,
                PhaseId = academicYear.PhaseId,
                YearName = academicYear.YearName,
                Description = academicYear.Description,
                IsCurrentAcademicYear = academicYear.IsCurrentAcademicYear,
                CreatedBy = academicYear.CreatedBy,
                CreatedOn = academicYear.CreatedOn,
                UpdatedBy = academicYear.UpdatedBy,
                UpdatedOn = academicYear.UpdatedOn,
                IsActive = academicYear.IsActive
            };

            return academicYearModel;
        }

        public static AcademicYear FromModel(this AcademicYearModel academicYearModel, AcademicYear academicYear)
        {
            academicYear.AcademicYearId = academicYearModel.AcademicYearId;
            academicYear.PhaseId = academicYearModel.PhaseId;
            academicYear.YearName = academicYearModel.YearName;
            academicYear.Description = academicYearModel.Description;
            academicYear.IsCurrentAcademicYear = academicYearModel.IsCurrentAcademicYear;
            academicYear.IsActive = academicYearModel.IsActive;
            academicYear.RequestType = academicYearModel.RequestType;
            academicYear.SetAuditValues(academicYearModel.RequestType);

            return academicYear;
        }
    }
}