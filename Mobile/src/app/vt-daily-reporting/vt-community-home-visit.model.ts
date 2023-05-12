

export class VTRCommunityHomeVisitModel {    
    VocationalParentsCount: number;
    OtherParentsCount: number;
    CommunityVisitDetails: string;

    constructor(vtDailyReportingItem?: any) {
        vtDailyReportingItem = vtDailyReportingItem || {};
        
        this.VocationalParentsCount = vtDailyReportingItem.VocationalParentsCount || 0;
        this.OtherParentsCount = vtDailyReportingItem.OtherParentsCount || 0;
        this.CommunityVisitDetails = vtDailyReportingItem.CommunityVisitDetails || '';
    }
}
