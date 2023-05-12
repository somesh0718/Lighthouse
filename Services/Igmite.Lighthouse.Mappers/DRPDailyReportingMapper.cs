using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class DRPDailyReportingMapper
    {
        public static DRPDailyReportingModel ToModel(this DRPDailyReporting dailyReporting)
        {
            if (dailyReporting == null)
                return null;

            DRPDailyReportingModel dailyReportingModel = new DRPDailyReportingModel
            {
                DRPDailyReportingId = dailyReporting.DRPDailyReportingId,                
                DRPId = dailyReporting.DRPId,
                ReportDate = dailyReporting.ReportDate,
                ReportType = dailyReporting.ReportType,
                WorkTypeDetails = dailyReporting.WorkTypeDetails,
                SchoolId = dailyReporting.SchoolId,
                GeoLocation = dailyReporting.GeoLocation,
                Latitude = dailyReporting.Latitude,
                Longitude = dailyReporting.Longitude,
                CreatedBy = dailyReporting.CreatedBy,
                CreatedOn = dailyReporting.CreatedOn,
                UpdatedBy = dailyReporting.UpdatedBy,
                UpdatedOn = dailyReporting.UpdatedOn,
                IsActive = dailyReporting.IsActive
            };

            return dailyReportingModel;
        }

        public static DRPDailyReporting FromModel(this DRPDailyReportingModel dailyReportingModel, DRPDailyReporting dailyReporting)
        {
            dailyReporting.DRPId = dailyReportingModel.DRPId;
            dailyReporting.ReportDate = dailyReportingModel.ReportDate;
            dailyReporting.ReportType = dailyReportingModel.ReportType;
            dailyReporting.SchoolId = dailyReportingModel.SchoolId;
            dailyReporting.WorkTypeDetails = dailyReportingModel.WorkTypeDetails;
            dailyReporting.GeoLocation = dailyReportingModel.GeoLocation;
            dailyReporting.Latitude = dailyReportingModel.Latitude;
            dailyReporting.Longitude = dailyReportingModel.Longitude;
            dailyReporting.IsActive = dailyReportingModel.IsActive;
            dailyReporting.RequestType = dailyReportingModel.RequestType;
            dailyReporting.SetAuditValues(dailyReportingModel.RequestType);

            return dailyReporting;
        }
    }
}