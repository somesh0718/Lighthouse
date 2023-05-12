import { Guid } from 'guid-typescript';

export class VTIssueReportingModel {
    VTId: string;
    AcademicYearId: string;
    VTIssueReportingId: string;
    IssueReportDate: any;
    MainIssue: string;
    SubIssue: string;
    StudentClass: string;
    Month: string;
    StudentType: string;
    NoOfStudents: any;
    IssueDetails: string;
    IssueStatus: string;

    GeoLocation: string;
    Latitude: string;
    Longitude: string;
    IsActive: boolean;
    RequestType: any;

    constructor(issueItem?: any) {
        issueItem = issueItem || {};

        // tslint:disable: no-string-literal
        this.VTIssueReportingId = issueItem.VTIssueReportingId || Guid.create()['value'];
        this.VTId = issueItem.VTId || Guid.create()['value'];
        this.IssueReportDate = issueItem.IssueReportDate || '';
        this.AcademicYearId = '';
        this.MainIssue = issueItem.MainIssue || '';
        this.SubIssue = issueItem.SubIssue || '';
        this.StudentClass = issueItem.StudentClass || '';
        this.Month = issueItem.Month || '';
        this.StudentType = issueItem.StudentType || '';
        this.NoOfStudents = issueItem.NoOfStudents || '';
        this.IssueDetails = issueItem.IssueDetails || '';
        this.Latitude = issueItem.Latitude || '';
        this.Longitude = issueItem.Longitude || '';
        this.GeoLocation = issueItem.GeoLocation || '';
        this.IsActive = issueItem.IsActive || true;
        this.RequestType = 0; // New
    }
}
