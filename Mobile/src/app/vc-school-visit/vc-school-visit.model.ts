import { Guid } from 'guid-typescript';

export class VCSchoolVisitModel {
    VCId: string;
    VCSchoolVisitId: string;
    VCSchoolSectorId: string;
    ReportDate: any;
    GeoLocation: string;
    Month: string;
    VTReportSubmitted: string;
    VTWorkingDays: any;
    VRLeaveDays: any;
    VTTeachingDays: any;
    ClassVisited: string;
    ClassTeachingDays: any;
    BoysEnrolledCheck: any;
    GirlsEnrolledCheck: any;
    AvgStudentAttendance: any;
    CMAvailability: string;
    CMDate: any;
    TEAvailability: string;
    TEDate: any;
    NoOfGLConducted: any;
    NoOfFVConducted: any;
    SchoolHMVisited: string;
    HMRatingVTattendance: any;
    HMRatingSyllabuscompletion: any;
    HMRatingVtreporting: any;
    HMRatingVtqualityteaching: any;
    HMRatingVtglfvquality: any;
    HMRatingInitiativestaken: any;
    IsActive: boolean;
    RequestType: any;

    constructor(vcSchoolVisitItem?: any) {
        vcSchoolVisitItem = vcSchoolVisitItem || {};

        this.VCId = vcSchoolVisitItem.VCId || Guid.create()['value'];
        this.VCSchoolVisitId = vcSchoolVisitItem.VCSchoolVisitId || Guid.create()['value'];
        this.VCSchoolSectorId = vcSchoolVisitItem.VCSchoolSectorId || Guid.create()['value'];
        this.ReportDate = vcSchoolVisitItem.ReportDate || '';
        this.GeoLocation = vcSchoolVisitItem.GeoLocation || '';
        this.Month = vcSchoolVisitItem.Month || '';
        this.VTReportSubmitted = vcSchoolVisitItem.VTReportSubmitted || '';
        this.VTWorkingDays = vcSchoolVisitItem.VTWorkingDays || 0;
        this.VRLeaveDays = vcSchoolVisitItem.VRLeaveDays || 0;
        this.VTTeachingDays = vcSchoolVisitItem.VTTeachingDays || 0;
        this.ClassVisited = vcSchoolVisitItem.ClassVisited || '';
        this.ClassTeachingDays = vcSchoolVisitItem.ClassTeachingDays || 0;
        this.BoysEnrolledCheck = vcSchoolVisitItem.BoysEnrolledCheck || 0;
        this.GirlsEnrolledCheck = vcSchoolVisitItem.GirlsEnrolledCheck || 0;
        this.AvgStudentAttendance = vcSchoolVisitItem.AvgStudentAttendance || 0;
        this.CMAvailability = vcSchoolVisitItem.CMAvailability || '';
        this.CMDate = vcSchoolVisitItem.CMDate || '';
        this.TEAvailability = vcSchoolVisitItem.TEAvailability || '';
        this.TEDate = vcSchoolVisitItem.TEDate || '';
        this.NoOfGLConducted = vcSchoolVisitItem.NoOfGLConducted || 0;
        this.NoOfFVConducted = vcSchoolVisitItem.NoOfFVConducted || 0;
        this.SchoolHMVisited = vcSchoolVisitItem.SchoolHMVisited || '';
        this.HMRatingVTattendance = vcSchoolVisitItem.HMRatingVTattendance || 0;
        this.HMRatingSyllabuscompletion = vcSchoolVisitItem.HMRatingSyllabuscompletion || 0;
        this.HMRatingVtreporting = vcSchoolVisitItem.HMRatingVtreporting || 0;
        this.HMRatingVtqualityteaching = vcSchoolVisitItem.HMRatingVtqualityteaching || 0;
        this.HMRatingVtglfvquality = vcSchoolVisitItem.HMRatingVtglfvquality || 0;
        this.HMRatingInitiativestaken = vcSchoolVisitItem.HMRatingInitiativestaken || 0;
        this.IsActive = vcSchoolVisitItem.IsActive || true;
        this.RequestType = 0; // New
    }
}
