

export class VTRLeaveModel {
    LeaveTypeId: string;
    LeaveApprovalStatus: string;
    LeaveApprover: string;
    LeaveReason: string;

    constructor(vtDailyReportingItem?: any) {
        vtDailyReportingItem = vtDailyReportingItem || {};

        this.LeaveTypeId = vtDailyReportingItem.LeaveTypeId || '';
        this.LeaveApprovalStatus = vtDailyReportingItem.LeaveApprovalStatus || '';
        this.LeaveApprover = vtDailyReportingItem.LeaveApprover || '';
        this.LeaveReason = vtDailyReportingItem.LeaveReason || '';
    }
}
