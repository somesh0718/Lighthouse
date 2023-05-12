export class VTTransferModel {
    AcademicYearId: string;
    AcademicYearToId: string;
    FromVTPId: string;
    FromVCId: string;
    FromVTId: string;
    FromSchoolId: string;
    IsVTResigned: number;

    ToVTPId: string;
    ToVCId: string;
    ToSchoolId: string;
    ToVTId: string;
    DateOfAllocation: Date;
    ToDateOfRemoval: Date;
    DateOfRemoval: Date;
    DateOfResignation: Date;
    UserId: string;
    Remarks: string;

    constructor(transferItem?: any) {
        transferItem = transferItem || {};
        this.AcademicYearId = transferItem.AcademicYearId || '';
        this.AcademicYearToId = transferItem.AcademicYearToId || '';
        this.FromVTPId = transferItem.FromVTPId || '';
        this.FromVCId = transferItem.FromVCId || '';
        this.FromVTId = transferItem.FromVTId || '';
        this.FromSchoolId = transferItem.FromSchoolId || '';
        this.IsVTResigned = transferItem.IsVTResigned || '';
        this.DateOfRemoval = transferItem.DateOfRemoval || '';
        this.DateOfResignation = transferItem.DateOfResignation || '';

        this.ToVTPId = transferItem.ToVTPId || '';
        this.ToVCId = transferItem.ToVCId || '';
        this.ToSchoolId = transferItem.ToSchoolId || '';
        this.ToVTId = transferItem.ToVTId || '';
        this.ToDateOfRemoval = transferItem.ToDateOfRemoval || '';
        this.DateOfAllocation = transferItem.DateOfAllocation || '';
        this.UserId = transferItem.UserId || '';
        this.Remarks = transferItem.Remarks || '';
    }
}
