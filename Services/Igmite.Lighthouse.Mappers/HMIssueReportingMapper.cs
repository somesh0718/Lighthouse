using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class HMIssueReportingMapper
    {
        public static HMIssueReportingModel ToModel(this HMIssueReporting hmIssueReporting)
        {
            if (hmIssueReporting == null)
                return null;

            HMIssueReportingModel hmIssueReportingModel = new HMIssueReportingModel
            {
                HMIssueReportingId = hmIssueReporting.HMIssueReportingId,
                AcademicYearId = hmIssueReporting.AcademicYearId,
                HMId = hmIssueReporting.HMId,
                IssueMappingId = hmIssueReporting.IssueMappingId,
                IssueReportDate = hmIssueReporting.IssueReportDate,
                MainIssue = hmIssueReporting.MainIssue,
                SubIssue = hmIssueReporting.SubIssue,
                StudentClass = hmIssueReporting.StudentClass,
                Month = hmIssueReporting.Month,
                StudentType = hmIssueReporting.StudentType,
                NoOfStudents = hmIssueReporting.NoOfStudents,
                IssueDetails = hmIssueReporting.IssueDetails,
                GeoLocation = hmIssueReporting.GeoLocation,
                Latitude = hmIssueReporting.Latitude,
                Longitude = hmIssueReporting.Longitude,
                ApprovalStatus = hmIssueReporting.ApprovalStatus,
                ApprovedDate = hmIssueReporting.ApprovedDate,
                Remarks = hmIssueReporting.Remarks,
                CreatedBy = hmIssueReporting.CreatedBy,
                CreatedOn = hmIssueReporting.CreatedOn,
                UpdatedBy = hmIssueReporting.UpdatedBy,
                UpdatedOn = hmIssueReporting.UpdatedOn,
                IsActive = hmIssueReporting.IsActive
            };

            return hmIssueReportingModel;
        }

        public static HMIssueReporting FromModel(this HMIssueReportingModel hmIssueReportingModel, HMIssueReporting hmIssueReporting)
        {
            hmIssueReporting.HMId = hmIssueReportingModel.HMId;
            hmIssueReporting.AcademicYearId = hmIssueReportingModel.AcademicYearId;
            hmIssueReporting.IssueMappingId = hmIssueReporting.IssueMappingId;
            hmIssueReporting.IssueReportDate = hmIssueReportingModel.IssueReportDate;
            hmIssueReporting.MainIssue = hmIssueReportingModel.MainIssue;
            hmIssueReporting.SubIssue = hmIssueReportingModel.SubIssue;
            hmIssueReporting.StudentClass = hmIssueReportingModel.StudentClass;
            hmIssueReporting.Month = hmIssueReportingModel.Month;
            hmIssueReporting.StudentType = hmIssueReportingModel.StudentType;
            hmIssueReporting.NoOfStudents = hmIssueReportingModel.NoOfStudents;
            hmIssueReporting.IssueDetails = hmIssueReportingModel.IssueDetails;
            hmIssueReporting.GeoLocation = hmIssueReportingModel.GeoLocation;
            hmIssueReporting.Latitude = hmIssueReportingModel.Latitude;
            hmIssueReporting.Longitude = hmIssueReportingModel.Longitude;
            hmIssueReporting.Remarks = hmIssueReporting.Remarks;
            hmIssueReporting.IsActive = hmIssueReportingModel.IsActive;
            hmIssueReporting.RequestType = hmIssueReportingModel.RequestType;
            hmIssueReporting.SetAuditValues(hmIssueReportingModel.RequestType);

            return hmIssueReporting;
        }
    }
}