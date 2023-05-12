using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;

namespace Igmite.Lighthouse.Mappers
{
    public static class VocationalTrainerMapper
    {
        public static VocationalTrainerModel ToModel(this VocationalTrainer vocationalTrainer)
        {
            if (vocationalTrainer == null)
                return null;

            VocationalTrainerModel vocationalTrainerModel = new VocationalTrainerModel
            {
                VTId = vocationalTrainer.VTId,
                FirstName = vocationalTrainer.FirstName,
                MiddleName = vocationalTrainer.MiddleName,
                LastName = vocationalTrainer.LastName,
                FullName = string.Format("{0} {1} {2}", vocationalTrainer.FirstName, vocationalTrainer.MiddleName, vocationalTrainer.LastName).TrimSpaces(),
                Mobile = vocationalTrainer.Mobile,
                Mobile1 = vocationalTrainer.Mobile1,
                Email = vocationalTrainer.Email,
                Gender = vocationalTrainer.Gender,
                DateOfBirth = vocationalTrainer.DateOfBirth,
                SocialCategory = vocationalTrainer.SocialCategory,
                AcademicQualification = vocationalTrainer.AcademicQualification,
                ProfessionalQualification = vocationalTrainer.ProfessionalQualification,
                ProfessionalQualificationDetails = vocationalTrainer.ProfessionalQualificationDetails,
                IndustryExperienceMonths = vocationalTrainer.IndustryExperienceMonths,
                TrainingExperienceMonths = vocationalTrainer.TrainingExperienceMonths,
                AadhaarNumber = vocationalTrainer.AadhaarNumber,
                CreatedBy = vocationalTrainer.CreatedBy,
                CreatedOn = vocationalTrainer.CreatedOn,
                UpdatedBy = vocationalTrainer.UpdatedBy,
                UpdatedOn = vocationalTrainer.UpdatedOn,
                IsActive = vocationalTrainer.IsActive
            };

            if (vocationalTrainer.VCTrainer != null)
            {
                vocationalTrainerModel.AcademicYearId = vocationalTrainer.VCTrainer.AcademicYearId;
                vocationalTrainerModel.VCId = vocationalTrainer.VCTrainer.VCId;
                vocationalTrainerModel.VTPId = vocationalTrainer.VCTrainer.VTPId;
                vocationalTrainerModel.DateOfJoining = vocationalTrainer.VCTrainer.DateOfJoining;
                vocationalTrainerModel.DateOfResignation = vocationalTrainer.VCTrainer.DateOfResignation;
                vocationalTrainerModel.NatureOfAppointment = vocationalTrainer.VCTrainer.NatureOfAppointment;
            }

            return vocationalTrainerModel;
        }

        public static VocationalTrainer FromModel(this VocationalTrainerModel vocationalTrainerModel, VocationalTrainer vocationalTrainer)
        {
            vocationalTrainer.VTId = vocationalTrainerModel.VTId;
            vocationalTrainer.FirstName = vocationalTrainerModel.FirstName;
            vocationalTrainer.MiddleName = vocationalTrainerModel.MiddleName;
            vocationalTrainer.LastName = vocationalTrainerModel.LastName;
            vocationalTrainer.FullName = string.Format("{0} {1} {2}", vocationalTrainerModel.FirstName, vocationalTrainerModel.MiddleName, vocationalTrainerModel.LastName).TrimSpaces();
            vocationalTrainer.Mobile = vocationalTrainerModel.Mobile;
            vocationalTrainer.Mobile1 = vocationalTrainerModel.Mobile1;
            vocationalTrainer.Email = vocationalTrainerModel.Email;
            vocationalTrainer.Gender = vocationalTrainerModel.Gender;
            vocationalTrainer.DateOfBirth = vocationalTrainerModel.DateOfBirth;
            vocationalTrainer.SocialCategory = vocationalTrainerModel.SocialCategory;
            vocationalTrainer.AcademicQualification = vocationalTrainerModel.AcademicQualification;
            vocationalTrainer.ProfessionalQualification = vocationalTrainerModel.ProfessionalQualification;
            vocationalTrainer.ProfessionalQualificationDetails = vocationalTrainerModel.ProfessionalQualificationDetails;
            vocationalTrainer.IndustryExperienceMonths = vocationalTrainerModel.IndustryExperienceMonths;
            vocationalTrainer.TrainingExperienceMonths = vocationalTrainerModel.TrainingExperienceMonths;
            vocationalTrainer.AadhaarNumber = vocationalTrainerModel.AadhaarNumber;
            vocationalTrainer.IsActive = (vocationalTrainerModel.DateOfResignation != null) ? false : vocationalTrainerModel.IsActive;
            vocationalTrainer.RequestType = vocationalTrainerModel.RequestType;
            vocationalTrainer.SetAuditValues(vocationalTrainerModel.RequestType);

            vocationalTrainer.VCTrainer.AuthUserId = vocationalTrainer.AuthUserId;
            vocationalTrainer.VCTrainer.AcademicYearId = vocationalTrainerModel.AcademicYearId;
            vocationalTrainer.VCTrainer.VTId = vocationalTrainerModel.VTId;
            vocationalTrainer.VCTrainer.VCId = vocationalTrainerModel.VCId;
            vocationalTrainer.VCTrainer.VTPId = vocationalTrainerModel.VTPId;
            vocationalTrainer.VCTrainer.DateOfJoining = vocationalTrainerModel.DateOfJoining;
            vocationalTrainer.VCTrainer.DateOfResignation = vocationalTrainerModel.DateOfResignation;
            vocationalTrainer.VCTrainer.NatureOfAppointment = vocationalTrainerModel.NatureOfAppointment;
            vocationalTrainer.VCTrainer.IsActive = vocationalTrainer.IsActive;
            vocationalTrainer.VCTrainer.RequestType = vocationalTrainerModel.RequestType;
            vocationalTrainer.VCTrainer.SetAuditValues(vocationalTrainerModel.RequestType);

            return vocationalTrainer;
        }
    }
}