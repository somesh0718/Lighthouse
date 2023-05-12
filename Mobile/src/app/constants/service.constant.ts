import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable()
export class ServiceConstants {
    public BaseUrl: string = environment.ApiBaseUrl;
    public Environment: any = environment;
    public RetryServieNo = 0;

    public MasterData = {
        GetAll: 'Lighthouse/GetMasterDataByType',
        GetClassesByVTId: 'Lighthouse/GetClassesByVTId',
        GetSectionsByVTClassId: 'Lighthouse/GetSectionsByVTClassId',
        GetUnitsByClassAndModuleId: 'Lighthouse/GetUnitsByClassAndModuleId',
        GetSessionsByUnitId: 'Lighthouse/GetSessionsByUnitId',
        GetStudentsByClassIdForVT: 'Lighthouse/GetStudentsByClassIdForVT',
        GetSchoolsByVCId: 'Lighthouse/GetSchoolsByVCId',
        GetCourseModuleUnitSessions: 'Lighthouse/GetCourseModuleUnitSessions',
        GetClassSectionsByVTId: 'Lighthouse/GetClassSectionsByVTId',
        GetCommonMasterData: 'Lighthouse/GetCommonMasterData',
        GetStudentsByVTId: 'Lighthouse/GetStudentsByVTId',
        GetSchoolsByDRPId: 'Lighthouse/GetSchoolsByDRPId',
        GetSchoolsByAcademicYearId: "Lighthouse/GetSchoolsByAcademicYearId",
    };

    public AcademicYear = {
        GetAll: 'AcademicYear/GetAcademicYears',
        GetAllByCriteria: 'AcademicYear/GetAcademicYearsByCriteria',
        GetById: 'AcademicYear/GetAcademicYearById',
        CreateOrUpdate: 'AcademicYear/CreateOrUpdateAcademicYearDetails',
        Delete: 'AcademicYear/DeleteAcademicYearById'
    };

    public AcademicYearSchoolVTPSectorJobRole = {
        GetAll: 'AcademicYearSchoolVTPSectorJobRole/GetAcademicYearSchoolVTPSectorJobRoles',
        GetAllByCriteria: 'AcademicYearSchoolVTPSectorJobRole/GetAcademicYearSchoolVTPSectorJobRolesByCriteria',
        GetById: 'AcademicYearSchoolVTPSectorJobRole/GetAcademicYearSchoolVTPSectorJobRoleById',
        CreateOrUpdate: 'AcademicYearSchoolVTPSectorJobRole/CreateOrUpdateAcademicYearSchoolVTPSectorJobRoleDetails',
        Delete: 'AcademicYearSchoolVTPSectorJobRole/DeleteAcademicYearSchoolVTPSectorJobRoleById'
    };

    public Account = {
        GetAll: 'Account/GetAccounts',
        GetAllByCriteria: 'Account/GetAccountsByCriteria',
        GetById: 'Account/GetAccountById',
        CreateOrUpdate: 'Account/CreateOrUpdateAccountDetails',
        Delete: 'Account/DeleteAccountById'
    };

    public Country = {
        GetAll: 'Country/GetCountries',
        GetAllByCriteria: 'Country/GetCountriesByCriteria',
        GetById: 'Country/GetCountryById',
        CreateOrUpdate: 'Country/CreateOrUpdateCountryDetails',
        Delete: 'Country/DeleteCountryById'
    };

    public DataType = {
        GetAll: 'DataType/GetDataTypes',
        GetAllByCriteria: 'DataType/GetDataTypesByCriteria',
        GetById: 'DataType/GetDataTypeById',
        CreateOrUpdate: 'DataType/CreateOrUpdateDataTypeDetails',
        Delete: 'DataType/DeleteDataTypeById'
    };

    public DataValue = {
        GetAll: 'DataValue/GetDataValues',
        GetAllByCriteria: 'DataValue/GetDataValuesByCriteria',
        GetById: 'DataValue/GetDataValueById',
        CreateOrUpdate: 'DataValue/CreateOrUpdateDataValueDetails',
        Delete: 'DataValue/DeleteDataValueById'
    };

    public District = {
        GetAll: 'District/GetDistricts',
        GetAllByCriteria: 'District/GetDistrictsByCriteria',
        GetById: 'District/GetDistrictById',
        CreateOrUpdate: 'District/CreateOrUpdateDistrictDetails',
        Delete: 'District/DeleteDistrictById'
    };

    public Division = {
        GetAll: 'Division/GetDivisions',
        GetAllByCriteria: 'Division/GetDivisionsByCriteria',
        GetById: 'Division/GetDivisionById',
        CreateOrUpdate: 'Division/CreateOrUpdateDivisionDetails',
        Delete: 'Division/DeleteDivisionById'
    };

    public Employee = {
        GetAll: 'Employee/GetEmployees',
        GetAllByCriteria: 'Employee/GetEmployeesByCriteria',
        GetById: 'Employee/GetEmployeeById',
        CreateOrUpdate: 'Employee/CreateOrUpdateEmployeeDetails',
        Delete: 'Employee/DeleteEmployeeById'
    };

    public ForgotPasswordHistory = {
        GetAll: 'ForgotPasswordHistory/GetForgotPasswordHistories',
        GetAllByCriteria: 'ForgotPasswordHistory/GetForgotPasswordHistoriesByCriteria',
        GetById: 'ForgotPasswordHistory/GetForgotPasswordHistoryById',
        CreateOrUpdate: 'ForgotPasswordHistory/CreateOrUpdateForgotPasswordHistoryDetails',
        Delete: 'ForgotPasswordHistory/DeleteForgotPasswordHistoryById'
    };

    public HeadMaster = {
        GetAll: 'HeadMaster/GetHeadMasters',
        GetAllByCriteria: 'HeadMaster/GetHeadMastersByCriteria',
        GetById: 'HeadMaster/GetHeadMasterById',
        CreateOrUpdate: 'HeadMaster/CreateOrUpdateHeadMasterDetails',
        Delete: 'HeadMaster/DeleteHeadMasterById'
    };

    public HMIssueReporting = {
        GetAll: 'HMIssueReporting/GetHMIssueReportings',
        GetAllByCriteria: 'HMIssueReporting/GetHMIssueReportingsByCriteria',
        GetById: 'HMIssueReporting/GetHMIssueReportingById',
        CreateOrUpdate: 'HMIssueReporting/CreateOrUpdateHMIssueReportingDetails',
        Delete: 'HMIssueReporting/DeleteHMIssueReportingById'
    };

    public JobRole = {
        GetAll: 'JobRole/GetJobRoles',
        GetAllByCriteria: 'JobRole/GetJobRolesByCriteria',
        GetById: 'JobRole/GetJobRoleById',
        CreateOrUpdate: 'JobRole/CreateOrUpdateJobRoleDetails',
        Delete: 'JobRole/DeleteJobRoleById'
    };

    public Phase = {
        GetAll: 'Phase/GetPhases',
        GetAllByCriteria: 'Phase/GetPhasesByCriteria',
        GetById: 'Phase/GetPhaseById',
        CreateOrUpdate: 'Phase/CreateOrUpdatePhaseDetails',
        Delete: 'Phase/DeletePhaseById'
    };

    public Role = {
        GetAll: 'Role/GetRoles',
        GetAllByCriteria: 'Role/GetRolesByCriteria',
        GetById: 'Role/GetRoleById',
        CreateOrUpdate: 'Role/CreateOrUpdateRoleDetails',
        Delete: 'Role/DeleteRoleById'
    };

    public School = {
        GetAll: 'School/GetSchools',
        GetAllByCriteria: 'School/GetSchoolsByCriteria',
        GetById: 'School/GetSchoolById',
        CreateOrUpdate: 'School/CreateOrUpdateSchoolDetails',
        Delete: 'School/DeleteSchoolById'
    };

    public Section = {
        GetAll: 'Section/GetSections',
        GetAllByCriteria: 'Section/GetSectionsByCriteria',
        GetById: 'Section/GetSectionById',
        CreateOrUpdate: 'Section/CreateOrUpdateSectionDetails',
        Delete: 'Section/DeleteSectionById'
    };

    public Sector = {
        GetAll: 'Sector/GetSectors',
        GetAllByCriteria: 'Sector/GetSectorsByCriteria',
        GetById: 'Sector/GetSectorById',
        CreateOrUpdate: 'Sector/CreateOrUpdateSectorDetails',
        Delete: 'Sector/DeleteSectorById'
    };

    public SiteHeader = {
        GetAll: 'SiteHeader/GetSiteHeaders',
        GetAllByCriteria: 'SiteHeader/GetSiteHeadersByCriteria',
        GetById: 'SiteHeader/GetSiteHeaderById',
        CreateOrUpdate: 'SiteHeader/CreateOrUpdateSiteHeaderDetails',
        Delete: 'SiteHeader/DeleteSiteHeaderById'
    };

    public SiteSubHeader = {
        GetAll: 'SiteSubHeader/GetSiteSubHeaders',
        GetAllByCriteria: 'SiteSubHeader/GetSiteSubHeadersByCriteria',
        GetById: 'SiteSubHeader/GetSiteSubHeaderById',
        CreateOrUpdate: 'SiteSubHeader/CreateOrUpdateSiteSubHeaderDetails',
        Delete: 'SiteSubHeader/DeleteSiteSubHeaderById'
    };

    public State = {
        GetAll: 'State/GetStates',
        GetAllByCriteria: 'State/GetStatesByCriteria',
        GetById: 'State/GetStateById',
        CreateOrUpdate: 'State/CreateOrUpdateStateDetails',
        Delete: 'State/DeleteStateById'
    };

    public StudentClassDetail = {
        GetAll: 'StudentClassDetail/GetStudentClassDetails',
        GetAllByCriteria: 'StudentClassDetail/GetStudentClassDetailsByCriteria',
        GetById: 'StudentClassDetail/GetStudentClassDetailById',
        CreateOrUpdate: 'StudentClassDetail/CreateOrUpdateStudentClassDetailDetails',
        Delete: 'StudentClassDetail/DeleteStudentClassDetailById'
    };

    public StudentClass = {
        GetAll: 'StudentClass/GetStudentClasses',
        GetAllByCriteria: 'StudentClass/GetStudentClassesByCriteria',
        GetById: 'StudentClass/GetStudentClassById',
        CreateOrUpdate: 'StudentClass/CreateOrUpdateStudentClassDetails',
        Delete: 'StudentClass/DeleteStudentClassById'
    };

    public TermsCondition = {
        GetAll: 'TermsCondition/GetTermsConditions',
        GetAllByCriteria: 'TermsCondition/GetTermsConditionsByCriteria',
        GetById: 'TermsCondition/GetTermsConditionById',
        CreateOrUpdate: 'TermsCondition/CreateOrUpdateTermsConditionDetails',
        Delete: 'TermsCondition/DeleteTermsConditionById'
    };

    public Transaction = {
        GetAll: 'Transaction/GetTransactions',
        GetAllByCriteria: 'Transaction/GetTransactionsByCriteria',
        GetById: 'Transaction/GetTransactionById',
        CreateOrUpdate: 'Transaction/CreateOrUpdateTransactionDetails',
        Delete: 'Transaction/DeleteTransactionById'
    };

    public UserOTPDetail = {
        GetAll: 'UserOTPDetail/GetUserOTPDetails',
        GetAllByCriteria: 'UserOTPDetail/GetUserOTPDetailsByCriteria',
        GetById: 'UserOTPDetail/GetUserOTPDetailById',
        CreateOrUpdate: 'UserOTPDetail/CreateOrUpdateUserOTPDetailDetails',
        Delete: 'UserOTPDetail/DeleteUserOTPDetailById'
    };

    public VCDailyReporting = {
        GetAll: 'VCDailyReporting/GetVCDailyReportings',
        GetAllByCriteria: 'VCDailyReporting/GetVCDailyReportingsByCriteria',
        GetById: 'VCDailyReporting/GetVCDailyReportingById',
        CreateOrUpdate: 'VCDailyReporting/CreateOrUpdateVCDailyReportingDetails',
        Delete: 'VCDailyReporting/DeleteVCDailyReportingById'
    };

    public VCIssueReporting = {
        GetAll: 'VCIssueReporting/GetVCIssueReportings',
        GetAllByCriteria: 'VCIssueReporting/GetVCIssueReportingsByCriteria',
        GetById: 'VCIssueReporting/GetVCIssueReportingById',
        CreateOrUpdate: 'VCIssueReporting/CreateOrUpdateVCIssueReportingDetails',
        Delete: 'VCIssueReporting/DeleteVCIssueReportingById'
    };

    public VCSchoolSector = {
        GetAll: 'VCSchoolSector/GetVCSchoolSectors',
        GetAllByCriteria: 'VCSchoolSector/GetVCSchoolSectorsByCriteria',
        GetById: 'VCSchoolSector/GetVCSchoolSectorById',
        CreateOrUpdate: 'VCSchoolSector/CreateOrUpdateVCSchoolSectorDetails',
        Delete: 'VCSchoolSector/DeleteVCSchoolSectorById'
    };

    public VCSchoolVisitGeoLocation = {
        GetAll: 'VCSchoolVisitGeoLocation/GetVCSchoolVisitGeoLocations',
        GetAllByCriteria: 'VCSchoolVisitGeoLocation/GetVCSchoolVisitGeoLocationsByCriteria',
        GetById: 'VCSchoolVisitGeoLocation/GetVCSchoolVisitGeoLocationById',
        CreateOrUpdate: 'VCSchoolVisitGeoLocation/CreateOrUpdateVCSchoolVisitGeoLocationDetails',
        Delete: 'VCSchoolVisitGeoLocation/DeleteVCSchoolVisitGeoLocationById'
    };

    public VCSchoolVisit = {
        GetAll: 'VCSchoolVisit/GetVCSchoolVisits',
        GetAllByCriteria: 'VCSchoolVisit/GetVCSchoolVisitsByCriteria',
        GetById: 'VCSchoolVisit/GetVCSchoolVisitById',
        CreateOrUpdate: 'VCSchoolVisit/CreateOrUpdateVCSchoolVisitDetails',
        Delete: 'VCSchoolVisit/DeleteVCSchoolVisitById'
    };

    public VocationalCoordinator = {
        GetAll: 'VocationalCoordinator/GetVocationalCoordinators',
        GetAllByCriteria: 'VocationalCoordinator/GetVocationalCoordinatorsByCriteria',
        GetById: 'VocationalCoordinator/GetVocationalCoordinatorById',
        CreateOrUpdate: 'VocationalCoordinator/CreateOrUpdateVocationalCoordinatorDetails',
        Delete: 'VocationalCoordinator/DeleteVocationalCoordinatorById'
    };

    public VocationalTrainer = {
        GetAll: 'VocationalTrainer/GetVocationalTrainers',
        GetAllByCriteria: 'VocationalTrainer/GetVocationalTrainersByCriteria',
        GetById: 'VocationalTrainer/GetVocationalTrainerById',
        CreateOrUpdate: 'VocationalTrainer/CreateOrUpdateVocationalTrainerDetails',
        Delete: 'VocationalTrainer/DeleteVocationalTrainerById'
    };

    public VocationalTrainingProvider = {
        GetAll: 'VocationalTrainingProvider/GetVocationalTrainingProviders',
        GetAllByCriteria: 'VocationalTrainingProvider/GetVocationalTrainingProvidersByCriteria',
        GetById: 'VocationalTrainingProvider/GetVocationalTrainingProviderById',
        CreateOrUpdate: 'VocationalTrainingProvider/CreateOrUpdateVocationalTrainingProviderDetails',
        Delete: 'VocationalTrainingProvider/DeleteVocationalTrainingProviderById'
    };

    public VTClass = {
        GetAll: 'VTClass/GetVTClasses',
        GetAllByCriteria: 'VTClass/GetVTClassesByCriteria',
        GetById: 'VTClass/GetVTClassById',
        CreateOrUpdate: 'VTClass/CreateOrUpdateVTClassDetails',
        Delete: 'VTClass/DeleteVTClassById'
    };

    public VTDailyReporting = {
        GetAll: 'VTDailyReporting/GetVTDailyReportings',
        GetAllByCriteria: 'VTDailyReporting/GetVTDailyReportingsByCriteria',
        GetById: 'VTDailyReporting/GetVTDailyReportingById',
        CreateOrUpdate: 'VTDailyReporting/CreateOrUpdateVTDailyReportingDetails',
        Delete: 'VTDailyReporting/DeleteVTDailyReportingById'
    };

    public VTFieldIndustryVisitConducted = {
        GetAll: 'VTFieldIndustryVisitConducted/GetVTFieldIndustryVisitConducteds',
        GetAllByCriteria: 'VTFieldIndustryVisitConducted/GetVTFieldIndustryVisitConductedsByCriteria',
        GetById: 'VTFieldIndustryVisitConducted/GetVTFieldIndustryVisitConductedById',
        CreateOrUpdate: 'VTFieldIndustryVisitConducted/CreateOrUpdateVTFieldIndustryVisitConductedDetails',
        Delete: 'VTFieldIndustryVisitConducted/DeleteVTFieldIndustryVisitConductedById'
    };

    public VTGuestLectureConducted = {
        GetAll: 'VTGuestLectureConducted/GetVTGuestLectureConducteds',
        GetAllByCriteria: 'VTGuestLectureConducted/GetVTGuestLectureConductedsByCriteria',
        GetById: 'VTGuestLectureConducted/GetVTGuestLectureConductedById',
        CreateOrUpdate: 'VTGuestLectureConducted/CreateOrUpdateVTGuestLectureConductedDetails',
        Delete: 'VTGuestLectureConducted/DeleteVTGuestLectureConductedById'
    };

    public VTIssueReporting = {
        GetAll: 'VTIssueReporting/GetVTIssueReportings',
        GetAllByCriteria: 'VTIssueReporting/GetVTIssueReportingsByCriteria',
        GetById: 'VTIssueReporting/GetVTIssueReportingById',
        CreateOrUpdate: 'VTIssueReporting/CreateOrUpdateVTIssueReportingDetails',
        Delete: 'VTIssueReporting/DeleteVTIssueReportingById'
    };

    public VTMonthlyTeachingPlan = {
        GetAll: 'VTMonthlyTeachingPlan/GetVTMonthlyTeachingPlans',
        GetAllByCriteria: 'VTMonthlyTeachingPlan/GetVTMonthlyTeachingPlansByCriteria',
        GetById: 'VTMonthlyTeachingPlan/GetVTMonthlyTeachingPlanById',
        CreateOrUpdate: 'VTMonthlyTeachingPlan/CreateOrUpdateVTMonthlyTeachingPlanDetails',
        Delete: 'VTMonthlyTeachingPlan/DeleteVTMonthlyTeachingPlanById'
    };

    public VTPMonthlyBillSubmissionStatus = {
        GetAll: 'VTPMonthlyBillSubmissionStatus/GetVTPMonthlyBillSubmissionStatus',
        GetAllByCriteria: 'VTPMonthlyBillSubmissionStatus/GetVTPMonthlyBillSubmissionStatusByCriteria',
        GetById: 'VTPMonthlyBillSubmissionStatus/GetVTPMonthlyBillSubmissionStatusById',
        CreateOrUpdate: 'VTPMonthlyBillSubmissionStatus/CreateOrUpdateVTPMonthlyBillSubmissionStatusDetails',
        Delete: 'VTPMonthlyBillSubmissionStatus/DeleteVTPMonthlyBillSubmissionStatusById'
    };

    public VTPracticalAssessment = {
        GetAll: 'VTPracticalAssessment/GetVTPracticalAssessments',
        GetAllByCriteria: 'VTPracticalAssessment/GetVTPracticalAssessmentsByCriteria',
        GetById: 'VTPracticalAssessment/GetVTPracticalAssessmentById',
        CreateOrUpdate: 'VTPracticalAssessment/CreateOrUpdateVTPracticalAssessmentDetails',
        Delete: 'VTPracticalAssessment/DeleteVTPracticalAssessmentById'
    };

    public VTPSector = {
        GetAll: 'VTPSector/GetVTPSectors',
        GetAllByCriteria: 'VTPSector/GetVTPSectorsByCriteria',
        GetById: 'VTPSector/GetVTPSectorById',
        CreateOrUpdate: 'VTPSector/CreateOrUpdateVTPSectorDetails',
        Delete: 'VTPSector/DeleteVTPSectorById'
    };

    public VTSchoolSector = {
        GetAll: 'VTSchoolSector/GetVTSchoolSectors',
        GetAllByCriteria: 'VTSchoolSector/GetVTSchoolSectorsByCriteria',
        GetById: 'VTSchoolSector/GetVTSchoolSectorById',
        CreateOrUpdate: 'VTSchoolSector/CreateOrUpdateVTSchoolSectorDetails',
        Delete: 'VTSchoolSector/DeleteVTSchoolSectorById'
    };

    public VTStatusOfInductionInserviceTraining = {
        GetAll: 'VTStatusOfInductionInserviceTraining/GetVTStatusOfInductionInserviceTrainings',
        GetAllByCriteria: 'VTStatusOfInductionInserviceTraining/GetVTStatusOfInductionInserviceTrainingsByCriteria',
        GetById: 'VTStatusOfInductionInserviceTraining/GetVTStatusOfInductionInserviceTrainingById',
        CreateOrUpdate: 'VTStatusOfInductionInserviceTraining/CreateOrUpdateVTStatusOfInductionInserviceTrainingDetails',
        Delete: 'VTStatusOfInductionInserviceTraining/DeleteVTStatusOfInductionInserviceTrainingById'
    };

    public VTStudentAssessment = {
        GetAll: 'VTStudentAssessment/GetVTStudentAssessments',
        GetAllByCriteria: 'VTStudentAssessment/GetVTStudentAssessmentsByCriteria',
        GetById: 'VTStudentAssessment/GetVTStudentAssessmentById',
        CreateOrUpdate: 'VTStudentAssessment/CreateOrUpdateVTStudentAssessmentDetails',
        Delete: 'VTStudentAssessment/DeleteVTStudentAssessmentById'
    };

    public VTStudentPlacementDetail = {
        GetAll: 'VTStudentPlacementDetail/GetVTStudentPlacementDetails',
        GetAllByCriteria: 'VTStudentPlacementDetail/GetVTStudentPlacementDetailsByCriteria',
        GetById: 'VTStudentPlacementDetail/GetVTStudentPlacementDetailById',
        CreateOrUpdate: 'VTStudentPlacementDetail/CreateOrUpdateVTStudentPlacementDetailDetails',
        Delete: 'VTStudentPlacementDetail/DeleteVTStudentPlacementDetailById'
    };

    public VTStudentResultOtherSubject = {
        GetAll: 'VTStudentResultOtherSubject/GetVTStudentResultOtherSubjects',
        GetAllByCriteria: 'VTStudentResultOtherSubject/GetVTStudentResultOtherSubjectsByCriteria',
        GetById: 'VTStudentResultOtherSubject/GetVTStudentResultOtherSubjectById',
        CreateOrUpdate: 'VTStudentResultOtherSubject/CreateOrUpdateVTStudentResultOtherSubjectDetails',
        Delete: 'VTStudentResultOtherSubject/DeleteVTStudentResultOtherSubjectById'
    };

    public VTStudentVEResult = {
        GetAll: 'VTStudentVEResult/GetVTStudentVEResults',
        GetAllByCriteria: 'VTStudentVEResult/GetVTStudentVEResultsByCriteria',
        GetById: 'VTStudentVEResult/GetVTStudentVEResultById',
        CreateOrUpdate: 'VTStudentVEResult/CreateOrUpdateVTStudentVEResultDetails',
        Delete: 'VTStudentVEResult/DeleteVTStudentVEResultById'
    };

    public BroadcastMessage = {
        GetAll: "BroadcastMessage/GetBroadcastMessages",
        GetAllByCriteria: "BroadcastMessage/GetBroadcastMessagesByCriteria",
        GetById: "BroadcastMessage/GetBroadcastMessageById",
        CreateOrUpdate: "BroadcastMessage/CreateOrUpdateBroadcastMessageDetails",
        Delete: "BroadcastMessage/DeleteBroadcastMessageById",
    }

    public ComplaintRegistration = {
        GetAll: "ComplaintRegistration/GetComplaintRegistrations",
        GetAllByCriteria: "ComplaintRegistration​/GetComplaintRegistrationsByCriteria",
        GetById: "ComplaintRegistration/GetComplaintRegistrationById",
        CreateOrUpdate: "ComplaintRegistration/CreateOrUpdateComplaintRegistrationDetails",
        Delete: "ComplaintRegistration/DeleteComplaintRegistrationById",
    }

    public DRPDailyReporting = {
        GetAll: "DRPDailyReporting/GetDRPDailyReportings",
        GetAllByCriteria: "DRPDailyReporting/GetDRPDailyReportingsByCriteria",
        GetById: "DRPDailyReporting/GetDRPDailyReportingById",
        CreateOrUpdate: "DRPDailyReporting/CreateOrUpdateDRPDailyReportingDetails",
        Delete: "DRPDailyReporting/DeleteDRPDailyReportingById"
    }
    public VcSchoolVisitReporting = {
        GetAll: "VCSchoolVisitReporting/GetVCSchoolVisits",
        GetAllByCriteria: "VCSchoolVisitReporting/GetVCSchoolVisitReportingByCriteria",
        GetById: "VCSchoolVisitReporting/GetVCSchoolVisitReportingById",
        CreateOrUpdate: "VCSchoolVisitReporting/CreateOrUpdateVCSchoolVisitReportingDetails",
        Delete: "VCSchoolVisitReporting/DeleteVCSchoolVisitById"
    };

    public ApplicationInfo = {
        GetAppVersion: "Lighthouse/GetAndroidSettings"
    }
}
