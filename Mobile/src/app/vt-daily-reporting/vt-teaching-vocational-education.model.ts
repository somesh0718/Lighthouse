
import { StudentAttendanceModel } from '../models/student.attendance.model';
import { ModuleUnitSessionModel } from '../models/module-unit-session-model';

export class VTRTeachingVocationalEducationModel {

    ClassTaughtId: string;
    DidYouTeachToday: boolean
    ClassSectionIds: string;
    ActivityTypeIds: string;
    ModuleId: string;
    UnitId: string;
    SessionsTaught: string;
    ClassTypeId: string;
    ClassTime: number;
    ClassPictureFile: any;
    LessonPlanPictureFile: any;
    ReasonOfNotConductingTheClassIds: string;
    ReasonDetails: string;

    UnitSessionsModels: ModuleUnitSessionModel[]
    StudentAttendances: StudentAttendanceModel[];

    SequenceNo: number;

    IsActive: boolean;
    RequestType: any;

    constructor(vtDailyReportingItem?: any) {
        vtDailyReportingItem = vtDailyReportingItem || {};

        this.ClassTaughtId = vtDailyReportingItem.ClassTaughtId || '';
        this.DidYouTeachToday = vtDailyReportingItem.DidYouTeachToday || true;
        this.ClassSectionIds = vtDailyReportingItem.ClassSectionIds || '';
        this.ActivityTypeIds = vtDailyReportingItem.ActivityTypeIds || '';
        this.ModuleId = vtDailyReportingItem.ModuleId || '';
        this.UnitId = vtDailyReportingItem.UnitId || '';
        this.SessionsTaught = vtDailyReportingItem.SessionsTaught || '';
        this.ClassTypeId = vtDailyReportingItem.ClassTypeId || '';
        this.ClassTime = vtDailyReportingItem.ClassTime || 0;
        this.ClassPictureFile = vtDailyReportingItem.ClassPictureFile || '';
        this.LessonPlanPictureFile = vtDailyReportingItem.LessonPlanPictureFile || '';
        this.ReasonOfNotConductingTheClassIds = vtDailyReportingItem.ReasonOfNotConductingTheClassIds || '';
        this.ReasonDetails = vtDailyReportingItem.ReasonDetails || '';

        this.UnitSessionsModels = <ModuleUnitSessionModel[]>[];
        this.StudentAttendances = <StudentAttendanceModel[]>[];

        this.SequenceNo = 1;

        this.IsActive = vtDailyReportingItem.IsActive || true;
        this.RequestType = 0; // New
    }
}
