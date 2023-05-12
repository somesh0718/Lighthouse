using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.Controllers
{
    /// <summary>
    /// Expose all Lighthouse WebAPI services
    /// </summary>
    [Route("LighthouseServices/[controller]"), ApiController]
    public class LighthouseController : Controller
    {
        private readonly ICommonManager commonManager;
        private readonly IAccountManager accountManager;

        public LighthouseController(ICommonManager _commonManager, IAccountManager _accountManager)
        {
            this.commonManager = _commonManager;
            this.accountManager = _accountManager;
        }

        [HttpGet, AllowAnonymous, Route("GetVersion")]
        public string GetVersion()
        {
            return string.Format("Lighthouse Service - {0}", Constants.Version);
        }

        [HttpGet, AllowAnonymous, Route("GetServerInformation")]
        public string GetServerInformation()
        {
            return string.Format("Address:{0}   Port:{1}", Constants.ServiceIPAddress, Constants.ServiceIPPort);
        }

        [HttpGet, AllowAnonymous, Route("SendSMSFromMSG91")]
        public string SendSMSFromMSG91()
        {
            SMSRequest smsRequest = new SMSRequest { MessageType = "OTP", SendTo = "9322826712" };
            this.commonManager.SendSMSFromMSG91(smsRequest);

            return "Sent SMS to user.";
        }

        [HttpGet, IgmiteAuthorize, Route("GetEncryptText")]
        public string GetEncryptText([FromQuery] string strValue)
        {
            string encryptString = Cryptography.CryptographyManager.Encrypt(strValue, true);

            return string.Format("{0}", encryptString);
        }

        [HttpGet, IgmiteAuthorize, Route("GetDecryptText")]
        public string GetDecryptText([FromQuery] string strValue)
        {
            string decryptString = Cryptography.CryptographyManager.Decrypt(strValue, true);

            return string.Format("{0}", decryptString);
        }

        [HttpGet, IgmiteAuthorize, Route("GetUserInfo")]
        public string GetUserInfo([FromQuery] string userId)
        {
            AccountModel account = this.accountManager.GetAccountByLoginId(userId);
            string userInfo = string.Format("UserId : {0} \nPassword:{1}\nAccountType:{2}", account.LoginId, account.Password, account.AccountType);

            return string.Format("{0}", userInfo);
        }

        [HttpGet, IgmiteAuthorize, Route("NoDirectAccess")]
        public string NoDirectAccess()
        {
            return string.Format("You are not authorized to perform this operation.");
        }

        /// <summary>
        /// Get list of division data
        /// </summary>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetMasterDataByType)]
        public async Task<ListResponse<DropdownModel<string>>> GetMasterDataForDropdown([FromBody] MasterDataRequest dataRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetMasterDataForDropdown(dataRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get TargetVocationalTrainers for academic rollover if VT is different
        /// </summary>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route("GetTargetVocationalTrainers")]
        public async Task<ListResponse<DropdownModel<string>>> GetTargetVocationalTrainers([FromBody] MasterDataForAcademicRollover request)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetTargetVocationalTrainers(request);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of division data
        /// </summary>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetSchoolVTPSectorsByUserId)]
        public async Task<ListResponse<DropdownModel<string>>> GetSchoolVTPSectorsByUserId([FromBody] DataRequest request)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetSchoolVTPSectorsByUserId(request.DataId, request.DataId1, request.DataId2);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of division data
        /// </summary>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetStudentsByUserId)]
        public async Task<ListResponse<DropdownModel<string>>> GetStudentsByUserId([FromBody] MasterDataRequest request)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> studentModels = await Task.Run(() =>
                {
                    return this.commonManager.GetStudentsByUserId(request.DataType, request.UserId);
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
        /// Get all roles
        /// </summary>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetAllRoles)]
        public async Task<ListResponse<DropdownModel<Guid>>> GetAllRoles()
        {
            ListResponse<DropdownModel<Guid>> response = new ListResponse<DropdownModel<Guid>>();

            try
            {
                IList<DropdownModel<Guid>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetAllRoles();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of class against Vocational Trainer
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vtId"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetClassesByVTId)]
        public async Task<ListResponse<DropdownModel<string>>> GetClassesByVTId([FromBody] DataRequest dataRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetClassesByVTId(dataRequest.DataId, Guid.Parse(dataRequest.DataId1));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of sections against class for Vocational Trainer
        /// </summary>
        /// <param name="vtId"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetSectionsByVTClassId)]
        public async Task<ListResponse<DropdownModel<string>>> GetSectionsByVTClassId([FromBody] DataRequest dataRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetSectionsByVTClassId(Guid.Parse(dataRequest.DataId), Guid.Parse(dataRequest.DataId1));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get List of Units by Class, Module and JobRole
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="moduleTypeId"></param>
        /// <param name="jobRoleId"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetUnitsByClassAndModuleId)]
        public async Task<ListResponse<DropdownModel<string>>> GetUnitsByClassAndModuleId([FromBody] DataRequest dataRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetUnitsByClassAndModuleId(Guid.Parse(dataRequest.DataId), dataRequest.DataId1, Guid.Parse(dataRequest.DataId2));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get List of Sessions by Unit
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetSessionsByUnitId)]
        public async Task<ListResponse<DropdownModel<string>>> GetSessionsByUnitId([FromBody] DataRequest dataRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetSessionsByUnitId(Guid.Parse(dataRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get List of Students by Class and Section
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="classId"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetStudentsByClassIdForVT)]
        public async Task<ListResponse<StudentAttendanceModel>> GetStudentsByClassIdForVT([FromBody] DataRequest dataRequest)
        {
            ListResponse<StudentAttendanceModel> response = new ListResponse<StudentAttendanceModel>();

            try
            {
                IList<StudentAttendanceModel> studentModels = await Task.Run(() =>
                {
                    return this.commonManager.GetStudentsByClassIdForVT(Guid.Parse(dataRequest.DataId), Guid.Parse(dataRequest.DataId1), Guid.Parse(dataRequest.DataId2));
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
        /// Get list of school by VC
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetSchoolsByVCId)]
        public async Task<ListResponse<DropdownModel<string>>> GetSchoolsByVCId([FromBody] DataRequest dataRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> schoolModels = await Task.Run(() =>
                {
                    return this.commonManager.GetSchoolsByVCId(dataRequest.DataId, Guid.Parse(dataRequest.DataId1));
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
        /// Get list of school by VC
        /// </summary>
        /// <param name="courseModuleId"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetSchoolsByDRPId)]
        public async Task<ListResponse<DropdownModel<string>>> GetSchoolsByDRPId([FromBody] DataRequest dataRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> schoolModels = await Task.Run(() =>
                {
                    return this.commonManager.GetSchoolsByDRPId(dataRequest.DataId, Guid.Parse(dataRequest.DataId1));
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
        /// Get list of Course Module >> Units >> Sessions by VT
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vtId"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetCourseModuleUnitSessions)]
        public async Task<ListResponse<ModuleUnitSessionModel>> GetCourseModuleUnitSessions([FromBody] DataRequest dataRequest)
        {
            ListResponse<ModuleUnitSessionModel> response = new ListResponse<ModuleUnitSessionModel>();

            try
            {
                IList<ModuleUnitSessionModel> moduleUnitSessionModels = await Task.Run(() =>
                {
                    return this.commonManager.GetCourseModuleUnitSessions(dataRequest.DataId, Guid.Parse(dataRequest.DataId1));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = moduleUnitSessionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of Master Data
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetCommonMasterData)]
        public async Task<ListResponse<MasterDataModel>> GetCommonMasterData([FromBody] DataRequest dataRequest)
        {
            ListResponse<MasterDataModel> response = new ListResponse<MasterDataModel>();

            try
            {
                IList<MasterDataModel> masterDataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetCommonMasterData(dataRequest.DataId);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = masterDataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of Class Sections by VT
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vtId"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetClassSectionsByVTId)]
        public async Task<ListResponse<ClassSectionModel>> GetClassSectionsByVTId([FromBody] DataRequest dataRequest)
        {
            ListResponse<ClassSectionModel> response = new ListResponse<ClassSectionModel>();

            try
            {
                IList<ClassSectionModel> classSectionModels = await Task.Run(() =>
                {
                    return this.commonManager.GetClassSectionsByVTId(dataRequest.DataId, Guid.Parse(dataRequest.DataId1));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = classSectionModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get list of student by VT
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vtId"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetStudentsByVTId)]
        public async Task<ListResponse<StudentByVTModel>> GetStudentsByVTId([FromBody] DataRequest dataRequest)
        {
            ListResponse<StudentByVTModel> response = new ListResponse<StudentByVTModel>();

            try
            {
                IList<StudentByVTModel> studentModels = await Task.Run(() =>
                {
                    return this.commonManager.GetStudentsByVTId(Guid.Parse(dataRequest.DataId1));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = studentModels.OrderBy(s => s.StudentName).ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous, Route("ErrorTesting")]
        public string ErrorTesting()
        {
            int zeroValue = 0;
            int result = 1;

            result = 12345 / zeroValue;

            return "Lighthouse Service - Error Handling";
        }

        /// <summary>
        /// DownloadReportFile
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous, Route("DownloadReportFile")]
        public FileContentResult DownloadReportFile([FromQuery] string fileId, [FromQuery] string folderName)
        {
            string reportPath = Path.Combine(Constants.RootPath, string.Format("Reports/{0}", folderName), fileId);

            var fileBytes = System.IO.File.ReadAllBytes(reportPath);

            return new FileContentResult(fileBytes, "application/pdf")
            {
                FileDownloadName = fileId
            };
        }

        /// <summary>
        /// Download File from Url
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous, Route("DownloadFile")]
        public FileContentResult DownloadFile([FromQuery] string fileUrl)
        {
            string reportPath = Path.Combine(Constants.RootPath, fileUrl);
            var fileBytes = System.IO.File.ReadAllBytes(reportPath);

            return new FileContentResult(fileBytes, this.GetContentType(fileUrl))
            {
                FileDownloadName = fileUrl.Split("\\").LastOrDefault()
            };
        }

        [HttpGet, AllowAnonymous, Route("DownloadReportFile1")]
        public async Task<IActionResult> DownloadReportFile1([FromQuery] string fileId)
        {
            string reportPath = Path.Combine(Constants.RootPath, "Reports/VTMonthlyAttendancePDF", fileId);

            var memory = new MemoryStream();
            using (var stream = new FileStream(reportPath, System.IO.FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            string contentType = GetContentType(reportPath);

            return File(memory, contentType, fileId);
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();

            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardData)]
        public async Task<ListResponse<DropdownModel<string>>> GetDashboardData([FromBody] DashboardDataRequest request)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetDashboardData(request);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardCardData)]
        public async Task<SingularResponse<DashboardCardModel>> GetDashboardCardData([FromBody] DashboardDataRequest dashboardRequest)
        {
            SingularResponse<DashboardCardModel> response = new SingularResponse<DashboardCardModel>();

            try
            {
                DashboardCardModel dashboardCardModel = await Task.Run(() =>
                {
                    return this.commonManager.GetDashboardCardData(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = dashboardCardModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Forgot password for user
        /// </summary>
        /// <param name="forgotPasswordRequest"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous, Route("ForgotPassword")]
        public async Task<SingularResponse<string>> ForgotPassword([FromBody] ForgotPasswordRequest forgotPasswordRequest)
        {
            SingularResponse<string> response = new SingularResponse<string>();

            try
            {
                response = await Task.Run(() =>
                {
                    return this.accountManager.ForgotPasswordByUserId(forgotPasswordRequest);
                });
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Reset password for user
        /// </summary>
        /// <param name="resetPasswordRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("ResetPassword")]
        public async Task<SingularResponse<string>> ResetPassword([FromBody] ResetPasswordRequest resetPasswordRequest)
        {
            SingularResponse<string> loginResults = new SingularResponse<string>();

            try
            {
                loginResults = await Task.Run(() =>
                {
                    return this.accountManager.ResetPasswordByUserId(resetPasswordRequest);
                });

                // Authentication successful so generate jwt token
                if (loginResults.Success)
                {
                    loginResults.Result = "Password reset successfully";
                }

                loginResults.Messages.Add(Constants.GetDataMessage);
            }
            catch (Exception ex)
            {
                loginResults.Errors.Add(ex.Message);
                loginResults.Success = false;
            }

            return loginResults;
        }

        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardSchoolChartData)]
        public async Task<ListResponse<DashboardSchoolModel>> GetDashboardSchoolChartData([FromBody] DashboardDataRequest request)
        {
            ListResponse<DashboardSchoolModel> response = new ListResponse<DashboardSchoolModel>();

            try
            {
                IList<DashboardSchoolModel> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetLighthouseDashboards<DashboardSchoolModel>(request);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardGuestLectureChartData)]
        public async Task<ListResponse<DashboardGuestLectureModel>> GetDashboardGuestLectureChartData([FromBody] DashboardDataRequest request)
        {
            ListResponse<DashboardGuestLectureModel> response = new ListResponse<DashboardGuestLectureModel>();

            try
            {
                IList<DashboardGuestLectureModel> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetLighthouseDashboards<DashboardGuestLectureModel>(request);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardCourseMaterialChartData)]
        public async Task<ListResponse<DashboardCourseMaterialModel>> GetDashboardCourseMaterialChartData([FromBody] DashboardDataRequest request)
        {
            ListResponse<DashboardCourseMaterialModel> response = new ListResponse<DashboardCourseMaterialModel>();

            try
            {
                IList<DashboardCourseMaterialModel> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetLighthouseDashboards<DashboardCourseMaterialModel>(request);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardToolsAndEquipmentChartData)]
        public async Task<ListResponse<DashboardToolEquipmentModel>> GetDashboardToolsAndEquipmentChartData([FromBody] DashboardDataRequest request)
        {
            ListResponse<DashboardToolEquipmentModel> response = new ListResponse<DashboardToolEquipmentModel>();

            try
            {
                IList<DashboardToolEquipmentModel> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetLighthouseDashboards<DashboardToolEquipmentModel>(request);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardFieldVisitChartData)]
        public async Task<ListResponse<DashboardFieldVisitModel>> GetDashboardFieldVisitChartData([FromBody] DashboardDataRequest request)
        {
            ListResponse<DashboardFieldVisitModel> response = new ListResponse<DashboardFieldVisitModel>();

            try
            {
                IList<DashboardFieldVisitModel> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetLighthouseDashboards<DashboardFieldVisitModel>(request);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardVTAttendanceChartData)]
        public async Task<ListResponse<DashboardModel>> GetDashboardVTAttendanceChartData([FromBody] DashboardDataRequest request)
        {
            ListResponse<DashboardModel> response = new ListResponse<DashboardModel>();

            try
            {
                IList<DashboardModel> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetDashboardVTAttendanceChartData(request);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardVCAttendanceChartData)]
        public async Task<ListResponse<DashboardModel>> GetDashboardVCAttendanceChartData([FromBody] DashboardDataRequest request)
        {
            ListResponse<DashboardModel> response = new ListResponse<DashboardModel>();

            try
            {
                IList<DashboardModel> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetDashboardVCAttendanceChartData(request);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardStudentAttendanceChartData)]
        public async Task<ListResponse<DashboardStudentAttendanceModel>> GetDashboardStudentAttendanceChartData([FromBody] DashboardDataRequest request)
        {
            ListResponse<DashboardStudentAttendanceModel> response = new ListResponse<DashboardStudentAttendanceModel>();

            try
            {
                IList<DashboardStudentAttendanceModel> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetDashboardStudentAttendanceChartData(request);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardSchoolVisitStatusChartData)]
        public async Task<SingularResponse<DashboardSchoolVisitStatusModel>> GetDashboardSchoolVisitStatusChartData([FromBody] DashboardDataRequest request)
        {
            SingularResponse<DashboardSchoolVisitStatusModel> response = new SingularResponse<DashboardSchoolVisitStatusModel>();

            try
            {
                DashboardSchoolVisitStatusModel dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetDashboardSchoolVisitStatusChartData(request);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = dataModels;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardIssueManagementStatusChartData)]
        public async Task<ListResponse<DashboardIssueManagementModel>> GetDashboardIssueManagementStatusChartData([FromBody] DashboardDataRequest request)
        {
            ListResponse<DashboardIssueManagementModel> response = new ListResponse<DashboardIssueManagementModel>();

            try
            {
                IList<DashboardIssueManagementModel> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetDashboardIssueManagementStatusChartData(request);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardIssueManagementChartData)]
        public async Task<ListResponse<DashboardIssueManagementModel>> GetDashboardIssueManagementChartData([FromBody] DashboardDataRequest request)
        {
            ListResponse<DashboardIssueManagementModel> response = new ListResponse<DashboardIssueManagementModel>();

            try
            {
                IList<DashboardIssueManagementModel> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetDashboardIssueManagementChartData(request);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardVocationalTrainersCardData)]
        public async Task<ListResponse<VocationalTrainersCardModel>> GetDashboardVocationalTrainersCardData([FromBody] DashboardDataRequest request)
        {
            ListResponse<VocationalTrainersCardModel> response = new ListResponse<VocationalTrainersCardModel>();

            try
            {
                IList<VocationalTrainersCardModel> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetLighthouseDashboards<VocationalTrainersCardModel>(request);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardJobRoleUnitsCardData)]
        public async Task<ListResponse<JobRoleUnitCardModel>> GetDashboardJobRoleUnitsCardData([FromBody] DashboardDataRequest request)
        {
            ListResponse<JobRoleUnitCardModel> response = new ListResponse<JobRoleUnitCardModel>();

            try
            {
                IList<JobRoleUnitCardModel> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetDashboardJobRoleUnitsCardData(request);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardClassesCardData)]
        public async Task<ListResponse<ClassCardModel>> GetDashboardClassesCardData([FromBody] DashboardDataRequest request)
        {
            ListResponse<ClassCardModel> response = new ListResponse<ClassCardModel>();

            try
            {
                IList<ClassCardModel> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetLighthouseDashboards<ClassCardModel>(request);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.Masters.GetDashboardStudentsCardData)]
        public async Task<ListResponse<StudentCardModel>> GetDashboardStudentsCardData([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<StudentCardModel> response = new ListResponse<StudentCardModel>();

            try
            {
                IList<StudentCardModel> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetLighthouseDashboards<StudentCardModel>(dashboardRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpPost, Route(ServiceConstants.Masters.GetDashboardSchoolVisitsByMonth)]
        public async Task<ListResponse<DashboardSchoolVisitByMonthModel>> GetDashboardSchoolVisitsByMonth([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<DashboardSchoolVisitByMonthModel> response = new ListResponse<DashboardSchoolVisitByMonthModel>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.commonManager.GetLighthouseDashboards<DashboardSchoolVisitByMonthModel>(dashboardRequest);
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

        [HttpPost, Route(ServiceConstants.Masters.GetDashboardSchoolVisitsByVTP)]
        public async Task<ListResponse<DashboardSchoolVisitByVTPModel>> GetDashboardSchoolVisitsByVTP([FromBody] DashboardDataRequest dashboardRequest)
        {
            ListResponse<DashboardSchoolVisitByVTPModel> response = new ListResponse<DashboardSchoolVisitByVTPModel>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.commonManager.GetLighthouseDashboards<DashboardSchoolVisitByVTPModel>(dashboardRequest);
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
        /// Save Lighthouse Settings
        /// </summary>
        /// <param name="settingModel"></param>
        /// <returns></returns>
        [HttpPost, IgmiteAuthorize, Route(ServiceConstants.SaveLighthouseSettings)]
        public async Task<SingularResponse<SettingModel>> SaveLighthouseSettings([FromBody] SettingModel settingModel)
        {
            SingularResponse<SettingModel> response = new SingularResponse<SettingModel>();

            try
            {
                SettingModel studentModel = await Task.Run(() =>
                {
                    return this.commonManager.SaveLighthouseSettings(settingModel);
                });

                response.Result = studentModel;
                response.Messages.Add(Constants.GetDataMessage);
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpGet, AllowAnonymous, Route("SendTestEmail")]
        public string SendTestEmail([FromQuery] string toEmailId, [FromQuery] int secureSocketId)
        {
            return this.commonManager.SendEmailTo(toEmailId, secureSocketId);
        }

        /// <summary>
        /// Get LH User data by hmId
        /// </summary>
        /// <param name="hmId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTPandVCIdBySchoolId")]
        public async Task<SingularResponse<LhUserModel>> GetVTPandVCIdBySchoolId([FromBody] DataRequest schoolRequest)
        {
            SingularResponse<LhUserModel> response = new SingularResponse<LhUserModel>();

            try
            {
                LhUserModel userModel = await Task.Run(() =>
                {
                    return this.commonManager.GetVTPandVCIdBySchoolId(Guid.Parse(schoolRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = userModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get LH User data by hmId
        /// </summary>
        /// <param name="hmId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTPVCAndSchoolIdByVTId")]
        public async Task<SingularResponse<LhUserModel>> GetVTPVCAndSchoolIdByVTId([FromBody] DataRequest vtRequest)
        {
            SingularResponse<LhUserModel> response = new SingularResponse<LhUserModel>();

            try
            {
                LhUserModel userModel = await Task.Run(() =>
                {
                    return this.commonManager.GetVTPVCAndSchoolIdByVTId(Guid.Parse(vtRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = userModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get LH User data by hmId
        /// </summary>
        /// <param name="hmId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTPVCAndSchoolIdByHMId")]
        public async Task<SingularResponse<LhUserModel>> GetVTPVCAndSchoolIdByHMId([FromBody] DataRequest hmRequest)
        {
            SingularResponse<LhUserModel> response = new SingularResponse<LhUserModel>();

            try
            {
                LhUserModel userModel = await Task.Run(() =>
                {
                    return this.commonManager.GetVTPVCAndSchoolIdByHMId(Guid.Parse(hmRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = userModel;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get VTP by HMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTPByHMId")]
        public async Task<ListResponse<DropdownModel<string>>> GetVTPByHMId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetVTPByHMId(Guid.Parse(hmRequest.DataId), Guid.Parse(hmRequest.DataId1));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get VC by HMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVCByHMId")]
        public async Task<ListResponse<DropdownModel<string>>> GetVCByHMId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetVCByHMId(Guid.Parse(hmRequest.DataId), Guid.Parse(hmRequest.DataId1), Guid.Parse(hmRequest.DataId2));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get VT by HMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTByHMId")]
        public async Task<ListResponse<DropdownModel<string>>> GetVTByHMId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetVTByHMId(Guid.Parse(hmRequest.DataId), Guid.Parse(hmRequest.DataId1), Guid.Parse(hmRequest.DataId2));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get School by HMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSchoolByHMId")]
        public async Task<ListResponse<DropdownModel<string>>> GetSchoolByHMId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetSchoolByHMId(Guid.Parse(hmRequest.DataId), Guid.Parse(hmRequest.DataId1), Guid.Parse(hmRequest.DataId2));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get VT by SchoolIdHMId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="hmId"></param>
        /// <param name="vcId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTBySchoolIdHMId")]
        public async Task<ListResponse<DropdownModel<string>>> GetVTBySchoolIdHMId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetVTBySchoolIdHMId(Guid.Parse(hmRequest.DataId), Guid.Parse(hmRequest.DataId1), Guid.Parse(hmRequest.DataId2), Guid.Parse(hmRequest.DataId3));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get VTP by AcademicYearId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTPByAYId")]
        public async Task<ListResponse<DropdownModel<string>>> GetVTPByAYId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetVTPByAYId(hmRequest.DataId, Guid.Parse(hmRequest.DataId1), Guid.Parse(hmRequest.DataId2));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get VC by AcademicYearId And VTPId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVCByAYAndVTPId")]
        public async Task<ListResponse<DropdownModel<string>>> GetVCByAYAndVTPId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetVCByAYAndVTPId(hmRequest.DataId, Guid.Parse(hmRequest.DataId1), Guid.Parse(hmRequest.DataId2), Guid.Parse(hmRequest.DataId3));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get VT by AcademicYearId And VCId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTByAYAndVCId")]
        public async Task<ListResponse<DropdownModel<string>>> GetVTByAYAndVCId([FromBody] DataRequest dataRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetVTByAYAndVCId(dataRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        ///  Get School by AcademicYearId And VCId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSchoolByAYAndVCId")]
        public async Task<ListResponse<DropdownModel<string>>> GetSchoolByAYAndVCId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetSchoolByAYAndVCId(hmRequest.DataId, Guid.Parse(hmRequest.DataId1), Guid.Parse(hmRequest.DataId2), Guid.Parse(hmRequest.DataId3), Guid.Parse(hmRequest.DataId4));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        ///  Get VT by AcademicYearId And SchoolId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="vcId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTByAYAndSchoolId")]
        public async Task<ListResponse<DropdownModel<string>>> GetVTByAYAndSchoolId([FromBody] DataRequest vtRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetVTByAYAndSchoolId(vtRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        ///  Get VT by AcademicYearId And VTPId And VCId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTByAYAndVTPIdVCId")]
        public async Task<ListResponse<DropdownModel<string>>> GetVTByAYAndVTPIdVCId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetVTByAYAndVTPIdVCId(hmRequest.DataId, Guid.Parse(hmRequest.DataId1), Guid.Parse(hmRequest.DataId2), Guid.Parse(hmRequest.DataId3), Guid.Parse(hmRequest.DataId4));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        ///  Get JobRole by AcademicYearId And VTId And SchoolId
        /// </summary>
        /// <param name="vtId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetJobRoleByVTIdAyIdSchoolId")]
        public async Task<ListResponse<DropdownModel<string>>> GetJobRoleByVTIdAyIdSchoolId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetJobRoleByVTIdAyIdSchoolId(Guid.Parse(hmRequest.DataId), Guid.Parse(hmRequest.DataId1), Guid.Parse(hmRequest.DataId2));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        ///  Get Sector by AcademicYearId And VTId And SchoolId
        /// </summary>
        /// <param name="vtId"></param>
        /// <param name="academicYearId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSectorByVTIdAyIdSchoolId")]
        public async Task<ListResponse<DropdownModel<string>>> GetSectorByVTIdAyIdSchoolId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetSectorByVTIdAyIdSchoolId(Guid.Parse(hmRequest.DataId), Guid.Parse(hmRequest.DataId1), Guid.Parse(hmRequest.DataId2));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        ///  Get Sector by AcademicYearId And VTPId 
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSectorByAyIdVTPId")]
        public async Task<ListResponse<DropdownModel<string>>> GetSectorByAyIdVTPId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetSectorByAyIdVTPId(Guid.Parse(hmRequest.DataId), Guid.Parse(hmRequest.DataId1));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        ///  Get Sector by AcademicYearId And VCId 
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSectorByAyIdVCId")]
        public async Task<ListResponse<DropdownModel<string>>> GetSectorByAyIdVCId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetSectorByAyIdVCId(Guid.Parse(hmRequest.DataId), Guid.Parse(hmRequest.DataId1));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get Student by ClassId And SectionId
        /// </summary>
        /// <param name="studentRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("GetStudentsByClassIdSectionId")]
        public async Task<ListResponse<DropdownModel<string>>> GetStudentsByClassIdSectionId([FromBody] DataRequest studentRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetStudentsByClassIdSectionId(studentRequest);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        ///  Get School by AcademicYearId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSchoolsByAcademicYearId")]
        public async Task<ListResponse<DropdownModel<string>>> GetSchoolsByAcademicYearId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetSchoolsByAcademicYearId(Guid.Parse(hmRequest.DataId));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        ///  Get VT by AcademicYearId And SchoolId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVocationalTrainersByAcademicYearIdAndSchoolId")]
        public async Task<ListResponse<DropdownModel<string>>> GetVocationalTrainersByAcademicYearIdAndSchoolId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetVocationalTrainersByAcademicYearIdAndSchoolId(Guid.Parse(hmRequest.DataId), Guid.Parse(hmRequest.DataId1));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        ///  Get School by RoleId And UserId And AcademicYearId
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="academicYearId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSchoolByAYIdAndRoleId")]
        public async Task<ListResponse<DropdownModel<string>>> GetSchoolByAYIdAndRoleId([FromBody] DataRequest roleRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetSchoolByAYIdAndRoleId(roleRequest.DataId, Guid.Parse(roleRequest.DataId1), Guid.Parse(roleRequest.DataId2));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        ///  Get VC by AcademicYearId And VTPId And SectorId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVCByVTPIdSectorId")]
        public async Task<ListResponse<DropdownModel<string>>> GetVCByVTPIdSectorId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetVCByVTPIdSectorId(Guid.Parse(hmRequest.DataId), Guid.Parse(hmRequest.DataId1), Guid.Parse(hmRequest.DataId2));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get VTP by AcademicYearId And SectorId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTPByAYIdSectorId")]
        public async Task<ListResponse<DropdownModel<string>>> GetVTPByAYIdSectorId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetVTPByAYIdSectorId(Guid.Parse(hmRequest.DataId), Guid.Parse(hmRequest.DataId1));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get School by AcademicYearId And VTPId And VCId And SectorId
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="vcId"></param>
        /// <param name="sectorId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetSchoolByVTPIdVCIdSectorId")]
        public async Task<ListResponse<DropdownModel<string>>> GetSchoolByVTPIdVCIdSectorId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetSchoolByVTPIdVCIdSectorId(Guid.Parse(hmRequest.DataId), Guid.Parse(hmRequest.DataId1), Guid.Parse(hmRequest.DataId2), Guid.Parse(hmRequest.DataId3));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get VT by AcademicYearId And VTPId And VCId 
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtpId"></param>
        /// <param name="vcId"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVTByAYIdAndVTPIdVCId")]
        public async Task<ListResponse<DropdownModel<string>>> GetVTByAYIdAndVTPIdVCId([FromBody] DataRequest hmRequest)
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetVTByAYIdAndVTPIdVCId(Guid.Parse(hmRequest.DataId), Guid.Parse(hmRequest.DataId1), Guid.Parse(hmRequest.DataId2));
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        [HttpGet, AllowAnonymous, Route("AppVersion")]
        public dynamic AppVersion()
        {
            var appVersion = this.commonManager.GetAppVersion(new AppVersionModel { PlatformName = "AndroidVersion", AppName = "LighthouseMH" });
            return appVersion;
        }

        /// <summary>
        /// Get Android Settings
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAndroidSettings")]
        public async Task<ListResponse<DropdownModel<string>>> GetAndroidSettings()
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetAndroidSettings();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Get Lighthouse Settings
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetLighthouseSettings")]
        public async Task<ListResponse<DropdownModel<string>>> GetLighthouseSettings()
        {
            ListResponse<DropdownModel<string>> response = new ListResponse<DropdownModel<string>>();

            try
            {
                IList<DropdownModel<string>> dataModels = await Task.Run(() =>
                {
                    return this.commonManager.GetLighthouseSettings();
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = dataModels.ToList();
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