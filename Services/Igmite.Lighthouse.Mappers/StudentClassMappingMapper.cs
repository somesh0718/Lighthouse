using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class StudentClassMappingMapper
    {
        public static StudentClassMappingModel ToModel(this StudentClassMapping studentClass)
        {
            if (studentClass == null)
                return null;

            StudentClassMappingModel studentClassModel = new StudentClassMappingModel
            {
                StudentClassMappingId = studentClass.StudentClassMappingId,
                AcademicYearId = studentClass.AcademicYearId,
                SchoolId = studentClass.SchoolId,
                ClassId = studentClass.ClassId,
                SectionId = studentClass.SectionId,
                VTId = studentClass.VTId,
                StudentId = studentClass.StudentId,
                StudentRollNumber = studentClass.StudentRollNumber,
                CreatedBy = studentClass.CreatedBy,
                CreatedOn = studentClass.CreatedOn,
                UpdatedBy = studentClass.UpdatedBy,
                UpdatedOn = studentClass.UpdatedOn,
                IsActive = studentClass.IsActive
            };

            return studentClassModel;
        }

        public static StudentClassMapping FromModel(this StudentClassMappingModel studentClassModel, StudentClassMapping studentClass)
        {
            studentClass.StudentClassMappingId = studentClassModel.StudentClassMappingId;
            studentClass.AcademicYearId = studentClassModel.AcademicYearId;
            studentClass.SchoolId = studentClassModel.SchoolId;
            studentClass.ClassId = studentClassModel.ClassId;
            studentClass.SectionId = studentClassModel.SectionId;
            studentClass.VTId = studentClassModel.VTId;
            studentClass.StudentId = studentClassModel.StudentId;
            studentClass.StudentRollNumber = studentClassModel.StudentRollNumber;
            studentClass.RequestType = studentClassModel.RequestType;
            studentClass.SetAuditValues(studentClassModel.RequestType);

            return studentClass;
        }
    }
}