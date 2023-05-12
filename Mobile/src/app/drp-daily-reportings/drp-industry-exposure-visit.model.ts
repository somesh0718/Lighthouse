
export class DRPIndustryExposureVisitModel {
    TypeOfIndustryLinkage: string;
    ContactPersonName: string;
    ContactPersonMobile: string;
    ContactPersonEmail: string;

    constructor(drpDailyReportingItem?: any) {
        drpDailyReportingItem = drpDailyReportingItem || {};

        this.TypeOfIndustryLinkage = drpDailyReportingItem.TypeOfIndustryLinkage || '';
        this.ContactPersonName = drpDailyReportingItem.ContactPersonName || '';
        this.ContactPersonMobile = drpDailyReportingItem.ContactPersonMobile || '';
        this.ContactPersonEmail = drpDailyReportingItem.ContactPersonEmail || '';
    }
}
