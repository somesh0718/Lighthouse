import { Guid } from 'guid-typescript';
import { ModuleUnitSessionModel } from '../models/module-unit-session-model';
import { StudentAttendanceModel } from '../models/student.attendance.model';

export class VTFieldIndustryVisitConductedModel {
    VTId: string;
    VTFieldIndustryVisitConductedId: string;
    ClassTaughtId: string;
    ReportingDate: any;
    SectionIds: string;
    FieldVisitTheme: string;
    FieldVisitActivities: string;
    ModuleId: string;
    UnitId: string;
    SessionIds: string;
    FVOrganisation: string;
    FVOrganisationAddress: string;
    FVDistance: number;
    FVPictureFile: any;
    FVContactPersonName: string;
    FVContactPersonMobile: string;
    FVContactPersonEmail: string;
    FVContactPersonDesignation: string;
    FVOrganisationInterestStatus: string;
    FVOrignisationOJTStatus: string;
    FeedbackFromOrgnisation: string;
    Remarks: string;
    FVStudentSafety: string;

    UnitSessionsModels: ModuleUnitSessionModel[];
    StudentAttendances: StudentAttendanceModel[];

    Latitude: string;
    Longitude: string;
    GeoLocation: string;

    IsActive: boolean;
    RequestType: any;

    constructor(vtFieldIndustryVisitConductedItem?: any) {
        vtFieldIndustryVisitConductedItem = vtFieldIndustryVisitConductedItem || {};

        // tslint:disable: no-string-literal
        this.VTId = vtFieldIndustryVisitConductedItem.VTId || Guid.create()['value'];
        this.VTFieldIndustryVisitConductedId = vtFieldIndustryVisitConductedItem.VTFieldIndustryVisitConductedId || Guid.create()['value'];
        this.ClassTaughtId = vtFieldIndustryVisitConductedItem.ClassTaughtId || '';
        this.ReportingDate = vtFieldIndustryVisitConductedItem.ReportingDate || '';
        this.SectionIds = vtFieldIndustryVisitConductedItem.SectionIds || '';
        this.FieldVisitTheme = vtFieldIndustryVisitConductedItem.FieldVisitTheme || '';
        this.FieldVisitActivities = vtFieldIndustryVisitConductedItem.FieldVisitActivities || '';
        this.ModuleId = vtFieldIndustryVisitConductedItem.ModuleId || '';
        this.UnitId = vtFieldIndustryVisitConductedItem.UnitId || '';
        this.SessionIds = vtFieldIndustryVisitConductedItem.SessionIds || '';
        this.FVOrganisation = vtFieldIndustryVisitConductedItem.FVOrganisation || '';
        this.FVOrganisationAddress = vtFieldIndustryVisitConductedItem.FVOrganisationAddress || '';
        this.FVDistance = vtFieldIndustryVisitConductedItem.FVDistance || '';
        this.FVPictureFile = vtFieldIndustryVisitConductedItem.FVPictureFile || '';
        this.FVContactPersonName = vtFieldIndustryVisitConductedItem.FVContactPersonName || '';
        this.FVContactPersonMobile = vtFieldIndustryVisitConductedItem.FVContactPersonMobile || '';
        this.FVContactPersonEmail = vtFieldIndustryVisitConductedItem.FVContactPersonEmail || '';
        this.FVContactPersonDesignation = vtFieldIndustryVisitConductedItem.FVContactPersonDesignation || '';
        this.FVOrganisationInterestStatus = vtFieldIndustryVisitConductedItem.FVOrganisationInterestStatus || '';
        this.FVOrignisationOJTStatus = vtFieldIndustryVisitConductedItem.FVOrignisationOJTStatus || '';
        this.FeedbackFromOrgnisation = vtFieldIndustryVisitConductedItem.FeedbackFromOrgnisation || '';
        this.Remarks = vtFieldIndustryVisitConductedItem.Remarks || '';
        this.FVStudentSafety = vtFieldIndustryVisitConductedItem.FVStudentSafety || '';

        // tslint:disable: no-angle-bracket-type-assertion
        this.UnitSessionsModels = <ModuleUnitSessionModel[]>[];
        this.StudentAttendances = <StudentAttendanceModel[]>[];

        this.Latitude = vtFieldIndustryVisitConductedItem.Latitude || '';
        this.Longitude = vtFieldIndustryVisitConductedItem.Longitude || '';
        this.GeoLocation = vtFieldIndustryVisitConductedItem.GeoLocation || '';


        this.IsActive = vtFieldIndustryVisitConductedItem.IsActive || true;
        this.RequestType = 0; // New
    }
}
