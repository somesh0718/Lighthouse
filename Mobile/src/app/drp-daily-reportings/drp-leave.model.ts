
export class DRPLeaveModel {
    LeaveTypeId: string;
    LeaveApprovalStatus: string;
    LeaveApprover: string;
    LeaveReason: string;

    constructor(drpDailyReportingItem?: any) {
        drpDailyReportingItem = drpDailyReportingItem || {};

        this.LeaveTypeId = drpDailyReportingItem.LeaveTypeId || '';
        this.LeaveApprovalStatus = drpDailyReportingItem.LeaveApprovalStatus || '';
        this.LeaveApprover = drpDailyReportingItem.LeaveApprover || '';
        this.LeaveReason = drpDailyReportingItem.LeaveReason || '';
    }
}
