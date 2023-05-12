using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;

namespace Igmite.Lighthouse.Mappers
{
    public static class VocationalTrainingProviderMapper
    {
        public static VocationalTrainingProviderModel ToModel(this VocationalTrainingProvider vocationalTrainingProvider)
        {
            if (vocationalTrainingProvider == null)
                return null;

            VocationalTrainingProviderModel vocationalTrainingProviderModel = new VocationalTrainingProviderModel
            {
                VTPId = vocationalTrainingProvider.VTPId,
                VTPShortName = vocationalTrainingProvider.VTPShortName,
                VTPName = vocationalTrainingProvider.VTPName,
                ApprovalYear = vocationalTrainingProvider.ApprovalYear,
                CertificationNo = vocationalTrainingProvider.CertificationNo,
                CertificationAgency = vocationalTrainingProvider.CertificationAgency,
                VTPMobileNo = vocationalTrainingProvider.VTPMobileNo,
                VTPEmailId = vocationalTrainingProvider.VTPEmailId,
                VTPAddress = vocationalTrainingProvider.VTPAddress,
                PrimaryContactPerson = vocationalTrainingProvider.PrimaryContactPerson,
                PrimaryMobileNumber = vocationalTrainingProvider.PrimaryMobileNumber,
                PrimaryContactEmail = vocationalTrainingProvider.PrimaryContactEmail,
                VTPStateCoordinator = vocationalTrainingProvider.VTPStateCoordinator,
                VTPStateCoordinatorMobile = vocationalTrainingProvider.VTPStateCoordinatorMobile,
                VTPStateCoordinatorEmail = vocationalTrainingProvider.VTPStateCoordinatorEmail,
                ContractApprovalDate = vocationalTrainingProvider.ContractApprovalDate,
                ContractEndDate = vocationalTrainingProvider.ContractEndDate,         

                CreatedBy = vocationalTrainingProvider.CreatedBy,
                CreatedOn = vocationalTrainingProvider.CreatedOn,
                UpdatedBy = vocationalTrainingProvider.UpdatedBy,
                UpdatedOn = vocationalTrainingProvider.UpdatedOn,
                IsActive = vocationalTrainingProvider.IsActive
            };
            if (vocationalTrainingProvider.MOUDocUpload != null)
            {
                vocationalTrainingProviderModel.MOUDocUpload = vocationalTrainingProvider.MOUDocUpload;
            }
            if (vocationalTrainingProvider.VTPAcademicYear.AcademicYearId != Guid.Empty)
            {
                vocationalTrainingProviderModel.AcademicYearId = vocationalTrainingProvider.VTPAcademicYear.AcademicYearId;
            }
            if (vocationalTrainingProvider.VTPAcademicYear.DateOfJoining != null)
            {
                vocationalTrainingProviderModel.DateOfJoining = vocationalTrainingProvider.VTPAcademicYear.DateOfJoining.Value;
            }
            if (vocationalTrainingProvider.VTPAcademicYear.DateOfResignation != null)
            {
                vocationalTrainingProviderModel.DateOfResignation = vocationalTrainingProvider.VTPAcademicYear.DateOfResignation;
            }

            return vocationalTrainingProviderModel;
        }

        public static VocationalTrainingProvider FromModel(this VocationalTrainingProviderModel vocationalTrainingProviderModel, VocationalTrainingProvider vocationalTrainingProvider)
        {
            vocationalTrainingProvider.VTPId = vocationalTrainingProviderModel.VTPId;
            vocationalTrainingProvider.VTPShortName = vocationalTrainingProviderModel.VTPShortName;
            vocationalTrainingProvider.VTPName = vocationalTrainingProviderModel.VTPName;
            vocationalTrainingProvider.ApprovalYear = vocationalTrainingProviderModel.ApprovalYear;
            vocationalTrainingProvider.CertificationNo = vocationalTrainingProviderModel.CertificationNo;
            vocationalTrainingProvider.CertificationAgency = vocationalTrainingProviderModel.CertificationAgency;
            vocationalTrainingProvider.VTPMobileNo = vocationalTrainingProviderModel.VTPMobileNo;
            vocationalTrainingProvider.VTPEmailId = vocationalTrainingProviderModel.VTPEmailId;
            vocationalTrainingProvider.VTPAddress = vocationalTrainingProviderModel.VTPAddress;
            vocationalTrainingProvider.PrimaryContactPerson = vocationalTrainingProviderModel.PrimaryContactPerson;
            vocationalTrainingProvider.PrimaryMobileNumber = vocationalTrainingProviderModel.PrimaryMobileNumber;
            vocationalTrainingProvider.PrimaryContactEmail = vocationalTrainingProviderModel.PrimaryContactEmail;
            vocationalTrainingProvider.VTPStateCoordinator = vocationalTrainingProviderModel.VTPStateCoordinator;
            vocationalTrainingProvider.VTPStateCoordinatorMobile = vocationalTrainingProviderModel.VTPStateCoordinatorMobile;
            vocationalTrainingProvider.VTPStateCoordinatorEmail = vocationalTrainingProviderModel.VTPStateCoordinatorEmail;
            vocationalTrainingProvider.ContractApprovalDate = vocationalTrainingProviderModel.ContractApprovalDate;
            vocationalTrainingProvider.ContractEndDate = vocationalTrainingProviderModel.ContractEndDate;
            vocationalTrainingProvider.MOUDocUpload = vocationalTrainingProviderModel.MOUDocUpload;
            vocationalTrainingProvider.IsActive = vocationalTrainingProviderModel.IsActive;
            vocationalTrainingProvider.RequestType = vocationalTrainingProviderModel.RequestType;
            vocationalTrainingProvider.SetAuditValues(vocationalTrainingProviderModel.RequestType);

            vocationalTrainingProvider.VTPAcademicYear.AuthUserId = vocationalTrainingProvider.AuthUserId;
            vocationalTrainingProvider.VTPAcademicYear.VTPId = vocationalTrainingProviderModel.VTPId;
            vocationalTrainingProvider.VTPAcademicYear.DateOfJoining = vocationalTrainingProviderModel.ContractApprovalDate;
            vocationalTrainingProvider.VTPAcademicYear.DateOfResignation = vocationalTrainingProviderModel.ContractEndDate;
            vocationalTrainingProvider.VTPAcademicYear.IsActive = vocationalTrainingProviderModel.IsActive;
            vocationalTrainingProvider.VTPAcademicYear.SetAuditValues(vocationalTrainingProviderModel.RequestType);

            return vocationalTrainingProvider;
        }
    }
}