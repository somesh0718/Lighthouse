export class StudentAttendanceModel {
    StudentId: string;
    ClassId: string;
    StudentName: string;
    IsPresent: boolean;

    constructor(dropdownModelItem?: any) {
        dropdownModelItem = dropdownModelItem || {};

        this.StudentId = dropdownModelItem.StudentId || "";
        this.ClassId = dropdownModelItem.ClassId || "";
        this.StudentName = dropdownModelItem.StudentName || "";
        this.IsPresent = dropdownModelItem.IsPresent || true;
    }
}
