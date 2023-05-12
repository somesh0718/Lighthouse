using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class VCDailyReportingMapper
    {
        public static VCDailyReportingModel ToModel(this VCDailyReporting vcDailyReporting)
        {
            if (vcDailyReporting == null)
                return null;

            VCDailyReportingModel vcDailyReportingModel = new VCDailyReportingModel
            {
                VCDailyReportingId = vcDailyReporting.VCDailyReportingId,
                VCSchoolSectorId = vcDailyReporting.VCSchoolSectorId,
                VCId = vcDailyReporting.VCId,
                ReportDate = vcDailyReporting.ReportDate,
                ReportType = vcDailyReporting.ReportType,
                WorkTypeDetails = vcDailyReporting.WorkTypeDetails,
                SchoolId = vcDailyReporting.SchoolId,
                GeoLocation = vcDailyReporting.GeoLocation,
                Latitude = vcDailyReporting.Latitude,
                Longitude = vcDailyReporting.Longitude,
                CreatedBy = vcDailyReporting.CreatedBy,
                CreatedOn = vcDailyReporting.CreatedOn,
                UpdatedBy = vcDailyReporting.UpdatedBy,
                UpdatedOn = vcDailyReporting.UpdatedOn,
                IsActive = vcDailyReporting.IsActive
            };

            return vcDailyReportingModel;
        }

        public static VCDailyReporting FromModel(this VCDailyReportingModel vcDailyReportingModel, VCDailyReporting vcDailyReporting)
        {
            vcDailyReporting.VCId = vcDailyReportingModel.VCId;
            vcDailyReporting.ReportDate = vcDailyReportingModel.ReportDate;
            vcDailyReporting.ReportType = vcDailyReportingModel.ReportType;
            vcDailyReporting.SchoolId = vcDailyReportingModel.SchoolId;
            vcDailyReporting.WorkTypeDetails = vcDailyReportingModel.WorkTypeDetails;
            vcDailyReporting.GeoLocation = vcDailyReportingModel.GeoLocation;
            vcDailyReporting.Latitude = vcDailyReportingModel.Latitude;
            vcDailyReporting.Longitude = vcDailyReportingModel.Longitude;
            vcDailyReporting.IsActive = vcDailyReportingModel.IsActive;
            vcDailyReporting.RequestType = vcDailyReportingModel.RequestType;
            vcDailyReporting.SetAuditValues(vcDailyReportingModel.RequestType);

            return vcDailyReporting;
        }
    }
}