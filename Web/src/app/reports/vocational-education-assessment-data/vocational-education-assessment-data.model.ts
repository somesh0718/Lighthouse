import { FuseUtils } from '@fuse/utils';

export class VocationalEducationAssessmentDataModel {
    VEAHeaderModels: any;
    VEADetailsModels: any;

    constructor() { }
}

export class VEAHeaderModels {
    SrNo: number;
    SchoolName: string;
    UDISE: string;
    HMName: string;
    ContactNo: string;
    EmailId: string;
    SchoolAddress: string;
    TotalNoOfStudents: string;
    VTPName: string;
    VTName: string;
    VTEmailId: string;
    VCName: string;
    VCEmailId: string;
}

export class VEADetailsModels {
    SrNo: number;
    StudentName: string;
    Class: string;
    Gender: string;
    DOB: string;
    AadhaarNumber: string;
    StudentRollNumber: string;
    PrimaryContact: string;
    AlternativeContact: string;
    FatherName: string;
    Category: string;
    Sector: string;
    JobRole: string;
    Assesment: string;
}
