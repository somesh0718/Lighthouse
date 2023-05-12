import { Injectable } from '@angular/core';
import { MessageConstants } from './constants/message.constant';
import { HtmlConstants } from './constants/html.constant';
import { ServiceConstants } from './constants/service.constant';
import { RequestType } from './constants/request.type';
import { RegexPatternConstants } from './constants/regex.pattern.constant';

@Injectable()
export class AppConstants {
    constructor() {
        this.Messages = new MessageConstants();
        this.Html = new HtmlConstants();
        this.Services = new ServiceConstants();
        this.Regex = new RegexPatternConstants();
    }

    public static get UserId(): string {
        return this._userId;
    }
    public static set UserId(value: string) {
        this._userId = value;
    }
    public static get IsAdmin(): boolean {
        return this._isAdmin;
    }
    public static set IsAdmin(value: boolean) {
        this._isAdmin = value;
    }
    public static get AuthToken(): string {
        return this._authToken;
    }
    public static set AuthToken(value: string) {
        this._authToken = value;
    }

    // tslint:disable: variable-name
    private static _userId: string;

    private static _isAdmin: boolean;

    private static _authToken: string;

    public static AccessTokenLocalStorage = 'accessToken';
    public static AccessTokenServer = 'X-Auth-Token';
    public static UserLocalStorage = 'ems-user';
    public static ResourceAccessLocalStorage = 'resourceAccessRaw';
    public static DefaultContentTypeHeader = 'application/json; charset=utf-8';
    public static Accept = 'application/json';
    public static LoginPageUrl = '/login';
    public static CurrentPageUrl = '';
    public static RegistrationPageUrl = '/new-account';
    public static ErrorInputClass = 'has-error';
    public static SuccessInputClass = 'has-success';
    public ServerDateFormat: string = "yyyy/MM/dd hh:mm:ss a";
    public ShortDateFormat: string = "dd/MM/yyyy";
    public DateValFormat: string = "yyyy-MM-dd";
    public DateFormat: string = "dd/MM/yyyy hh:mm a";
    public FullDateFormat: string = "dd/MM/yyyy hh:mm:ss a";
    public DefaultImageUrl: string = "/src/assets/images/no-image.png";
    public DefaultImageState: any = JSON.parse('{"detail":{"checked":false,"value":"false"}}');
    public BackDatedReportingDays: number = 7;
    public ForceToUpdateAppDays:number = 7;
    public AutoUploadSyncInDays:number = 7;
    public AutoMasterSyncInDays:number = 7;

    public Messages: MessageConstants;
    public Html: HtmlConstants;
    public Services: ServiceConstants;
    public PageType: typeof RequestType = RequestType;
    public Regex: RegexPatternConstants;

    public Actions = {
        Add: 'add',
        New: 'new',
        Save: 'save',
        Edit: 'edit',
        View: 'view',
        Update: 'update',
        Delete: 'delete',
        Cancel: 'cancel',
        Clear: 'clear'
    };

    public static masterList: Array<any> = [
        {
            title: 'GetCourseModuleUnitSessions',
            sync: false,
            masterTable: 'CourseModuleUnitSessions',
            masterTableCreateQuery: 'create TABLE IF NOT EXISTS CourseModuleUnitSessions (Id INTEGER PRIMARY KEY, UnitId TEXT, ClassId TEXT, ClassName TEXT, ModuleTypeId TEXT, ModuleName TEXT, SectorId TEXT, SectorName TEXT, JobRoleId TEXT, JobRoleName TEXT, UnitName TEXT, SessionId TEXT, SessionName TEXT)',
        },
        {
            title: 'GetClassSectionsByVTId',
            sync: false,
            masterTable: 'ClassSectionsByVTId',
            masterTableCreateQuery: 'create TABLE IF NOT EXISTS ClassSectionsByVTId (Id INTEGER PRIMARY KEY, VTId TEXT, ClassId TEXT, ClassName TEXT, SectionId TEXT, SectionName TEXT)',
        },
        {
            title: 'GetCommonMasterData',
            sync: false,
            masterTable: 'CommonMasterData',
            masterTableCreateQuery: 'create TABLE IF NOT EXISTS CommonMasterData (Id INTEGER PRIMARY KEY, DataValueId TEXT, DataTypeId TEXT, ParentId TEXT, Code TEXT, Name TEXT, Description TEXT, DisplayOrder INTEGER)',
        },
        {
            title: 'GetStudentsByVTId',
            sync: false,
            masterTable: 'StudentsByVTId',
            masterTableCreateQuery: 'create TABLE IF NOT EXISTS StudentsByVTId (Id INTEGER PRIMARY KEY, VTId TEXT, ClassId TEXT, SectionId TEXT, StudentId TEXT, StudentName TEXT, IsPresent INTEGER)',
        },
        {
            title: 'GetSchoolsByVCId',
            sync: false,
            masterTable: 'SchoolsByVCId',
            masterTableCreateQuery: 'create TABLE IF NOT EXISTS SchoolsByVCId (IdUnique INTEGER PRIMARY KEY, Id TEXT, Name TEXT, Description TEXT, IsSelected INTEGER, IsDisabled INTEGER, SequenceNo INTEGER)',
        },
        {
            title: 'GetSchoolsByDRPId',
            sync: false,
            masterTable: 'SchoolsByDRPId',
            masterTableCreateQuery: 'create TABLE IF NOT EXISTS SchoolsByDRPId (IdUnique INTEGER PRIMARY KEY, Id TEXT, Name TEXT, Description TEXT, IsSelected INTEGER, IsDisabled INTEGER, SequenceNo INTEGER)',
        },
        {
            title:'GetVTByVCId',
            sync:false,
            masterTable:'VTByVCId',
            masterTableCreateQuery:'create TABLE IF NOT EXISTS VTByVCId (IdUnique INTEGER PRIMARY KEY, Id TEXT, Name TEXT, Description TEXT, IsSelected INTEGER, IsDisabled INTEGER, SequenceNo INTEGER)',
        },
        {
            title:'GetDistrictByStateId',
            sync:false,
            masterTable:'DistrictByStateId',
            masterTableCreateQuery:'create TABLE IF NOT EXISTS DistrictByStateId (IdUnique INTEGER PRIMARY KEY, Id TEXT, Name TEXT, Description TEXT, IsSelected INTEGER, IsDisabled INTEGER, SequenceNo INTEGER)',
        },
        {
            title:'GetMainIssueByUser',
            sync:false,
            masterTable:'MainIssueByUser',
            masterTableCreateQuery:'create TABLE IF NOT EXISTS MainIssueByUser (IdUnique INTEGER PRIMARY KEY, Id TEXT, Name TEXT, Description TEXT, IsSelected INTEGER, IsDisabled INTEGER, SequenceNo INTEGER)',
        },
        {
            title:'GetSubIssueByUser',
            sync:false,
            masterTable:'SubIssueByUser',
            masterTableCreateQuery:'create TABLE IF NOT EXISTS SubIssueByUser (IdUnique INTEGER PRIMARY KEY, Id TEXT, Name TEXT, Description TEXT, IsSelected INTEGER, IsDisabled INTEGER, SequenceNo INTEGER)',
        },
        {
            title:'GetSectorsByUser',
            sync:false,
            masterTable:'SectorsByUser',
            masterTableCreateQuery:'create TABLE IF NOT EXISTS SectorsByUser (IdUnique INTEGER PRIMARY KEY, Id TEXT, Name TEXT, Description TEXT, IsSelected INTEGER, IsDisabled INTEGER, SequenceNo INTEGER)',
        },
        {
            title:'GetJobRolesByUser',
            sync:false,
            masterTable:'JobRolesByUser',
            masterTableCreateQuery:'create TABLE IF NOT EXISTS JobRolesByUser (IdUnique INTEGER PRIMARY KEY, Id TEXT, Name TEXT, Description TEXT, IsSelected INTEGER, IsDisabled INTEGER, SequenceNo INTEGER)',
        },
    ];

    public static appPages = [
        {
            title: 'VT Daily Reporting',
            url: '/vt-daily-reporting',
            access: true,
            uploadTable: 'UploadVTDailyReporting',
            uploadTableCreateQuery: 'create TABLE IF NOT EXISTS UploadVTDailyReporting (Id INTEGER PRIMARY KEY, VTId TEXT, VTDailyReportingId TEXT, VTSchoolSectorId TEXT, ReportingDate INTEGER, ReportType TEXT, SchoolEventCelebration TEXT, WorkAssignedByHeadMaster TEXT, SchoolExamDuty TEXT, OtherWork TEXT, ObservationDetails TEXT, OBStudentCount INTEGER, WorkingDayTypeIds TEXT, TeachingVocationalEducations TEXT, TrainingOfTeacher TEXT, OnJobTrainingCoordination TEXT, AssessorInOtherSchoolForExam TEXT, ParentTeachersMeeting TEXT, CommunityHomeVisit TEXT, VisitToIndustries TEXT, VisitToEducationalInstitutions TEXT, AssignmentFromVocationalDepartment TEXT, TeachingNonVocationalSubject TEXT, Leave TEXT, Holiday TEXT, ObservationDay TEXT, Latitude TEXT, Longitude TEXT,GeoLocation TEXT, SequenceNo INTEGER, IsActive INTEGER, RequestType INTEGER)',
            getTable: 'GetVTDailyReporting',
            getTableCreateQuery: 'create TABLE IF NOT EXISTS GetVTDailyReporting (Id INTEGER PRIMARY KEY, VTId TEXT, VTDailyReportingId TEXT, VTSchoolSectorId TEXT, ReportingDate INTEGER, ReportType TEXT, ClassName TEXT, SectionName TEXT, StudentsPresent TEXT, ModuleTaught1 TEXT, ModuleTaught2 TEXT, ClassType TEXT, ClassHours INTEGER, ClassPicture TEXT, LessonPlanPicture TEXT, TrainingBy TEXT, TrainingType TEXT, TrainingDetails TEXT, SchoolNameExam TEXT, UdiseExam TEXT, SectorExam TEXT, JobRoleExam TEXT, ClassExam TEXT, BoysPresentExam INTEGER, GirlsPresentExam INTEGER, PhotoExam TEXT, FieldVisitDetails TEXT, ClassTaughtOther TEXT, SubjectTaughtOther TEXT, NonVeApproval TEXT, AssignmentBy TEXT, AssignmentDetails TEXT, AssignmentPhoto TEXT, HolidayType TEXT, HolidayDetails TEXT, OBSDayDetails TEXT, OBSDayStudentsPresent TEXT, PTAParentsCount INTEGER, PTAParentsInteracted TEXT, PTAParentsDetails TEXT, PTAParentsContactdetails TEXT, LeaveType TEXT, LeaveApproval TEXT, LeaveApprovedby TEXT, LeaveDetails TEXT, IsActive INTEGER, RequestType INTEGER, SchoolName TEXT, SectorName TEXT, WorkTypes TEXT)',
            getSync: false,
        },
        {
            title: 'VT Field Industry Visit Conducted',
            url: '/vt-field-industry-visit-conducted',
            access: true,
            uploadTable: 'UploadVTFieldIndustryVisitConducted',
            uploadTableCreateQuery: 'create TABLE IF NOT EXISTS UploadVTFieldIndustryVisitConducted (Id INTEGER PRIMARY KEY, VTId TEXT, VTFieldIndustryVisitConductedId TEXT, ClassTaughtId TEXT, ReportingDate TEXT, SectionIds TEXT, FieldVisitTheme TEXT, FieldVisitActivities TEXT, ModuleId TEXT, UnitId TEXT, SessionIds TEXT, FVOrganisation TEXT, FVOrganisationAddress TEXT, FVDistance TEXT, FVPictureFile TEXT, FVContactPersonName TEXT, FVContactPersonMobile TEXT, FVContactPersonEmail TEXT, FVContactPersonDesignation TEXT, FVOrganisationInterestStatus, FVOrignisationOJTStatus, FeedbackFromOrgnisation TEXT, Remarks TEXT, FVStudentSafety TEXT, UnitSessionsModels TEXT, StudentAttendances TEXT, Latitude TEXT, Longitude TEXT,GeoLocation TEXT, IsActive INTEGER, RequestType INTEGER)',
            getTable: 'GetVTFieldIndustryVisitConducted',
            getTableCreateQuery: 'create TABLE IF NOT EXISTS GetVTFieldIndustryVisitConducted (Id INTEGER PRIMARY KEY, VTId TEXT, VTFieldIndustryVisitConductedId TEXT, ClassTaughtId TEXT, ReportingDate TEXT, SectionIds TEXT, FieldVisitTheme TEXT, FieldVisitActivities TEXT, ModuleId TEXT, UnitId TEXT, SessionIds TEXT, FVOrganisation TEXT, FVOrganisationAddress TEXT, FVDistance TEXT, FVPictureFile TEXT, FVContactPersonName TEXT, FVContactPersonMobile TEXT, FVContactPersonEmail TEXT, FVContactPersonDesignation TEXT, FVOrganisationInterestStatus, FVOrignisationOJTStatus, FeedbackFromOrgnisation TEXT, Remarks TEXT, FVStudentSafety TEXT, UnitSessionsModels TEXT, StudentAttendances TEXT, Latitude TEXT, Longitude TEXT,GeoLocation TEXT, IsActive INTEGER, RequestType INTEGER, ClassName TEXT, ApprovalStatus TEXT, ApprovedDate TEXT)',
            getSync: false,
        },
        {
            title: 'VT Guest Lecture Conducted',
            url: '/vt-guest-lecture-conducted',
            access: true,
            uploadTable: 'UploadVTGuestLectureConducted',
            uploadTableCreateQuery: 'create TABLE IF NOT EXISTS UploadVTGuestLectureConducted (Id INTEGER PRIMARY KEY, VTGuestLectureId TEXT, VTId TEXT, ClassTaughtId TEXT, SectionIds TEXT, ReportingDate TEXT, GLType TEXT, GLTopic TEXT, ModuleId TEXT, UnitId TEXT, SessionIds TEXT, ClassTime INTEGER, MethodologyIds TEXT, GLMethodologyDetails TEXT, GLLecturerPhotoFile TEXT, GLConductedBy TEXT, GLPersonDetails TEXT, GLName TEXT, GLMobile TEXT, GLEmail TEXT, GLQualification TEXT, GLAddress TEXT, GLWorkStatus TEXT, GLCompany TEXT, GLDesignation TEXT, GLWorkExperience TEXT, GLPhotoFile TEXT, UnitSessionsModels TEXT, StudentAttendances TEXT, Latitude TEXT, Longitude TEXT,GeoLocation TEXT, IsActive INTEGER, RequestType INTEGER)',
            getTable: 'GetVTGuestLectureConducted',
            getTableCreateQuery: 'create TABLE IF NOT EXISTS GetVTGuestLectureConducted (Id INTEGER PRIMARY KEY, VTGuestLectureId TEXT, VTId TEXT, ClassTaughtId TEXT, SectionIds TEXT, ReportingDate TEXT, GLType TEXT, GLTopic TEXT, ModuleId TEXT, UnitId TEXT, SessionIds TEXT, ClassTime INTEGER, MethodologyIds TEXT, GLMethodologyDetails TEXT, GLLecturerPhotoFile TEXT, GLConductedBy TEXT, GLPersonDetails TEXT, GLName TEXT, GLMobile TEXT, GLEmail TEXT, GLQualification TEXT, GLAddress TEXT, GLWorkStatus TEXT, GLCompany TEXT, GLDesignation TEXT, GLWorkExperience TEXT, GLPhotoFile TEXT, UnitSessionsModels TEXT, StudentAttendances TEXT, Latitude TEXT, Longitude TEXT,GeoLocation TEXT, IsActive INTEGER, RequestType INTEGER, ClassName TEXT, ApprovalStatus TEXT, ApprovedDate TEXT)',
            getSync: false,
        },
        {
            title: 'VT Issue Reporting',
            url: '/vt-issue-reporting',
            access: true,
            uploadTable: 'UploadVTIssueReporting',
            uploadTableCreateQuery: 'CREATE TABLE IF NOT EXISTS UploadVTIssueReporting (Id INTEGER PRIMARY KEY, VTIssueReportingId TEXT, VTId TEXT, IssueReportDate TEXT, MainIssue TEXT, SubIssue TEXT, StudentClass TEXT, Month TEXT,  StudentType TEXT, NoOfStudents INTEGER, IssueDetails TEXT, GeoLocation TEXT, Latitude TEXT, Longitude TEXT, IssueStatus TEXT, IsActive INTEGER, RequestType INTEGER)',
            getTable: 'GetVTIssueReporting',
            getTableCreateQuery: 'CREATE TABLE IF NOT EXISTS GetVTIssueReporting (Id INTEGER PRIMARY KEY, IssueReportingId TEXT, IssueReportDate TEXT, MainIssue TEXT, SubIssue TEXT, StudentType TEXT, NoOfStudents INTEGER, ApprovalStatus TEXT)',
            getSync: true,
        },
        {
            title: 'VC Daily Reporting',
            url: '/vc-daily-reporting',
            access: true,
            uploadTable: 'UploadVCDailyReporting',
            uploadTableCreateQuery: 'CREATE TABLE IF NOT EXISTS UploadVCDailyReporting (Id INTEGER PRIMARY KEY, VCDailyReportingId TEXT, VCId TEXT, ReportDate TEXT, ReportType TEXT, WorkingDayTypeIds TEXT, WorkTypeDetails TEXT, SchoolId TEXT, Leave TEXT, Holiday TEXT, IndustryExposureVisit TEXT, Latitude TEXT, Longitude TEXT, GeoLocation TEXT, IsActive INTEGER, RequestType INTEGER)',
            getTable: 'GetVCDailyReporting',
            getTableCreateQuery: 'CREATE TABLE IF NOT EXISTS GetVCDailyReporting (Id INTEGER PRIMARY KEY, VCDailyReportingId TEXT, VCId TEXT, ReportDate TEXT, ReportType TEXT, WorkingDayTypeIds TEXT, WorkTypeDetails TEXT, SchoolId TEXT, Leave TEXT, Holiday TEXT, IndustryExposureVisit TEXT, Latitude TEXT, Longitude TEXT, GeoLocation TEXT, IsActive INTEGER, RequestType INTEGER, VCName TEXT, WorkTypes TEXT)',
            getSync: false,
        },
        {
            title: 'VC School Visits',
            url: '/vc-school-visit',
            access: true,
            uploadTable: 'UploadVCSchoolVisits',
            uploadTableCreateQuery: 'create TABLE IF NOT EXISTS UploadVCSchoolVisits (Id INTEGER PRIMARY KEY, VCId TEXT,VCSchoolVisitId TEXT, VCSchoolSectorId TEXT, ReportDate INTEGER, GeoLocation TEXT, Month TEXT, VTReportSubmitted TEXT, VTWorkingDays INTEGER, VRLeaveDays INTEGER, VTTeachingDays INTEGER, ClassVisited TEXT, ClassTeachingDays INTEGER, BoysEnrolledCheck INTEGER, GirlsEnrolledCheck INTEGER, AvgStudentAttendance INTEGER, CMAvailability TEXT, CMDate INTEGER, TEAvailability TEXT, TEDate INTEGER, NoOfGLConducted INTEGER, NoOfFVConducted INTEGER, SchoolHMVisited TEXT, HMRatingVTattendance INTEGER, HMRatingSyllabuscompletion INTEGER, HMRatingVtreporting INTEGER, HMRatingVtqualityteaching INTEGER, HMRatingVtglfvquality INTEGER, HMRatingInitiativestaken INTEGER, IsActive INTEGER, RequestType INTEGER)',
            getTable: 'GetVCSchoolVisits',
            getTableCreateQuery: 'create TABLE IF NOT EXISTS GetVCSchoolVisits (Id INTEGER PRIMARY KEY, VCId TEXT, VCSchoolVisitId TEXT, VCSchoolSectorId TEXT, ReportDate INTEGER, GeoLocation TEXT, Month TEXT, VTReportSubmitted TEXT, VTWorkingDays INTEGER, VRLeaveDays INTEGER, VTTeachingDays INTEGER, ClassVisited TEXT, ClassTeachingDays INTEGER, BoysEnrolledCheck INTEGER, GirlsEnrolledCheck INTEGER, AvgStudentAttendance INTEGER, CMAvailability TEXT, CMDate INTEGER, TEAvailability TEXT, TEDate INTEGER, NoOfGLConducted INTEGER, NoOfFVConducted INTEGER, SchoolHMVisited TEXT, HMRatingVTattendance INTEGER, HMRatingSyllabuscompletion INTEGER, HMRatingVtreporting INTEGER, HMRatingVtqualityteaching INTEGER, HMRatingVtglfvquality INTEGER, HMRatingInitiativestaken INTEGER, IsActive INTEGER, RequestType INTEGER, SchoolName TEXT, SectorName TEXT)',
            getSync: false,
        },
        {
            title: 'VC Issue Reporting',
            url: '/vc-issue-reporting',
            access: true,
            uploadTable: 'UploadVCIssueReporting',
            uploadTableCreateQuery: 'CREATE TABLE IF NOT EXISTS UploadVCIssueReporting (Id INTEGER PRIMARY KEY, VCIssueReportingId TEXT, VCId TEXT, IssueReportDate TEXT, MainIssue TEXT, SubIssue TEXT, StudentClass TEXT, Month TEXT,  StudentType TEXT, NoOfStudents INTEGER, IssueDetails TEXT, GeoLocation TEXT, Latitude TEXT, Longitude TEXT, IssueStatus TEXT, IsActive INTEGER, RequestType INTEGER)',
            getTable: 'GetVCIssueReporting',
            getTableCreateQuery: 'CREATE TABLE IF NOT EXISTS GetVCIssueReporting (Id INTEGER PRIMARY KEY, IssueReportingId TEXT, IssueReportDate TEXT, MainIssue TEXT, SubIssue TEXT, StudentType TEXT, NoOfStudents INTEGER, ApprovalStatus TEXT)',
            getSync: true,
        },
        {
            title: 'HM Issue Reporting',
            url: '/hm-issue-reporting',
            access: true,
            uploadTable: 'UploadHMIssueReporting',
            uploadTableCreateQuery: 'CREATE TABLE IF NOT EXISTS UploadHMIssueReporting (Id INTEGER PRIMARY KEY, HMIssueReportingId TEXT, HMId TEXT, IssueReportDate TEXT, MainIssue TEXT, SubIssue TEXT, StudentClass TEXT, Month TEXT,  StudentType TEXT, NoOfStudents INTEGER, IssueDetails TEXT, GeoLocation TEXT, Latitude TEXT, Longitude TEXT, IssueStatus TEXT, IsActive INTEGER, RequestType INTEGER)',
            getTable: 'GetHMIssueReporting',
            getTableCreateQuery: 'CREATE TABLE IF NOT EXISTS GetHMIssueReporting (Id INTEGER PRIMARY KEY, IssueReportingId TEXT, IssueReportDate TEXT, MainIssue TEXT, SubIssue TEXT, StudentType TEXT, NoOfStudents INTEGER, ApprovalStatus TEXT)',
            getSync: true,
        },       
        {
            title: 'VC School Visit Reporting',
            url: '/vc-school-visit-reporting',
            access: true,
            uploadTable: 'UploadVCSchoolVisitReporting',
            uploadTableCreateQuery: 'create TABLE IF NOT EXISTS UploadVCSchoolVisitReporting (Id INTEGER PRIMARY KEY, VCSchoolVisitReportingId TEXT, VCId TEXT, CompanyName TEXT, Month TEXT, VisitDate TEXT, SchoolId TEXT, DistrictCode TEXT, SchoolEmailId TEXT, PrincipalName TEXT, PrincipalPhoneNo INTEGER, SectorId TEXT, JobRoleId TEXT, VTId TEXT, VTPhoneNo INTEGER, Labs TEXT, Books TEXT, NoOfGLConducted INTEGER, NoOfIndustrialVisits INTEGER, SVPhotoWithPrincipalFile TEXT, SVPhotoWithStudentFile TEXT, Class9Boys INTEGER, Class9Girls INTEGER,Class10Boys INTEGER, Class10Girls INTEGER, Class11Boys INTEGER, Class11Girls INTEGER,Class12Boys INTEGER, Class12Girls INTEGER,  TotalBoys INTEGER, TotalGirls INTEGER, Latitude TEXT, Longitude TEXT, GeoLocation TEXT, IsActive INTEGER, RequestType INTEGER)',
            getTable: 'GetVCSchoolVisitReporting',
            getTableCreateQuery: 'create TABLE IF NOT EXISTS GetVCSchoolVisitReporting (Id INTEGER PRIMARY KEY, VCSchoolVisitReportingId TEXT, VCId TEXT, VCName TEXT, VTName TEXT, SchoolName TEXT, DistrictName TEXT, VisitDate TEXT, TotalBoys INTEGER, TotalGirls INTEGER, IsActive INTEGER)',
            getSync: false,
        },
        {
            title: 'DRP Daily Reporting',
            url: '/drp-daily-reporting',
            access: true,
            uploadTable: 'UploadDRPDailyReporting',
            uploadTableCreateQuery: 'CREATE TABLE IF NOT EXISTS UploadDRPDailyReporting (Id INTEGER PRIMARY KEY, DRPDailyReportingId TEXT, DRPId TEXT, ReportDate TEXT, ReportType TEXT, WorkingDayTypeIds TEXT, WorkTypeDetails TEXT, SchoolId TEXT, Leave TEXT, Holiday TEXT, IndustryExposureVisit TEXT, Latitude TEXT, Longitude TEXT, GeoLocation TEXT, IsActive INTEGER, RequestType INTEGER)',
            getTable: 'GetDRPDailyReporting',
            getTableCreateQuery: 'CREATE TABLE IF NOT EXISTS GetDRPDailyReporting (Id INTEGER PRIMARY KEY, DRPDailyReportingId TEXT, DRPId TEXT, ReportDate TEXT, ReportType TEXT, WorkingDayTypeIds TEXT, WorkTypeDetails TEXT, SchoolId TEXT, Leave TEXT, Holiday TEXT, IndustryExposureVisit TEXT, Latitude TEXT, Longitude TEXT, GeoLocation TEXT, IsActive INTEGER, RequestType INTEGER, DRPName TEXT, WorkTypes TEXT)',
            getSync: false,
        },
        {
            title: 'Complaint Registration',
            url: '/complaint-registration',
            access: true,
            uploadTable: 'UploadComplaintRegistration',
            uploadTableCreateQuery: 'create TABLE IF NOT EXISTS ComplaintRegistration (Id INTEGER PRIMARY KEY, ComplaintRegistrationId TEXT, UserType TEXT, UserName TEXT, EmailId TEXT, Subject TEXT, IssueStatus TEXT, IsActive INTEGER)',
            getTable: 'ComplaintRegistration',
            getTableCreateQuery: 'create TABLE IF NOT EXISTS ComplaintRegistration (Id INTEGER PRIMARY KEY, ComplaintRegistrationId TEXT, UserType TEXT, UserName TEXT, EmailId TEXT, Subject TEXT, IssueStatus TEXT, IsActive INTEGER)',
            getSync: true,
        },
        {
            title: 'Broadcast Message',
            url: '/broadcast-message',
            access: true,
            uploadTable: 'UploadBroadcastMessage',
            uploadTableCreateQuery: 'create TABLE IF NOT EXISTS BroadcastMessages (Id INTEGER PRIMARY KEY, BroadcastMessageId TEXT, MessageText TEXT, FromDate TEXT, ToDate TEXT, ApplicableFor TEXT, IsActive INTEGER)',
            getTable: 'BroadcastMessages',
            getTableCreateQuery: 'create TABLE IF NOT EXISTS BroadcastMessages (Id INTEGER PRIMARY KEY, BroadcastMessageId TEXT, MessageText TEXT, FromDate TEXT, ToDate TEXT, ApplicableFor TEXT, IsActive INTEGER)',
            getSync: true,
        }
    ];

    public DocumentType = {
        VTP: "VTPCertificate",
        VTReporting: "VTDailyReporting",
        GuestLecture: "GuestLecture",
        FieldVisit: "FieldIndustryVisits",
        BulkUploadData: 'BulkUpload',
        ComplaintRegistration: 'ComplaintScreenshots',
        VCSchoolVisitReport: 'VCSchoolVisitReport'
    };
}
