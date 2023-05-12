using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class VTFieldIndustryVisitConductedMapper
    {
        public static VTFieldIndustryVisitConductedModel ToModel(this VTFieldIndustryVisitConducted vtFieldIndustryVisitConducted)
        {
            if (vtFieldIndustryVisitConducted == null)
                return null;

            VTFieldIndustryVisitConductedModel vtFieldIndustryVisitConductedModel = new VTFieldIndustryVisitConductedModel
            {
                VTFieldIndustryVisitConductedId = vtFieldIndustryVisitConducted.VTFieldIndustryVisitConductedId,
                VTSchoolSectorId = vtFieldIndustryVisitConducted.VTSchoolSectorId,
                VTId = vtFieldIndustryVisitConducted.VTId,
                ClassTaughtId = vtFieldIndustryVisitConducted.ClassTaughtId,
                SectionTaughtId = vtFieldIndustryVisitConducted.SectionTaughtId,
                SectionIds = vtFieldIndustryVisitConducted.SectionTaughtId,
                ReportingDate = vtFieldIndustryVisitConducted.ReportingDate,
                FieldVisitTheme = vtFieldIndustryVisitConducted.FieldVisitTheme,
                FieldVisitActivities = vtFieldIndustryVisitConducted.FieldVisitActivities,
                FVOrganisation = vtFieldIndustryVisitConducted.FVOrganisation,
                FVOrganisationAddress = vtFieldIndustryVisitConducted.FVOrganisationAddress,
                FVDistance = vtFieldIndustryVisitConducted.FVDistance,
                FVPicture = vtFieldIndustryVisitConducted.FVPicture,
                FVContactPersonName = vtFieldIndustryVisitConducted.FVContactPersonName,
                FVContactPersonMobile = vtFieldIndustryVisitConducted.FVContactPersonMobile,
                FVContactPersonEmail = vtFieldIndustryVisitConducted.FVContactPersonEmail,
                FVContactPersonDesignation = vtFieldIndustryVisitConducted.FVContactPersonDesignation,
                FVOrganisationInterestStatus = vtFieldIndustryVisitConducted.FVOrganisationInterestStatus,
                FVOrignisationOJTStatus = vtFieldIndustryVisitConducted.FVOrignisationOJTStatus,
                FeedbackFromOrgnisation = vtFieldIndustryVisitConducted.FeedbackFromOrgnisation,
                Remarks = vtFieldIndustryVisitConducted.Remarks,
                GeoLocation = vtFieldIndustryVisitConducted.GeoLocation,
                Latitude = vtFieldIndustryVisitConducted.Latitude,
                Longitude = vtFieldIndustryVisitConducted.Longitude,
                ApprovalStatus = vtFieldIndustryVisitConducted.ApprovalStatus,
                ApprovedDate = vtFieldIndustryVisitConducted.ApprovedDate,
                CreatedBy = vtFieldIndustryVisitConducted.CreatedBy,
                CreatedOn = vtFieldIndustryVisitConducted.CreatedOn,
                UpdatedBy = vtFieldIndustryVisitConducted.UpdatedBy,
                UpdatedOn = vtFieldIndustryVisitConducted.UpdatedOn,
                IsActive = vtFieldIndustryVisitConducted.IsActive
            };

            return vtFieldIndustryVisitConductedModel;
        }

        public static VTFieldIndustryVisitConducted FromModel(this VTFieldIndustryVisitConductedModel vtFieldIndustryVisitConductedModel, VTFieldIndustryVisitConducted vtFieldIndustryVisitConducted)
        {
            vtFieldIndustryVisitConducted.VTId = vtFieldIndustryVisitConductedModel.VTId;
            vtFieldIndustryVisitConducted.ClassTaughtId = vtFieldIndustryVisitConductedModel.ClassTaughtId;
            vtFieldIndustryVisitConducted.SectionTaughtId = vtFieldIndustryVisitConductedModel.SectionIds;
            vtFieldIndustryVisitConducted.ReportingDate = vtFieldIndustryVisitConductedModel.ReportingDate;
            vtFieldIndustryVisitConducted.FieldVisitTheme = vtFieldIndustryVisitConductedModel.FieldVisitTheme;
            vtFieldIndustryVisitConducted.FieldVisitActivities = vtFieldIndustryVisitConductedModel.FieldVisitActivities;
            vtFieldIndustryVisitConducted.FVOrganisation = vtFieldIndustryVisitConductedModel.FVOrganisation;
            vtFieldIndustryVisitConducted.FVOrganisationAddress = vtFieldIndustryVisitConductedModel.FVOrganisationAddress;
            vtFieldIndustryVisitConducted.FVDistance = vtFieldIndustryVisitConductedModel.FVDistance;
            vtFieldIndustryVisitConducted.FVPicture = null;
            vtFieldIndustryVisitConducted.FVContactPersonName = vtFieldIndustryVisitConductedModel.FVContactPersonName;
            vtFieldIndustryVisitConducted.FVContactPersonMobile = vtFieldIndustryVisitConductedModel.FVContactPersonMobile;
            vtFieldIndustryVisitConducted.FVContactPersonEmail = vtFieldIndustryVisitConductedModel.FVContactPersonEmail;
            vtFieldIndustryVisitConducted.FVContactPersonDesignation = vtFieldIndustryVisitConductedModel.FVContactPersonDesignation;
            vtFieldIndustryVisitConducted.FVOrganisationInterestStatus = vtFieldIndustryVisitConductedModel.FVOrganisationInterestStatus;
            vtFieldIndustryVisitConducted.FVOrignisationOJTStatus = vtFieldIndustryVisitConductedModel.FVOrignisationOJTStatus;
            vtFieldIndustryVisitConducted.FeedbackFromOrgnisation = vtFieldIndustryVisitConductedModel.FeedbackFromOrgnisation;
            vtFieldIndustryVisitConducted.Remarks = vtFieldIndustryVisitConductedModel.Remarks;
            vtFieldIndustryVisitConducted.GeoLocation = vtFieldIndustryVisitConductedModel.GeoLocation;
            vtFieldIndustryVisitConducted.Latitude = vtFieldIndustryVisitConductedModel.Latitude;
            vtFieldIndustryVisitConducted.Longitude = vtFieldIndustryVisitConductedModel.Longitude;
            vtFieldIndustryVisitConducted.ApprovalStatus = vtFieldIndustryVisitConductedModel.ApprovalStatus;
            vtFieldIndustryVisitConducted.ApprovedDate = vtFieldIndustryVisitConductedModel.ApprovedDate;
            vtFieldIndustryVisitConducted.IsActive = vtFieldIndustryVisitConductedModel.IsActive;
            vtFieldIndustryVisitConducted.RequestType = vtFieldIndustryVisitConductedModel.RequestType;
            vtFieldIndustryVisitConducted.SetAuditValues(vtFieldIndustryVisitConductedModel.RequestType);

            return vtFieldIndustryVisitConducted;
        }
    }
}