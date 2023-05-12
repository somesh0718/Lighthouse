using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class VCSchoolVisitReportingMapper
    {
        public static VCSchoolVisitReportingModel ToModel(this VCSchoolVisitReporting vcSchoolVisitReporting)
        {
            if (vcSchoolVisitReporting == null)
                return null;

            VCSchoolVisitReportingModel vcSchoolVisitReportingModel = new VCSchoolVisitReportingModel
            {
                VCSchoolVisitReportingId = vcSchoolVisitReporting.VCSchoolVisitReportingId,
                VCId = vcSchoolVisitReporting.VCId,
                CompanyName = vcSchoolVisitReporting.CompanyName,
                Month = vcSchoolVisitReporting.Month,
                VisitDate = vcSchoolVisitReporting.VisitDate,
                SchoolId = vcSchoolVisitReporting.SchoolId,
                DistrictCode = vcSchoolVisitReporting.DistrictCode,
                SchoolEmailId = vcSchoolVisitReporting.SchoolEmailId,
                PrincipalName = vcSchoolVisitReporting.PrincipalName,
                PrincipalPhoneNo = vcSchoolVisitReporting.PrincipalPhoneNo,
                SectorId = vcSchoolVisitReporting.SectorId,
                JobRoleId = vcSchoolVisitReporting.JobRoleId,
                VTId = vcSchoolVisitReporting.VTId,
                VTPhoneNo = vcSchoolVisitReporting.VTPhoneNo,
                Labs = vcSchoolVisitReporting.Labs,
                Books = vcSchoolVisitReporting.Books,
                NoOfGLConducted = vcSchoolVisitReporting.NoOfGLConducted,
                NoOfIndustrialVisits = vcSchoolVisitReporting.NoOfIndustrialVisits,
                SVPhotoWithPrincipal = vcSchoolVisitReporting.SVPhotoWithPrincipal,
                SVPhotoWithStudents = vcSchoolVisitReporting.SVPhotoWithStudents,
                Class9Boys = vcSchoolVisitReporting.Class9Boys,
                Class9Girls = vcSchoolVisitReporting.Class9Girls,
                Class10Boys = vcSchoolVisitReporting.Class10Boys,
                Class10Girls = vcSchoolVisitReporting.Class10Girls,
                Class11Boys = vcSchoolVisitReporting.Class11Boys,
                Class11Girls = vcSchoolVisitReporting.Class11Girls,
                Class12Boys = vcSchoolVisitReporting.Class12Boys,
                Class12Girls = vcSchoolVisitReporting.Class12Girls,
                TotalBoys = vcSchoolVisitReporting.TotalBoys,
                TotalGirls = vcSchoolVisitReporting.TotalGirls,
                GeoLocation = vcSchoolVisitReporting.GeoLocation,
                Latitude = vcSchoolVisitReporting.Latitude,
                Longitude = vcSchoolVisitReporting.Longitude,

                CreatedBy = vcSchoolVisitReporting.CreatedBy,
                CreatedOn = vcSchoolVisitReporting.CreatedOn,
                UpdatedBy = vcSchoolVisitReporting.UpdatedBy,
                UpdatedOn = vcSchoolVisitReporting.UpdatedOn,
                IsActive = vcSchoolVisitReporting.IsActive
            };

            return vcSchoolVisitReportingModel;
        }

        public static VCSchoolVisitReporting FromModel(this VCSchoolVisitReportingModel vcSchoolVisitReportingModel, VCSchoolVisitReporting vcSchoolVisitReporting)
        {
            vcSchoolVisitReporting.VCId = vcSchoolVisitReportingModel.VCId;
            vcSchoolVisitReporting.CompanyName = vcSchoolVisitReportingModel.CompanyName;
            vcSchoolVisitReporting.Month = vcSchoolVisitReportingModel.Month;
            vcSchoolVisitReporting.VisitDate = vcSchoolVisitReportingModel.VisitDate;
            vcSchoolVisitReporting.SchoolId = vcSchoolVisitReportingModel.SchoolId;
            vcSchoolVisitReporting.DistrictCode = vcSchoolVisitReportingModel.DistrictCode;
            vcSchoolVisitReporting.SchoolEmailId = vcSchoolVisitReportingModel.SchoolEmailId;
            vcSchoolVisitReporting.PrincipalName = vcSchoolVisitReportingModel.PrincipalName;
            vcSchoolVisitReporting.PrincipalPhoneNo = vcSchoolVisitReportingModel.PrincipalPhoneNo;
            vcSchoolVisitReporting.SectorId = vcSchoolVisitReportingModel.SectorId;
            vcSchoolVisitReporting.JobRoleId = vcSchoolVisitReportingModel.JobRoleId;
            vcSchoolVisitReporting.VTId = vcSchoolVisitReportingModel.VTId;
            vcSchoolVisitReporting.VTPhoneNo = vcSchoolVisitReportingModel.VTPhoneNo;
            vcSchoolVisitReporting.Labs = vcSchoolVisitReportingModel.Labs;
            vcSchoolVisitReporting.Books = vcSchoolVisitReportingModel.Books;
            vcSchoolVisitReporting.NoOfGLConducted = vcSchoolVisitReportingModel.NoOfGLConducted;
            vcSchoolVisitReporting.NoOfIndustrialVisits = vcSchoolVisitReportingModel.NoOfIndustrialVisits;
            vcSchoolVisitReporting.SVPhotoWithPrincipal = vcSchoolVisitReportingModel.SVPhotoWithPrincipal;
            vcSchoolVisitReporting.SVPhotoWithStudents = vcSchoolVisitReportingModel.SVPhotoWithStudents;
            vcSchoolVisitReporting.Class9Boys = vcSchoolVisitReportingModel.Class9Boys;
            vcSchoolVisitReporting.Class9Girls = vcSchoolVisitReportingModel.Class9Girls;
            vcSchoolVisitReporting.Class10Boys = vcSchoolVisitReportingModel.Class10Boys;
            vcSchoolVisitReporting.Class10Girls = vcSchoolVisitReportingModel.Class10Girls;
            vcSchoolVisitReporting.Class11Boys = vcSchoolVisitReportingModel.Class11Boys;
            vcSchoolVisitReporting.Class11Girls = vcSchoolVisitReportingModel.Class11Girls;
            vcSchoolVisitReporting.Class12Boys = vcSchoolVisitReportingModel.Class12Boys;
            vcSchoolVisitReporting.Class12Girls = vcSchoolVisitReportingModel.Class12Girls;
            vcSchoolVisitReporting.TotalBoys = vcSchoolVisitReportingModel.TotalBoys;
            vcSchoolVisitReporting.TotalGirls = vcSchoolVisitReportingModel.TotalGirls;
            vcSchoolVisitReporting.GeoLocation = vcSchoolVisitReportingModel.GeoLocation;
            vcSchoolVisitReporting.Latitude = vcSchoolVisitReportingModel.Latitude;
            vcSchoolVisitReporting.Longitude = vcSchoolVisitReportingModel.Longitude;

            vcSchoolVisitReporting.IsActive = vcSchoolVisitReportingModel.IsActive;
            vcSchoolVisitReporting.RequestType = vcSchoolVisitReportingModel.RequestType;
            vcSchoolVisitReporting.SetAuditValues(vcSchoolVisitReportingModel.RequestType);

            return vcSchoolVisitReporting;
        }
    }
}