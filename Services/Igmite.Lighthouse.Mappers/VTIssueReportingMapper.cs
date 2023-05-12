using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class VTIssueReportingMapper
    {
        public static VTIssueReportingModel ToModel(this VTIssueReporting vtIssueReporting)
        {
            if (vtIssueReporting == null)
                return null;

            VTIssueReportingModel vtIssueReportingModel = new VTIssueReportingModel
            {
                VTIssueReportingId = vtIssueReporting.VTIssueReportingId,
                AcademicYearId = vtIssueReporting.AcademicYearId,
                VTId = vtIssueReporting.VTId,
                IssueMappingId = vtIssueReporting.IssueMappingId,
                IssueReportDate = vtIssueReporting.IssueReportDate,
                MainIssue = vtIssueReporting.MainIssue,
                SubIssue = vtIssueReporting.SubIssue,
                StudentClass = vtIssueReporting.StudentClass,
                Month = vtIssueReporting.Month,
                StudentType = vtIssueReporting.StudentType,
                NoOfStudents = vtIssueReporting.NoOfStudents,
                IssueDetails = vtIssueReporting.IssueDetails,
                GeoLocation = vtIssueReporting.GeoLocation,
                Latitude = vtIssueReporting.Latitude,
                Longitude = vtIssueReporting.Longitude,
                ApprovalStatus = vtIssueReporting.ApprovalStatus,
                ApprovedDate = vtIssueReporting.ApprovedDate,
                Remarks = vtIssueReporting.Remarks,
                CreatedBy = vtIssueReporting.CreatedBy,
                CreatedOn = vtIssueReporting.CreatedOn,
                UpdatedBy = vtIssueReporting.UpdatedBy,
                UpdatedOn = vtIssueReporting.UpdatedOn,
                IsActive = vtIssueReporting.IsActive
            };

            return vtIssueReportingModel;
        }

        public static VTIssueReporting FromModel(this VTIssueReportingModel vtIssueReportingModel, VTIssueReporting vtIssueReporting)
        {
            vtIssueReporting.VTId = vtIssueReportingModel.VTId;
            vtIssueReporting.AcademicYearId = vtIssueReportingModel.AcademicYearId;
            vtIssueReporting.IssueMappingId = vtIssueReporting.IssueMappingId;
            vtIssueReporting.IssueReportDate = vtIssueReportingModel.IssueReportDate;
            vtIssueReporting.MainIssue = vtIssueReportingModel.MainIssue;
            vtIssueReporting.SubIssue = vtIssueReportingModel.SubIssue;
            vtIssueReporting.StudentClass = vtIssueReportingModel.StudentClass;
            vtIssueReporting.Month = vtIssueReportingModel.Month;
            vtIssueReporting.StudentType = vtIssueReportingModel.StudentType;
            vtIssueReporting.NoOfStudents = vtIssueReportingModel.NoOfStudents;
            vtIssueReporting.IssueDetails = vtIssueReportingModel.IssueDetails;
            vtIssueReporting.GeoLocation = vtIssueReportingModel.GeoLocation;
            vtIssueReporting.Latitude = vtIssueReportingModel.Latitude;
            vtIssueReporting.Longitude = vtIssueReportingModel.Longitude;
            vtIssueReporting.Remarks = vtIssueReporting.Remarks;
            vtIssueReporting.IsActive = vtIssueReportingModel.IsActive;
            vtIssueReporting.RequestType = vtIssueReportingModel.RequestType;
            vtIssueReporting.SetAuditValues(vtIssueReportingModel.RequestType);

            return vtIssueReporting;
        }
    }
}