

export class VTRAssessorInOtherSchoolForExamModel {
    SchoolName: string;
    Udise: string;
    ClassId: string;
    BoysPresent: number;
    GirlsPresent: number;
    ExamDetails: string;

    constructor(vtDailyReportingItem?: any) {
        vtDailyReportingItem = vtDailyReportingItem || {};

        this.SchoolName = vtDailyReportingItem.SchoolName || '';
        this.Udise = vtDailyReportingItem.Udise || '';
        this.ClassId = vtDailyReportingItem.ClassId || '';
        this.BoysPresent = vtDailyReportingItem.BoysPresent || 0;
        this.GirlsPresent = vtDailyReportingItem.GirlsPresent || 0;
        this.ExamDetails = vtDailyReportingItem.ExamDetails || '';
    }
}
