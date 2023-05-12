
export class VCLeaveModel {
    LeaveTypeId: string;
    LeaveApprovalStatus: string;
    LeaveApprover: string;
    LeaveReason: string;

    constructor(vcDailyReportingItem?: any) {
        vcDailyReportingItem = vcDailyReportingItem || {};

        this.LeaveTypeId = vcDailyReportingItem.LeaveTypeId || '';
        this.LeaveApprovalStatus = vcDailyReportingItem.LeaveApprovalStatus || '';
        this.LeaveApprover = vcDailyReportingItem.LeaveApprover || '';
        this.LeaveReason = vcDailyReportingItem.LeaveReason || '';
    }
}
