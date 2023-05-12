

export class VTRTeachingNonVocationalSubjectModel {
    OtherClassTakenDetails: string;
    OtherClassTime: number;

    constructor(vtDailyReportingItem?: any) {
        vtDailyReportingItem = vtDailyReportingItem || {};
        
        this.OtherClassTakenDetails = vtDailyReportingItem.OtherClassTakenDetails || '';
        this.OtherClassTime = vtDailyReportingItem.OtherClassTime || 0;
    }
}
