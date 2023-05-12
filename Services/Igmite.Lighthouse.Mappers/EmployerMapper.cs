using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class EmployerMapper
    {
        public static EmployerModel ToModel(this Employer employer)
        {
            if (employer == null)
                return null;

            EmployerModel employerModel = new EmployerModel
            {
                EmployerId = employer.EmployerId,
                StateCode = employer.StateCode,
                DivisionId = employer.DivisionId,
                DistrictCode = employer.DistrictCode,
                BlockName = employer.BlockName,
                Address = employer.Address,
                City = employer.City,
                Pincode = employer.Pincode,
                BusinessType = employer.BusinessType,
                EmployeeCount = employer.EmployeeCount,
                Outlets = employer.Outlets,
                Contact1 = employer.Contact1,
                Mobile1 = employer.Mobile1,
                Designation1 = employer.Designation1,
                EmailId1 = employer.EmailId1,
                Contact2 = employer.Contact2,
                Mobile2 = employer.Mobile2,
                Designation2 = employer.Designation2,
                EmailId2 = employer.EmailId2,
                CreatedBy = employer.CreatedBy,
                CreatedOn = employer.CreatedOn,
                UpdatedBy = employer.UpdatedBy,
                UpdatedOn = employer.UpdatedOn,
                IsActive = employer.IsActive
            };

            return employerModel;
        }

        public static Employer FromModel(this EmployerModel employerModel, Employer employer)
        {
            employer.EmployerId = employerModel.EmployerId;
            employer.StateCode = employerModel.StateCode;
            employer.DivisionId = employerModel.DivisionId;
            employer.DistrictCode = employerModel.DistrictCode;
            employer.BlockName = employerModel.BlockName;
            employer.Address = employerModel.Address;
            employer.City = employerModel.City;
            employer.Pincode = employerModel.Pincode;
            employer.BusinessType = employerModel.BusinessType;
            employer.EmployeeCount = employerModel.EmployeeCount;
            employer.Outlets = employerModel.Outlets;
            employer.Contact1 = employerModel.Contact1;
            employer.Mobile1 = employerModel.Mobile1;
            employer.Designation1 = employerModel.Designation1;
            employer.EmailId1 = employerModel.EmailId1;
            employer.Contact2 = employerModel.Contact2;
            employer.Mobile2 = employerModel.Mobile2;
            employer.Designation2 = employerModel.Designation2;
            employer.EmailId2 = employerModel.EmailId2;
            employer.IsActive = employerModel.IsActive;
            employer.RequestType = employerModel.RequestType;
            employer.SetAuditValues(employerModel.RequestType);

            return employer;
        }
    }
}