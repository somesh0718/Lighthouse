namespace Igmite.Lighthouse.Models
{
    public class ServiceConstants
    {
        public const string ServiceName = "LighthouseServices/[controller]";
        public const string SaveLighthouseSettings = "SaveLighthouseSettings";

        public class Masters
        {
            public const string GetMasterDataByType = "GetMasterDataByType";
            public const string GetAllRoles = "GetAllRoles";
            public const string GetClassesByVTId = "GetClassesByVTId";
            public const string GetSectionsByVTClassId = "GetSectionsByVTClassId";
            public const string GetUnitsByClassAndModuleId = "GetUnitsByClassAndModuleId";
            public const string GetSessionsByUnitId = "GetSessionsByUnitId";
            public const string GetStudentsByUserId = "GetStudentsByUserId";
            public const string GetStudentsByClassIdForVT = "GetStudentsByClassIdForVT";
            public const string GetSchoolsByVCId = "GetSchoolsByVCId";
            public const string GetSchoolsByDRPId = "GetSchoolsByDRPId";
            public const string GetCourseModuleUnitSessions = "GetCourseModuleUnitSessions";
            public const string GetCommonMasterData = "GetCommonMasterData";
            public const string GetClassSectionsByVTId = "GetClassSectionsByVTId";
            public const string GetStudentsByVTId = "GetStudentsByVTId";
            public const string GetSchoolVTPSectorsByUserId = "GetSchoolVTPSectorsByUserId";

            public const string AcademicYears = "AcademicYears";
            public const string Divisions = "Divisions";
            public const string JobRoles = "JobRoles";
            public const string Phases = "Phases";
            public const string Roles = "Roles";
            public const string SchoolCategories = "SchoolCategories";
            public const string SchoolClasses = "SchoolClasses";
            public const string Sections = "Sections";
            public const string Sectors = "Sectors";
            public const string SiteHeaders = "SiteHeaders";
            public const string Transactions = "Transactions";
            public const string States = "States";
            public const string Districts = "Districts";
            public const string DataTypes = "DataTypes";
            public const string DataValues = "DataValues";
            public const string HeadMasters = "HeadMasters";
            public const string VocationalTrainingProviders = "VocationalTrainingProviders";
            public const string VocationalCoordinators = "VocationalCoordinators";
            public const string VocationalTrainers = "VocationalTrainers";
            public const string Schools = "Schools";
            public const string SectorJobRoles = "SectorJobRoles";
            public const string Students = "Students";
            public const string VTClasses = "VTClasses";
            public const string VTPSectors = "VTPSectors";
            public const string VTSchoolSectors = "VTSchoolSectors";
            public const string VCSchoolSectors = "VCSchoolSectors";
            public const string GetDashboardData = "GetDashboardData";
            public const string GetDashboardCardData = "GetDashboardCardData";
            public const string GetDashboardSchoolChartData = "GetDashboardSchoolChartData";
            public const string GetDashboardGuestLectureChartData = "GetDashboardGuestLectureChartData";
            public const string GetDashboardCourseMaterialChartData = "GetDashboardCourseMaterialChartData";
            public const string GetDashboardToolsAndEquipmentChartData = "GetDashboardToolsAndEquipmentChartData";
            public const string GetDashboardFieldVisitChartData = "GetDashboardFieldVisitChartData";
            public const string GetDashboardVTAttendanceChartData = "GetDashboardVTAttendanceChartData";
            public const string GetDashboardVCAttendanceChartData = "GetDashboardVCAttendanceChartData";
            public const string GetDashboardStudentAttendanceChartData = "GetDashboardStudentAttendanceChartData";
            public const string GetDashboardSchoolVisitStatusChartData = "GetDashboardSchoolVisitStatusChartData";
            public const string GetDashboardIssueManagementStatusChartData = "GetDashboardIssueManagementStatusChartData";
            public const string GetDashboardIssueManagementChartData = "GetDashboardIssueManagementChartData";

            public const string GetDashboardVocationalTrainersCardData = "GetDashboardVocationalTrainersCardData";
            public const string GetDashboardJobRoleUnitsCardData = "GetDashboardJobRoleUnitsCardData";
            public const string GetDashboardClassesCardData = "GetDashboardClassesCardData";
            public const string GetDashboardStudentsCardData = "GetDashboardStudentsCardData";
            public const string GetDashboardSchoolVisitsByMonth = "GetDashboardSchoolVisitsByMonth";
            public const string GetDashboardSchoolVisitsByVTP = "GetDashboardSchoolVisitsByVTP";
        }

        public class Dashboard
        {
            public static class Compare
            {
                public const string Schools = "GetCompareSchoolsData";
                public const string CourseMaterials = "GetCompareCourseMaterialsData";
                public const string ToolsAndEquipments = "GetCompareToolsAndEquipmentsData";
                public const string Students = "GetCompareStudentsData";
                public const string NewEnrolmentAndDropoutStudents = "GetCompareNewEnrolmentAndDropoutStudentsData";
                public const string GuestLectures = "GetCompareGuestLecturesData";
                public const string FieldVisits = "GetCompareFieldVisitsData";
                public const string Trainers = "GetCompareTrainersData";
                public const string Coordinators = "GetCompareCoordinatorsData";
                public const string VTVCReporting = "GetCompareVTVCReportingData";
            }
        }

        public class Transaction
        {
            public static class Division
            {
                public const string GetAll = "GetDivisions";
                public const string GetByCriteria = "GetDivisionsByCriteria";
                public const string GetByName = "GetDivisionsByName";
                public const string GetById = "GetDivisionById";
                public const string CreateOrUpdate = "CreateOrUpdateDivisionDetails";
                public const string Create = "CreateDivision";
                public const string Update = "UpdateDivision";
                public const string DeleteById = "DeleteDivisionById";
            }

            public static class CourseModule
            {
                public const string GetAll = "GetCourseModules";
                public const string GetByCriteria = "GetCourseModulesByCriteria";
                public const string GetByName = "GetCourseModulesByName";
                public const string GetById = "GetCourseModuleById";
                public const string CreateOrUpdate = "CreateOrUpdateCourseModuleDetails";
                public const string Create = "CreateCourseModule";
                public const string Update = "UpdateCourseModule";
                public const string DeleteById = "DeleteCourseModuleById";
            }
        }

        public class Report
        {
            public const string GetGuestLectureConductedReportsByCriteria = "GetGuestLectureConductedReportsByCriteria";
            public const string GetFieldIndustryVisitConductedReportsByCriteria = "GetFieldIndustryVisitConductedReportsByCriteria";
            public const string GetVTIssueReportsByCriteria = "GetVTIssueReportsByCriteria";
            public const string GetVCIssueReportsByCriteria = "GetVCIssueReportsByCriteria";
            public const string GetVCReportingAttendanceReportsByCriteria = "GetVCReportingAttendanceReportsByCriteria";
            public const string GetVCSchoolSectorReportsByCriteria = "GetVCSchoolSectorReportsByCriteria";
            public const string GetVTSchoolSectorReportsByCriteria = "GetVTSchoolSectorReportsByCriteria";
            public const string GetSchoolVTPSectorReportsByCriteria = "GetSchoolVTPSectorReportsByCriteria";
            public const string GetSchoolInformationReport = "GetSchoolInformationReport";

            public const string GetCourseMaterialStatusReport = "GetCourseMaterialStatusReport";
            public const string GetToolsAndEquipmentStatusReport = "GetToolsAndEquipmentStatusReport";
            public const string GetStudentEnrollmentReport = "GetStudentEnrollmentReport";
            public const string GetGuestLectureStatusReport = "GetGuestLectureStatusReport";
            public const string GetFieldAndIndustryVisitStatusReport = "GetFieldAndIndustryVisitStatusReport";
            public const string GetVTReportingAttendanceReport = "GetVTReportingAttendanceReport";
            public const string GetStudentAttendanceReportingReport = "GetStudentAttendanceReportingReport";
            public const string GetStudentDetailsReport = "GetStudentDetailsReport";
            public const string GetVCSchoolVisitSummaryReport = "GetVCSchoolVisitSummaryReport";
            public const string GetVocationalTrainerAttendanceReport = "GetVocationalTrainerAttendanceReport";
            public const string GetVTPBillSubmissionStatusReport = "GetVTPBillSubmissionStatusReport";
            public const string GetVTMonthlyAttendanceReport = "GetVTMonthlyAttendanceReport";
            public const string GetVCMonthlyAttendanceReport = "GetVCMonthlyAttendanceReport";
            public const string GetVocationalEducationAssessmentReport = "GetVocationalEducationAssessmentReport";
            public const string GetVTDailyAttendanceTrackingByCriteria = "GetVTDailyAttendanceTrackingByCriteria";
            public const string GetVCDailyAttendanceTrackingByCriteria = "GetVCDailyAttendanceTrackingByCriteria";
            public const string GetVTDailyReportNotSubmittedTrackingByCriteria = "GetVTDailyReportNotSubmittedTrackingByCriteria";
            public const string GetVTStudentTrackingByCriteria = "GetVTStudentTrackingByCriteria";
            public const string GetVTCourseModuleDailyTrackingByCriteria = "GetVTCourseModuleDailyTrackingByCriteria";
            public const string GetVTDailyMonthlyTrackingByCriteria = "GetVTDailyMonthlyTrackingByCriteria";
            public const string GetVCDailyMonthlyTrackingByCriteria = "GetVCDailyMonthlyTrackingByCriteria";
            public const string GetVTPMonthlyTrackingByCriteria = "GetVTPMonthlyTrackingByCriteria";
            public const string GetStudentAssesmentReport = "GetStudentAssesmentReport";
            public const string GetLabConditionReport = "GetLabConditionReport";
            public const string GetToolListReport = "GetToolListReport";
            public const string GetMaterialListReport = "GetMaterialListReport";
            public const string GetPraticalAssesmentReport = "GetPraticalAssesmentReport";
        }
    }
}