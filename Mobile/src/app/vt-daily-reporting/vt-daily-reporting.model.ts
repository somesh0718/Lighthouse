import { Guid } from 'guid-typescript';
import { VTRTeachingVocationalEducationModel } from './vt-teaching-vocational-education.model';
import { VTRCommunityHomeVisitModel } from './vt-community-home-visit.model';
import { VTRParentTeachersMeetingModel } from './vt-parent-teachers-meeting.model';
import { VTRTrainingOfTeacherModel } from './vt-training-of-teacher.model';
import { VTROnJobTrainingCoordinationModel } from './vt-on-job-training-coordination.model';
import { VTRAssessorInOtherSchoolForExamModel } from './vt-assessor-in-other-school-for-exam.model';
import { VTRVisitToIndustryModel } from './vt-visit-to-industry.model';
import { VTRVisitToEducationalInstitutionModel } from './vt-visit-to-educational-institution.model';
import { VTRAssignmentFromVocationalDepartmentModel } from './vt-assignment-from-vocational-department.model';
import { VTRTeachingNonVocationalSubjectModel } from './vt-teaching-non-vocational-subject.model';
import { VTRWorkAssignedByHeadMasterModel } from './vt-work-assigned-by-head-master.model';
import { VTRHolidayModel } from './vt-holiday.model';
import { VTRObservationDayModel } from './vt-observation-day.model';
import { VTRLeaveModel } from './vt-leave.model';
import { ModuleUnitSessionModel } from '../models/module-unit-session-model';

export class VTDailyReportingModel {
    VTId: string;
    VTDailyReportingId: string;
    VTSchoolSectorId: string;
    ReportingDate: any;
    ReportType: string;
    WorkingDayTypeIds: any;

    TeachingVocationalEducation: VTRTeachingVocationalEducationModel;
    TeachingVocationalEducations = [];

    ParentTeachersMeeting: VTRParentTeachersMeetingModel;
    CommunityHomeVisit: VTRCommunityHomeVisitModel;
    TrainingOfTeacher: VTRTrainingOfTeacherModel;
    OnJobTrainingCoordination: VTROnJobTrainingCoordinationModel;
    AssessorInOtherSchoolForExam: VTRAssessorInOtherSchoolForExamModel;

    VisitToIndustry: VTRVisitToIndustryModel;
    VisitToIndustries = [];

    VisitToEducationalInstitution: VTRVisitToEducationalInstitutionModel;
    VisitToEducationalInstitutions = [];

    AssignmentFromVocationalDepartment: VTRAssignmentFromVocationalDepartmentModel;
    TeachingNonVocationalSubject: VTRTeachingNonVocationalSubjectModel;
    Holiday: VTRHolidayModel;
    ObservationDay: VTRObservationDayModel;
    Leave: VTRLeaveModel;

    UnitSessionsModels = [];
    SchoolEventCelebration: string;
    WorkAssignedByHeadMaster: string;
    SchoolExamDuty: string;
    OtherWork: string;
    ObservationDetails: string;
    OBStudentCount: number;

    Latitude: string;
    Longitude: string;
    GeoLocation: string;


    IsActive: boolean;
    RequestType: any;

    constructor(vtDailyReportingItem?: any) {
        vtDailyReportingItem = vtDailyReportingItem || {};

        // tslint:disable: no-string-literal
        this.VTId = vtDailyReportingItem.VTId || Guid.create()['value'];
        this.VisitToIndustries = ([] as VTRVisitToIndustryModel[]);
        this.VisitToEducationalInstitutions = ([] as VTRVisitToEducationalInstitutionModel[]);
        this.UnitSessionsModels = ([] as ModuleUnitSessionModel[]);

        this.VTDailyReportingId = vtDailyReportingItem.VTDailyReportingId || Guid.create()['value'];
        this.VTSchoolSectorId = vtDailyReportingItem.VTSchoolSectorId || Guid.create()['value'];
        this.ReportingDate = vtDailyReportingItem.ReportingDate || '';
        this.ReportType = vtDailyReportingItem.ReportType || '';
        this.WorkingDayTypeIds = vtDailyReportingItem.WorkingDayTypeIds || '';

        this.SchoolEventCelebration = vtDailyReportingItem.SchoolEventCelebration || '';
        this.WorkAssignedByHeadMaster = vtDailyReportingItem.WorkAssignedByHeadMaster || '';
        this.SchoolExamDuty = vtDailyReportingItem.SchoolExamDuty || '';
        this.OtherWork = vtDailyReportingItem.OtherWork || '';
        this.ObservationDetails = vtDailyReportingItem.ObservationDetails || '';
        this.OBStudentCount = vtDailyReportingItem.OBStudentCount || '0';

        if (vtDailyReportingItem.TeachingVocationalEducation != null) {
            this.TeachingVocationalEducation = new VTRTeachingVocationalEducationModel();
            vtDailyReportingItem.TeachingVocationalEducation = vtDailyReportingItem.TeachingVocationalEducation || new VTRTeachingVocationalEducationModel();

            this.TeachingVocationalEducation.ClassTaughtId = vtDailyReportingItem.TeachingVocationalEducation.ClassTaughtId || '';
            this.TeachingVocationalEducation.ClassSectionIds = vtDailyReportingItem.TeachingVocationalEducation.ClassSectionIds || '';
            this.TeachingVocationalEducation.ActivityTypeIds = vtDailyReportingItem.TeachingVocationalEducation.ActivityTypeIds || '';
            this.TeachingVocationalEducation.ModuleId = vtDailyReportingItem.TeachingVocationalEducation.ModuleId || '';
            this.TeachingVocationalEducation.UnitId = vtDailyReportingItem.TeachingVocationalEducation.UnitId || '';
            this.TeachingVocationalEducation.SessionsTaught = vtDailyReportingItem.TeachingVocationalEducation.SessionsTaught || '';
            this.TeachingVocationalEducation.ClassTypeId = vtDailyReportingItem.TeachingVocationalEducation.ClassTypeId || '';
            this.TeachingVocationalEducation.ClassTime = vtDailyReportingItem.TeachingVocationalEducation.ClassTime || 0;
            this.TeachingVocationalEducation.ClassPictureFile = vtDailyReportingItem.TeachingVocationalEducation.ClassPictureFile || '';
            this.TeachingVocationalEducation.LessonPlanPictureFile = vtDailyReportingItem.TeachingVocationalEducation.LessonPlanPictureFile || '';
            this.TeachingVocationalEducation.ReasonOfNotConductingTheClassIds = vtDailyReportingItem.TeachingVocationalEducation.ReasonOfNotConductingTheClassIds || '';
            this.TeachingVocationalEducation.ReasonDetails = vtDailyReportingItem.TeachingVocationalEducation.ReasonDetails || '';
            this.TeachingVocationalEducation.SequenceNo = 1;
        }

        // Training Of Teacher
        if (vtDailyReportingItem.TrainingOfTeacher != null) {
            this.TrainingOfTeacher = new VTRTrainingOfTeacherModel();
            vtDailyReportingItem.TrainingOfTeacher = vtDailyReportingItem.TrainingOfTeacher || new VTRTrainingOfTeacherModel();

            this.TrainingOfTeacher.TrainingBy = vtDailyReportingItem.TrainingOfTeacher.TrainingBy || '';
            this.TrainingOfTeacher.TrainingTypeId = vtDailyReportingItem.TrainingOfTeacher.TrainingTypeId || '';
            this.TrainingOfTeacher.TrainingTopicIds = vtDailyReportingItem.TrainingOfTeacher.TrainingTopicIds || '';
            this.TrainingOfTeacher.TrainingDetails = vtDailyReportingItem.TrainingOfTeacher.TrainingDetails || '';
        }

        // On Job Training Activity
        if (vtDailyReportingItem.OnJobTrainingCoordination != null) {
            this.OnJobTrainingCoordination = new VTROnJobTrainingCoordinationModel();
            vtDailyReportingItem.OnJobTrainingCoordination = vtDailyReportingItem.OnJobTrainingCoordination || new VTROnJobTrainingCoordinationModel();

            this.OnJobTrainingCoordination.OnJobTrainingActivityId = vtDailyReportingItem.OnJobTrainingCoordination.OnJobTrainingActivityId || '';
            this.OnJobTrainingCoordination.IndustryName = vtDailyReportingItem.OnJobTrainingCoordination.IndustryName || '';
            this.OnJobTrainingCoordination.IndustryContactPerson = vtDailyReportingItem.OnJobTrainingCoordination.IndustryContactPerson || '';
            this.OnJobTrainingCoordination.IndustryContactNumber = vtDailyReportingItem.OnJobTrainingCoordination.IndustryContactNumber || 0;
            this.OnJobTrainingCoordination.OJTActivityDetails = vtDailyReportingItem.OnJobTrainingCoordination.OJTActivityDetails || '';
        }

        // Assessor In Other School Exam
        if (vtDailyReportingItem.AssessorInOtherSchoolForExam != null) {
            this.AssessorInOtherSchoolForExam = new VTRAssessorInOtherSchoolForExamModel();
            vtDailyReportingItem.AssessorInOtherSchoolForExam = vtDailyReportingItem.AssessorInOtherSchoolForExam || new VTRAssessorInOtherSchoolForExamModel();

            this.AssessorInOtherSchoolForExam.SchoolName = vtDailyReportingItem.AssessorInOtherSchoolForExam.SchoolName || '';
            this.AssessorInOtherSchoolForExam.Udise = vtDailyReportingItem.AssessorInOtherSchoolForExam.Udise || '';
            this.AssessorInOtherSchoolForExam.ClassId = vtDailyReportingItem.AssessorInOtherSchoolForExam.ClassId || '';
            this.AssessorInOtherSchoolForExam.BoysPresent = vtDailyReportingItem.AssessorInOtherSchoolForExam.BoysPresent || 0;
            this.AssessorInOtherSchoolForExam.GirlsPresent = vtDailyReportingItem.AssessorInOtherSchoolForExam.GirlsPresent || 0;
            this.AssessorInOtherSchoolForExam.ExamDetails = vtDailyReportingItem.AssessorInOtherSchoolForExam.ExamDetails || '';
        }

        // Assignment From vocational department
        if (vtDailyReportingItem.AssignmentFromVocationalDepartment != null) {
            this.AssignmentFromVocationalDepartment = new VTRAssignmentFromVocationalDepartmentModel();
            vtDailyReportingItem.AssignmentFromVocationalDepartment = vtDailyReportingItem.AssignmentFromVocationalDepartment || new VTRAssignmentFromVocationalDepartmentModel();

            this.AssignmentFromVocationalDepartment.AssigmentNumber = vtDailyReportingItem.AssignmentFromVocationalDepartment.AssigmentNumber || 0;
            this.AssignmentFromVocationalDepartment.AssignmentDetails = vtDailyReportingItem.AssignmentFromVocationalDepartment.AssignmentDetails || '';
            this.AssignmentFromVocationalDepartment.AssignmentPhotoFile = vtDailyReportingItem.AssignmentFromVocationalDepartment.AssignmentPhotoFile || '';
        }

        // Parent Teachers Meeting : Start
        if (vtDailyReportingItem.ParentTeachersMeeting != null) {
            this.ParentTeachersMeeting = new VTRParentTeachersMeetingModel();
            vtDailyReportingItem.ParentTeachersMeeting = vtDailyReportingItem.ParentTeachersMeeting || new VTRParentTeachersMeetingModel();

            this.ParentTeachersMeeting.VocationalParentsCount = vtDailyReportingItem.ParentTeachersMeeting.VocationalParentsCount || 0;
            this.ParentTeachersMeeting.OtherParentsCount = vtDailyReportingItem.ParentTeachersMeeting.OtherParentsCount || 0;
            this.ParentTeachersMeeting.PTADetails = vtDailyReportingItem.ParentTeachersMeeting.PTADetails || '';
        }
        // Parent Teachers Meeting : End

        // Community Home Visit : Start
        if (vtDailyReportingItem.CommunityHomeVisit != null) {
            this.CommunityHomeVisit = new VTRCommunityHomeVisitModel();
            vtDailyReportingItem.CommunityHomeVisit = vtDailyReportingItem.CommunityHomeVisit || new VTRCommunityHomeVisitModel();

            this.CommunityHomeVisit.VocationalParentsCount = vtDailyReportingItem.CommunityHomeVisit.VocationalParentsCount || 0;
            this.CommunityHomeVisit.OtherParentsCount = vtDailyReportingItem.CommunityHomeVisit.OtherParentsCount || 0;
            this.CommunityHomeVisit.CommunityVisitDetails = vtDailyReportingItem.CommunityHomeVisit.CommunityVisitDetails || '';
        }
        // Community Home Visit : End

        // Industry Visit : Start
        if (vtDailyReportingItem.VisitToIndustry != null) {
            this.VisitToIndustry = new VTRVisitToIndustryModel();
            vtDailyReportingItem.VisitToIndustry = vtDailyReportingItem.VisitToIndustry || new VTRVisitToIndustryModel();

            this.VisitToIndustry.IndustryVisitCount = vtDailyReportingItem.VisitToIndustry.IndustryVisitCount || 0;
            this.VisitToIndustry.IndustryName = vtDailyReportingItem.VisitToIndustry.IndustryName || '';
            this.VisitToIndustry.IndustryContactPerson = vtDailyReportingItem.VisitToIndustry.IndustryContactPerson || '';
            this.VisitToIndustry.IndustryContactNumber = vtDailyReportingItem.VisitToIndustry.IndustryContactNumber || 0;
            this.VisitToIndustry.IndustryVisitDetails = vtDailyReportingItem.VisitToIndustry.IndustryVisitDetails || '';
        }

        // Visit To Educational Institution
        if (vtDailyReportingItem.VisitToEducationalInstitution != null) {
            this.VisitToEducationalInstitution = new VTRVisitToEducationalInstitutionModel();
            vtDailyReportingItem.VisitToEducationalInstitution = vtDailyReportingItem.VisitToEducationalInstitution || new VTRVisitToEducationalInstitutionModel();

            this.VisitToEducationalInstitution.EducationalInstituteVisitCount = vtDailyReportingItem.VisitToEducationalInstitution.EducationalInstituteVisitCount || 0;
            this.VisitToEducationalInstitution.EducationalInstitute = vtDailyReportingItem.VisitToEducationalInstitution.EducationalInstitute || '';
            this.VisitToEducationalInstitution.InstituteContactPerson = vtDailyReportingItem.VisitToEducationalInstitution.InstituteContactPerson || '';
            this.VisitToEducationalInstitution.InstituteContactNumber = vtDailyReportingItem.VisitToEducationalInstitution.InstituteContactNumber || 0;
            this.VisitToEducationalInstitution.InstituteVisitDetails = vtDailyReportingItem.VisitToEducationalInstitution.InstituteVisitDetails || '';
        }

        // TeachingNonVocationalSubject
        if (vtDailyReportingItem.TeachingNonVocationalSubject != null) {
            this.TeachingNonVocationalSubject = new VTRTeachingNonVocationalSubjectModel();
            vtDailyReportingItem.TeachingNonVocationalSubject = vtDailyReportingItem.TeachingNonVocationalSubject || new VTRTeachingNonVocationalSubjectModel();

            this.TeachingNonVocationalSubject.OtherClassTakenDetails = vtDailyReportingItem.TeachingNonVocationalSubject.OtherClassTakenDetails || '';
            this.TeachingNonVocationalSubject.OtherClassTime = vtDailyReportingItem.TeachingNonVocationalSubject.OtherClassTime || 0;
        }

        // Holiday
        if (vtDailyReportingItem.Holiday != null) {
            this.Holiday = new VTRHolidayModel();
            vtDailyReportingItem.Holiday = vtDailyReportingItem.Holiday || new VTRHolidayModel();
            this.Holiday.HolidayTypeId = vtDailyReportingItem.Holiday.HolidayTypeId || '';
            this.Holiday.HolidayDetails = vtDailyReportingItem.Holiday.HolidayDetails || '';
        }

        // Observation Day
        if (vtDailyReportingItem.ObservationDay != null) {
            this.ObservationDay = new VTRObservationDayModel();
            vtDailyReportingItem.ObservationDay = vtDailyReportingItem.ObservationDay || new VTRObservationDayModel();

            this.ObservationDay.VTId = vtDailyReportingItem.ObservationDay.VTId || '';
            this.ObservationDay.ClassId = vtDailyReportingItem.ObservationDay.ClassId || '';
            this.ObservationDay.StudentId = vtDailyReportingItem.ObservationDay.StudentId || '';
            this.ObservationDay.IsPresent = vtDailyReportingItem.ObservationDay.IsPresent || '';
        }

        // Leave
        if (vtDailyReportingItem.Leave != null) {
            this.Leave = new VTRLeaveModel();
            vtDailyReportingItem.Leave = vtDailyReportingItem.Leave || new VTRLeaveModel();

            this.Leave.LeaveTypeId = vtDailyReportingItem.Leave.LeaveTypeId || '';
            this.Leave.LeaveApprovalStatus = vtDailyReportingItem.Leave.LeaveApprovalStatus || '';
            this.Leave.LeaveApprover = vtDailyReportingItem.Leave.LeaveApprover || '';
            this.Leave.LeaveReason = vtDailyReportingItem.Leave.LeaveReason || '';
        }

        this.Latitude = vtDailyReportingItem.Latitude || '';
        this.Longitude = vtDailyReportingItem.Longitude || '';
        this.GeoLocation = vtDailyReportingItem.GeoLocation || '';

        this.IsActive = vtDailyReportingItem.IsActive || true;
        this.RequestType = 0; // New
    }
}
