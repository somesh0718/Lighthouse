import { Guid } from 'guid-typescript';

export class ComplaintRegistrationModel {
    ComplaintRegistrationId: string;
    //CategoryName: string;
    UserType: String;
    UserName: String;
    EmailId : String;
    Subject: String;
    IssueDetails: String;
    //IssueStatus: String;
    ScreenshotFile: any;
    GeoLocation: string;
    Latitude: string;
    Longitude: string;
    IsActive: boolean;
    RequestType: any;

    constructor(complaintRegistrationItem?: any) {
        complaintRegistrationItem = complaintRegistrationItem || {};

        this.ComplaintRegistrationId = complaintRegistrationItem.ComplaintRegistrationId || Guid.create()['value'];
        //this.CategoryName = complaintRegistrationItem.CategoryName || '';
        this.UserType = complaintRegistrationItem.UserType || '';
        this.UserName = complaintRegistrationItem.UserName || '';
        this.EmailId = complaintRegistrationItem.EmailId || '';
        this.Subject = complaintRegistrationItem.Subject || '';
        this.IssueDetails = complaintRegistrationItem.IssueDetails || '';
        //this.IssueStatus = complaintRegistrationItem.IssueStatus || '';
        this.ScreenshotFile = complaintRegistrationItem.ScreenshotFile || '';
        this.Latitude = complaintRegistrationItem.Latitude || '';
        this.Longitude = complaintRegistrationItem.Longitude || '';
        this.GeoLocation = complaintRegistrationItem.GeoLocation || '';
        this.IsActive = complaintRegistrationItem.IsActive || true;
        this.RequestType = 0; // New
    }
    
}
