
export class DRPHolidayModel {
    HolidayTypeId: string;
    HolidayDetails: string;

    constructor(drpDailyReportingItem?: any) {
        drpDailyReportingItem = drpDailyReportingItem || {};

        this.HolidayTypeId = drpDailyReportingItem.HolidayTypeId || '';
        this.HolidayDetails = drpDailyReportingItem.HolidayDetails || '';
    }
}
