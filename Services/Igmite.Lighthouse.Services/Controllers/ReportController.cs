using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.Controllers
{
    /// <summary>
    /// Expose all report WebAPI services
    /// </summary>
    [Route(ServiceConstants.ServiceName), ApiController]
    public class ReportController : BaseController
    {
        private readonly IReportManager reportManager;

        /// <summary>
        /// Initializes the Report controller class.
        /// </summary>
        /// <param name="_reportManager"></param>
        public ReportController(IReportManager _reportManager)
        {
            this.reportManager = _reportManager;
        }

        /// <summary>
        /// Get Guest Lecture Conducted Report with filter criteria}
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetGuestLectureConductedReportsByCriteria)]
        public async Task<ListResponse<GuestLectureConductedReport>> GetGuestLectureConductedReportsByCriteria([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<GuestLectureConductedReport> response = new ListResponse<GuestLectureConductedReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<GuestLectureConductedReport>(searchModel);
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
        /// Get Field Industry Visit Conducted Report with filter criteria}
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetFieldIndustryVisitConductedReportsByCriteria)]
        public async Task<ListResponse<FieldIndustryVisitConductedReport>> GetFieldIndustryVisitConductedReportsByCriteria([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<FieldIndustryVisitConductedReport> response = new ListResponse<FieldIndustryVisitConductedReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<FieldIndustryVisitConductedReport>(searchModel);
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
        /// Get VT Issue Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVTIssueReportsByCriteria)]
        public async Task<ListResponse<VTIssueReport>> GetVTIssueReportsByCriteria([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<VTIssueReport> response = new ListResponse<VTIssueReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<VTIssueReport>(searchModel);
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
        /// Get VC Issue Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVCIssueReportsByCriteria)]
        public async Task<ListResponse<VCIssueReport>> GetVCIssueReportsByCriteria([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<VCIssueReport> response = new ListResponse<VCIssueReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<VCIssueReport>(searchModel);
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
        /// Get VC Issue Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVCReportingAttendanceReportsByCriteria)]
        public async Task<ListResponse<VCReportingAttendanceReport>> GetVCReportingAttendanceReportsByCriteria([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<VCReportingAttendanceReport> response = new ListResponse<VCReportingAttendanceReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<VCReportingAttendanceReport>(searchModel);
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
        /// Get VC School Sector Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVCSchoolSectorReportsByCriteria)]
        public async Task<ListResponse<VCSchoolSectorReport>> GetVCSchoolSectorReportsByCriteria([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<VCSchoolSectorReport> response = new ListResponse<VCSchoolSectorReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<VCSchoolSectorReport>(searchModel);
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
        /// Get VT School Sector Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVTSchoolSectorReportsByCriteria)]
        public async Task<ListResponse<VTSchoolSectorReport>> GetVTSchoolSectorReportsByCriteria([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<VTSchoolSectorReport> response = new ListResponse<VTSchoolSectorReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<VTSchoolSectorReport>(searchModel);
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
        /// Get VC Issue Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetSchoolVTPSectorReportsByCriteria)]
        public async Task<ListResponse<SchoolVTPSectorReport>> GetSchoolVTPSectorReportsByCriteria([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<SchoolVTPSectorReport> response = new ListResponse<SchoolVTPSectorReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<SchoolVTPSectorReport>(searchModel);
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
        /// Get School Information Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetSchoolInformationReport)]
        public async Task<ListResponse<SchoolInfoReport>> GetSchoolInformationReport([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<SchoolInfoReport> response = new ListResponse<SchoolInfoReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<SchoolInfoReport>(searchModel);
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
        /// Get Course Material Status Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetCourseMaterialStatusReport)]
        public async Task<ListResponse<CourseMaterialStatusReport>> GetCourseMaterialStatusReport([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<CourseMaterialStatusReport> response = new ListResponse<CourseMaterialStatusReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<CourseMaterialStatusReport>(searchModel);
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
        /// Get Tools And Equipment Status Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetToolsAndEquipmentStatusReport)]
        public async Task<ListResponse<ToolsAndEquipmentStatusReport>> GetToolsAndEquipmentStatusReport([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<ToolsAndEquipmentStatusReport> response = new ListResponse<ToolsAndEquipmentStatusReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<ToolsAndEquipmentStatusReport>(searchModel);
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
        /// Get Student Enrollment Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetStudentEnrollmentReport)]
        public async Task<ListResponse<StudentEnrollmentReport>> GetStudentEnrollmentReport([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<StudentEnrollmentReport> response = new ListResponse<StudentEnrollmentReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<StudentEnrollmentReport>(searchModel);
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
        /// Get Guest Lecture Status Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetGuestLectureStatusReport)]
        public async Task<ListResponse<GuestLectureStatusReport>> GetGuestLectureStatusReport([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<GuestLectureStatusReport> response = new ListResponse<GuestLectureStatusReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<GuestLectureStatusReport>(searchModel);
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
        /// Get Field And Industry Visit Status Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetFieldAndIndustryVisitStatusReport)]
        public async Task<ListResponse<FieldAndIndustryVisitStatusReport>> GetFieldAndIndustryVisitStatusReport([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<FieldAndIndustryVisitStatusReport> response = new ListResponse<FieldAndIndustryVisitStatusReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<FieldAndIndustryVisitStatusReport>(searchModel);
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
        /// Get VT Reporting Attendance Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVTReportingAttendanceReport)]
        public async Task<ListResponse<VTReportingAttendanceReport>> GetVTReportingAttendanceReportsByCriteria([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<VTReportingAttendanceReport> response = new ListResponse<VTReportingAttendanceReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<VTReportingAttendanceReport>(searchModel);
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
        /// Get Student Attendance Reporting Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetStudentAttendanceReportingReport)]
        public async Task<ListResponse<StudentAttendanceReportingReport>> GetStudentAttendanceReportingReport([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<StudentAttendanceReportingReport> response = new ListResponse<StudentAttendanceReportingReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<StudentAttendanceReportingReport>(searchModel);
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
        /// Get Student Details Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetStudentDetailsReport)]
        public async Task<ListResponse<StudentDetailsReport>> GetStudentDetailsReport([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<StudentDetailsReport> response = new ListResponse<StudentDetailsReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<StudentDetailsReport>(searchModel);
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
        /// Get VC School Visit Summary Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVCSchoolVisitSummaryReport)]
        public async Task<ListResponse<VCSchoolVisitSummaryReport>> GetVCSchoolVisitSummaryReport([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<VCSchoolVisitSummaryReport> response = new ListResponse<VCSchoolVisitSummaryReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<VCSchoolVisitSummaryReport>(searchModel);
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
        /// Get Vocational Trainer Attendance Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVocationalTrainerAttendanceReport)]
        public async Task<ListResponse<VocationalTrainerAttendanceReport>> GetVocationalTrainerAttendanceReport([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<VocationalTrainerAttendanceReport> response = new ListResponse<VocationalTrainerAttendanceReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<VocationalTrainerAttendanceReport>(searchModel);
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
        /// Get VTP Bill Submission Status Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVTPBillSubmissionStatusReport)]
        public async Task<ListResponse<VTPBillSubmissionStatusReport>> GetVTPBillSubmissionStatusReport([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<VTPBillSubmissionStatusReport> response = new ListResponse<VTPBillSubmissionStatusReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<VTPBillSubmissionStatusReport>(searchModel);
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
        /// Download VT Monthly Attendance Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVTMonthlyAttendanceReport)]
        public async Task<SingularResponse<string>> GetVTMonthlyAttendanceReport([FromBody] ReportFilterModel searchModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            string reportPath = string.Empty;
            try
            {
                var monthlyAttendanceReport = await Task.Run(() =>
                {
                    return this.reportManager.GetVTAttendanceHeaderForPDF(searchModel.UserId, searchModel.VTId.Value, searchModel.ReportDate.Value);
                });

                reportPath = this.GenerateVTMonthlyAttendancePDF(monthlyAttendanceReport, searchModel.ReportDate.Value);

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = reportPath;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Download VC Monthly Attendance Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVCMonthlyAttendanceReport)]
        public async Task<SingularResponse<string>> GetVCMonthlyAttendanceReport([FromBody] ReportFilterModel searchModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            string reportPath = string.Empty;
            try
            {
                var monthlyAttendanceReport = await Task.Run(() =>
                {
                    return this.reportManager.GetVCAttendanceHeaderForPDF(searchModel.UserId, searchModel.VCId.Value, searchModel.ReportDate.Value);
                });

                reportPath = this.GenerateVCMonthlyAttendancePDF(monthlyAttendanceReport, searchModel.ReportDate.Value);

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = reportPath;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// Download VC Monthly Attendance Report with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVocationalEducationAssessmentReport)]
        public async Task<SingularResponse<string>> GetVocationalEducationAssessmentReport([FromBody] ReportFilterModel searchModel)
        {
            SingularResponse<string> response = new SingularResponse<string>();
            string reportPath = string.Empty;
            try
            {
                var vocationalEducationAssessmentReport = await Task.Run(() =>
                {
                    return this.reportManager.GetVocationalEducationAssessmentForPDF(searchModel.AcademicYearId.Value, searchModel.VTId.Value, searchModel.SchoolId.Value);
                });

                reportPath = this.GetVocationalEducationAssessmentPDF(vocationalEducationAssessmentReport);

                response.Messages.Add(Constants.GetDataMessage);
                response.Result = reportPath;
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        private string GenerateVTMonthlyAttendancePDF(VTMonthlyAttendanceReport monthlyAttendanceReport, DateTime reportDate)
        {
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            DateTime dTime = Constants.GetCurrentDateTime;

            Document doc = new Document();
            doc.SetMargins(30, 40, 20, 20);
            doc.SetPageSize(iTextSharp.text.PageSize.LETTER.Rotate());

            PdfWriter.GetInstance(doc, workStream).CloseStream = false;
            doc.Open();

            //Create PDF Table with 5 columns
            //PdfPTable tableLayout = new PdfPTable(7);

            PdfPTable headTable = new PdfPTable(1);
            PdfPTable searchDetails = new PdfPTable(4);
            //Create PDF Table

            #region Table 1 - VT Monthly Attendance Header

            PdfPTable table1 = new PdfPTable(4);
            table1.WidthPercentage = 100;
            table1.SpacingBefore = 30;
            table1.SetWidths(new int[] { 45, 50, 20, 30 });

            #region Table 1 - Rows

            #region Row 1 -Published Date

            table1.AddCell(new PdfPCell(new Phrase(string.Format("Published Date: {0}", Constants.GetCurrentDateTime.ToString("dd/MM/yyyy")), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 50,
                PaddingBottom = 5,
                Colspan = 4
            });

            #endregion Row 1 -Published Date

            #region Row 2 - Vocational Trainer Monthly Attendance Report

            table1.AddCell(new PdfPCell(new Phrase(string.Format("Vocational Trainer Monthly Attendance Report of {0}", monthlyAttendanceReport.VTAttendanceHeader.MonthYear), new Font(Font.FontFamily.HELVETICA, 13, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
                Colspan = 4
            });

            #endregion Row 2 - Vocational Trainer Monthly Attendance Report

            #region Row 3 - Name of School & Month

            table1.AddCell(new PdfPCell(new Phrase("Name of School", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VTAttendanceHeader.SchoolName, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("Month", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VTAttendanceHeader.MonthYear, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
                Colspan = 3
            });

            #endregion Row 3 - Name of School & Month

            #region Row 4 - UDISE & Number of Working Days

            table1.AddCell(new PdfPCell(new Phrase("UDISE", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VTAttendanceHeader.UDISE, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("Working Days", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VTAttendanceHeader.WorkingDays.ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
                Colspan = 3
            });

            #endregion Row 4 - UDISE & Number of Working Days

            #region Row 5 - Name of Vocational Trainer(VT) & Sundays

            table1.AddCell(new PdfPCell(new Phrase("Name of Vocational Trainer(VT)", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VTAttendanceHeader.VTName, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("Sundays", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VTAttendanceHeader.Sundays.ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            #endregion Row 5 - Name of Vocational Trainer(VT) & Sundays

            #region Row 6 - Trade / Sector Name & Paid Leaves

            table1.AddCell(new PdfPCell(new Phrase("Trade / Sector Name", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VTAttendanceHeader.SectorName, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("Paid Leaves", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VTAttendanceHeader.PaidLeaves.ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            #endregion Row 6 - Trade / Sector Name & Paid Leaves

            #region Row 7 - VTP Name & Unpaid Leaves

            table1.AddCell(new PdfPCell(new Phrase("VTP Name", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VTAttendanceHeader.VTPName, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("Unpaid Leaves", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VTAttendanceHeader.UnpaidLeaves.ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            #endregion Row 7 - VTP Name & Unpaid Leaves

            #region Row 7 - Vocational Coordinator Name (VC) & Long Term Holidays

            table1.AddCell(new PdfPCell(new Phrase("Vocational Coordinator Name (VC)", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VTAttendanceHeader.VCName, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("Long Term Holidays", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VTAttendanceHeader.LongTermHolidays.ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            #endregion Row 7 - Vocational Coordinator Name (VC) & Long Term Holidays

            #region Row 8 - Contact Number & LH & GH

            table1.AddCell(new PdfPCell(new Phrase("Contact Number", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VTAttendanceHeader.VTMobile, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("LH & GH", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VTAttendanceHeader.LocalGovHolidays.ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            #endregion Row 8 - Contact Number & LH & GH

            #region Row 9 - Date of Joining & Total Paid Days

            table1.AddCell(new PdfPCell(new Phrase("Date of Joining", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VTAttendanceHeader.VTDateOfJoining.ToString("dd/MM/yyyy"), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("Total Paid Days", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VTAttendanceHeader.TotalPaidDays.ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            #endregion Row 9 - Date of Joining & Total Paid Days

            #endregion Table 1 - Rows

            doc.Add(table1);

            #endregion Table 1 - VT Monthly Attendance Header

            #region Table 2 - VT Monthly Attendance Header

            PdfPTable table2 = new PdfPTable(monthlyAttendanceReport.VTAttendanceHeader.TotalDays + 1);
            table2.WidthPercentage = 100;

            float[] relativeWidths = new float[monthlyAttendanceReport.VTAttendanceHeader.TotalDays + 1];

            relativeWidths[0] = 10;

            for (var i = 1; i <= monthlyAttendanceReport.VTAttendanceHeader.TotalDays; i++)
            {
                relativeWidths[i] = 3;
            }

            table2.SetWidths(relativeWidths);

            #region Table 2 Rows

            #region Row 1

            table2.AddCell(new PdfPCell(new Phrase("Days", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            for (int i = 1; i <= monthlyAttendanceReport.VTAttendanceHeader.TotalDays; i++)
            {
                table2.AddCell(new PdfPCell(new Phrase(i.ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    PaddingBottom = 8,
                });
            }

            #endregion Row 1

            #region Row 2

            table2.AddCell(new PdfPCell(new Phrase("Day Type", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            for (int dayIndex = 1; dayIndex <= monthlyAttendanceReport.VTAttendanceHeader.TotalDays; dayIndex++)
            {
                string dayType = string.Empty;
                DateTime reportingMonthDate = Convert.ToDateTime(string.Format("{0}/{1}", reportDate.ToString("yyyy/MM"), dayIndex));

                if (reportingMonthDate.ToString("dddd") == "Sunday")
                {
                    dayType = "S";
                }

                table2.AddCell(new PdfPCell(new Phrase(dayType, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    PaddingBottom = 8,
                });
            }

            #endregion Row 2

            #region Row 3

            table2.AddCell(new PdfPCell(new Phrase("VT Submission", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            for (int dayIndex = 1; dayIndex <= monthlyAttendanceReport.VTAttendanceHeader.TotalDays; dayIndex++)
            {
                var attendanceDetails = monthlyAttendanceReport.VTAttendanceDetails.FirstOrDefault(v => v.ReportingDate.Day == dayIndex);

                //NOTE: ​P - Present / W - Working Day, O - Observation Day, A - Absent / L - On Leave, S - Sunday,
                //      LH - Local Holiday, GH - Government Holiday, NA - Report Not Submitted)
                //Day Type: Indicates whether a given day is working day, holiday or observation day

                string dayType = string.Empty, submissionType = string.Empty;

                if (attendanceDetails != null)
                {
                    switch (attendanceDetails.ReportType)
                    {
                        case "37": //WorkingDay
                            dayType = "W";
                            submissionType = "W";
                            break;

                        case "38": //OnLeave
                            dayType = "L";
                            submissionType = "L";
                            break;

                        case "40": //Holiday
                            dayType = "H";
                            submissionType = "H";
                            break;

                        case "123": //ObservationDay
                            dayType = "O";
                            submissionType = "O";
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    dayType = "NA";
                    submissionType = "NA";
                }

                table2.AddCell(new PdfPCell(new Phrase(submissionType, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    PaddingBottom = 8,
                });
            }

            #endregion Row 3

            #region Row 4

            table2.AddCell(new PdfPCell(new Phrase("Revised Submission", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            for (int i = 1; i <= monthlyAttendanceReport.VTAttendanceHeader.TotalDays; i++)
            {
                table2.AddCell(new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    PaddingBottom = 8,
                });
            }

            #endregion Row 4

            #region Row 5

            table2.AddCell(new PdfPCell(new Phrase("Attendance Exception approval by the HM", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            for (int i = 1; i <= monthlyAttendanceReport.VTAttendanceHeader.TotalDays; i++)
            {
                table2.AddCell(new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    PaddingBottom = 8,
                });
            }

            #endregion Row 5

            #endregion Table 2 Rows

            doc.Add(table2);

            #endregion Table 2 - VT Monthly Attendance Header

            #region Table 3

            PdfPTable table3 = new PdfPTable(1);
            table3.WidthPercentage = 100;
            table3.SpacingBefore = 30;

            #region Table 3 Rows

            #region Row 1

            table3.AddCell(new PdfPCell(new Phrase("(verified by HM/ HMs (Seal & Signature))", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Border = 0,
                PaddingTop = 10,
                PaddingBottom = 5,
                PaddingRight = 50
            });

            #endregion Row 1

            #endregion Table 3 Rows

            doc.Add(table3);

            #endregion Table 3

            #region Table 4

            PdfPTable table4 = new PdfPTable(3);
            table4.WidthPercentage = 100;
            table4.SpacingBefore = 45;

            #region Table 4 Rows

            #region Row 1

            table4.AddCell(new PdfPCell(new Phrase("Signature of the VT", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingBottom = 0,
            });

            table4.AddCell(new PdfPCell(new Phrase("Signature of the VC", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingBottom = 0,
                PaddingLeft = 50
            });

            table4.AddCell(new PdfPCell(new Phrase("Seal & Signature of HM/ HMs", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingBottom = 0,
                PaddingLeft = 60
            });

            #endregion Row 1

            #region Row 2

            table4.AddCell(new PdfPCell(new Phrase("Date:", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingBottom = 5,
            });

            table4.AddCell(new PdfPCell(new Phrase("Date:", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingBottom = 5,
                PaddingLeft = 50
            });

            table4.AddCell(new PdfPCell(new Phrase("Date:", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingBottom = 5,
                PaddingLeft = 60
            });

            #endregion Row 2

            #endregion Table 4 Rows

            doc.Add(table4);

            #endregion Table 4

            #region Table 5

            PdfPTable table5 = new PdfPTable(1);
            table5.WidthPercentage = 100;
            table5.SpacingBefore = 20;

            #region Table 5 Rows

            #region Table 5 Row 1

            table5.AddCell(new PdfPCell(new Phrase("(NOTE: P - Present / W - Working Day, O - Observation Day, A - Absent / L - On Leave, S - Sunday, LH - Local Holiday, GH - Government Holiday, NA - Report Not Submitted)", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                //PaddingBottom = 5,
            });

            #endregion Table 5 Row 1

            #region Table 5 Row 2

            table5.AddCell(new PdfPCell(new Phrase("*Day Type: Indicates whether a given day is working day, holiday or observation day", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                //PaddingBottom = 5,
            });

            #endregion Table 5 Row 2

            #region Table 5 Row 3

            table5.AddCell(new PdfPCell(new Phrase("*VT Submission: Indicates the value submitted by VT in the ‘Daily Reporting Form’ for that date", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                //PaddingBottom = 5,
            });

            #endregion Table 5 Row 3

            #region Table 5 Row 4

            table5.AddCell(new PdfPCell(new Phrase("*Revised Submission: To be used for making any correction to the submitted values, with approval form HM", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                //PaddingBottom = 5,
            });

            #endregion Table 5 Row 4

            #endregion Table 5 Rows

            doc.Add(table5);

            #endregion Table 5

            // Closing the document
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            //file name to be created
            string reportFileName = string.Format("{0}_{1}.pdf", monthlyAttendanceReport.VTAttendanceHeader.VTName, dTime.ToString("yyyyMMddHHmmss"));

            //file will created in this path
            // "Reports\\VTMonthlyAttendancePDF"
            string reportPDFPath = Path.Combine(Constants.RootPath, "Reports/VTMonthlyAttendancePDF", reportFileName);

            //You have to rewind the MemoryStream before copying
            workStream.Seek(0, SeekOrigin.Begin);

            using (FileStream fs = new FileStream(reportPDFPath, FileMode.OpenOrCreate))
            {
                workStream.CopyTo(fs);
                fs.Flush();
            }

            //return File(workStream, "application/pdf", strPDFFileName);
            //return Path.Combine("Reports\\VTMonthlyAttendancePDF", reportFileName);
            return reportFileName;
        }

        private string GenerateVCMonthlyAttendancePDF(VCMonthlyAttendanceReport monthlyAttendanceReport, DateTime reportDate)
        {
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            DateTime dTime = Constants.GetCurrentDateTime;

            Document doc = new Document();
            doc.SetMargins(30, 40, 20, 20);
            doc.SetPageSize(iTextSharp.text.PageSize.LETTER.Rotate());

            PdfWriter.GetInstance(doc, workStream).CloseStream = false;
            doc.Open();

            //Create PDF Table with 5 columns
            //PdfPTable tableLayout = new PdfPTable(7);

            PdfPTable headTable = new PdfPTable(1);
            PdfPTable searchDetails = new PdfPTable(4);
            //Create PDF Table

            #region Table 1 - VC Monthly Attendance Header

            PdfPTable table1 = new PdfPTable(4);
            table1.WidthPercentage = 100;
            table1.SpacingBefore = 30;
            table1.SetWidths(new int[] { 45, 50, 20, 30 });

            #region Table 1 - Rows

            #region Row 1 -Published Date

            table1.AddCell(new PdfPCell(new Phrase(string.Format("Published Date: {0}", Constants.GetCurrentDateTime.ToString("dd/MM/yyyy")), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 50,
                PaddingBottom = 5,
                Colspan = 4
            });

            #endregion Row 1 -Published Date

            #region Row 2 - Vocational Trainer Monthly Attendance Report

            table1.AddCell(new PdfPCell(new Phrase(string.Format("Vocational Coordinator Monthly Attendance Report of {0}", monthlyAttendanceReport.VCAttendanceHeader.MonthYear), new Font(Font.FontFamily.HELVETICA, 13, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
                Colspan = 4
            });

            #endregion Row 2 - Vocational Trainer Monthly Attendance Report

            #region Row 3 - Name of VTP & Month

            table1.AddCell(new PdfPCell(new Phrase("Name of VTP", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VCAttendanceHeader.VTPName, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("Month", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VCAttendanceHeader.MonthYear, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            #endregion Row 3 - Name of VTP & Month

            #region Row 4 - Vocational Coordinator Name (VC) & Working Days

            table1.AddCell(new PdfPCell(new Phrase("Name of Vocational Coordinator(VC)", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VCAttendanceHeader.VCName, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("Working Days", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VCAttendanceHeader.WorkingDays.ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            #endregion Row 4 - Vocational Coordinator Name (VC) & Working Days

            #region Row 5 - Contact Number & Sundays

            table1.AddCell(new PdfPCell(new Phrase("Contact Number", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VCAttendanceHeader.VCMobile, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("Sundays", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VCAttendanceHeader.Sundays.ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            #endregion Row 5 - Contact Number & Sundays

            #region Row 6 - Date of Joining & Paid Leaves

            table1.AddCell(new PdfPCell(new Phrase("Date of Joining", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VCAttendanceHeader.VCDateOfJoining.ToString("dd/MM/yyyy"), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("Paid Leaves", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VCAttendanceHeader.PaidLeaves.ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            #endregion Row 6 - Date of Joining & Paid Leaves

            #region Row 7 - Long Term Holidays & Unpaid Leaves

            table1.AddCell(new PdfPCell(new Phrase("Long Term Holidays", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VCAttendanceHeader.LongTermHolidays.ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("Unpaid Leaves", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VCAttendanceHeader.UnpaidLeaves.ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            #endregion Row 7 - Long Term Holidays & Unpaid Leaves

            #region Row 8 - LH & GH & Total Paid Days

            table1.AddCell(new PdfPCell(new Phrase("LH & GH", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VCAttendanceHeader.LocalGovHolidays.ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("Total Paid Days", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(monthlyAttendanceReport.VCAttendanceHeader.TotalPaidDays.ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            #endregion Row 8 - LH & GH & Total Paid Days

            #endregion Table 1 - Rows

            doc.Add(table1);

            #endregion Table 1 - VC Monthly Attendance Header

            #region Table 2 - VC Monthly Attendance Header

            PdfPTable table2 = new PdfPTable(monthlyAttendanceReport.VCAttendanceHeader.TotalDays + 1);
            table2.WidthPercentage = 100;

            float[] relativeWidths = new float[monthlyAttendanceReport.VCAttendanceHeader.TotalDays + 1];

            relativeWidths[0] = 10;

            for (var i = 1; i <= monthlyAttendanceReport.VCAttendanceHeader.TotalDays; i++)
            {
                relativeWidths[i] = 3;
            }

            table2.SetWidths(relativeWidths);

            #region Table 2 Rows

            #region Row 1

            table2.AddCell(new PdfPCell(new Phrase("Days", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            for (int i = 1; i <= monthlyAttendanceReport.VCAttendanceHeader.TotalDays; i++)
            {
                table2.AddCell(new PdfPCell(new Phrase(i.ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    PaddingBottom = 8,
                });
            }

            #endregion Row 1

            #region Row 2

            table2.AddCell(new PdfPCell(new Phrase("Day Type", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            for (int dayIndex = 1; dayIndex <= monthlyAttendanceReport.VCAttendanceHeader.TotalDays; dayIndex++)
            {
                string dayType = string.Empty;
                DateTime reportingMonthDate = Convert.ToDateTime(string.Format("{0}/{1}", reportDate.ToString("yyyy/MM"), dayIndex));

                if (reportingMonthDate.ToString("dddd") == "Sunday")
                {
                    dayType = "S";
                }

                table2.AddCell(new PdfPCell(new Phrase(dayType, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    PaddingBottom = 8,
                });
            }

            #endregion Row 2

            #region Row 3

            table2.AddCell(new PdfPCell(new Phrase("VC Submission", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            for (int dayIndex = 1; dayIndex <= monthlyAttendanceReport.VCAttendanceHeader.TotalDays; dayIndex++)
            {
                var attendanceDetails = monthlyAttendanceReport.VCAttendanceDetails.FirstOrDefault(v => v.ReportingDate.Day == dayIndex);

                //NOTE: ​P - Present / W - Working Day, O - Observation Day, A - Absent / L - On Leave, S - Sunday,
                //      LH - Local Holiday, GH - Government Holiday, NA - Report Not Submitted)
                //Day Type: Indicates whether a given day is working day, holiday or observation day

                string dayType = string.Empty, submissionType = string.Empty;

                if (attendanceDetails != null)
                {
                    switch (attendanceDetails.ReportType)
                    {
                        case "49": //WorkingDay
                            dayType = "W";
                            submissionType = "W";
                            break;

                        case "47": //OnLeave
                            dayType = "L";
                            submissionType = "L";
                            break;

                        case "48": //Holiday
                            dayType = "H";
                            submissionType = "H";
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    dayType = "NA";
                    submissionType = "NA";
                }

                table2.AddCell(new PdfPCell(new Phrase(submissionType, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    PaddingBottom = 8,
                });
            }

            #endregion Row 3

            #region Row 4

            table2.AddCell(new PdfPCell(new Phrase("Revised Submission", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            for (int i = 1; i <= monthlyAttendanceReport.VCAttendanceHeader.TotalDays; i++)
            {
                table2.AddCell(new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    PaddingBottom = 8,
                });
            }

            #endregion Row 4

            #region Row 5

            //table2.AddCell(new PdfPCell(new Phrase("Attendance Exception approval by the HM", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            //{
            //    HorizontalAlignment = Element.ALIGN_LEFT,
            //    PaddingBottom = 8,
            //});

            //for (int i = 1; i <= monthlyAttendanceReport.VCAttendanceHeader.TotalDays; i++)
            //{
            //    table2.AddCell(new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            //    {
            //        HorizontalAlignment = Element.ALIGN_RIGHT,
            //        PaddingBottom = 8,
            //    });
            //}

            #endregion Row 5

            #endregion Table 2 Rows

            doc.Add(table2);

            #endregion Table 2 - VC Monthly Attendance Header

            #region Table 3

            PdfPTable table3 = new PdfPTable(1);
            table3.WidthPercentage = 100;
            table3.SpacingBefore = 30;

            #region Table 3 Rows

            #region Row 1

            table3.AddCell(new PdfPCell(new Phrase("(verified by Reporting Manager (Seal & Signature))", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Border = 0,
                PaddingTop = 10,
                PaddingBottom = 5,
                PaddingRight = 50
            });

            #endregion Row 1

            #endregion Table 3 Rows

            doc.Add(table3);

            #endregion Table 3

            #region Table 4

            PdfPTable table4 = new PdfPTable(3);
            table4.WidthPercentage = 100;
            table4.SpacingBefore = 45;

            #region Table 4 Rows

            #region Row 1

            table4.AddCell(new PdfPCell(new Phrase("Signature of the VC", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingBottom = 0,
            });

            table4.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingBottom = 0,
                PaddingLeft = 50
            });

            table4.AddCell(new PdfPCell(new Phrase("Seal & Signature of Reporting Manager", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingBottom = 0,
                PaddingLeft = 60
            });

            #endregion Row 1

            #region Row 2

            table4.AddCell(new PdfPCell(new Phrase("Date:", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingBottom = 5,
            });

            table4.AddCell(new PdfPCell(new Phrase(" ", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingBottom = 5,
                PaddingLeft = 50
            });

            table4.AddCell(new PdfPCell(new Phrase("Date:", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingBottom = 5,
                PaddingLeft = 60
            });

            #endregion Row 2

            #endregion Table 4 Rows

            doc.Add(table4);

            #endregion Table 4

            #region Table 5

            PdfPTable table5 = new PdfPTable(1);
            table5.WidthPercentage = 100;
            table5.SpacingBefore = 20;

            #region Table 5 Rows

            #region Table 5 Row 1

            table5.AddCell(new PdfPCell(new Phrase("(NOTE: P - Present / W - Working Day, A - Absent / L - On Leave, S - Sunday, H - Holiday, NA - Report Not Submitted)", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                //PaddingBottom = 5,
            });

            #endregion Table 5 Row 1

            #region Table 5 Row 2

            table5.AddCell(new PdfPCell(new Phrase("*Day Type: Indicates whether a given day is working day, holiday or leave", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                //PaddingBottom = 5,
            });

            #endregion Table 5 Row 2

            #region Table 5 Row 3

            table5.AddCell(new PdfPCell(new Phrase("*VC Submission: Indicates the value submitted by VC in the ‘Daily Reporting Form’ for that date", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                //PaddingBottom = 5,
            });

            #endregion Table 5 Row 3

            #region Table 5 Row 4

            table5.AddCell(new PdfPCell(new Phrase("*Revised Submission: To be used for making any correction to the submitted values, with approval form Reporting Manager", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                //PaddingBottom = 5,
            });

            #endregion Table 5 Row 4

            #endregion Table 5 Rows

            doc.Add(table5);

            #endregion Table 5

            // Closing the document
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            //file name to be created
            string reportFileName = string.Format("{0}_{1}.pdf", monthlyAttendanceReport.VCAttendanceHeader.VCName, dTime.ToString("yyyyMMddHHmmss"));

            string reportPDFPath = Path.Combine(Constants.RootPath, "Reports/VCMonthlyAttendancePDF", reportFileName);

            //You have to rewind the MemoryStream before copying
            workStream.Seek(0, SeekOrigin.Begin);

            using (FileStream fs = new FileStream(reportPDFPath, FileMode.OpenOrCreate))
            {
                workStream.CopyTo(fs);
                fs.Flush();
            }

            return reportFileName;
        }

        private string GetVocationalEducationAssessmentPDF(VocationalEducationAssessmentReport vocationalEducationAssessmentReport)
        {
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            DateTime dTime = Constants.GetCurrentDateTime;

            Document doc = new Document();
            doc.SetMargins(30, 40, 20, 20);
            doc.SetPageSize(iTextSharp.text.PageSize.LETTER.Rotate());

            PdfWriter.GetInstance(doc, workStream).CloseStream = false;
            doc.Open();

            //Create PDF Table with 5 columns
            //PdfPTable tableLayout = new PdfPTable(7);

            PdfPTable headTable = new PdfPTable(1);
            PdfPTable searchDetails = new PdfPTable(4);
            //Create PDF Table

            #region Table 1 - Vocational Education Assessment Header

            PdfPTable table1 = new PdfPTable(4);
            table1.WidthPercentage = 100;
            table1.SpacingBefore = 30;
            table1.SetWidths(new int[] { 45, 50, 20, 30 });

            #region Table 1 - Rows

            #region Row 1 - Vocational Education Assessment Report

            table1.AddCell(new PdfPCell(new Phrase("Vocational Education Assessment Data", new Font(Font.FontFamily.HELVETICA, 13, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
                Colspan = 4
            });

            #endregion Row 1 - Vocational Education Assessment Report

            #region Row 2 - School & VTP Name

            table1.AddCell(new PdfPCell(new Phrase("Name of School", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(vocationalEducationAssessmentReport.VEAHeaderModels.SchoolName, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("VTP Name", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(vocationalEducationAssessmentReport.VEAHeaderModels.VTPName, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            #endregion Row 2 - School & VTP Name

            #region Row 3 - UDISE & Name of the Vocational Trainer

            table1.AddCell(new PdfPCell(new Phrase("UDISE", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(vocationalEducationAssessmentReport.VEAHeaderModels.UDISE, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("Name of Vocational Trainer(VT)", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(vocationalEducationAssessmentReport.VEAHeaderModels.VTName, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            #endregion Row 3 - UDISE & Name of the Vocational Trainer

            #region Row 4 - Head / Co-ordinating person at School Name / Contact Number (VT)

            table1.AddCell(new PdfPCell(new Phrase("Head / Co-ordinating person at School Name", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(vocationalEducationAssessmentReport.VEAHeaderModels.HMName, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("Contact Number (VT)", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(vocationalEducationAssessmentReport.VEAHeaderModels.VTMobile, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            #endregion Row 4 - Head / Co-ordinating person at School Name / Contact Number (VT)

            #region Row 5 - Contact No & Vocational Coordinator Name(VC)

            table1.AddCell(new PdfPCell(new Phrase("Contact No", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(vocationalEducationAssessmentReport.VEAHeaderModels.HMMobile, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("Vocational Coordinator Name(VC)", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(vocationalEducationAssessmentReport.VEAHeaderModels.VCName, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            #endregion Row 5 - Contact No & Vocational Coordinator Name(VC)

            #region Row 6 - Email Id / Contact Number(VC)

            table1.AddCell(new PdfPCell(new Phrase("Email Id", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(vocationalEducationAssessmentReport.VEAHeaderModels.VCEmailId, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase("Contact Number(VC)", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(vocationalEducationAssessmentReport.VEAHeaderModels.VCMobile, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            #endregion Row 6 - Email Id / Contact Number(VC)

            #region Row 7 - School Address

            table1.AddCell(new PdfPCell(new Phrase("School Address", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(vocationalEducationAssessmentReport.VEAHeaderModels.SchoolAddress.ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
                Colspan = 3
            });

            #endregion Row 7 - School Address

            #region Row 7 - Total No of Students

            table1.AddCell(new PdfPCell(new Phrase("Total No of Students", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
            });

            table1.AddCell(new PdfPCell(new Phrase(vocationalEducationAssessmentReport.VEAHeaderModels.TotalNoOfStudents.ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 8,
                Colspan = 3
            });

            #endregion Row 7 - Total No of Students

            #endregion Table 1 - Rows

            doc.Add(table1);

            #endregion Table 1 - Vocational Education Assessment Header

            #region Table 2 - VEA Header

            PdfPTable table2 = new PdfPTable(15);
            table2.WidthPercentage = 100;
            table2.SpacingBefore = 45;

            #region Table 2 Rows

            #region Row 1

            table2.AddCell(new PdfPCell(new Phrase("Sl.No.", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
            });

            table2.AddCell(new PdfPCell(new Phrase("Name of Candidate", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
            });

            table2.AddCell(new PdfPCell(new Phrase(" Class/Standard", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
            });

            table2.AddCell(new PdfPCell(new Phrase("Gender(M/F)", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
            });

            table2.AddCell(new PdfPCell(new Phrase("Date Of Birth", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
            });

            table2.AddCell(new PdfPCell(new Phrase("Student ID 1 AADHAAR number", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
            });

            table2.AddCell(new PdfPCell(new Phrase("Student ID 2 If AADHAAR is not available, Unique ID generated by the Education Department / Education Board / Council", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
            });

            table2.AddCell(new PdfPCell(new Phrase("Primary Contact Number ", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
            });

            table2.AddCell(new PdfPCell(new Phrase("Alternative Contact Number", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
            });

            table2.AddCell(new PdfPCell(new Phrase("Father's Name", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
            });

            table2.AddCell(new PdfPCell(new Phrase("Category(Gen/OBC/SC/ST)", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
            });

            table2.AddCell(new PdfPCell(new Phrase("Sector", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
            });

            table2.AddCell(new PdfPCell(new Phrase("Job Role", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
            });

            table2.AddCell(new PdfPCell(new Phrase("Stream", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
            });

            table2.AddCell(new PdfPCell(new Phrase("Assessment to be conducted on curriculum, pertaining to Level", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
            });

            #endregion Row 1

            #region Row

            for (int scaIndex = 0; scaIndex < vocationalEducationAssessmentReport.VEADetailsModels.Count; scaIndex++)
            {
                table2.AddCell(new PdfPCell(new Phrase((scaIndex + 1).ToString(), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    PaddingBottom = 8,
                });

                table2.AddCell(new PdfPCell(new Phrase(Convert.ToString(vocationalEducationAssessmentReport.VEADetailsModels[scaIndex].StudentName), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 8,
                });

                table2.AddCell(new PdfPCell(new Phrase(Convert.ToString(vocationalEducationAssessmentReport.VEADetailsModels[scaIndex].Class), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 8,
                });

                table2.AddCell(new PdfPCell(new Phrase(Convert.ToString(vocationalEducationAssessmentReport.VEADetailsModels[scaIndex].Gender), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 8,
                });

                string dateOfBirth = (vocationalEducationAssessmentReport.VEADetailsModels[scaIndex].DOB != null) ? vocationalEducationAssessmentReport.VEADetailsModels[scaIndex].DOB.ToString(Constants.DateFormatExportViewOnly) : string.Empty;
                table2.AddCell(new PdfPCell(new Phrase(dateOfBirth, new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 8,
                });

                table2.AddCell(new PdfPCell(new Phrase(Convert.ToString(vocationalEducationAssessmentReport.VEADetailsModels[scaIndex].AadhaarNumber), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    PaddingBottom = 8,
                });

                table2.AddCell(new PdfPCell(new Phrase(Convert.ToString(vocationalEducationAssessmentReport.VEADetailsModels[scaIndex].StudentRollNumber), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    PaddingBottom = 8,
                });

                table2.AddCell(new PdfPCell(new Phrase(Convert.ToString(vocationalEducationAssessmentReport.VEADetailsModels[scaIndex].PrimaryContact), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 8,
                });

                table2.AddCell(new PdfPCell(new Phrase(Convert.ToString(vocationalEducationAssessmentReport.VEADetailsModels[scaIndex].AlternativeContact), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 8,
                });

                table2.AddCell(new PdfPCell(new Phrase(Convert.ToString(vocationalEducationAssessmentReport.VEADetailsModels[scaIndex].FatherName), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 8,
                });

                table2.AddCell(new PdfPCell(new Phrase(Convert.ToString(vocationalEducationAssessmentReport.VEADetailsModels[scaIndex].Category), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 8,
                });

                table2.AddCell(new PdfPCell(new Phrase(Convert.ToString(vocationalEducationAssessmentReport.VEADetailsModels[scaIndex].Sector), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 8,
                });

                table2.AddCell(new PdfPCell(new Phrase(Convert.ToString(vocationalEducationAssessmentReport.VEADetailsModels[scaIndex].JobRole), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 8,
                });

                table2.AddCell(new PdfPCell(new Phrase(Convert.ToString(vocationalEducationAssessmentReport.VEADetailsModels[scaIndex].StreamName), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    PaddingBottom = 8,
                });

                table2.AddCell(new PdfPCell(new Phrase(Convert.ToString(vocationalEducationAssessmentReport.VEADetailsModels[scaIndex].Assesment), new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    PaddingBottom = 8,
                });
            }

            #endregion Row

            #endregion Table 2 Rows

            doc.Add(table2);

            #endregion Table 2 - VEA Header

            #region Table 3

            PdfPTable table3 = new PdfPTable(3);
            table3.WidthPercentage = 100;
            table3.SpacingBefore = 45;

            #region Table 3 Rows

            #region Row 1

            table3.AddCell(new PdfPCell(new Phrase("Signature of the VT", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingBottom = 0,
            });

            table3.AddCell(new PdfPCell(new Phrase("Signature of the VC", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingBottom = 0,
                PaddingLeft = 50
            });

            table3.AddCell(new PdfPCell(new Phrase("Seal & Signature of HM/ HMs", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingBottom = 0,
                PaddingLeft = 60
            });

            #endregion Row 1

            #region Row 2

            table3.AddCell(new PdfPCell(new Phrase("Date:", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingBottom = 5,
            });

            table3.AddCell(new PdfPCell(new Phrase("Date:", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingBottom = 5,
                PaddingLeft = 50
            });

            table3.AddCell(new PdfPCell(new Phrase("Date:", new Font(Font.FontFamily.HELVETICA, 9, 0, new BaseColor(0, 0, 0))))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingBottom = 5,
                PaddingLeft = 60
            });

            #endregion Row 2

            #endregion Table 3 Rows

            doc.Add(table3);

            #endregion Table 3

            // Closing the document
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            //file name to be created
            string reportFileName = string.Format("{0}_{1}.pdf", vocationalEducationAssessmentReport.VEAHeaderModels.UDISE, dTime.ToString("yyyyMMddHHmmss"));

            // VocationalEducationAssessmentPDF - VEAReports
            string reportPDFPath = Path.Combine(Constants.RootPath, "Reports/VEAReports", reportFileName);

            //You have to rewind the MemoryStream before copying
            workStream.Seek(0, SeekOrigin.Begin);

            using (FileStream fs = new FileStream(reportPDFPath, FileMode.OpenOrCreate))
            {
                workStream.CopyTo(fs);
                fs.Flush();
            }

            return reportFileName;
        }

        /// <summary>
        /// Get VT Daily Attendance Tracking with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVTDailyAttendanceTrackingByCriteria)]
        public async Task<ListResponse<VTDailyAttendanceTrackingReport>> GetVTDailyAttendanceTrackingByCriteria([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<VTDailyAttendanceTrackingReport> response = new ListResponse<VTDailyAttendanceTrackingReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<VTDailyAttendanceTrackingReport>(searchModel);
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
        /// Get VC Daily Attendance Tracking with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVCDailyAttendanceTrackingByCriteria)]
        public async Task<ListResponse<VCDailyAttendanceTrackingReport>> GetVCDailyAttendanceTrackingByCriteria([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<VCDailyAttendanceTrackingReport> response = new ListResponse<VCDailyAttendanceTrackingReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<VCDailyAttendanceTrackingReport>(searchModel);
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
        /// Get VT Daily Attendance Tracking with filter criteria
        /// </summary>
        /// <param name="User Id"></param>
        /// <param name="From Date"></param>
        /// <param name="To Date"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVTDailyReportNotSubmittedTrackingByCriteria)]
        public async Task<ListResponse<VTDailyReportingNotSubmittedModel>> GetVTDailyReportNotSubmittedTrackingByCriteria([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<VTDailyReportingNotSubmittedModel> response = new ListResponse<VTDailyReportingNotSubmittedModel>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<VTDailyReportingNotSubmittedModel>(searchModel);
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
        /// Get VT Student Tracking with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVTStudentTrackingByCriteria)]
        public async Task<ListResponse<VTStudentTrackingReport>> GetVTStudentTrackingByCriteria([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<VTStudentTrackingReport> response = new ListResponse<VTStudentTrackingReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<VTStudentTrackingReport>(searchModel);
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
        /// Get VT Course Module Tracking with filter criteria
        /// </summary>
        /// <param name="User Id"></param>
        /// <param name="From Date"></param>
        /// <param name="To Date"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVTCourseModuleDailyTrackingByCriteria)]
        public async Task<ListResponse<VTCourseModuleDailyTrackingReport>> GetVTCourseModuleDailyTrackingByCriteria([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<VTCourseModuleDailyTrackingReport> response = new ListResponse<VTCourseModuleDailyTrackingReport>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<VTCourseModuleDailyTrackingReport>(searchModel);
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
        /// Get VT Daily Monthly Tracking with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVTDailyMonthlyTrackingByCriteria)]
        public async Task<ListResponse<VTDailyMonthlyModel>> GetVTDailyMonthlyTrackingByCriteria([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<VTDailyMonthlyModel> response = new ListResponse<VTDailyMonthlyModel>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<VTDailyMonthlyModel>(searchModel);
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
        /// Get VC Daily Monthly Tracking with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVCDailyMonthlyTrackingByCriteria)]
        public async Task<ListResponse<VCDailyMonthlyModel>> GetVCDailyMonthlyTrackingByCriteria([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<VCDailyMonthlyModel> response = new ListResponse<VCDailyMonthlyModel>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<VCDailyMonthlyModel>(searchModel);
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
        /// Get VTP Monthly Tracking with filter criteria
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetVTPMonthlyTrackingByCriteria)]
        public async Task<ListResponse<VTPMonthlyModel>> GetVTPMonthlyTrackingByCriteria([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<VTPMonthlyModel> response = new ListResponse<VTPMonthlyModel>();

            try
            {
                var reportModels = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<VTPMonthlyModel>(searchModel);
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
        /// List of StudentAssessment with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetStudentAssesmentReport)]
        public async Task<ListResponse<VEAModel>> GetStudentAssesmentReport([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<VEAModel> response = new ListResponse<VEAModel>();

            try
            {
                var studentClassDetailModel = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<VEAModel>(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = studentClassDetailModel.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of Lab Condition with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetLabConditionReport)]
        public async Task<ListResponse<LabConditionModel>> GetLabConditionReport([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<LabConditionModel> response = new ListResponse<LabConditionModel>();

            try
            {
                var studentClassDetailModel = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<LabConditionModel>(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = studentClassDetailModel.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of Tool List with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetToolListReport)]
        public async Task<ListResponse<ToolListModel>> GetToolListReport([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<ToolListModel> response = new ListResponse<ToolListModel>();

            try
            {
                var studentClassDetailModel = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<ToolListModel>(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = studentClassDetailModel.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of Material List with search filter
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetMaterialListReport)]
        public async Task<ListResponse<MaterialListModel>> GetMaterialListReport([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<MaterialListModel> response = new ListResponse<MaterialListModel>();

            try
            {
                var studentClassDetailModel = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<MaterialListModel>(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = studentClassDetailModel.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// List of Pratical Assesment
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, Route(ServiceConstants.Report.GetPraticalAssesmentReport)]
        public async Task<ListResponse<PraticalAssesmentModel>> GetPraticalAssesmentReport([FromBody] ReportFilterModel searchModel)
        {
            ListResponse<PraticalAssesmentModel> response = new ListResponse<PraticalAssesmentModel>();

            try
            {
                var praticalAssesmentModel = await Task.Run(() =>
                {
                    return this.reportManager.GetLighthouseReports<PraticalAssesmentModel>(searchModel);
                });

                response.Messages.Add(Constants.GetDataMessage);
                response.Results = praticalAssesmentModel.ToList();
            }
            catch (Exception ex)
            {
                response.Errors.Add(Logging.ErrorManager.Instance.GetErrorMessages(ex));
                response.Success = false;
            }

            return response;
        }

        // Method to add single cell to the Header
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.YELLOW)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 5,
                BackgroundColor = new iTextSharp.text.BaseColor(128, 0, 0)
            });
        }

        // Method to add single cell to the body
        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 5,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }
    }
}