using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;

namespace Igmite.Lighthouse.Mappers
{
    public static class VCSchoolVisitMapper
    {
        public static VCSchoolVisitModel ToModel(this VCSchoolVisit vcSchoolVisit)
        {
            if (vcSchoolVisit == null)
                return null;

            VCSchoolVisitModel vcSchoolVisitModel = new VCSchoolVisitModel
            {
                VCSchoolVisitId = vcSchoolVisit.VCSchoolVisitId,
                VCSchoolSectorId = vcSchoolVisit.VCSchoolSectorId,
                VCId = vcSchoolVisit.VCId,
                ReportDate = vcSchoolVisit.ReportDate,
                GeoLocation = vcSchoolVisit.GeoLocation,
                Month = vcSchoolVisit.Month,
                VTReportSubmitted = vcSchoolVisit.VTReportSubmitted,
                VTWorkingDays = vcSchoolVisit.VTWorkingDays,
                VTLeaveDays = vcSchoolVisit.VTLeaveDays,
                VTTeachingDays = vcSchoolVisit.VTTeachingDays,
                ClassVisited = vcSchoolVisit.ClassVisited,
                ClassTeachingDays = vcSchoolVisit.ClassTeachingDays,
                BoysEnrolledCheck = vcSchoolVisit.BoysEnrolledCheck,
                GirlsEnrolledCheck = vcSchoolVisit.GirlsEnrolledCheck,
                AvgStudentAttendance = vcSchoolVisit.AvgStudentAttendance,
                CMAvailability = vcSchoolVisit.CMAvailability,
                CMDate = vcSchoolVisit.CMDate,
                TEAvailability = vcSchoolVisit.TEAvailability,
                TEDate = vcSchoolVisit.TEDate,
                NoOfGLConducted = vcSchoolVisit.NoOfGLConducted,
                NoOfFVConducted = vcSchoolVisit.NoOfFVConducted,
                SchoolHMVisited = vcSchoolVisit.SchoolHMVisited,
                HMRatingVTattendance = vcSchoolVisit.HMRatingVTattendance,
                HMRatingSyllabuscompletion = vcSchoolVisit.HMRatingSyllabuscompletion,
                HMRatingVtreporting = vcSchoolVisit.HMRatingVtreporting,
                HMRatingVtqualityteaching = vcSchoolVisit.HMRatingVtqualityteaching,
                HMRatingVtglfvquality = vcSchoolVisit.HMRatingVtglfvquality,
                HMRatingInitiativestaken = vcSchoolVisit.HMRatingInitiativestaken,
                Latitude = vcSchoolVisit.Latitude,
                Longitude = vcSchoolVisit.Longitude,
                CreatedBy = vcSchoolVisit.CreatedBy,
                CreatedOn = vcSchoolVisit.CreatedOn,
                UpdatedBy = vcSchoolVisit.UpdatedBy,
                UpdatedOn = vcSchoolVisit.UpdatedOn,
                IsActive = vcSchoolVisit.IsActive
            };

            return vcSchoolVisitModel;
        }

        public static VCSchoolVisit FromModel(this VCSchoolVisitModel vcSchoolVisitModel, VCSchoolVisit vcSchoolVisit)
        {
            vcSchoolVisit.VCId = vcSchoolVisitModel.VCId;
            vcSchoolVisit.ReportDate = vcSchoolVisitModel.ReportDate;
            vcSchoolVisit.GeoLocation = vcSchoolVisitModel.GeoLocation;
            vcSchoolVisit.Month = vcSchoolVisitModel.Month;
            vcSchoolVisit.VTReportSubmitted = vcSchoolVisitModel.VTReportSubmitted;
            vcSchoolVisit.VTWorkingDays = vcSchoolVisitModel.VTWorkingDays;
            vcSchoolVisit.VTLeaveDays = vcSchoolVisitModel.VTLeaveDays;
            vcSchoolVisit.VTTeachingDays = vcSchoolVisitModel.VTTeachingDays;
            vcSchoolVisit.ClassVisited = vcSchoolVisitModel.ClassVisited;
            vcSchoolVisit.ClassTeachingDays = vcSchoolVisitModel.ClassTeachingDays;
            vcSchoolVisit.BoysEnrolledCheck = vcSchoolVisitModel.BoysEnrolledCheck;
            vcSchoolVisit.GirlsEnrolledCheck = vcSchoolVisitModel.GirlsEnrolledCheck;
            vcSchoolVisit.AvgStudentAttendance = vcSchoolVisitModel.AvgStudentAttendance;
            vcSchoolVisit.CMAvailability = vcSchoolVisitModel.CMAvailability;
            vcSchoolVisit.CMDate = vcSchoolVisitModel.CMDate;
            vcSchoolVisit.TEAvailability = vcSchoolVisitModel.TEAvailability;
            vcSchoolVisit.TEDate = vcSchoolVisitModel.TEDate;
            vcSchoolVisit.NoOfGLConducted = vcSchoolVisitModel.NoOfGLConducted;
            vcSchoolVisit.NoOfFVConducted = vcSchoolVisitModel.NoOfFVConducted;
            vcSchoolVisit.SchoolHMVisited = vcSchoolVisitModel.SchoolHMVisited;
            vcSchoolVisit.HMRatingVTattendance = vcSchoolVisitModel.HMRatingVTattendance;
            vcSchoolVisit.HMRatingSyllabuscompletion = vcSchoolVisitModel.HMRatingSyllabuscompletion;
            vcSchoolVisit.HMRatingVtreporting = vcSchoolVisitModel.HMRatingVtreporting;
            vcSchoolVisit.HMRatingVtqualityteaching = vcSchoolVisitModel.HMRatingVtqualityteaching;
            vcSchoolVisit.HMRatingVtglfvquality = vcSchoolVisitModel.HMRatingVtglfvquality;
            vcSchoolVisit.HMRatingInitiativestaken = vcSchoolVisitModel.HMRatingInitiativestaken;
            vcSchoolVisit.Latitude = vcSchoolVisitModel.Latitude;
            vcSchoolVisit.Longitude = vcSchoolVisitModel.Longitude;
            vcSchoolVisit.IsActive = vcSchoolVisitModel.IsActive;
            vcSchoolVisit.RequestType = vcSchoolVisitModel.RequestType;
            vcSchoolVisit.SetAuditValues(vcSchoolVisitModel.RequestType);

            return vcSchoolVisit;
        }
    }
}