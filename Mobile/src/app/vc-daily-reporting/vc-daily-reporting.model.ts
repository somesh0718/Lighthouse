import { Guid } from 'guid-typescript';
import { VCLeaveModel } from './vc-leave.model';
import { VCHolidayModel } from './vc-holiday.model';
import { VCIndustryExposureVisitModel } from './vc-industry-exposure-visit.model';
import { VCPraticalModel } from './vc-pratical.model';
/* tslint:disable:no-string-literal */

export class VCDailyReportingModel {
    VCDailyReportingId: string;
    VCId: string;
    ReportDate: any;
    ReportType: string;
    WorkingDayTypeIds: string;
    WorkTypeDetails: string;
    SchoolId: string;

    Leave: VCLeaveModel;
    Holiday: VCHolidayModel;
    IndustryExposureVisit: VCIndustryExposureVisitModel;
    Pratical: VCPraticalModel;

    Latitude: string;
    Longitude: string;
    GeoLocation: string;

    IsActive: boolean;
    RequestType: any;

    constructor(vcDailyReportingItem?: any) {
        vcDailyReportingItem = vcDailyReportingItem || {};

        this.VCDailyReportingId = vcDailyReportingItem.VCDailyReportingId || Guid.create()['value'];
        this.VCId = vcDailyReportingItem.VCId || Guid.create()['value'];
        this.ReportDate = vcDailyReportingItem.ReportDate || '';
        this.ReportType = vcDailyReportingItem.ReportType || '';
        this.WorkingDayTypeIds = vcDailyReportingItem.WorkingDayTypeIds || '';
        this.SchoolId = vcDailyReportingItem.SchoolId || '';
        this.WorkTypeDetails = vcDailyReportingItem.WorkTypeDetails || '';

        if (vcDailyReportingItem.IndustryExposureVisit != null) {
            this.IndustryExposureVisit = new VCIndustryExposureVisitModel();
            vcDailyReportingItem.IndustryExposureVisit = vcDailyReportingItem.IndustryExposureVisit || new VCIndustryExposureVisitModel();

            this.IndustryExposureVisit.TypeOfIndustryLinkage = vcDailyReportingItem.IndustryExposureVisit.TypeOfIndustryLinkage || '';
            this.IndustryExposureVisit.ContactPersonName = vcDailyReportingItem.IndustryExposureVisit.ContactPersonName || '';
            this.IndustryExposureVisit.ContactPersonMobile = vcDailyReportingItem.IndustryExposureVisit.ContactPersonMobile || '';
            this.IndustryExposureVisit.ContactPersonEmail = vcDailyReportingItem.IndustryExposureVisit.ContactPersonEmail || '';
        }

        if (vcDailyReportingItem.Pratical != null) {
            this.Pratical = new VCPraticalModel();
            vcDailyReportingItem.Pratical = vcDailyReportingItem.Pratical || new VCPraticalModel();

            this.Pratical.AcademicYearId = vcDailyReportingItem.Pratical.AcademicYearId || '';
            this.Pratical.VCId = vcDailyReportingItem.Pratical.VCId || '';
            this.Pratical.IsPratical = vcDailyReportingItem.Pratical.IsPratical || '';
            this.Pratical.VTId = vcDailyReportingItem.Pratical.VTId || '';
            this.Pratical.SectorId = vcDailyReportingItem.Pratical.SectorId || '';
            this.Pratical.JobRoleId = vcDailyReportingItem.Pratical.JobRoleId || '';
            this.Pratical.ClassId = vcDailyReportingItem.Pratical.ClassId || '';
            this.Pratical.StudentCount = vcDailyReportingItem.Pratical.StudentCount || '';
            this.Pratical.VTPresent = vcDailyReportingItem.Pratical.VTPresent || '';
            this.Pratical.PresentStudentCount = vcDailyReportingItem.Pratical.PresentStudentCount || '';
            this.Pratical.PresentStudentCount = vcDailyReportingItem.Pratical.PresentStudentCount || '';
            this.Pratical.AssesorName = vcDailyReportingItem.Pratical.AssesorName || '';
            this.Pratical.AssesorMobileNo = vcDailyReportingItem.Pratical.AssesorMobileNo || '';
            this.Pratical.Remarks = vcDailyReportingItem.Pratical.Remarks || '';
        }

        if (vcDailyReportingItem.Holiday != null) {
            this.Holiday = new VCHolidayModel();
            vcDailyReportingItem.Holiday = vcDailyReportingItem.Holiday || new VCHolidayModel();

            this.Holiday.HolidayTypeId = vcDailyReportingItem.Holiday.HolidayTypeId || '';
            this.Holiday.HolidayDetails = vcDailyReportingItem.Holiday.HolidayDetails || '';
        }

        if (vcDailyReportingItem.Leave != null) {
            this.Leave = new VCLeaveModel();
            vcDailyReportingItem.Leave = vcDailyReportingItem.Leave || new VCLeaveModel();

            this.Leave.LeaveTypeId = vcDailyReportingItem.Leave.LeaveTypeId || '';
            this.Leave.LeaveApprovalStatus = vcDailyReportingItem.Leave.LeaveApprovalStatus || '';
            this.Leave.LeaveApprover = vcDailyReportingItem.Leave.LeaveApprover || '';
            this.Leave.LeaveReason = vcDailyReportingItem.Leave.LeaveReason || '';
        }


        this.Latitude = vcDailyReportingItem.Latitude || '';
        this.Longitude = vcDailyReportingItem.Longitude || '';
        this.GeoLocation = vcDailyReportingItem.GeoLocation || '';

        this.IsActive = vcDailyReportingItem.IsActive || true;
        this.RequestType = 0; // New
    }
}

