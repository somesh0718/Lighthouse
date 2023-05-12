

export class VTRParentTeachersMeetingModel {
    VocationalParentsCount: number;
    OtherParentsCount: number;
    PTADetails: string;

    constructor(vtDailyReportingItem?: any) {
        vtDailyReportingItem = vtDailyReportingItem || {};

        this.VocationalParentsCount = vtDailyReportingItem.VocationalParentsCount || 0;
        this.OtherParentsCount = vtDailyReportingItem.OtherParentsCount || 0;
        this.PTADetails = vtDailyReportingItem.PTADetails || '';
    }
}
