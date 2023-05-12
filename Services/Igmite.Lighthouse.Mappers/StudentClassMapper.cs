using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;

namespace Igmite.Lighthouse.Mappers
{
    public static class StudentClassMapper
    {
        public static StudentClassModel ToModel(this StudentClass studentClass)
        {
            if (studentClass == null)
                return null;

            StudentClassModel studentClassModel = new StudentClassModel
            {
                StudentId = studentClass.StudentId,
                VTId = studentClass.VTId,
                SchoolId = studentClass.SchoolId,
                AcademicYearId = studentClass.AcademicYearId,
                ClassId = studentClass.ClassId,
                SectionId = studentClass.SectionId,
                DateOfEnrollment = studentClass.DateOfEnrollment,
                FirstName = studentClass.FirstName,
                MiddleName = studentClass.MiddleName,
                LastName = studentClass.LastName,
                FullName = string.Format("{0} {1} {2}", studentClass.FirstName, studentClass.MiddleName, studentClass.LastName).TrimSpaces(),
                Gender = studentClass.Gender,
                Mobile = studentClass.Mobile,
                DateOfDropout = studentClass.DateOfDropout,
                DropoutReason = studentClass.DropoutReason,
                CreatedBy = studentClass.CreatedBy,
                CreatedOn = studentClass.CreatedOn,
                UpdatedBy = studentClass.UpdatedBy,
                UpdatedOn = studentClass.UpdatedOn,
                IsActive = studentClass.IsActive
            };

            return studentClassModel;
        }

        public static StudentClass FromModel(this StudentClassModel studentClassModel, StudentClass studentClass)
        {
            studentClass.VTId = studentClassModel.VTId;
            studentClass.AcademicYearId = studentClassModel.AcademicYearId;
            studentClass.ClassId = studentClassModel.ClassId;
            studentClass.SectionId = studentClassModel.SectionId;
            studentClass.DateOfEnrollment = studentClassModel.DateOfEnrollment;
            studentClass.FirstName = studentClassModel.FirstName;
            studentClass.MiddleName = studentClassModel.MiddleName;
            studentClass.LastName = studentClassModel.LastName;
            studentClass.FullName = string.Format("{0} {1} {2}", studentClassModel.FirstName, studentClassModel.MiddleName, studentClassModel.LastName).TrimSpaces();
            studentClass.Gender = studentClassModel.Gender;
            studentClass.Mobile = studentClassModel.Mobile;
            studentClass.DateOfDropout = studentClassModel.DateOfDropout;
            studentClass.DropoutReason = studentClassModel.DropoutReason;
            studentClass.IsActive = (studentClassModel.DateOfDropout.HasValue) ? false : studentClassModel.IsActive;
            studentClass.RequestType = studentClassModel.RequestType;
            studentClass.SetAuditValues(studentClassModel.RequestType);

            return studentClass;
        }
    }
}