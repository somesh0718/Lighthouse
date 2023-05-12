using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.Controllers
{
    /// <summary>
    /// Expose all block WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class DashboardController : BaseController
    {
        private readonly IDashboardManager dashboardManager;
        private readonly ICommonManager commonManager;

        /// <summary>
        /// Initializes the Dashboard controller class.
        /// </summary>
        /// <param name="_commonManager"></param>
        public DashboardController(IDashboardManager _dashboardManager, ICommonManager _commonManager)
        {
            this.dashboardManager = _dashboardManager;
            this.commonManager = _commonManager;
        }

        /// <summary>
        /// Get summary of School details for Dashboard
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Masters.GetDashboardSchoolChartData)]
        public async Task<ListResponse<DashboardSchoolModel>> GetDashboardSchoolChartData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<DashboardSchoolModel> response = new ListResponse<DashboardSchoolModel>();

            try
            {
                var schoolModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<DashboardSchoolModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = schoolModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get summary of Trainer details for Dashboard
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardVocationalTrainersCardData)]
        public async Task<ListResponse<VocationalTrainersCardModel>> GetDashboardVocationalTrainersCardData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<VocationalTrainersCardModel> response = new ListResponse<VocationalTrainersCardModel>();

            try
            {
                var trainerModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<VocationalTrainersCardModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = trainerModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get summary of Class details for Dashboard
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardClassesCardData)]
        public async Task<ListResponse<ClassCardModel>> GetDashboardClassesCardData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<ClassCardModel> response = new ListResponse<ClassCardModel>();

            try
            {
                var classModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<ClassCardModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = classModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get summary of Field Visit details for Dashboard
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardFieldVisitChartData)]
        public async Task<ListResponse<DashboardFieldVisitModel>> GetDashboardFieldVisitChartData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<DashboardFieldVisitModel> response = new ListResponse<DashboardFieldVisitModel>();

            try
            {
                var fieldVisitModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<DashboardFieldVisitModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = fieldVisitModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get summary of Guest Lecture details for Dashboard
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardGuestLectureChartData)]
        public async Task<ListResponse<DashboardGuestLectureModel>> GetDashboardGuestLectureChartData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<DashboardGuestLectureModel> response = new ListResponse<DashboardGuestLectureModel>();

            try
            {
                var guestLectureModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<DashboardGuestLectureModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = guestLectureModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get summary of Course Materials details for Dashboard
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardCourseMaterialChartData)]
        public async Task<ListResponse<DashboardCourseMaterialModel>> GetDashboardCourseMaterialChartData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<DashboardCourseMaterialModel> response = new ListResponse<DashboardCourseMaterialModel>();

            try
            {
                var courseMaterialModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<DashboardCourseMaterialModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = courseMaterialModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get summary of Tools & Equipments details for Dashboard
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardToolsAndEquipmentChartData)]
        public async Task<ListResponse<DashboardToolEquipmentModel>> GetDashboardToolsAndEquipmentChartData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<DashboardToolEquipmentModel> response = new ListResponse<DashboardToolEquipmentModel>();

            try
            {
                var toolEquipmentModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<DashboardToolEquipmentModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = toolEquipmentModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get summary of Student details for Dashboard
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardStudentsCardData)]
        public async Task<ListResponse<StudentCardModel>> GetDashboardStudentsCardData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<StudentCardModel> response = new ListResponse<StudentCardModel>();

            try
            {
                var studentModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<StudentCardModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = studentModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get summary of trainer attendance for Dashboard
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardVTAttendanceChartData)]
        public async Task<ListResponse<DashboardModel>> GetDashboardVTAttendanceChartData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<DashboardModel> response = new ListResponse<DashboardModel>();

            try
            {
                var vtAttendanceModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<DashboardModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = vtAttendanceModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get summary of trainer attendance for Dashboard
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardStudentAttendanceChartData)]
        public async Task<ListResponse<DashboardStudentAttendanceModel>> GetDashboardStudentAttendanceChartData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<DashboardStudentAttendanceModel> response = new ListResponse<DashboardStudentAttendanceModel>();

            try
            {
                var studentModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<DashboardStudentAttendanceModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = studentModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get School details with Jobrole Units and Classes
        /// </summary>
        /// <param name="dashboardRequest"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Dashboard.Compare.Schools)]
        public async Task<ListResponse<CompareSchoolModel>> GetCompareSchoolsData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<CompareSchoolModel> response = new ListResponse<CompareSchoolModel>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<CompareSchoolModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = reportModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get Course Material details with Jobrole Units and Classes
        /// </summary>
        /// <param name="dashboardRequest"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Dashboard.Compare.CourseMaterials)]
        public async Task<ListResponse<CompareCourseMaterialModel>> GetCompareCourseMaterialsData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<CompareCourseMaterialModel> response = new ListResponse<CompareCourseMaterialModel>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<CompareCourseMaterialModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = reportModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get Tools & Equipments details with Jobrole Units and Classes
        /// </summary>
        /// <param name="dashboardRequest"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Dashboard.Compare.ToolsAndEquipments)]
        public async Task<ListResponse<CompareToolsAndEquipmentModel>> GetCompareToolsAndEquipmentsData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<CompareToolsAndEquipmentModel> response = new ListResponse<CompareToolsAndEquipmentModel>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<CompareToolsAndEquipmentModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = reportModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get Student details with Classes and Students
        /// </summary>
        /// <param name="dashboardRequest"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Dashboard.Compare.Students)]
        public async Task<ListResponse<CompareStudentModel>> GetCompareStudentsData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<CompareStudentModel> response = new ListResponse<CompareStudentModel>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<CompareStudentModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = reportModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get School details with New Enrolment and Dropout Students
        /// </summary>
        /// <param name="dashboardRequest"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Dashboard.Compare.NewEnrolmentAndDropoutStudents)]
        public async Task<ListResponse<CompareNewEnrolmentAndDropoutStudentModel>> GetCompareNewEnrolmentAndDropoutStudentsData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<CompareNewEnrolmentAndDropoutStudentModel> response = new ListResponse<CompareNewEnrolmentAndDropoutStudentModel>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<CompareNewEnrolmentAndDropoutStudentModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = reportModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get Guest Lecture details with classes and GL conducted count
        /// </summary>
        /// <param name="dashboardRequest"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Dashboard.Compare.GuestLectures)]
        public async Task<ListResponse<CompareGuestLectureModel>> GetCompareGuestLecturesData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<CompareGuestLectureModel> response = new ListResponse<CompareGuestLectureModel>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<CompareGuestLectureModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = reportModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get Field Visit details with classes and GL conducted count
        /// </summary>
        /// <param name="dashboardRequest"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Dashboard.Compare.FieldVisits)]
        public async Task<ListResponse<CompareFieldVisitModel>> GetCompareFieldVisitsData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<CompareFieldVisitModel> response = new ListResponse<CompareFieldVisitModel>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<CompareFieldVisitModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = reportModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get Trainer details with reporting attendance
        /// </summary>
        /// <param name="dashboardRequest"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Dashboard.Compare.Trainers)]
        public async Task<ListResponse<CompareTrainerModel>> GetCompareTrainersData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<CompareTrainerModel> response = new ListResponse<CompareTrainerModel>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<CompareTrainerModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = reportModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get Coordinator details with reporting attendance
        /// </summary>
        /// <param name="dashboardRequest"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Dashboard.Compare.Coordinators)]
        public async Task<ListResponse<CompareCoordinatorModel>> GetCompareCoordinatorsData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<CompareCoordinatorModel> response = new ListResponse<CompareCoordinatorModel>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<CompareCoordinatorModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = reportModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get VT & VC Reporting details with attendance
        /// </summary>
        /// <param name="dashboardRequest"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Dashboard.Compare.VTVCReporting)]
        public async Task<ListResponse<CompareVTVCReportingModel>> GetCompareVTVCReportingData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<CompareVTVCReportingModel> response = new ListResponse<CompareVTVCReportingModel>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.dashboardManager.GetLighthouseDashboards<CompareVTVCReportingModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = reportModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }
    }
}