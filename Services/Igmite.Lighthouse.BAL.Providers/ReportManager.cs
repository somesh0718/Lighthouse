using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Igmite.Lighthouse.BAL.Providers
{
    /// <summary>
    /// Manager of the Report entity
    /// </summary>
    public class ReportManager : GenericManager<DataTypeModel>, IReportManager
    {
        private readonly IReportRepository reportRepository;

        /// <summary>
        /// Initializes the report manager.
        /// </summary>
        /// <param name="reportRepository"></param>
        public ReportManager(IReportRepository _reportRepository)
        {
            this.reportRepository = _reportRepository;
        }

        /// <summary>
        /// Get Lighthouse report data with filter criteria
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IList<T> GetLighthouseReports<T>(ReportFilterModel searchModel)
        {
            return this.reportRepository.GetLighthouseReports<T>(searchModel);
        }

        /// <summary>
        /// Get VT Attendance Header For a Vocational Trainner
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vtId"></param>
        /// <param name="reportDate"></param>
        /// <returns></returns>
        public VTMonthlyAttendanceReport GetVTAttendanceHeaderForPDF(string userId, Guid vtId, DateTime reportDate)
        {
            var vtMonthlyAttendanceReport = new VTMonthlyAttendanceReport();

            vtMonthlyAttendanceReport.VTAttendanceHeader = this.reportRepository.GetVTAttendanceHeaderForPDF(userId, vtId, reportDate);
            vtMonthlyAttendanceReport.VTAttendanceDetails = this.reportRepository.GetVTAttendanceDetailForPDF(userId, vtId, reportDate);

            //vtMonthlyAttendanceReport.VTAttendanceHeader.WorkingDays = vtMonthlyAttendanceReport.VTAttendanceDetails.Where(v => v.ReportType == "37").Select(d => d.ReportingDate.Date).Distinct().Count();
            //vtMonthlyAttendanceReport.VTAttendanceHeader.AbsentDays = vtMonthlyAttendanceReport.VTAttendanceDetails.Where(v => v.ReportType == "38").Select(d => d.ReportingDate.Date).Distinct().Count();

            return vtMonthlyAttendanceReport;
        }

        /// <summary>
        /// Get VC Attendance Header For a Vocational Trainner
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vcId"></param>
        /// <param name="reportDate"></param>
        /// <returns></returns>
        public VCMonthlyAttendanceReport GetVCAttendanceHeaderForPDF(string userId, Guid vcId, DateTime reportDate)
        {
            var vcMonthlyAttendanceReport = new VCMonthlyAttendanceReport();

            vcMonthlyAttendanceReport.VCAttendanceHeader = this.reportRepository.GetVCAttendanceHeaderForPDF(userId, vcId, reportDate);
            vcMonthlyAttendanceReport.VCAttendanceDetails = this.reportRepository.GetVCAttendanceDetailForPDF(userId, vcId, reportDate);

            //vcMonthlyAttendanceReport.VCAttendanceHeader.WorkingDays = vcMonthlyAttendanceReport.VCAttendanceDetails.Where(v => v.ReportType == "49").Select(d => d.ReportingDate.Date).Distinct().Count();
            //vcMonthlyAttendanceReport.VCAttendanceHeader.AbsentDays = vcMonthlyAttendanceReport.VCAttendanceDetails.Where(v => v.ReportType == "47").Select(d => d.ReportingDate.Date).Distinct().Count();

            return vcMonthlyAttendanceReport;
        }

        /// <summary>
        /// Get VocationalEducationAssessment For a Student
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public VocationalEducationAssessmentReport GetVocationalEducationAssessmentForPDF(Guid academicYearId, Guid vtId, Guid schoolId)
        {
            return this.reportRepository.GetVocationalEducationAssessmentForPDF( academicYearId, vtId, schoolId);
        }
    }
}