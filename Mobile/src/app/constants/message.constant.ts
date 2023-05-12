import { Injectable } from "@angular/core";

@Injectable()
export class MessageConstants {
    public RecordSavedMessage: string = "Record saved successfully";
    public DeleteConfirmationMessage: string = "Are you sure to delete this record?";
    public RecordDeletedMessage: string = "Record deleted successfully";    
    public NoDataFoundMessage: string = "No data found...!!";
    public DefaultImageText: string = "Use default Image";
}
