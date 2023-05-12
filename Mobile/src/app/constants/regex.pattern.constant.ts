
import { Injectable } from "@angular/core";

@Injectable()
export class RegexPatternConstants {
    // public OnlyChars: string = "^[a-zA-Z\s]*$";
    public OnlyChars: string = "[A-Z][a-z]*$";
    public AlphaNumeric: string = "^[a-zA-Z0-9]*$";
    public AlphaNumericWithDashDotMinusSpace: string = "^[a-zA-Z0-9_.-\s]*$";
    public CharsWithSpace: string = "^[a-zA-Z ]*$";
    public Number: string = "^[0-9]*$";
   // public Email: string = "^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
    public Email: string = '^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+[.][a-z]+$';
    public QPCode: string = "^([A-Z]{3})([\s/])?([A-Z]{1})([0-9]{4})$";
    public FirstCharsCapital = "^([A-Z][a-z]*)$";  //"^([A-Z][a-z]*((\\s[A-Za-z])?[a-z]*)*)$";
    public CharsWithTitleCase = "([A-Z][a-z]*\\s*)+$";  
    public AlphaNumericWithTitleCase = "([A-Z][a-z]*\\s*)+[0-9]$";  
    public UDISE: string = "^(24[0-9]{9})$";
    public MobileNumber: string = "^((?![0-5])[0-9]{10})$";
}
