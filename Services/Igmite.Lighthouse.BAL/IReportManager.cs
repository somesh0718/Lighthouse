using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;

namespace Igmite.Lighthouse.BAL
{
    public interface IReportManager : IGenericManager<DataTypeModel>
    {
        /// <summary>
        /// Get Lighthouse report data with filter criteria
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        IList<T> GetLighthouseReports<T>(ReportFilterModel searchModel);

        /// <summary>
        /// Get VT Attendance Header For a Vocational Trainner
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vtId"></param>
        /// <param name="reportDate"></param>
        /// <returns></returns>
        VTMonthlyAttendanceReport GetVTAttendanceHeaderForPDF(string userId, Guid vtId, DateTime reportDate);

        /// <summary>
        /// Get VC Attendance Header For a Vocational Trainner
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vcId"></param>
        /// <param name="reportDate"></param>
        /// <returns></returns>
        VCMonthlyAttendanceReport GetVCAttendanceHeaderForPDF(string userId, Guid vcId, DateTime reportDate);

        /// <summary>
        /// Get VocationalEducationAssessment For a Student
        /// </summary>
        /// <param name="academicYearId"></param>
        /// <param name="vtId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        VocationalEducationAssessmentReport GetVocationalEducationAssessmentForPDF(Guid academicYearId, Guid vtId, Guid schoolId);
    }
}