import { FuseUtils } from '@fuse/utils';

export class StudentClassDetailModel {
    StudentId: string;
    FatherName: string;
    MotherName: string;
    GuardianName: string;
    DateOfBirth: Date;
    AadhaarNumber: string;
    StudentRollNumber: string;
    SocialCategory: string;
    Religion: string;
    CWSNStatus: string;
    Mobile: string;
    Mobile1: string;
    IsActive: boolean;
    RequestType: any;
    AcademicYearId: string;
    VTPId: string;
    VCId: string;
    VTId: string;
    SchoolId: string;
    ClassId: string;
    SectionId: string;
    AssessmentConducted: string;
    WhatsAppNo: string;
    SectorId: string;
    JobRoleId: string;
    StreamId: string;
    IsStudentVE9And10: string;
    IsSameStudentTrade: string;


    constructor(studentClassDetailItem?: any) {
        studentClassDetailItem = studentClassDetailItem || {};

        this.StudentId = studentClassDetailItem.StudentId || '';
        this.FatherName = studentClassDetailItem.FatherName || '';
        this.MotherName = studentClassDetailItem.MotherName || '';
        this.GuardianName = studentClassDetailItem.GuardianName || '';
        this.DateOfBirth = studentClassDetailItem.DateOfBirth || '';
        this.AadhaarNumber = studentClassDetailItem.AadhaarNumber || null;
        this.StudentRollNumber = studentClassDetailItem.AlternateId || '';
        this.SocialCategory = studentClassDetailItem.SocialCategory || '';
        this.Religion = studentClassDetailItem.Religion || '';
        this.CWSNStatus = studentClassDetailItem.CWSNStatus || '';
        this.Mobile = studentClassDetailItem.Mobile || '';
        this.Mobile1 = studentClassDetailItem.Mobile1 || '';
        this.IsActive = studentClassDetailItem.IsActive || true;
        this.RequestType = 0; // New

        this.AcademicYearId = studentClassDetailItem.AcademicYearId || '';
        this.VTPId = studentClassDetailItem.VTPId || '';
        this.VCId = studentClassDetailItem.VCId || '';
        this.VTId = studentClassDetailItem.VTId || FuseUtils.NewGuid();
        this.SchoolId = studentClassDetailItem.SchoolId || FuseUtils.NewGuid();
        this.ClassId = studentClassDetailItem.ClassId || '';
        this.SectionId = studentClassDetailItem.SectionId || '';

        this.AssessmentConducted = studentClassDetailItem.AssessmentConducted || '';
        this.WhatsAppNo = studentClassDetailItem.WhatsAppNo || '';
        this.SectorId = studentClassDetailItem.SectorId || '';
        this.JobRoleId = studentClassDetailItem.JobRoleId || '';
        this.StreamId = studentClassDetailItem.StreamId || null;        
        this.IsStudentVE9And10 = studentClassDetailItem.IsStudentVE9And10 || null;
        this.IsSameStudentTrade = studentClassDetailItem.IsSameStudentTrade || null;

    }
}
