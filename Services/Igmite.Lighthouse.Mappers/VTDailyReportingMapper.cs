using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class VTDailyReportingMapper
    {
        public static VTDailyReportingModel ToModel(this VTDailyReporting vtDailyReporting)
        {
            if (vtDailyReporting == null)
                return null;

            VTDailyReportingModel vtDailyReportingModel = new VTDailyReportingModel
            {
                VTDailyReportingId = vtDailyReporting.VTDailyReportingId,
                VTSchoolSectorId = vtDailyReporting.VTSchoolSectorId,
                VTId = vtDailyReporting.VTId,
                ReportingDate = vtDailyReporting.ReportingDate,
                ReportType = vtDailyReporting.ReportType,
                SchoolEventCelebration = vtDailyReporting.SchoolEventCelebration,
                WorkAssignedByHeadMaster = vtDailyReporting.WorkAssignedByHeadMaster,
                SchoolExamDuty = vtDailyReporting.SchoolExamDuty,
                OtherWork = vtDailyReporting.OtherWork,
                ObservationDetails = vtDailyReporting.ObservationDetails,
                OBStudentCount = vtDailyReporting.OBStudentCount,
                GeoLocation = vtDailyReporting.GeoLocation,
                Latitude = vtDailyReporting.Latitude,
                Longitude = vtDailyReporting.Longitude,
                ApprovalStatus = vtDailyReporting.ApprovalStatus,
                ApprovedDate = vtDailyReporting.ApprovedDate,
                CreatedBy = vtDailyReporting.CreatedBy,
                CreatedOn = vtDailyReporting.CreatedOn,
                UpdatedBy = vtDailyReporting.UpdatedBy,
                UpdatedOn = vtDailyReporting.UpdatedOn,
                IsActive = vtDailyReporting.IsActive
            };

            return vtDailyReportingModel;
        }

        public static VTDailyReporting FromModel(this VTDailyReportingModel vtDailyReportingModel, VTDailyReporting vtDailyReporting)
        {
            vtDailyReporting.VTId = vtDailyReportingModel.VTId;
            vtDailyReporting.ReportingDate = vtDailyReportingModel.ReportingDate;
            vtDailyReporting.ReportType = vtDailyReportingModel.ReportType;

            vtDailyReporting.SchoolEventCelebration = vtDailyReportingModel.SchoolEventCelebration;
            vtDailyReporting.WorkAssignedByHeadMaster = vtDailyReportingModel.WorkAssignedByHeadMaster;
            vtDailyReporting.SchoolExamDuty = vtDailyReportingModel.SchoolExamDuty;
            vtDailyReporting.OtherWork = vtDailyReportingModel.OtherWork;
            vtDailyReporting.ObservationDetails = vtDailyReportingModel.ObservationDetails;
            vtDailyReporting.OBStudentCount = vtDailyReportingModel.OBStudentCount;

            vtDailyReporting.GeoLocation = vtDailyReportingModel.GeoLocation;
            vtDailyReporting.Latitude = vtDailyReportingModel.Latitude;
            vtDailyReporting.Longitude = vtDailyReportingModel.Longitude;
            vtDailyReporting.ApprovalStatus = vtDailyReportingModel.ApprovalStatus;
            vtDailyReporting.ApprovedDate = vtDailyReportingModel.ApprovedDate;
            vtDailyReporting.IsActive = vtDailyReportingModel.IsActive;
            vtDailyReporting.RequestType = vtDailyReportingModel.RequestType;
            vtDailyReporting.SetAuditValues(vtDailyReportingModel.RequestType);

            return vtDailyReporting;
        }
    }
}