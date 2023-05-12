using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class StudentClassDetailMapper
    {
        public static StudentClassDetailModel ToModel(this StudentClassDetail studentClassDetail)
        {
            if (studentClassDetail == null)
                return null;

            StudentClassDetailModel studentClassDetailModel = new StudentClassDetailModel
            {
                StudentId = studentClassDetail.StudentId,
                SectorId = studentClassDetail.SectorId,
                JobRoleId = studentClassDetail.JobRoleId,
                FatherName = studentClassDetail.FatherName,
                MotherName = studentClassDetail.MotherName,
                GuardianName = studentClassDetail.GuardianName,
                DateOfBirth = studentClassDetail.DateOfBirth,
                AadhaarNumber = studentClassDetail.AadhaarNumber,
                StudentRollNumber = studentClassDetail.StudentRollNumber,
                SocialCategory = studentClassDetail.SocialCategory,
                Religion = studentClassDetail.Religion,
                CWSNStatus = studentClassDetail.CWSNStatus,
                Mobile = studentClassDetail.Mobile,
                Mobile1 = studentClassDetail.Mobile1,
                WhatsAppNo = studentClassDetail.WhatsAppNo,
                AssessmentConducted = studentClassDetail.AssessmentConducted,
                StreamId = studentClassDetail.StreamId,
                IsStudentVE9And10 = studentClassDetail.IsStudentVE9And10,
                IsSameStudentTrade = studentClassDetail.IsSameStudentTrade,
                CreatedBy = studentClassDetail.CreatedBy,
                CreatedOn = studentClassDetail.CreatedOn,
                UpdatedBy = studentClassDetail.UpdatedBy,
                UpdatedOn = studentClassDetail.UpdatedOn,
                IsActive = studentClassDetail.IsActive
            };

            return studentClassDetailModel;
        }

        public static StudentClassDetail FromModel(this StudentClassDetailModel studentClassDetailModel, StudentClassDetail studentClassDetail)
        {
            studentClassDetail.StudentId = studentClassDetailModel.StudentId;
            studentClassDetail.SectorId = studentClassDetailModel.SectorId;
            studentClassDetail.JobRoleId = studentClassDetailModel.JobRoleId;
            studentClassDetail.FatherName = studentClassDetailModel.FatherName;
            studentClassDetail.MotherName = studentClassDetailModel.MotherName;
            studentClassDetail.GuardianName = studentClassDetailModel.GuardianName;
            studentClassDetail.DateOfBirth = studentClassDetailModel.DateOfBirth;
            studentClassDetail.AadhaarNumber = studentClassDetailModel.AadhaarNumber;
            studentClassDetail.StudentRollNumber = studentClassDetailModel.StudentRollNumber;
            studentClassDetail.SocialCategory = studentClassDetailModel.SocialCategory;
            studentClassDetail.Religion = studentClassDetailModel.Religion;
            studentClassDetail.CWSNStatus = studentClassDetailModel.CWSNStatus;
            studentClassDetail.Mobile = studentClassDetailModel.Mobile;
            studentClassDetail.Mobile1 = studentClassDetailModel.Mobile1;
            studentClassDetail.WhatsAppNo = studentClassDetailModel.WhatsAppNo;
            studentClassDetail.AssessmentConducted = studentClassDetailModel.AssessmentConducted;
            studentClassDetail.StreamId = studentClassDetailModel.StreamId;
            studentClassDetail.IsStudentVE9And10 = studentClassDetailModel.IsStudentVE9And10;
            studentClassDetail.IsSameStudentTrade = studentClassDetailModel.IsSameStudentTrade;
            studentClassDetail.IsActive = studentClassDetailModel.IsActive;
            studentClassDetail.RequestType = studentClassDetailModel.RequestType;
            studentClassDetail.SetAuditValues(studentClassDetailModel.RequestType);

            return studentClassDetail;
        }
    }
}