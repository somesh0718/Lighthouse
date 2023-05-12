using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Igmite.Lighthouse.DAL.EF
{
    public class IgmiteDbContext : DbContext
    {
        public IgmiteDbContext(DbContextOptions<IgmiteDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Log(CoreEventId.LazyLoadOnDisposedContextWarning));

            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseMySql(EntityConstants.SQLConnectionString)
                .UseLazyLoadingProxies(false) // <-- enable Lazy Loading
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassSectionModel>().HasKey(c => new { c.VTId, c.ClassId, c.SectionId });
            modelBuilder.Entity<ModuleUnitSessionModel>().HasKey(s => new { s.ClassId, s.ModuleTypeId, s.UnitId, s.SessionId });
            modelBuilder.Entity<StudentByVTModel>().HasKey(s => new { s.VTId, s.ClassId, s.SectionId, s.StudentId });
            modelBuilder.Entity<VTAttendanceDetailModel>().HasKey(a => new { a.VTId, a.ReportType, a.ReportingDate });
            modelBuilder.Entity<VCAttendanceDetailModel>().HasKey(a => new { a.VCId, a.ReportType, a.ReportingDate });
            modelBuilder.Entity<DsDataManagement>().HasKey(a => new { a.AcademicYearId, a.ReportDate });
            modelBuilder.Entity<StudentClassViewModel>().HasKey(a => new { a.StudentId, a.AcademicYear, a.ClassName });
            modelBuilder.Entity<VTPSectorViewModel>().HasKey(a => new { a.VTPSectorId, a.AcademicYear, a.VTPName });
            modelBuilder.Entity<SchoolVTPSectorViewModel>().HasKey(a => new { a.SchoolVTPSectorId, a.AcademicYear, a.VTPName });
            modelBuilder.Entity<VTSchoolSectorViewModel>().HasKey(a => new { a.VTSchoolSectorId, a.AcademicYear, a.VTName });
            modelBuilder.Entity<VTClassViewModel>().HasKey(a => new { a.VTClassId, a.AcademicYear, a.VTName });
            modelBuilder.Entity<VCSchoolSectorViewModel>().HasKey(a => new { a.VCSchoolSectorId, a.AcademicYear, a.VCName });
            modelBuilder.Entity<LighthouseParams>().Property(x => x.LighthouseParamId).UseMySqlIdentityColumn();
            modelBuilder.Entity<MessageTemplate>().Property(x => x.MessageTemplateId).UseMySqlIdentityColumn();
            modelBuilder.Entity<VocationalTrainer>().Ignore(t => t.VCTrainer);
            modelBuilder.Entity<VocationalCoordinatorViewModel>().HasKey(a => new { a.AcademicYearId, a.VTPId, a.VCId });
            modelBuilder.Entity<VocationalTrainerViewModel>().HasKey(a => new { a.AcademicYearId, a.VTPId, a.VCId, a.VTId });
            modelBuilder.Entity<HeadMasterViewModel>().HasKey(a => new { a.AcademicYearId, a.SchoolId, a.HMId });
            modelBuilder.Entity<DropdownModel<string>>().HasKey(a => new { a.Id, a.Name });

            base.OnModelCreating(modelBuilder);
        }

        #region "Entity"

        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        public virtual DbSet<AccountRole> AccountRoles { get; set; }
        public virtual DbSet<AccountWorkLocation> AccountWorkLocations { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<LogoutHistory> LogoutHistories { get; set; }
        public virtual DbSet<AccountTransaction> AccountTransactions { get; set; }
        public virtual DbSet<AccountUserOTP> AccountUserOTPs { get; set; }
        public virtual DbSet<AccountUserTerm> AccountUserTerms { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CourseMaterial> CourseMaterials { get; set; }
        public virtual DbSet<CourseModule> CourseModules { get; set; }
        public virtual DbSet<CourseUnitSession> CourseUnitSessions { get; set; }
        public virtual DbSet<DataType> DataTypes { get; set; }
        public virtual DbSet<DataValue> DataValues { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Employer> Employers { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<ForgotPasswordHistory> ForgotPasswordHistories { get; set; }
        public virtual DbSet<HeadMaster> HeadMasters { get; set; }
        public virtual DbSet<HMIssueReporting> HMIssueReportings { get; set; }
        public virtual DbSet<HMSchoolsMap> HMSchoolMap { get; set; }
        public virtual DbSet<JobRole> JobRoles { get; set; }
        public virtual DbSet<Phase> Phases { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleTransaction> RoleTransactions { get; set; }
        public virtual DbSet<SchoolCategory> SchoolCategories { get; set; }
        public virtual DbSet<SchoolClass> SchoolClasses { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<SchoolVEIncharge> SchoolVEIncharges { get; set; }
        public virtual DbSet<SchoolVTPSector> SchoolVTPSectors { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<SectorJobRole> SectorJobRoles { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }
        public virtual DbSet<SiteHeader> SiteHeaders { get; set; }
        public virtual DbSet<SiteSubHeader> SiteSubHeaders { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<StudentClassDetail> StudentClassDetails { get; set; }
        public virtual DbSet<StudentClass> StudentClasses { get; set; }
        public virtual DbSet<TermsCondition> TermsConditions { get; set; }
        public virtual DbSet<ToolEquipment> ToolEquipments { get; set; }
        public virtual DbSet<ToolEquipmentRoomDamaged> ToolEquipmentsRoomDamaged { get; set; }
        public virtual DbSet<TEToolList> TEToolLists { get; set; }
        public virtual DbSet<TEMaterialList> TEMaterialLists { get; set; }
        public virtual DbSet<TEAndRMList> TEAndRMList { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<UserAcceptance> UserAcceptances { get; set; }
        public virtual DbSet<UserOTPDetail> UserOTPDetails { get; set; }
        public virtual DbSet<VCDailyReporting> VCDailyReportings { get; set; }
        public virtual DbSet<DRPDailyReporting> DRPDailyReportings { get; set; }
        public virtual DbSet<VCIssueReporting> VCIssueReportings { get; set; }
        public virtual DbSet<VCRHoliday> VCRHolidays { get; set; }
        public virtual DbSet<VCRLeave> VCRLeaves { get; set; }
        public virtual DbSet<VCRSchoolVisit> VCRSchoolVisits { get; set; }
        public virtual DbSet<VCRPratical> VCRPraticals { get; set; }
        public virtual DbSet<DRPRIndustryExposureVisit> DRPRIndustryExposureVisits { get; set; }
        public virtual DbSet<VCRIndustryExposureVisit> VCRIndustryExposureVisits { get; set; }
        public virtual DbSet<VCRWorkingDayType> VCRWorkingDayTypes { get; set; }
        public virtual DbSet<DRPRHoliday> DRPRHolidays { get; set; }
        public virtual DbSet<DRPRLeave> DRPRLeaves { get; set; }
        public virtual DbSet<DRPRSchoolVisit> DRPRSchoolVisits { get; set; }
        public virtual DbSet<DRPRWorkingDayType> DRPRWorkingDayTypes { get; set; }
        public virtual DbSet<VCSchoolSector> VCSchoolSectors { get; set; }
        public virtual DbSet<VCSchoolVisit> VCSchoolVisits { get; set; }
        public virtual DbSet<VCSchoolVisitReporting> VCSchoolVisitReporting { get; set; }
        public virtual DbSet<VocationalCoordinator> VocationalCoordinators { get; set; }
        public virtual DbSet<VocationalTrainer> VocationalTrainers { get; set; }
        public virtual DbSet<VCTrainerMap> VCTrainersMap { get; set; }
        public virtual DbSet<VocationalTrainingProvider> VocationalTrainingProviders { get; set; }
        public virtual DbSet<VTClass> VTClasses { get; set; }
        public virtual DbSet<VTClassSection> VTClassSections { get; set; }
        public virtual DbSet<VTDailyReporting> VTDailyReportings { get; set; }
        public virtual DbSet<VTFieldIndustryVisitConducted> VTFieldIndustryVisitConducteds { get; set; }
        public virtual DbSet<VTFSection> VTFSections { get; set; }
        public virtual DbSet<VTFUnitSessionsTaught> VTFUnitSessionsTaughts { get; set; }
        public virtual DbSet<VTFStudentAttendance> VTFStudentAttendances { get; set; }
        public virtual DbSet<VTGuestLectureConducted> VTGuestLectureConducteds { get; set; }
        public virtual DbSet<VTGStudentAttendance> VTGStudentAttendances { get; set; }
        public virtual DbSet<VTGUnitSessionsTaught> VTGUnitSessionsTaughts { get; set; }
        public virtual DbSet<VTGMethodology> VTGMethodologies { get; set; }
        public virtual DbSet<VTGSection> VTGSections { get; set; }
        public virtual DbSet<VTIssueReporting> VTIssueReportings { get; set; }
        public virtual DbSet<VTMonthlyTeachingPlan> VTMonthlyTeachingPlans { get; set; }
        public virtual DbSet<VTPAcademicYearsMap> VTPAcademicYearMap { get; set; }
        public virtual DbSet<VTPCoordinatorsMap> VTPCoordinatorMap { get; set; }
        public virtual DbSet<VTPMonthlyBillSubmissionStatus> VTPMonthlyBillSubmissionStatus { get; set; }
        public virtual DbSet<VTPracticalAssessment> VTPracticalAssessments { get; set; }
        public virtual DbSet<VTPSectorJobRole> VTPSectorJobRoles { get; set; }
        public virtual DbSet<VTPSector> VTPSectors { get; set; }
        public virtual DbSet<VTRActivityType> VTRActivityTypes { get; set; }
        public virtual DbSet<VTRReasonOfNotConductingTheClass> VTRReasonOfNotConductingTheClasses { get; set; }
        public virtual DbSet<VTRAssessorInOtherSchoolForExam> VTRAssessorInOtherSchoolForExams { get; set; }
        public virtual DbSet<VTRAssignmentFromVocationalDepartment> VTRAssignmentFromVocationalDepartments { get; set; }
        public virtual DbSet<VTRClassSectionsTaught> VTRClassSectionsTaughts { get; set; }
        public virtual DbSet<VTRCommunityHomeVisit> VTRCommunityHomeVisits { get; set; }
        public virtual DbSet<VTRHoliday> VTRHolidays { get; set; }
        public virtual DbSet<VTRLeave> VTRLeaves { get; set; }
        public virtual DbSet<VTRObservationDay> VTRObservationDays { get; set; }
        public virtual DbSet<VTROnJobTrainingCoordination> VTROnJobTrainingCoordinations { get; set; }
        public virtual DbSet<VTRParentTeachersMeeting> VTRParentTeachersMeetings { get; set; }
        public virtual DbSet<VTRStudentAttendance> VTRStudentAttendances { get; set; }
        public virtual DbSet<VTRTeachingNonVocationalSubject> VTRTeachingNonVocationalSubjects { get; set; }
        public virtual DbSet<VTRTeachingVocationalEducation> VTRTeachingVocationalEducations { get; set; }
        public virtual DbSet<VTRTrainingOfTeacher> VTRTrainingOfTeachers { get; set; }
        public virtual DbSet<VTRTrainingTopic> VTRTrainingTopics { get; set; }
        public virtual DbSet<VTRUnitSessionsTaught> VTRUnitSessionsTaughts { get; set; }
        public virtual DbSet<VTRVisitToEducationalInstitution> VTRVisitToEducationalInstitutions { get; set; }
        public virtual DbSet<VTRVisitToIndustry> VTRVisitToIndustries { get; set; }
        public virtual DbSet<VTRWorkingDayType> VTRWorkingDayTypes { get; set; }
        public virtual DbSet<VTSchoolSectorJobRole> VTSchoolSectorJobRoles { get; set; }
        public virtual DbSet<VTSchoolSector> VTSchoolSectors { get; set; }
        public virtual DbSet<VTStatusOfInductionInserviceTraining> VTStatusOfInductionInserviceTrainings { get; set; }
        public virtual DbSet<VTStudentAssessment> VTStudentAssessments { get; set; }
        public virtual DbSet<VTStudentPlacementDetail> VTStudentPlacementDetails { get; set; }
        public virtual DbSet<VTStudentResultOtherSubject> VTStudentResultOtherSubjects { get; set; }
        public virtual DbSet<VTStudentVEResult> VTStudentVEResults { get; set; }
        public virtual DbSet<LabConditionModel> LabConditionModels { get; set; }
        public virtual DbSet<ToolListModel> ToolListModels { get; set; }
        public virtual DbSet<MaterialListModel> MaterialListModels { get; set; }
        public virtual DbSet<PraticalAssesmentModel> PraticalAssesmentModels { get; set; }
        public virtual DbSet<BroadcastMessage> BroadcastMessages { get; set; }
        public virtual DbSet<IssueMapping> IssueMapping { get; set; }
        public virtual DbSet<IssueApprovalHistory> IssueApprovalHistories { get; set; }
        public virtual DbSet<Block> Blocks { get; set; }
        public virtual DbSet<Cluster> Clusters { get; set; }
        public virtual DbSet<StudentClassMapping> StudentClassMapping { get; set; }
        public virtual DbSet<VTReportSubmission> VTReportSubmissions { get; set; }
        public virtual DbSet<StudentsForExitForm> StudentsForExitForm { get; set; }
        public virtual DbSet<StudentsForExitSurveyViewModel> StudentsForExitSurveyViewModels { get; set; }
        public virtual DbSet<ExitSurveyDetails> ExitSurveyDetails { get; set; }
        public virtual DbSet<ExitSurveyReportModel> ExitSurveyReports { get; set; }
        public virtual DbSet<DsDataManagement> DsDataManagement { get; set; }
        public virtual DbSet<AcademicRollOverResponse> AcademicRollOver { get; set; }

        #endregion "Entity"

        #region "ViewModel"

        public virtual DbSet<AcademicYearViewModel> AcademicYearViewModels { get; set; }
        public virtual DbSet<AccountRoleViewModel> AccountRoleViewModels { get; set; }
        public virtual DbSet<AccountViewModel> AccountViewModels { get; set; }
        public virtual DbSet<AccountTransactionViewModel> AccountTransactionViewModels { get; set; }
        public virtual DbSet<AccountUserOTPViewModel> AccountUserOTPViewModels { get; set; }
        public virtual DbSet<AccountUserTermViewModel> AccountUserTermViewModels { get; set; }
        public virtual DbSet<CountryViewModel> CountryViewModels { get; set; }
        public virtual DbSet<CourseMaterialViewModel> CourseMaterialViewModels { get; set; }
        public virtual DbSet<CourseModuleViewModel> CourseModuleViewModels { get; set; }
        public virtual DbSet<DataTypeViewModel> DataTypeViewModels { get; set; }
        public virtual DbSet<DataValueViewModel> DataValueViewModels { get; set; }
        public virtual DbSet<DistrictViewModel> DistrictViewModels { get; set; }
        public virtual DbSet<DivisionViewModel> DivisionViewModels { get; set; }
        public virtual DbSet<EmployeeViewModel> EmployeeViewModels { get; set; }
        public virtual DbSet<EmployerViewModel> EmployerViewModels { get; set; }
        public virtual DbSet<ErrorLogViewModel> ErrorLogViewModels { get; set; }
        public virtual DbSet<ForgotPasswordHistoryViewModel> ForgotPasswordHistoryViewModels { get; set; }
        public virtual DbSet<HeadMasterViewModel> HeadMasterViewModels { get; set; }
        public virtual DbSet<HMIssueReportingViewModel> HMIssueReportingViewModels { get; set; }
        public virtual DbSet<JobRoleViewModel> JobRoleViewModels { get; set; }
        public virtual DbSet<PhaseViewModel> PhaseViewModels { get; set; }
        public virtual DbSet<RoleViewModel> RoleViewModels { get; set; }
        public virtual DbSet<RoleTransactionViewModel> RoleTransactionViewModels { get; set; }
        public virtual DbSet<SchoolCategoryViewModel> SchoolCategoryViewModels { get; set; }
        public virtual DbSet<SchoolClassViewModel> SchoolClassViewModels { get; set; }
        public virtual DbSet<SchoolViewModel> SchoolViewModels { get; set; }
        public virtual DbSet<SchoolVEInchargeViewModel> SchoolVEInchargeViewModels { get; set; }
        public virtual DbSet<SchoolVTPSectorViewModel> SchoolVTPSectorViewModels { get; set; }
        public virtual DbSet<SectionViewModel> SectionViewModels { get; set; }
        public virtual DbSet<SectorJobRoleViewModel> SectorJobRoleViewModels { get; set; }
        public virtual DbSet<SectorViewModel> SectorViewModels { get; set; }
        public virtual DbSet<SiteHeaderViewModel> SiteHeaderViewModels { get; set; }
        public virtual DbSet<SiteSubHeaderViewModel> SiteSubHeaderViewModels { get; set; }
        public virtual DbSet<StateViewModel> StateViewModels { get; set; }
        public virtual DbSet<StudentClassDetailViewModel> StudentClassDetailViewModels { get; set; }
        public virtual DbSet<StudentClassViewModel> StudentClassViewModels { get; set; }
        public virtual DbSet<TermsConditionViewModel> TermsConditionViewModels { get; set; }
        public virtual DbSet<ToolEquipmentViewModel> ToolEquipmentViewModels { get; set; }
        public virtual DbSet<TransactionViewModel> TransactionViewModels { get; set; }
        public virtual DbSet<UserOTPDetailViewModel> UserOTPDetailViewModels { get; set; }
        public virtual DbSet<VCDailyReportingViewModel> VCDailyReportingViewModels { get; set; }
        public virtual DbSet<DRPDailyReportingViewModel> DRPDailyReportingViewModels { get; set; }
        public virtual DbSet<VCIssueReportingViewModel> VCIssueReportingViewModels { get; set; }
        public virtual DbSet<VCSchoolSectorViewModel> VCSchoolSectorViewModels { get; set; }
        public virtual DbSet<VCSchoolVisitViewModel> VCSchoolVisitViewModels { get; set; }
        public virtual DbSet<VocationalCoordinatorViewModel> VocationalCoordinatorViewModels { get; set; }
        public virtual DbSet<VocationalTrainerViewModel> VocationalTrainerViewModels { get; set; }
        public virtual DbSet<VocationalTrainingProviderViewModel> VocationalTrainingProviderViewModels { get; set; }
        public virtual DbSet<VTClassViewModel> VTClassViewModels { get; set; }
        public virtual DbSet<VTDailyReportingViewModel> VTDailyReportingViewModels { get; set; }
        public virtual DbSet<VTFieldIndustryVisitConductedViewModel> VTFieldIndustryVisitConductedViewModels { get; set; }
        public virtual DbSet<VTGuestLectureConductedViewModel> VTGuestLectureConductedViewModels { get; set; }
        public virtual DbSet<VTIssueReportingViewModel> VTIssueReportingViewModels { get; set; }
        public virtual DbSet<VTMonthlyTeachingPlanViewModel> VTMonthlyTeachingPlanViewModels { get; set; }
        public virtual DbSet<VTPMonthlyBillSubmissionStatusViewModel> VTPMonthlyBillSubmissionStatusViewModels { get; set; }
        public virtual DbSet<VTPracticalAssessmentViewModel> VTPracticalAssessmentViewModels { get; set; }
        public virtual DbSet<VTPSectorJobRoleViewModel> VTPSectorJobRoleViewModels { get; set; }
        public virtual DbSet<VTPSectorViewModel> VTPSectorViewModels { get; set; }
        public virtual DbSet<VTSchoolSectorViewModel> VTSchoolSectorViewModels { get; set; }
        public virtual DbSet<VTStatusOfInductionInserviceTrainingViewModel> VTStatusOfInductionInserviceTrainingViewModels { get; set; }
        public virtual DbSet<VTStudentAssessmentViewModel> VTStudentAssessmentViewModels { get; set; }
        public virtual DbSet<VTStudentPlacementDetailViewModel> VTStudentPlacementDetailViewModels { get; set; }
        public virtual DbSet<VTStudentResultOtherSubjectViewModel> VTStudentResultOtherSubjectViewModels { get; set; }
        public virtual DbSet<VTStudentVEResultViewModel> VTStudentVEResultViewModels { get; set; }
        public virtual DbSet<VEAHeaderModel> VEAHeaderModels { get; set; }
        public virtual DbSet<VEADetailsModel> VEADetailsModels { get; set; }
        public virtual DbSet<VEAModel> VEAModels { get; set; }
        public virtual DbSet<BroadcastMessageViewModel> BroadcastMessageViewModels { get; set; }
        public virtual DbSet<IssueMappingViewModel> IssueMappingViewModels { get; set; }
        public virtual DbSet<IssueViewModel> IssueViewModels { get; set; }
        public virtual DbSet<BlockViewModel> BlockViewModels { get; set; }
        public virtual DbSet<ClusterViewModel> ClusterViewModels { get; set; }
        public virtual DbSet<VCSchoolVisitReportingViewModel> VCSchoolVisitReportingViewModels { get; set; }
        public virtual DbSet<VCSchoolModel> VCSchoolModels { get; set; }
        public virtual DbSet<VTPSchoolModel> VTPSchoolModels { get; set; }

        #endregion "ViewModel"

        #region "Reports"

        public virtual DbSet<FieldIndustryVisitConductedReport> FieldIndustryVisitConductedReports { get; set; }
        public virtual DbSet<GuestLectureConductedReport> GuestLectureConductedReports { get; set; }
        public virtual DbSet<VTIssueReport> VTIssueReports { get; set; }
        public virtual DbSet<VCIssueReport> VCIssueReports { get; set; }
        public virtual DbSet<VCReportingAttendanceReport> VCReportingAttendanceReports { get; set; }
        public virtual DbSet<VCSchoolSectorReport> VCSchoolSectorReports { get; set; }
        public virtual DbSet<VTSchoolSectorReport> VTSchoolSectorReports { get; set; }
        public virtual DbSet<SchoolVTPSectorReport> SchoolVTPSectorReports { get; set; }
        public virtual DbSet<SchoolInfoReport> SchoolInfoReports { get; set; }
        public virtual DbSet<CourseMaterialStatusReport> CourseMaterialStatusReports { get; set; }
        public virtual DbSet<ToolsAndEquipmentStatusReport> ToolsAndEquipmentStatusReports { get; set; }
        public virtual DbSet<StudentEnrollmentReport> StudentEnrollmentReports { get; set; }
        public virtual DbSet<GuestLectureStatusReport> GuestLectureStatusReports { get; set; }
        public virtual DbSet<FieldAndIndustryVisitStatusReport> FieldAndIndustryVisitStatusReports { get; set; }
        public virtual DbSet<VTReportingAttendanceReport> VTReportingAttendanceReports { get; set; }
        public virtual DbSet<StudentAttendanceReportingReport> StudentAttendanceReportingReports { get; set; }
        public virtual DbSet<StudentDetailsReport> StudentDetailsReports { get; set; }
        public virtual DbSet<VCSchoolVisitSummaryReport> VCSchoolVisitSummaryReports { get; set; }
        public virtual DbSet<VocationalTrainerAttendanceReport> VocationalTrainerAttendanceReports { get; set; }
        public virtual DbSet<VTPBillSubmissionStatusReport> VTPBillSubmissionStatusReports { get; set; }
        public virtual DbSet<VTAttendanceHeaderModel> VTAttendanceHeaderModels { get; set; }
        public virtual DbSet<VTAttendanceDetailModel> VTAttendanceDetailModels { get; set; }
        public virtual DbSet<VCAttendanceHeaderModel> VCAttendanceHeaderModels { get; set; }
        public virtual DbSet<VCAttendanceDetailModel> VCAttendanceDetailModels { get; set; }
        public virtual DbSet<VTDailyAttendanceTrackingReport> VTDailyAttendanceTrackingReports { get; set; }
        public virtual DbSet<VTDailyReportingNotSubmittedModel> VTDailyReportingNotSubmittedModels { get; set; }
        public virtual DbSet<VCDailyAttendanceTrackingReport> VCDailyAttendanceTrackingReports { get; set; }
        public virtual DbSet<VTStudentTrackingReport> VTStudentTrackingReports { get; set; }
        public virtual DbSet<VTCourseModuleDailyTrackingReport> VTCourseModuleDailyTrackingReports { get; set; }
        public virtual DbSet<DashboardSchoolVisitByMonthModel> DashboardSchoolVisitByMonthModels { get; set; }
        public virtual DbSet<DashboardSchoolVisitByVTPModel> DashboardSchoolVisitByVTPModels { get; set; }
        public virtual DbSet<DashboardCourseMaterialModel> DashboardCourseMaterialModels { get; set; }
        public virtual DbSet<DashboardToolEquipmentModel> DashboardToolEquipmentModels { get; set; }
        public virtual DbSet<CompareSchoolModel> CompareSchoolModels { get; set; }
        public virtual DbSet<CompareCourseMaterialModel> CompareCourseMaterialModels { get; set; }
        public virtual DbSet<CompareToolsAndEquipmentModel> CompareToolsAndEquipmentModels { get; set; }
        public virtual DbSet<CompareStudentModel> CompareStudentModels { get; set; }
        public virtual DbSet<CompareNewEnrolmentAndDropoutStudentModel> CompareNewEnrolmentAndDropoutStudentModels { get; set; }
        public virtual DbSet<CompareGuestLectureModel> CompareGuestLectureModels { get; set; }
        public virtual DbSet<CompareFieldVisitModel> CompareFieldVisitModels { get; set; }
        public virtual DbSet<CompareTrainerModel> CompareTrainerModels { get; set; }
        public virtual DbSet<CompareCoordinatorModel> CompareCoordinatorModels { get; set; }
        public virtual DbSet<CompareVTVCReportingModel> CompareVTVCReportingModels { get; set; }
        public virtual DbSet<VTDailyMonthlyModel> VTDailyMonthlyModels { get; set; }
        public virtual DbSet<VCDailyMonthlyModel> VCDailyMonthlyModels { get; set; }
        public virtual DbSet<VTPMonthlyModel> VTPMonthlyModels { get; set; }

        #endregion "Reports"

        public virtual DbSet<DropdownModel<string>> DropdownModels { get; set; }
        public virtual DbSet<DashboardCardModel> DashboardCardModels { get; set; }
        public virtual DbSet<LoginResponce> LoginResponces { get; set; }
        public virtual DbSet<RoleTransactionResponce> RoleTransactionsBy { get; set; }
        public virtual DbSet<UserTransactionResponce> UserTransactionsBy { get; set; }
        public virtual DbSet<StudentAttendanceModel> StudentAttendanceModels { get; set; }
        public virtual DbSet<UnitSessionsModel> UnitSessionsModels { get; set; }
        public virtual DbSet<ModuleUnitSessionModel> ModuleUnitSessionModels { get; set; }
        public virtual DbSet<MasterDataModel> MasterDataModels { get; set; }
        public virtual DbSet<ClassSectionModel> ClassSectionModels { get; set; }
        public virtual DbSet<StudentByVTModel> StudentByVTModels { get; set; }
        public virtual DbSet<DashboardModel> DashboardModels { get; set; }
        public virtual DbSet<DashboardSchoolModel> DashboardSchoolModels { get; set; }
        public virtual DbSet<DashboardGuestLectureModel> DashboardGuestLectureModels { get; set; }
        public virtual DbSet<DashboardFieldVisitModel> DashboardFieldVisitModels { get; set; }
        public virtual DbSet<DashboardStudentAttendanceModel> DashboardStudentAttendanceModels { get; set; }
        public virtual DbSet<DashboardSchoolVisitStatusModel> DashboardSchoolVisitStatusModels { get; set; }
        public virtual DbSet<DashboardIssueManagementModel> DashboardIssueManagementModels { get; set; }
        public virtual DbSet<VocationalTrainersCardModel> VocationalTrainersCardModels { get; set; }
        public virtual DbSet<JobRoleUnitCardModel> JobRoleUnitCardModels { get; set; }
        public virtual DbSet<ClassCardModel> ClassCardModels { get; set; }
        public virtual DbSet<StudentCardModel> StudentCardModels { get; set; }
        public virtual DbSet<ComplaintRegistration> ComplaintRegistrations { get; set; }
        public virtual DbSet<ComplaintRegistrationViewModel> ComplaintRegistrationViewModels { get; set; }
        public virtual DbSet<ExecuteScriptQuery> ExecuteScriptQuery { get; set; }
        public virtual DbSet<AccountWorkLocationModel> AccountWorkLocationModels { get; set; }
        public virtual DbSet<SchoolStudentModel> SchoolStudentModels { get; set; }
        public virtual DbSet<SchoolVTPModel> SchoolVTPModels { get; set; }
        public virtual DbSet<SchoolStudentDetailModel> SchoolStudentDetailModels { get; set; }
        public virtual DbSet<LighthouseParams> LighthouseParams { get; set; }
        public virtual DbSet<MessageTemplate> MessageTemplates { get; set; }
        public virtual DbSet<MessageTemplateViewModel> MessageTemplateViewModels { get; set; }
        public virtual DbSet<VTNotReportedDailyAttendanceViewModel> VTNotReportedDailyAttendanceViewModels { get; set; }
        public virtual DbSet<MainIssue> MainIssues { get; set; }
        public virtual DbSet<SubIssue> SubIssues { get; set; }
    }
}