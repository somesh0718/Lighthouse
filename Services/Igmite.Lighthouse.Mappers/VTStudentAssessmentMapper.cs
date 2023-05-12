using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class VTStudentAssessmentMapper
    {
        public static VTStudentAssessmentModel ToModel(this VTStudentAssessment vtStudentAssessment)
        {
            if (vtStudentAssessment == null)
                return null;

            VTStudentAssessmentModel vtStudentAssessmentModel = new VTStudentAssessmentModel
            {
                VTStudentAssessmentId = vtStudentAssessment.VTStudentAssessmentId,
                VTClassId = vtStudentAssessment.VTClassId,
                TestimonialType = vtStudentAssessment.TestimonialType,
                StudentName = vtStudentAssessment.StudentName,
                StudentGender = vtStudentAssessment.StudentGender,
                StudentPhoto = vtStudentAssessment.StudentPhoto,
                OJTCompany = vtStudentAssessment.OJTCompany,
                OJTCompanyAddress = vtStudentAssessment.OJTCompanyAddress,
                OJTFieldSuperName = vtStudentAssessment.OJTFieldSuperName,
                OJTFieldSuperMobile = vtStudentAssessment.OJTFieldSuperMobile,
                OJTFieldSuperEmail = vtStudentAssessment.OJTFieldSuperEmail,
                GroupPhoto = vtStudentAssessment.GroupPhoto,
                TestimonialTitle = vtStudentAssessment.TestimonialTitle,
                TestimonialDetails = vtStudentAssessment.TestimonialDetails,
                CreatedBy = vtStudentAssessment.CreatedBy,
                CreatedOn = vtStudentAssessment.CreatedOn,
                UpdatedBy = vtStudentAssessment.UpdatedBy,
                UpdatedOn = vtStudentAssessment.UpdatedOn,
                IsActive = vtStudentAssessment.IsActive
            };

            return vtStudentAssessmentModel;
        }
        public static VTStudentAssessment FromModel(this VTStudentAssessmentModel vtStudentAssessmentModel, VTStudentAssessment vtStudentAssessment)
        {
            vtStudentAssessment.VTStudentAssessmentId = vtStudentAssessmentModel.VTStudentAssessmentId;
            vtStudentAssessment.VTClassId = vtStudentAssessmentModel.VTClassId;
            vtStudentAssessment.TestimonialType = vtStudentAssessmentModel.TestimonialType;
            vtStudentAssessment.StudentName = vtStudentAssessmentModel.StudentName;
            vtStudentAssessment.StudentGender = vtStudentAssessmentModel.StudentGender;
            vtStudentAssessment.StudentPhoto = vtStudentAssessmentModel.StudentPhoto;
            vtStudentAssessment.OJTCompany = vtStudentAssessmentModel.OJTCompany;
            vtStudentAssessment.OJTCompanyAddress = vtStudentAssessmentModel.OJTCompanyAddress;
            vtStudentAssessment.OJTFieldSuperName = vtStudentAssessmentModel.OJTFieldSuperName;
            vtStudentAssessment.OJTFieldSuperMobile = vtStudentAssessmentModel.OJTFieldSuperMobile;
            vtStudentAssessment.OJTFieldSuperEmail = vtStudentAssessmentModel.OJTFieldSuperEmail;
            vtStudentAssessment.GroupPhoto = vtStudentAssessmentModel.GroupPhoto;
            vtStudentAssessment.TestimonialTitle = vtStudentAssessmentModel.TestimonialTitle;
            vtStudentAssessment.TestimonialDetails = vtStudentAssessmentModel.TestimonialDetails;
            vtStudentAssessment.IsActive = vtStudentAssessmentModel.IsActive;
            vtStudentAssessment.RequestType = vtStudentAssessmentModel.RequestType;
            vtStudentAssessment.SetAuditValues(vtStudentAssessmentModel.RequestType);

            return vtStudentAssessment;
        }
    }
}
