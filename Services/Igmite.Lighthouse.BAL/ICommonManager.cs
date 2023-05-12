using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Models.Common;
using System;
using System.Collections.Generic;

namespace Igmite.Lighthouse.BAL
{
    public interface ICommonManager : IGenericManager<AccountModel>
    {
        IList<DropdownModel<Guid>> GetRoles();

        IList<DropdownModel<Guid>> GetAllRoles();

        IList<DropdownModel<Guid>> GetAccounts();

        IList<DropdownModel<Guid>> GetSiteHeaders();

        IList<DropdownModel<Guid>> GetTransactions();

        IList<DropdownModel<string>> GetCountries();

        IList<DropdownModel<string>> GetStates(string countryCode);

        IList<DropdownModel<string>> GetDistricts(string stateCode);

        IList<HeaderMenuModel> GetAccountTransactions(string userId, string action);

        IList<DropdownModel<string>> GetDropdownDataById(string dataTypeId);

        IList<DropdownModel<string>> GetMasterDataForDropdown(MasterDataRequest dataRequest);

        IList<DropdownModel<string>> GetTargetVocationalTrainers(MasterDataForAcademicRollover request);

        IList<DropdownModel<string>> GetStudentsByUserId(string userType, string userId);

        IList<DropdownModel<string>> GetSchoolVTPSectorsByUserId(string userId, string userTypeId, string academicYearId);

        IList<DropdownModel<Guid>> GetAdministrator();

        IList<DropdownModel<string>> GetClassesByVTId(string userId, Guid vtId);

        IList<DropdownModel<string>> GetSectionsByVTClassId(Guid vtId, Guid classId);

        /// <summary>
        /// Get VTP & VC Id by SchoolId
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        LhUserModel GetVTPandVCIdBySchoolId(Guid schoolId);

        /// <summary>
        /// Get VTP, VC & School Id by VTId
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        LhUserModel GetVTPVCAndSchoolIdByVTId(Guid vtId);

        /// <summary>
        /// Get VTP, VC & School Id by HMId
        /// </summary>
        /// <param name="hmId"></param>
        /// <returns></returns>
        LhUserModel GetVTPVCAndSchoolIdByHMId(Guid hmId);

        /// <summary>
        /// Get VTP by HMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetVTPByHMId(Guid academicYearId, Guid hmId);

        /// <summary>
        /// Get VC by HMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetVCByHMId(Guid academicYearId, Guid hmId, Guid vtpId);

        /// <summary>
        /// Get VT by HMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetVTByHMId(Guid academicYearId, Guid hmId, Guid vcId);

        /// <summary>
        /// Get School by HMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetSchoolByHMId(Guid academicYearId, Guid hmId, Guid vcId);

        /// <summary>
        /// Get VT by SchoolIdHMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <param name="vcId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetVTBySchoolIdHMId(Guid academicYearId, Guid hmId, Guid vcId, Guid schoolId);

        /// <summary>
        /// Get VTP by AcademicYearId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetVTPByAYId(string roleId, Guid userId, Guid academicYearId);

        /// <summary>
        /// Get VC by AcademicYearId And VTPId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetVCByAYAndVTPId(string roleId, Guid userId, Guid academicYearId, Guid vtpId);

        /// <summary>
        /// Get VT by AcademicYearId And VCId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetVTByAYAndVCId(DataRequest dataRequest);

        /// <summary>
        ///  Get School by AcademicYearId And VCId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetSchoolByAYAndVCId(string roleId, Guid userId, Guid academicYearId, Guid vtpId, Guid vcId);

        /// <summary>
        /// Get VT by AcademicYearId And SchoolId
        /// </summary>
        /// <param name="vtRequest"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetVTByAYAndSchoolId(DataRequest vtRequest);

        // <summary>
        /// Get VT by AcademicYearId And VTPId And VCId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetVTByAYAndVTPIdVCId(string roleId, Guid userId, Guid academicYearId, Guid vtpId, Guid vcId);

        /// <summary>
        /// Get List of Units by Class, Module and JobRole
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="moduleTypeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetUnitsByClassAndModuleId(Guid classId, string moduleTypeId, Guid userId);

        /// <summary>
        /// Get List of Sessions by Unit
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetSessionsByUnitId(Guid unitId);

        /// <summary>
        /// Get List of Students by Class and Section
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        IList<StudentAttendanceModel> GetStudentsByClassIdForVT(Guid userId, Guid classId, Guid sectionId);

        /// <summary>
        /// Get list of school by VC
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetSchoolsByVCId(string userId, Guid vcId);

        /// <summary>
        /// Get list of school by DRP
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="drpId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetSchoolsByDRPId(string userId, Guid drpId);

        /// <summary>
        /// Get list of Course Module >> Units >> Sessions by VT
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vtId"></param>
        /// <returns></returns>
        IList<ModuleUnitSessionModel> GetCourseModuleUnitSessions(string userId, Guid vtId);

        /// <summary>
        /// Get list of Master Data
        /// </summary>
        /// <returns></returns>
        IList<MasterDataModel> GetCommonMasterData(string userId);

        /// <summary>
        /// Get list of Class Sections by VT
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vtId"></param>
        /// <returns></returns>
        IList<ClassSectionModel> GetClassSectionsByVTId(string userId, Guid vtId);

        /// <summary>
        /// Get list of student by VT
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        IList<StudentByVTModel> GetStudentsByVTId(Guid vtId);

        /// <summary>
        ///  Get JobRole by AcademicYearId And VTId And SchoolId
        /// </summary>
        /// <param name="vtId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetJobRoleByVTIdAyIdSchoolId(Guid vtId, Guid academicYearId, Guid schoolId);

        /// <summary>
        ///  Get Sector by AcademicYearId And VTId And SchoolId
        /// </summary>
        /// <param name="vtId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetSectorByVTIdAyIdSchoolId(Guid vtId, Guid academicYearId, Guid schoolId);

        /// <summary>
        ///  Get Sector by AcademicYearId And VTPId 
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetSectorByAyIdVTPId(Guid academicYearId, Guid vtpId);

        /// <summary>
        ///  Get Sector by AcademicYearId And VCId 
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetSectorByAyIdVCId(Guid academicYearId, Guid vcId);

        /// <summary>
        /// Get Student by ClassId And SectionId
        /// </summary>
        /// <param name="studentRequest"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetStudentsByClassIdSectionId(DataRequest studentRequest);

        /// <summary>
        ///  Get School by AcademicYearId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetSchoolsByAcademicYearId(Guid academicYearId);

        /// <summary>
        ///  Get VT by AcademicYearId And SchoolId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetVocationalTrainersByAcademicYearIdAndSchoolId(Guid academicYearId, Guid schoolId);

        /// <summary>
        /// Get School by RoleId And UserId And AcademicYearId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetSchoolByAYIdAndRoleId(string roleId, Guid userId, Guid academicYearId);

        /// <summary>
        ///  Get VC by AcademicYearId And VTPId And SectorId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetVCByVTPIdSectorId(Guid academicYearId, Guid vtpId, Guid sectorId);

        /// <summary>
        /// Get VTP by AcademicYearId And SectorId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetVTPByAYIdSectorId(Guid academicYearId, Guid sectorId);

        /// <summary>
        /// Get VTP by AcademicYearId And VTPId And VCId And SectorId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="vcId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetSchoolByVTPIdVCIdSectorId(Guid academicYearId, Guid vtpId, Guid vcId, Guid sectorId);

        /// <summary>
        /// Get VT by AcademicYearId And VTPId And VCId 
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        IList<DropdownModel<string>> GetVTByAYIdAndVTPIdVCId(Guid academicYearId, Guid vtpId, Guid vcId);

        /// <summary>
        /// Save Lighthouse Settings
        /// </summary>
        /// <param name="settingModel"></param>
        /// <returns></returns>
        SettingModel SaveLighthouseSettings(SettingModel settingModel);

        IList<DropdownModel<string>> GetDashboardData(DashboardDataRequest para);

        DashboardCardModel GetDashboardCardData(DashboardDataRequest dashboardRequest);

        IList<DashboardModel> GetDashboardVTAttendanceChartData(DashboardDataRequest para);

        IList<DashboardModel> GetDashboardVCAttendanceChartData(DashboardDataRequest para);

        IList<DashboardStudentAttendanceModel> GetDashboardStudentAttendanceChartData(DashboardDataRequest dashboardRequest);

        DashboardSchoolVisitStatusModel GetDashboardSchoolVisitStatusChartData(DashboardDataRequest dashboardRequest);

        IList<DashboardIssueManagementModel> GetDashboardIssueManagementStatusChartData(DashboardDataRequest dashboardRequest);

        IList<DashboardIssueManagementModel> GetDashboardIssueManagementChartData(DashboardDataRequest dashboardRequest);

        IList<JobRoleUnitCardModel> GetDashboardJobRoleUnitsCardData(DashboardDataRequest dashboardRequest);

        IList<T> GetLighthouseDashboards<T>(DashboardDataRequest dashboardRequest);

        string SendEmailTo(string toEmailId, int secureSocketId);

        /// <summary>
        /// Send SMS to Users
        /// </summary>
        /// <returns></returns>
        bool SendSMSFromMSG91(SMSRequest smsRequest);

        /// <summary>
        /// Send AppVersion to Users
        /// </summary>
        /// <returns></returns>
        dynamic GetAppVersion(AppVersionModel appVersionModel);

        /// <summary>
        /// Get Android Settings
        /// </summary>
        /// <returns></returns>
        IList<DropdownModel<string>> GetAndroidSettings();

        /// <summary>
        /// Get Lighthouse Settings
        /// </summary>
        /// <returns></returns>
        IList<DropdownModel<string>> GetLighthouseSettings();
    }
}