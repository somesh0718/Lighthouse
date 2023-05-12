using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Repository of the Dashboard Job Works
    /// </summary>
    public class DashboardJobManager : GenericManager<RoleModel>, IDashboardJobManager
    {
        private readonly IDashboardJobRepository dashboardJobRepository;
        private readonly ICommonRepository commonRepository;

        /// <summary>
        /// Initializes the Dashboard Job manager.
        /// </summary>
        /// <param name="dashboardJobRepository"></param>
        public DashboardJobManager(IDashboardJobRepository _dashboardJobRepository, ICommonRepository _commonRepository)
        {
            this.dashboardJobRepository = _dashboardJobRepository;
            this.commonRepository = _commonRepository;
        }

        /// <summary>
        /// Generate Information for Summary Dashboard
        /// </summary>
        public async Task GenerateDashboardInformationAsync()
        {
            DateTime reportDate = Constants.GetCurrentDateTime.AddDays(-1).Date;

            KeyValuePair<Guid, IList<string>> reportParams = await this.dashboardJobRepository.IsExecutedGeneratingDashboardDataScripts(reportDate);

            Guid academicYearId = reportParams.Key;

            if (!reportParams.Value.Contains("Schools"))
                await this.dashboardJobRepository.GenerateDashboardSchoolInfoAsync(academicYearId, reportDate);

            if (!reportParams.Value.Contains("Trainers"))
                await this.dashboardJobRepository.GenerateDashboardTrainerInfoAsync(academicYearId, reportDate);

            if (!reportParams.Value.Contains("Classes"))
                await this.dashboardJobRepository.GenerateDashboardClassInfoAsync(academicYearId, reportDate);

            if (!reportParams.Value.Contains("Students"))
                await this.dashboardJobRepository.GenerateDashboardStudentInfoAsync(academicYearId, reportDate);

            if (!reportParams.Value.Contains("SchoolClasses"))
                await this.dashboardJobRepository.GenerateDashboardSchoolClassesInfoAsync(academicYearId, reportDate);

            if (!reportParams.Value.Contains("CourseMaterials"))
                await this.dashboardJobRepository.GenerateDashboardCourseMaterialInfoAsync(academicYearId, reportDate);

            if (!reportParams.Value.Contains("SectorJobRoles"))
                await this.dashboardJobRepository.GenerateDashboardSectorJobRoleInfoAsync(academicYearId, reportDate);

            if (!reportParams.Value.Contains("ToolsAndEquipments"))
                await this.dashboardJobRepository.GenerateDashboardToolsAndEquipmentsInfoAsync(academicYearId, reportDate);

            if (!reportParams.Value.Contains("FieldVisits"))
                await this.dashboardJobRepository.GenerateDashboardFieldVisitInfoAsync(academicYearId, reportDate);

            if (!reportParams.Value.Contains("GuestLectures"))
                await this.dashboardJobRepository.GenerateDashboardGuestLectureInfoAsync(academicYearId, reportDate);

            if (!reportParams.Value.Contains("TrainerAttendances"))
                await this.dashboardJobRepository.GenerateDashboardTrainerAttendanceInfoAsync(academicYearId, reportDate);

            if (!reportParams.Value.Contains("CoordinatorAttendances"))
                await this.dashboardJobRepository.GenerateDashboardCoordinatorAttendanceInfoAsync(academicYearId, reportDate);

            if (!reportParams.Value.Contains("StudentAttendances"))
                await this.dashboardJobRepository.GenerateDashboardStudentAttendanceInfoAsync(academicYearId, reportDate);
        }

        /// <summary>
        /// Generate Not Submitted Reporting data for all Vocational Trainers
        /// </summary>
        public async Task GenerateNotSubmittedReportByVTAsync()
        {
            DateTime currentDate = Constants.GetCurrentDateTime;
            DateTime nextMonthDate = Constants.GetCurrentDateTime.AddMonths(1);

            int lastDayOfMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);

            if (currentDate.Day > (lastDayOfMonth - 3))
            {
                lastDayOfMonth = DateTime.DaysInMonth(currentDate.Year, nextMonthDate.Month);

                DateTime startDate = Convert.ToDateTime(nextMonthDate.ToString("yyyy/MM/") + "01");
                DateTime endDate = Convert.ToDateTime(nextMonthDate.ToString("yyyy/MM/") + lastDayOfMonth.ToString());

                KeyValuePair<Guid, IList<string>> reportParams = await this.dashboardJobRepository.IsExecutedGeneratingDashboardDataScripts(currentDate);

                if (!reportParams.Value.Contains("VTNotSubmitted"))
                    await this.dashboardJobRepository.GenerateNotSubmittedReportByVTAsync(startDate, endDate);
            }
        }

        /// <summary>
        /// Send Weekly VC/VT Attendance Message To User
        /// </summary>
        public async Task SendWeeklyAttendanceMessageToUserAsync()
        {
            DateTime currentDate = Constants.GetCurrentDateTime;

            await Task.Run(() =>
            {
                var vtNotReportedDailyAttendance = this.dashboardJobRepository.GetVTNotReportedDailyAttendances(currentDate, currentDate.AddDays(6));
                IList<MessageTemplate> messageTemplates = this.commonRepository.GetMessageTemplates();
                IList<string> sendToMobiles = new List<string>();

                MessageTemplate messageTemplateItem = messageTemplates.FirstOrDefault(m => m.MessageTypeId == "VT" && m.MessageSubTypeId == "VTDR");

                if (messageTemplateItem != null && messageTemplateItem.IsActive && messageTemplateItem.ApplicableFor.Contains("SMS"))
                {
                    for (int attIndex = 0; attIndex < vtNotReportedDailyAttendance.Count; attIndex++)
                    {
                        try
                        {
                            SmsServiceProvider smsServiceProvider = new SmsServiceProvider();

                            VTRequest vtRequest = new VTRequest();
                            vtRequest.MessageType = messageTemplateItem.MessageSubTypeId;
                            vtRequest.SendTo = Constants.IsDeveloperMode ? Constants.TestToMobile : vtNotReportedDailyAttendance[attIndex].VTMobile;

                            vtRequest.VTName = vtNotReportedDailyAttendance[attIndex].VTName;
                            vtRequest.VTEmailId = vtNotReportedDailyAttendance[attIndex].VTEmailId;
                            vtRequest.ReportingDate = vtNotReportedDailyAttendance[attIndex].FromReportDate.ToString("dd/MM/yyyy hh:mm:ss tt");

                            if (!sendToMobiles.Contains(vtRequest.SendTo))
                            {
                                smsServiceProvider.SendSMSFromMSG91(vtRequest.SendTo, vtRequest, messageTemplateItem);
                                sendToMobiles.Add(vtRequest.SendTo);
                            }
                        }
                        catch (Exception exSMS)
                        {
                            throw new Exception("Sending SMS for GL - failed", exSMS);
                        }
                    }
                }

                return true;
            });
        }
    }
}