using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.EmailServices;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Models.Common;
using Igmite.Lighthouse.Platform;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Igmite.Lighthouse.BAL.Providers
{
    public class CommonManager : GenericManager<AccountModel>, ICommonManager
    {
        private readonly ICommonRepository commonRepository;
        private readonly IEmailSender emailSender;

        /// <summary>
        /// Initializes the common manager class.
        /// </summary>
        /// <param name="commonRepository"></param>
        public CommonManager(ICommonRepository _commonRepository, IEmailSender _emailSender)
        {
            this.commonRepository = _commonRepository;
            this.emailSender = _emailSender;
        }

        public IList<DropdownModel<Guid>> GetRoles()
        {
            return this.commonRepository.GetRoles();
        }

        public IList<DropdownModel<Guid>> GetAllRoles()
        {
            return this.commonRepository.GetAllRoles();
        }

        public IList<DropdownModel<Guid>> GetAccounts()
        {
            return this.commonRepository.GetAccounts();
        }

        public IList<DropdownModel<Guid>> GetSiteHeaders()
        {
            return this.commonRepository.GetSiteHeaders();
        }

        public IList<DropdownModel<Guid>> GetTransactions()
        {
            return this.commonRepository.GetTransactions();
        }

        public IList<DropdownModel<string>> GetCountries()
        {
            return this.commonRepository.GetCountries();
        }

        public IList<DropdownModel<string>> GetStates(string countryCode)
        {
            return this.commonRepository.GetStates(countryCode);
        }

        public IList<DropdownModel<string>> GetDistricts(string stateCode)
        {
            return this.commonRepository.GetDistricts(stateCode);
        }

        public IList<HeaderMenuModel> GetAccountTransactions(string userId, string action)
        {
            return this.commonRepository.GetAccountTransactions(userId, action);
        }

        public IList<DropdownModel<string>> GetDropdownDataById(string dataTypeId)
        {
            return this.commonRepository.GetDropdownDataById(dataTypeId);
        }

        public IList<DropdownModel<string>> GetMasterDataForDropdown(MasterDataRequest dataRequest)
        {
            return this.commonRepository.GetMasterDataForDropdown(dataRequest);
        }

        public IList<DropdownModel<string>> GetTargetVocationalTrainers(MasterDataForAcademicRollover request)
        {
            return this.commonRepository.GetTargetVocationalTrainers(request);
        }

        public IList<DropdownModel<string>> GetStudentsByUserId(string userType, string userId)
        {
            return this.commonRepository.GetStudentsByUserId(userType, userId);
        }

        public IList<DropdownModel<string>> GetSchoolVTPSectorsByUserId(string userId, string userTypeId, string academicYearId)
        {
            return this.commonRepository.GetSchoolVTPSectorsByUserId(userId, userTypeId, academicYearId);
        }

        public IList<DropdownModel<Guid>> GetAdministrator()
        {
            return this.commonRepository.GetAdministrator();
        }

        public IList<DropdownModel<string>> GetClassesByVTId(string userId, Guid vtId)
        {
            return this.commonRepository.GetClassesByVTId(userId, vtId);
        }

        public IList<DropdownModel<string>> GetSectionsByVTClassId(Guid vtId, Guid classId)
        {
            return this.commonRepository.GetSectionsByVTClassId(vtId, classId);
        }

        /// <summary>
        /// Get List of Units by Class, Module and JobRole
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="moduleTypeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetUnitsByClassAndModuleId(Guid classId, string moduleTypeId, Guid userId)
        {
            return this.commonRepository.GetUnitsByClassAndModuleId(classId, moduleTypeId, userId);
        }

        /// <summary>
        /// Get List of Sessions by Unit
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSessionsByUnitId(Guid unitId)
        {
            return this.commonRepository.GetSessionsByUnitId(unitId);
        }

        /// <summary>
        /// Get List of Students by Class and Section
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public IList<StudentAttendanceModel> GetStudentsByClassIdForVT(Guid userId, Guid classId, Guid sectionId)
        {
            return this.commonRepository.GetStudentsByClassIdForVT(userId, classId, sectionId);
        }

        /// <summary>
        /// Get list of school by VC
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSchoolsByVCId(string userId, Guid vcId)
        {
            return this.commonRepository.GetSchoolsByVCId(userId, vcId);
        }

        /// <summary>
        /// Get list of school by DRP
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="drpId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSchoolsByDRPId(string userId, Guid drpId)
        {
            return this.commonRepository.GetSchoolsByDRPId(userId, drpId);
        }

        /// <summary>
        /// Get list of Course Module >> Units >> Sessions by VT
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public IList<ModuleUnitSessionModel> GetCourseModuleUnitSessions(string userId, Guid vtId)
        {
            return this.commonRepository.GetCourseModuleUnitSessions(userId, vtId);
        }

        /// <summary>
        /// Get list of Master Data
        /// </summary>
        /// <returns></returns>
        public IList<MasterDataModel> GetCommonMasterData(string userId)
        {
            return this.commonRepository.GetCommonMasterData(userId);
        }

        /// <summary>
        /// Get list of Class Sections by VT
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public IList<ClassSectionModel> GetClassSectionsByVTId(string userId, Guid vtId)
        {
            return this.commonRepository.GetClassSectionsByVTId(userId, vtId);
        }

        /// <summary>
        /// Get list of student by VT
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public IList<StudentByVTModel> GetStudentsByVTId(Guid vtId)
        {
            return this.commonRepository.GetStudentsByVTId(vtId);
        }

        /// <summary>
        /// Save Lighthouse Settings
        /// </summary>
        /// <param name="settingModel"></param>
        /// <returns></returns>
        public SettingModel SaveLighthouseSettings(SettingModel settingModel)
        {
            SettingModel setting = this.commonRepository.SaveLighthouseSettings(settingModel);

            setting.Password = Cryptography.CryptographyManager.Decrypt(setting.Password, true);

            return setting;
        }

        /// <summary>
        /// Get VTP & VC Id by SchoolId
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public LhUserModel GetVTPandVCIdBySchoolId(Guid schoolId)
        {
            return this.commonRepository.GetVTPandVCIdBySchoolId(schoolId);
        }

        /// <summary>
        /// Get VTP, VC & School Id by VTId
        /// </summary>
        /// <param name="vtId"></param>
        /// <returns></returns>
        public LhUserModel GetVTPVCAndSchoolIdByVTId(Guid vtId)
        {
            return this.commonRepository.GetVTPVCAndSchoolIdByVTId(vtId);
        }

        /// <summary>
        /// Get VTP, VC & School Id by HMId
        /// </summary>
        /// <param name="hmId"></param>
        /// <returns></returns>
        public LhUserModel GetVTPVCAndSchoolIdByHMId(Guid hmId)
        {
            return this.commonRepository.GetVTPVCSchoolIdByHMId(hmId);
        }

        /// <summary>
        /// Get VTP by HMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVTPByHMId(Guid academicYearId, Guid hmId)
        {
            return this.commonRepository.GetVTPByHMId(academicYearId, hmId);
        }

        /// <summary>
        /// Get VC by HMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVCByHMId(Guid academicYearId, Guid hmId, Guid vtpId)
        {
            return this.commonRepository.GetVCByHMId(academicYearId, hmId, vtpId);
        }

        /// <summary>
        /// Get VT by HMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVTByHMId(Guid academicYearId, Guid hmId, Guid vcId)
        {
            return this.commonRepository.GetVTByHMId(academicYearId, hmId, vcId);
        }

        /// <summary>
        /// Get School by HMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSchoolByHMId(Guid academicYearId, Guid hmId, Guid vcId)
        {
            return this.commonRepository.GetSchoolByHMId(academicYearId, hmId, vcId);
        }

        /// <summary>
        /// Get VT by SchoolIdHMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <param name="vcId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVTBySchoolIdHMId(Guid academicYearId, Guid hmId, Guid vcId, Guid schoolId)
        {
            return this.commonRepository.GetVTBySchoolIdHMId(academicYearId, hmId, vcId, schoolId);
        }

        /// <summary>
        /// Get VTP by AcademicYearId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVTPByAYId(string roleId, Guid userId, Guid academicYearId)
        {
            return this.commonRepository.GetVTPByAYId(roleId, userId, academicYearId);
        }

        /// <summary>
        /// Get VC by AcademicYearId And VTPId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVCByAYAndVTPId(string roleId, Guid userId, Guid academicYearId, Guid vtpId)
        {
            return this.commonRepository.GetVCByAYAndVTPId(roleId, userId, academicYearId, vtpId);
        }

        /// <summary>
        /// Get VT by AcademicYearId And VCId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVTByAYAndVCId(DataRequest dataRequest)
        {
            return this.commonRepository.GetVTByAYAndVCId(dataRequest);
        }

        /// <summary>
        /// Get School by AcademicYearId And VCId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSchoolByAYAndVCId(string roleId, Guid userId, Guid academicYearId, Guid vtpId, Guid vcId)
        {
            return this.commonRepository.GetSchoolByAYAndVCId(roleId, userId, academicYearId, vtpId, vcId);
        }

        /// <summary>
        /// Get VT by AcademicYearId And VTPId And VCId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVTByAYAndVTPIdVCId(string roleId, Guid userId, Guid academicYearId, Guid vtpId, Guid vcId)
        {
            return this.commonRepository.GetVTByAYAndVTPIdVCId(roleId, userId, academicYearId, vtpId, vcId);
        }

        /// <summary>
        /// Get VT by AcademicYearId And SchoolId
        /// </summary>
        /// <param name="vtRequest"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVTByAYAndSchoolId(DataRequest vtRequest)
        {
            return this.commonRepository.GetVTByAYAndSchoolId(vtRequest);
        }

        /// <summary>
        ///  Get JobRole by AcademicYearId And VTId And SchoolId
        /// </summary>
        /// <param name="vtId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetJobRoleByVTIdAyIdSchoolId(Guid vtId, Guid academicYearId, Guid schoolId)
        {
            return this.commonRepository.GetJobRoleByVTIdAyIdSchoolId(vtId, academicYearId, schoolId);
        }

        /// <summary>
        ///  Get Sector by AcademicYearId And VTId And SchoolId
        /// </summary>
        /// <param name="vtId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSectorByVTIdAyIdSchoolId(Guid vtId, Guid academicYearId, Guid schoolId)
        {
            return this.commonRepository.GetSectorByVTIdAyIdSchoolId(vtId, academicYearId, schoolId);
        }

        /// <summary>
        ///  Get Sector by AcademicYearId And VTPId 
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSectorByAyIdVTPId( Guid academicYearId, Guid vtpId)
        {
            return this.commonRepository.GetSectorByAyIdVTPId(academicYearId, vtpId);
        }

        /// <summary>
        ///  Get Sector by AcademicYearId And VCId 
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSectorByAyIdVCId(Guid academicYearId, Guid vcId)
        {
            return this.commonRepository.GetSectorByAyIdVCId(academicYearId, vcId);
        }

        /// <summary>
        /// Get Student by ClassId And SectionId
        /// </summary>
        /// <param name="studentRequest"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetStudentsByClassIdSectionId(DataRequest studentRequest)
        {
            return this.commonRepository.GetStudentsByClassIdSectionId(studentRequest);
        }

        /// <summary>
        ///  Get School by AcademicYearId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSchoolsByAcademicYearId(Guid academicYearId)
        {
            return this.commonRepository.GetSchoolsByAcademicYearId(academicYearId);
        }

        /// <summary>
        ///  Get VT by AcademicYearId And SchoolId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVocationalTrainersByAcademicYearIdAndSchoolId(Guid academicYearId, Guid schoolId)
        {
            return this.commonRepository.GetVocationalTrainersByAcademicYearIdAndSchoolId(academicYearId, schoolId);
        }

        /// <summary>
        ///  Get School by RoleId And UserId And AcademicYearId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSchoolByAYIdAndRoleId(string roleId, Guid userId, Guid academicYearId)
        {
            return this.commonRepository.GetSchoolByAYIdAndRoleId(roleId, userId, academicYearId);
        }

        /// <summary>
        ///  Get VC by AcademicYearId And VTPId And SectorId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVCByVTPIdSectorId(Guid academicYearId, Guid vtpId, Guid sectorId)
        {
            return this.commonRepository.GetVCByVTPIdSectorId( academicYearId,  vtpId,  sectorId);
        }

        /// <summary>
        /// Get VTP by AcademicYearId And SectorId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVTPByAYIdSectorId(Guid academicYearId, Guid sectorId)
        {
            return this.commonRepository.GetVTPByAYIdSectorId(academicYearId, sectorId);
        }

        /// <summary>
        /// Get School by AcademicYearId And VTPId And VCId And SectorId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="vcId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetSchoolByVTPIdVCIdSectorId(Guid academicYearId, Guid vtpId, Guid vcId, Guid sectorId)
        {
            return this.commonRepository.GetSchoolByVTPIdVCIdSectorId( academicYearId, vtpId,  vcId, sectorId);
        }

        /// <summary>
        /// Get VT by AcademicYearId And VTPId And VCId 
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetVTByAYIdAndVTPIdVCId(Guid academicYearId, Guid vtpId, Guid vcId)
        {
            return this.commonRepository.GetVTByAYIdAndVTPIdVCId(academicYearId, vtpId, vcId);
        }

        public IList<DropdownModel<string>> GetDashboardData(DashboardDataRequest para)
        {
            return this.commonRepository.GetDashboardData(para);
        }

        public DashboardCardModel GetDashboardCardData(DashboardDataRequest dashboardRequest)
        {
            return this.commonRepository.GetDashboardCardData(dashboardRequest);
        }

        public IList<DashboardModel> GetDashboardVTAttendanceChartData(DashboardDataRequest para)
        {
            return this.commonRepository.GetDashboardVTAttendanceChartData(para);
        }

        public IList<DashboardModel> GetDashboardVCAttendanceChartData(DashboardDataRequest para)
        {
            return this.commonRepository.GetDashboardVCAttendanceChartData(para);
        }

        public IList<DashboardStudentAttendanceModel> GetDashboardStudentAttendanceChartData(DashboardDataRequest dashboardRequest)
        {
            return this.commonRepository.GetDashboardStudentAttendanceChartData(dashboardRequest);
        }

        public DashboardSchoolVisitStatusModel GetDashboardSchoolVisitStatusChartData(DashboardDataRequest dashboardRequest)
        {
            return this.commonRepository.GetDashboardSchoolVisitStatusChartData(dashboardRequest);
        }

        public IList<DashboardIssueManagementModel> GetDashboardIssueManagementStatusChartData(DashboardDataRequest dashboardRequest)
        {
            return this.commonRepository.GetDashboardIssueManagementStatusChartData(dashboardRequest);
        }

        public IList<DashboardIssueManagementModel> GetDashboardIssueManagementChartData(DashboardDataRequest dashboardRequest)
        {
            return this.commonRepository.GetDashboardIssueManagementChartData(dashboardRequest);
        }

        public IList<JobRoleUnitCardModel> GetDashboardJobRoleUnitsCardData(DashboardDataRequest dashboardRequest)
        {
            return this.commonRepository.GetDashboardJobRoleUnitsCardData(dashboardRequest);
        }

        /// <summary>
        /// Get Lighthouse dashboard data with filter criteria
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dashboardRequest"></param>
        /// <returns></returns>
        public IList<T> GetLighthouseDashboards<T>(DashboardDataRequest dashboardRequest)
        {
            return this.commonRepository.GetLighthouseDashboards<T>(dashboardRequest);
        }

        /// <summary>
        /// Test email sending features from Lighthouse 2.0
        /// </summary>
        /// <param name="toEmailId"></param>
        /// <returns></returns>
        public string SendEmailTo(string toEmailId, int secureSocketId)
        {
            try
            {
                string subject = "Test Email from Lighthouse";

                StringBuilder sbNewUserTemplate = new StringBuilder();
                sbNewUserTemplate.AppendFormat("<p>Hi,</p>\n");
                sbNewUserTemplate.AppendFormat("<p>Welcome to Lighthouse {0}.</p>\n", Constants.StateCode);
                sbNewUserTemplate.AppendFormat("<p><b>Username:</b> {0}</p>\n", toEmailId);
                sbNewUserTemplate.AppendLine("<p>Thanks,</p>");
                sbNewUserTemplate.AppendLine("<p>Your Lighthouse Team</p>");

                Message message = new Message(new string[] { toEmailId }, subject, sbNewUserTemplate.ToString(), null);

                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(this.emailSender.EmailConfig.From));
                emailMessage.To.AddRange(message.To);
                emailMessage.Subject = message.Subject;

                var bodyBuilder = new BodyBuilder { HtmlBody = message.Content };

                emailMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    try
                    {
                        if (this.emailSender.EmailConfig.IsEnabled)
                        {
                            SecureSocketOptions secureSocketOptions = SecureSocketOptions.None;

                            if (secureSocketId == 1)
                                secureSocketOptions = SecureSocketOptions.Auto;
                            else if (secureSocketId == 2)
                                secureSocketOptions = SecureSocketOptions.SslOnConnect;
                            else if (secureSocketId == 3)
                                secureSocketOptions = SecureSocketOptions.StartTls;
                            else if (secureSocketId == 4)
                                secureSocketOptions = SecureSocketOptions.StartTlsWhenAvailable;

                            client.Connect(this.emailSender.EmailConfig.SmtpServer, this.emailSender.EmailConfig.Port, secureSocketOptions);
                            client.AuthenticationMechanisms.Remove("XOAUTH2");
                            client.Authenticate(this.emailSender.EmailConfig.UserName, this.emailSender.EmailConfig.Password);

                            client.Send(emailMessage);
                        }
                    }
                    catch (Exception ex)
                    {
                        client.Disconnect(true);
                        client.Dispose();

                        throw ex;
                    }

                    client.Disconnect(true);
                    client.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("BAL > SaveOrUpdateVocationalTrainerDetails>Sending Email", ex);
            }
            return string.Format("Send Test email to {0}", Constants.TestToEmail);
        }

        /// <summary>
        /// Send SMS to Users
        /// </summary>
        /// <returns></returns>
        public bool SendSMSFromMSG91(SMSRequest smsRequest)
        {
            bool smsResponse = false;
            SmsServiceProvider smsServiceProvider = new SmsServiceProvider();

            if (smsRequest.MessageType == "OTP")
            {
                OTPRequest otpRequest = new OTPRequest();
                otpRequest.OTPNumber = StringUtility.GetOTPToken(6);
                otpRequest.OTPDateTime = DateTime.Now.AddMinutes(5).ToString("dd/MM/yyyy hh:mm:ss tt");

                smsResponse = smsServiceProvider.SendSMSFromMSG91(otpRequest.SendTo, otpRequest, null);
            }

            return smsResponse;
        }

        /// <summary>
        /// Send AppVersion to Users
        /// </summary>
        /// <returns></returns>
        public dynamic GetAppVersion(AppVersionModel appVersionModel)
        {
            return this.commonRepository.GetAppVersion(appVersionModel);
        }

        /// <summary>
        /// Get Android Settings
        /// </summary>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetAndroidSettings()
        {
            return this.commonRepository.GetAndroidSettings();
        }

        /// <summary>
        /// Get Lighthouse Settings
        /// </summary>
        /// <returns></returns>
        public IList<DropdownModel<string>> GetLighthouseSettings()
        {
            return this.commonRepository.GetLighthouseSettings();
        }
    }
}