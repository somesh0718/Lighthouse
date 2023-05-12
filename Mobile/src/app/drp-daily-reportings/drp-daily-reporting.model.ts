import { Guid } from 'guid-typescript';
import { DRPLeaveModel } from './drp-leave.model';
import { DRPHolidayModel } from './drp-holiday.model';
import { DRPIndustryExposureVisitModel } from './drp-industry-exposure-visit.model';
  /* tslint:disable:no-string-literal */

export class DRPDailyReportingModel {
    DRPDailyReportingId: string;
    DRPId: string;
    ReportDate: any;
    ReportType: string;
    WorkingDayTypeIds: string;
    WorkTypeDetails: string;
    SchoolId: string;

    Leave: DRPLeaveModel;
    Holiday: DRPHolidayModel;
    IndustryExposureVisit: DRPIndustryExposureVisitModel;

    Latitude: string;
    Longitude: string;
    GeoLocation: string;

    IsActive: boolean;
    RequestType: any;

    constructor(drpDailyReportingItem?: any) {
        drpDailyReportingItem = drpDailyReportingItem || {};

        this.DRPDailyReportingId = drpDailyReportingItem.DRPDailyReportingId || Guid.create()['value'];
        this.DRPId = drpDailyReportingItem.DRPId || Guid.create()['value'];
        this.ReportDate = drpDailyReportingItem.ReportDate || '';
        this.ReportType = drpDailyReportingItem.ReportType || '';
        this.WorkingDayTypeIds = drpDailyReportingItem.WorkingDayTypeIds || '';
        this.SchoolId = drpDailyReportingItem.SchoolId || '';
        this.WorkTypeDetails = drpDailyReportingItem.WorkTypeDetails || '';

        if (drpDailyReportingItem.IndustryExposureVisit != null) {
            this.IndustryExposureVisit = new DRPIndustryExposureVisitModel();
            drpDailyReportingItem.IndustryExposureVisit = drpDailyReportingItem.IndustryExposureVisit || new DRPIndustryExposureVisitModel();

            this.IndustryExposureVisit.TypeOfIndustryLinkage = drpDailyReportingItem.IndustryExposureVisit.TypeOfIndustryLinkage || '';
            this.IndustryExposureVisit.ContactPersonName = drpDailyReportingItem.IndustryExposureVisit.ContactPersonName || '';
            this.IndustryExposureVisit.ContactPersonMobile = drpDailyReportingItem.IndustryExposureVisit.ContactPersonMobile || '';
            this.IndustryExposureVisit.ContactPersonEmail = drpDailyReportingItem.IndustryExposureVisit.ContactPersonEmail || '';
        }

        if (drpDailyReportingItem.Holiday != null) {
            this.Holiday = new DRPHolidayModel();
            drpDailyReportingItem.Holiday = drpDailyReportingItem.Holiday || new DRPHolidayModel();

            this.Holiday.HolidayTypeId = drpDailyReportingItem.Holiday.HolidayTypeId || '';
            this.Holiday.HolidayDetails = drpDailyReportingItem.Holiday.HolidayDetails || '';
        }

        if (drpDailyReportingItem.Leave != null) {
            this.Leave = new DRPLeaveModel();
            drpDailyReportingItem.Leave = drpDailyReportingItem.Leave || new DRPLeaveModel();

            this.Leave.LeaveTypeId = drpDailyReportingItem.Leave.LeaveTypeId || '';
            this.Leave.LeaveApprovalStatus = drpDailyReportingItem.Leave.LeaveApprovalStatus || '';
            this.Leave.LeaveApprover = drpDailyReportingItem.Leave.LeaveApprover || '';
            this.Leave.LeaveReason = drpDailyReportingItem.Leave.LeaveReason || '';
        }


        this.Latitude = drpDailyReportingItem.Latitude || '';
        this.Longitude = drpDailyReportingItem.Longitude || '';
        this.GeoLocation = drpDailyReportingItem.GeoLocation || '';

        this.IsActive = drpDailyReportingItem.IsActive || true;
        this.RequestType = 0; // New
    }
}

