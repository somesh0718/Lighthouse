using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class SchoolMapper
    {
        public static SchoolModel ToModel(this School school)
        {
            if (school == null)
                return null;

            SchoolModel schoolModel = new SchoolModel
            {
                SchoolId = school.SchoolId,
                SchoolName = school.SchoolName,
                SchoolCategoryId = school.SchoolCategoryId,
                SchoolTypeId = school.SchoolTypeId,
                SchoolManagementId = school.SchoolManagementId,
                Udise = school.Udise,
                AcademicYearId = school.AcademicYearId,
                PhaseId = school.PhaseId,
                StateCode = school.StateCode,
                StateName = school.StateCode,
                DivisionId = school.DivisionId,
                DistrictCode = school.DistrictCode,
                BlockId = school.BlockId,
                ClusterId = school.ClusterId,
                BlockName = school.BlockName,
                Village = school.Village,
                Panchayat = school.Panchayat,
                Pincode = school.Pincode,
                Demography = school.Demography,
                IsImplemented = school.IsImplemented,
                CreatedBy = school.CreatedBy,
                CreatedOn = school.CreatedOn,
                UpdatedBy = school.UpdatedBy,
                UpdatedOn = school.UpdatedOn,
                IsActive = school.IsActive
            };

            return schoolModel;
        }

        public static School FromModel(this SchoolModel schoolModel, School school)
        {
            school.SchoolId = schoolModel.SchoolId;
            school.SchoolName = schoolModel.SchoolName;
            school.SchoolCategoryId = schoolModel.SchoolCategoryId;
            school.SchoolTypeId = schoolModel.SchoolTypeId;
            school.SchoolManagementId = schoolModel.SchoolManagementId;
            school.Udise = schoolModel.Udise;
            school.AcademicYearId = schoolModel.AcademicYearId;
            school.PhaseId = schoolModel.PhaseId;
            school.StateCode = schoolModel.StateName;
            school.DivisionId = schoolModel.DivisionId;
            school.DistrictCode = schoolModel.DistrictCode;
            school.BlockId = schoolModel.BlockId;
            school.ClusterId = schoolModel.ClusterId;
            school.BlockName = schoolModel.BlockName;
            school.Village = schoolModel.Village;
            school.Panchayat = schoolModel.Panchayat;
            school.Pincode = schoolModel.Pincode;
            school.Demography = schoolModel.Demography;
            school.IsImplemented = schoolModel.IsImplemented;
            school.IsActive = schoolModel.IsActive;
            school.RequestType = schoolModel.RequestType;
            school.SetAuditValues(schoolModel.RequestType);

            return school;
        }
    }
}