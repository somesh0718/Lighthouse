import { FuseUtils } from '@fuse/utils';

export class VCPraticalModel {
    VTRPraticalId: string;
    AcademicYearId: string;
    VCId: string;
    IsPratical: string;
    SchoolId: string;
    VTId: string;
    SectorId: string;
    JobRoleId: string;
    ClassId: string;
    StudentCount: number;
    VTPresent: string;
    PresentStudentCount: number;
    AssesorName: string;
    AssesorMobileNo: string;
    Remarks: string;

    constructor(vcDailyReportingItem?: any) {
        vcDailyReportingItem = vcDailyReportingItem || {};

        this.VTRPraticalId = vcDailyReportingItem.VTRPraticalId || FuseUtils.NewGuid();
        this.AcademicYearId = vcDailyReportingItem.AcademicYearId || '';
        this.VCId = vcDailyReportingItem.VCId || '';
        this.IsPratical = vcDailyReportingItem.IsPratical || '';
        this.SchoolId = vcDailyReportingItem.SchoolId || '';
        this.VTId = vcDailyReportingItem.VTId || '';
        this.SectorId = vcDailyReportingItem.SectorId || '';
        this.JobRoleId = vcDailyReportingItem.JobRoleId || '';
        this.ClassId = vcDailyReportingItem.ClassId || '';
        this.VTPresent = vcDailyReportingItem.VTPresent || '';
        this.StudentCount = vcDailyReportingItem.StudentCount || '';
        this.PresentStudentCount = vcDailyReportingItem.PresentStudentCount || '';
        this.AssesorName = vcDailyReportingItem.AssesorName || '';
        this.AssesorMobileNo = vcDailyReportingItem.AssesorMobileNo || '';
        this.Remarks = vcDailyReportingItem.Remarks || '';
    }
}
