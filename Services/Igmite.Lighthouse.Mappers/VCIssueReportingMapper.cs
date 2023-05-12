using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class VCIssueReportingMapper
    {
        public static VCIssueReportingModel ToModel(this VCIssueReporting vcIssueReporting)
        {
            if (vcIssueReporting == null)
                return null;

            VCIssueReportingModel vcIssueReportingModel = new VCIssueReportingModel
            {
                VCIssueReportingId = vcIssueReporting.VCIssueReportingId,
                AcademicYearId = vcIssueReporting.AcademicYearId,
                VCId = vcIssueReporting.VCId,
                IssueMappingId = vcIssueReporting.IssueMappingId,
                IssueReportDate = vcIssueReporting.IssueReportDate,
                MainIssue = vcIssueReporting.MainIssue,
                SubIssue = vcIssueReporting.SubIssue,
                StudentClass = vcIssueReporting.StudentClass,
                Month = vcIssueReporting.Month,
                StudentType = vcIssueReporting.StudentType,
                NoOfStudents = vcIssueReporting.NoOfStudents,
                IssueDetails = vcIssueReporting.IssueDetails,
                GeoLocation = vcIssueReporting.GeoLocation,
                Latitude = vcIssueReporting.Latitude,
                Longitude = vcIssueReporting.Longitude,
                ApprovalStatus = vcIssueReporting.ApprovalStatus,
                ApprovedDate = vcIssueReporting.ApprovedDate,
                Remarks = vcIssueReporting.Remarks,
                CreatedBy = vcIssueReporting.CreatedBy,
                CreatedOn = vcIssueReporting.CreatedOn,
                UpdatedBy = vcIssueReporting.UpdatedBy,
                UpdatedOn = vcIssueReporting.UpdatedOn,
                IsActive = vcIssueReporting.IsActive
            };

            return vcIssueReportingModel;
        }

        public static VCIssueReporting FromModel(this VCIssueReportingModel vcIssueReportingModel, VCIssueReporting vcIssueReporting)
        {
            vcIssueReporting.AcademicYearId = vcIssueReportingModel.AcademicYearId;
            vcIssueReporting.VCId = vcIssueReportingModel.VCId;
            vcIssueReporting.IssueMappingId = vcIssueReporting.IssueMappingId;
            vcIssueReporting.IssueReportDate = vcIssueReportingModel.IssueReportDate;
            vcIssueReporting.MainIssue = vcIssueReportingModel.MainIssue;
            vcIssueReporting.SubIssue = vcIssueReportingModel.SubIssue;
            vcIssueReporting.StudentClass = vcIssueReportingModel.StudentClass;
            vcIssueReporting.Month = vcIssueReportingModel.Month;
            vcIssueReporting.StudentType = vcIssueReportingModel.StudentType;
            vcIssueReporting.NoOfStudents = vcIssueReportingModel.NoOfStudents;
            vcIssueReporting.IssueDetails = vcIssueReportingModel.IssueDetails;
            vcIssueReporting.GeoLocation = vcIssueReportingModel.GeoLocation;
            vcIssueReporting.Latitude = vcIssueReportingModel.Latitude;
            vcIssueReporting.Longitude = vcIssueReportingModel.Longitude;            
            vcIssueReporting.Remarks = vcIssueReporting.Remarks;
            vcIssueReporting.IsActive = vcIssueReportingModel.IsActive;
            vcIssueReporting.RequestType = vcIssueReportingModel.RequestType;
            vcIssueReporting.SetAuditValues(vcIssueReportingModel.RequestType);

            return vcIssueReporting;
        }
    }
}