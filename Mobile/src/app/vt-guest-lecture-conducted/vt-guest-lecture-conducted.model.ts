import { Guid } from 'guid-typescript';
import { ModuleUnitSessionModel } from '../models/module-unit-session-model';
import { StudentAttendanceModel } from '../models/student.attendance.model';

export class VTGuestLectureConductedModel {
    VTGuestLectureId: string;
    VTId: string;
    ClassTaughtId: string;
    SectionIds: string;
    ReportingDate: any;
    GLType: string;
    GLTopic: string;
    ModuleId: string;
    UnitId: string;
    SessionIds: string;
    ClassTime: number;
    MethodologyIds: string;
    GLMethodologyDetails: string;
    GLLecturerPhotoFile: any;
    GLConductedBy: string;
    GLPersonDetails: string;
    GLName: string;
    GLMobile: string;
    GLEmail: string;
    GLQualification: string;
    GLAddress: string;
    GLWorkStatus: string;
    GLCompany: string;
    GLDesignation: string;
    GLWorkExperience: any;
    GLPhotoFile: any;

    UnitSessionsModels: ModuleUnitSessionModel[];
    StudentAttendances: StudentAttendanceModel[];

    Latitude: string;
    Longitude: string;
    GeoLocation: string;

    IsActive: boolean;
    RequestType: any;

    constructor(vtGuestLectureConductedItem?: any) {
        vtGuestLectureConductedItem = vtGuestLectureConductedItem || {};
        // tslint:disable: no-string-literal

        this.VTGuestLectureId = vtGuestLectureConductedItem.VTGuestLectureId || Guid.create()['value'];
        this.VTId = vtGuestLectureConductedItem.VTId || Guid.create()['value'];
        this.ClassTaughtId = vtGuestLectureConductedItem.ClassTaughtId || '';
        this.SectionIds = vtGuestLectureConductedItem.SectionIds || '';
        this.ReportingDate = vtGuestLectureConductedItem.ReportingDate || '';
        this.GLType = vtGuestLectureConductedItem.GLType || '';
        this.GLTopic = vtGuestLectureConductedItem.GLTopic || '';
        this.ModuleId = vtGuestLectureConductedItem.ModuleId || '';
        this.UnitId = vtGuestLectureConductedItem.UnitId || '';
        this.SessionIds = vtGuestLectureConductedItem.SessionIds || '';
        this.ClassTime = vtGuestLectureConductedItem.ClassTime || '';
        this.MethodologyIds = vtGuestLectureConductedItem.MethodologyIds || '';
        this.GLMethodologyDetails = vtGuestLectureConductedItem.GLMethodologyDetails || '';
        this.GLLecturerPhotoFile = vtGuestLectureConductedItem.GLLecturerPhotoFile || null;
        this.GLConductedBy = vtGuestLectureConductedItem.GLConductedBy || '';
        this.GLPersonDetails = vtGuestLectureConductedItem.GLPersonDetails || '';
        this.GLName = vtGuestLectureConductedItem.GLLecturerName || '';
        this.GLMobile = vtGuestLectureConductedItem.GLMobile || '';
        this.GLEmail = vtGuestLectureConductedItem.GLEmail || '';
        this.GLQualification = vtGuestLectureConductedItem.GLQualification || '';
        this.GLAddress = vtGuestLectureConductedItem.GLAddress || '';
        this.GLWorkStatus = vtGuestLectureConductedItem.GLWorkStatus || '';
        this.GLCompany = vtGuestLectureConductedItem.GLCompany || '';
        this.GLDesignation = vtGuestLectureConductedItem.GLDesignation || '';
        this.GLWorkExperience = vtGuestLectureConductedItem.GLWorkExperience || '';
        this.GLPhotoFile = vtGuestLectureConductedItem.GLPhotoFile || '';

        // tslint:disable: no-angle-bracket-type-assertion
        this.UnitSessionsModels = <ModuleUnitSessionModel[]> [];
        this.StudentAttendances = <StudentAttendanceModel[]> [];

        this.Latitude = vtGuestLectureConductedItem.Latitude || '';
        this.Longitude = vtGuestLectureConductedItem.Longitude || '';
        this.GeoLocation = vtGuestLectureConductedItem.GeoLocation || '';

        this.IsActive = vtGuestLectureConductedItem.IsActive || true;
        this.RequestType = 0; // New
    }
}
