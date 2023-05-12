using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class VTGuestLectureConductedMapper
    {
        public static VTGuestLectureConductedModel ToModel(this VTGuestLectureConducted vtGuestLectureConducted)
        {
            if (vtGuestLectureConducted == null)
                return null;

            VTGuestLectureConductedModel vtGuestLectureConductedModel = new VTGuestLectureConductedModel
            {
                VTGuestLectureId = vtGuestLectureConducted.VTGuestLectureId,
                VTSchoolSectorId = vtGuestLectureConducted.VTSchoolSectorId,
                VTId = vtGuestLectureConducted.VTId,
                ClassTaughtId = vtGuestLectureConducted.ClassTaughtId,
                SectionTaughtId = vtGuestLectureConducted.SectionTaughtId,
                SectionIds = vtGuestLectureConducted.SectionTaughtId,
                ReportingDate = vtGuestLectureConducted.ReportingDate,
                GLType = vtGuestLectureConducted.GLType,
                GLTopic = vtGuestLectureConducted.GLTopic,
                ClassTime = vtGuestLectureConducted.ClassTime,
                GLMethodologyDetails = vtGuestLectureConducted.GLMethodologyDetails,
                GLPhotoInClass = vtGuestLectureConducted.GLPhotoInClass,
                GLConductedBy = vtGuestLectureConducted.GLConductedBy,
                GLPersonDetails = vtGuestLectureConducted.GLPersonDetails,
                GLName = vtGuestLectureConducted.GLName,
                GLMobile = vtGuestLectureConducted.GLMobile,
                GLEmail = vtGuestLectureConducted.GLEmail,
                GLQualification = vtGuestLectureConducted.GLQualification,
                GLWorkExperience = vtGuestLectureConducted.GLWorkExperience,
                GLAddress = vtGuestLectureConducted.GLAddress,
                GLWorkStatus = vtGuestLectureConducted.GLWorkStatus,
                GLCompany = vtGuestLectureConducted.GLCompany,
                GLDesignation = vtGuestLectureConducted.GLDesignation,
                GLPhoto = vtGuestLectureConducted.GLPhoto,
                GeoLocation = vtGuestLectureConducted.GeoLocation,
                Latitude = vtGuestLectureConducted.Latitude,
                Longitude = vtGuestLectureConducted.Longitude,
                ApprovalStatus = vtGuestLectureConducted.ApprovalStatus,
                ApprovedDate = vtGuestLectureConducted.ApprovedDate,
                CreatedBy = vtGuestLectureConducted.CreatedBy,
                CreatedOn = vtGuestLectureConducted.CreatedOn,
                UpdatedBy = vtGuestLectureConducted.UpdatedBy,
                UpdatedOn = vtGuestLectureConducted.UpdatedOn,
                IsActive = vtGuestLectureConducted.IsActive
            };

            return vtGuestLectureConductedModel;
        }

        public static VTGuestLectureConducted FromModel(this VTGuestLectureConductedModel vtGuestLectureConductedModel, VTGuestLectureConducted vtGuestLectureConducted)
        {
            vtGuestLectureConducted.VTId = vtGuestLectureConductedModel.VTId;
            vtGuestLectureConducted.ClassTaughtId = vtGuestLectureConductedModel.ClassTaughtId;
            vtGuestLectureConducted.SectionTaughtId = vtGuestLectureConductedModel.SectionIds;
            vtGuestLectureConducted.ReportingDate = vtGuestLectureConductedModel.ReportingDate;
            vtGuestLectureConducted.GLType = vtGuestLectureConductedModel.GLType;
            vtGuestLectureConducted.GLTopic = vtGuestLectureConductedModel.GLTopic;
            vtGuestLectureConducted.ClassTime = vtGuestLectureConductedModel.ClassTime;
            vtGuestLectureConducted.GLMethodologyDetails = vtGuestLectureConductedModel.GLMethodologyDetails;
            vtGuestLectureConducted.GLPhotoInClass = null; // vtGuestLectureConductedModel.GLPhotoInClass;
            vtGuestLectureConducted.GLConductedBy = vtGuestLectureConductedModel.GLConductedBy;
            vtGuestLectureConducted.GLPersonDetails = vtGuestLectureConductedModel.GLPersonDetails;
            vtGuestLectureConducted.GLName = vtGuestLectureConductedModel.GLName;
            vtGuestLectureConducted.GLMobile = vtGuestLectureConductedModel.GLMobile;
            vtGuestLectureConducted.GLEmail = vtGuestLectureConductedModel.GLEmail;
            vtGuestLectureConducted.GLQualification = vtGuestLectureConductedModel.GLQualification;
            vtGuestLectureConducted.GLWorkExperience = vtGuestLectureConductedModel.GLWorkExperience;
            vtGuestLectureConducted.GLAddress = vtGuestLectureConductedModel.GLAddress;
            vtGuestLectureConducted.GLWorkStatus = vtGuestLectureConductedModel.GLWorkStatus;
            vtGuestLectureConducted.GLCompany = vtGuestLectureConductedModel.GLCompany;
            vtGuestLectureConducted.GLDesignation = vtGuestLectureConductedModel.GLDesignation;
            vtGuestLectureConducted.GLPhoto = null;// vtGuestLectureConductedModel.GLPhoto;
            vtGuestLectureConducted.ApprovalStatus = vtGuestLectureConductedModel.ApprovalStatus;
            vtGuestLectureConducted.ApprovedDate = vtGuestLectureConductedModel.ApprovedDate;
            vtGuestLectureConducted.GeoLocation = vtGuestLectureConductedModel.GeoLocation;
            vtGuestLectureConducted.Latitude = vtGuestLectureConductedModel.Latitude;
            vtGuestLectureConducted.Longitude = vtGuestLectureConductedModel.Longitude;
            vtGuestLectureConducted.IsActive = vtGuestLectureConductedModel.IsActive;
            vtGuestLectureConducted.RequestType = vtGuestLectureConductedModel.RequestType;
            vtGuestLectureConducted.SetAuditValues(vtGuestLectureConductedModel.RequestType);

            return vtGuestLectureConducted;
        }
    }
}