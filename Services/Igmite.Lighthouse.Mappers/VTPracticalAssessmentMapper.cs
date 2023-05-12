using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class VTPracticalAssessmentMapper
    {
        public static VTPracticalAssessmentModel ToModel(this VTPracticalAssessment vtPracticalAssessment)
        {
            if (vtPracticalAssessment == null)
                return null;

            VTPracticalAssessmentModel vtPracticalAssessmentModel = new VTPracticalAssessmentModel
            {
                VTPracticalAssessmentId = vtPracticalAssessment.VTPracticalAssessmentId,
                VTClassId = vtPracticalAssessment.VTClassId,
                AssessmentDate = vtPracticalAssessment.AssessmentDate,
                BoysPresent = vtPracticalAssessment.BoysPresent,
                GirlsPresent = vtPracticalAssessment.GirlsPresent,
                AssessorName = vtPracticalAssessment.AssessorName,
                AssessorMobile = vtPracticalAssessment.AssessorMobile,
                AssessorEmail = vtPracticalAssessment.AssessorEmail,
                AssessorQualification = vtPracticalAssessment.AssessorQualification,
                AssessorTimeReached = vtPracticalAssessment.AssessorTimeReached,
                AssessorIdCheck = vtPracticalAssessment.AssessorIdCheck,
                AssessorIdType = vtPracticalAssessment.AssessorIdType,
                AssessorSSCLetter = vtPracticalAssessment.AssessorSSCLetter,
                AssessorBehaviour = vtPracticalAssessment.AssessorBehaviour,
                AssessorDemands = vtPracticalAssessment.AssessorDemands,
                AssessorBehaiourFormality = vtPracticalAssessment.AssessorBehaiourFormality,
                AssessorGroupPhoto = vtPracticalAssessment.AssessorGroupPhoto,
                VCPMUNameVisit = vtPracticalAssessment.VCPMUNameVisit,
                RemarksDetails = vtPracticalAssessment.RemarksDetails,
                CreatedBy = vtPracticalAssessment.CreatedBy,
                CreatedOn = vtPracticalAssessment.CreatedOn,
                UpdatedBy = vtPracticalAssessment.UpdatedBy,
                UpdatedOn = vtPracticalAssessment.UpdatedOn,
                IsActive = vtPracticalAssessment.IsActive
            };

            return vtPracticalAssessmentModel;
        }
        public static VTPracticalAssessment FromModel(this VTPracticalAssessmentModel vtPracticalAssessmentModel, VTPracticalAssessment vtPracticalAssessment)
        {
            vtPracticalAssessment.VTPracticalAssessmentId = vtPracticalAssessmentModel.VTPracticalAssessmentId;
            vtPracticalAssessment.VTClassId = vtPracticalAssessmentModel.VTClassId;
            vtPracticalAssessment.AssessmentDate = vtPracticalAssessmentModel.AssessmentDate;
            vtPracticalAssessment.BoysPresent = vtPracticalAssessmentModel.BoysPresent;
            vtPracticalAssessment.GirlsPresent = vtPracticalAssessmentModel.GirlsPresent;
            vtPracticalAssessment.AssessorName = vtPracticalAssessmentModel.AssessorName;
            vtPracticalAssessment.AssessorMobile = vtPracticalAssessmentModel.AssessorMobile;
            vtPracticalAssessment.AssessorEmail = vtPracticalAssessmentModel.AssessorEmail;
            vtPracticalAssessment.AssessorQualification = vtPracticalAssessmentModel.AssessorQualification;
            vtPracticalAssessment.AssessorTimeReached = vtPracticalAssessmentModel.AssessorTimeReached;
            vtPracticalAssessment.AssessorIdCheck = vtPracticalAssessmentModel.AssessorIdCheck;
            vtPracticalAssessment.AssessorIdType = vtPracticalAssessmentModel.AssessorIdType;
            vtPracticalAssessment.AssessorSSCLetter = vtPracticalAssessmentModel.AssessorSSCLetter;
            vtPracticalAssessment.AssessorBehaviour = vtPracticalAssessmentModel.AssessorBehaviour;
            vtPracticalAssessment.AssessorDemands = vtPracticalAssessmentModel.AssessorDemands;
            vtPracticalAssessment.AssessorBehaiourFormality = vtPracticalAssessmentModel.AssessorBehaiourFormality;
            vtPracticalAssessment.AssessorGroupPhoto = vtPracticalAssessmentModel.AssessorGroupPhoto;
            vtPracticalAssessment.VCPMUNameVisit = vtPracticalAssessmentModel.VCPMUNameVisit;
            vtPracticalAssessment.RemarksDetails = vtPracticalAssessmentModel.RemarksDetails;
            vtPracticalAssessment.IsActive = vtPracticalAssessmentModel.IsActive;
            vtPracticalAssessment.RequestType = vtPracticalAssessmentModel.RequestType;
            vtPracticalAssessment.SetAuditValues(vtPracticalAssessmentModel.RequestType);

            return vtPracticalAssessment;
        }
    }
}
