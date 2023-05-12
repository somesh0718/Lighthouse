import { Injectable, OnInit, Component, ElementRef } from "@angular/core";
import { Subject } from 'rxjs';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { Observable, fromEvent } from 'rxjs';
import { map } from 'rxjs/operators';
import { PageRightModel } from '../models/page.right.model';
import { FileUploadModel } from "../models/file.upload.model";
import { AppConstants } from "../app.constants";
import { UserModel } from "../models/user.model";

@Component({
    providers: [AppConstants],
    templateUrl: './base.page.html',
    styleUrls: ['./base.page.scss']
})
@Injectable()
export class BasePage<T> implements OnInit {
    public Constants: AppConstants;
    public IsLoading: boolean = false;
    //public PageRights: any = { IsView: true, IsEdit: true, IsDelete: true, IsReadOnly: false, ActionType: 'New' };
    public PageRights: PageRightModel
    public UnsubscribeAll: Subject<any>;
    public DateFormatPipe = new DatePipe('en-US');
    public AllowedImageExtensions = ".jpg, .jpeg, .png, .gif, .tif, .bmp";
    public AllowedExcelExtensions = ".xls, .xlsx";

    public get CurrentDate(): Date {
        return new Date();
    }

    public get MinimumAgeDate(): Date {
        let maxDate = new Date(Date.now());

        maxDate.setDate(maxDate.getDate());
        maxDate.setFullYear(maxDate.getFullYear() - 18);

        return maxDate;
    }

    public get MinimumAgeChildrenDate(): Date {
        let maxDate = new Date(Date.now());

        maxDate.setDate(maxDate.getDate());
        maxDate.setFullYear(maxDate.getFullYear() - 12);

        return maxDate;
    }

    private _currentUser: UserModel;
    public get UserModel(): UserModel {
        return this._currentUser;
    }

    public set UserModel(value: UserModel) {
        if (value == null) {
            //this.UserModel = JSON.parse(sessionStorage.getItem("currentUser"));
        }
        this._currentUser = value;
    }

    constructor() {
        this.Constants = new AppConstants();
        this.initPageRights();

        // Set the private defaults
        this.UnsubscribeAll = new Subject();

        var _currentUserText = sessionStorage.getItem("currentUser");
        if (_currentUserText != null) {
            this.UserModel = JSON.parse(_currentUserText);
            AppConstants.UserId = this.UserModel.UserId;
            AppConstants.IsAdmin = this.UserModel.IsAdmin;
            AppConstants.AuthToken = this.UserModel.AuthToken;
        }
    }

    ngOnInit() { }

    getErrorMessage(formGroup: FormGroup, controlId: string): string {
        let errorText = '';
        let isNumericCtrl = false;

        // if (controlId == 'ClassTypeId') {
        //     debugger;
        // }

        let messageText = controlId.replace(/(_|-)/g, ' ').trim()
            .replace(/\w\S*/g, function (str) {
                return str.charAt(0).toUpperCase() + str.substr(1)
            })
            .replace(/([a-z])([A-Z])/g, '$1 $2')
            .replace(/([A-Z])([A-Z][a-z])/g, '$1 $2')
            .replace('Id', '');

        let controlName = controlId.substring(0, 1).toLowerCase() + controlId.substring(1, controlId.length);

        try {
            let fieldCtrl: any = document.getElementsByName(controlName)[0];
            messageText = fieldCtrl.labels[0].innerText.replace('*', '').trim()
        } catch (error) {
        }

        if (formGroup.controls[controlId].hasError('required')) {
            errorText = messageText + ' is required';
        }
        else if (formGroup.controls[controlId].hasError('minlength')) {
            try {
                isNumericCtrl = (document.getElementsByName(controlName)[0]).hasAttribute('digitOnly');
            } catch (error) { }

            errorText = 'Min ' + formGroup.controls[controlId].errors.minlength.requiredLength + (isNumericCtrl ? ' digits' : ' chars') + ' are required';
        }
        else if (formGroup.controls[controlId].hasError('maxlength')) {
            try {
                isNumericCtrl = (document.getElementsByName(controlName)[0]).hasAttribute('digitOnly');
            } catch (error) { }

            errorText = 'Max ' + formGroup.controls[controlId].errors.maxlength.requiredLength + (isNumericCtrl ? ' digits' : ' chars') + ' are required';
        }
        else if (formGroup.controls[controlId].hasError('pattern')) {
            errorText = 'Invalid ' + messageText;
        }
        else if (formGroup.controls[controlId].hasError('matDatepickerParse')) {
            errorText = messageText + ' is required';
        }

        return errorText;
    }

    setValueFromFormGroup(formGroup: FormGroup, formModel: T): T {
        for (let roleProp of Object.keys(formGroup.value)) {
            if (formGroup.value[roleProp] != null) {
                if (formModel[roleProp] != formGroup.value[roleProp]) {
                    formModel[roleProp] = formGroup.value[roleProp];

                    if (roleProp.indexOf('Date') > -1 && formGroup.value[roleProp] != null && formGroup.value[roleProp] != "") {
                        formModel[roleProp] = this.DateFormatPipe.transform(formGroup.value[roleProp], this.Constants.ServerDateFormat);
                    }
                }
            }
            else {
                formModel[roleProp] = null;
            }
        }

        return formModel;
    }

    getHtmlMessage(errors: string[]): string {
        let errorMessages = '<ul style="list-style: none; margin: 0; padding: 0;">';

        for (let errorText of errors) {
            errorMessages += '<li>' + errorText + '</li>';
        }

        return errorMessages + '</ul>';
    }

    getDateValue(dateText: any): any {
        if (dateText != null && dateText != undefined && dateText != "")
            return new Date(dateText);
        else
            return '';
    }

    onChangeDateEnableDisableCheckBox(formControl: FormGroup, evt) {
        if (evt.value == null) {
            formControl.controls['IsActive'].enable();
            formControl.get('IsActive').setValue(true);
        }
        else {
            formControl.controls['IsActive'].disable();
            formControl.get('IsActive').setValue(false);
        }
    }

    clearDateValueInFormControl(formControl: FormGroup, controlName: string, event) {
        event.stopPropagation();
        formControl.get(controlName).setValue(null);
        formControl.controls['IsActive'].enable();
        formControl.get('IsActive').setValue(true);
    }

    getDateFromYMD(year: number, month: number, day: number): Date {
        let maxDate = new Date(Date.now());

        maxDate.setDate(maxDate.getDate());
        maxDate.setFullYear(maxDate.getFullYear() - year);
        maxDate.setMonth(maxDate.getMonth() - month);
        maxDate.setDate(maxDate.getDay() - day);

        return maxDate;
    }

    readBase64FromFile(file: File | Blob): Observable<any> {
        const reader = new FileReader();
        let fileLoadingEnd = fromEvent(reader, 'loadend').pipe(
            map((read: any) => {
                return read.target.result;
            })
        );

        reader.readAsDataURL(file);

        return fileLoadingEnd;
    }

    initPageRights() {
        var userRoleTransactionJson = sessionStorage.getItem('userRoleTransactions');

        if (userRoleTransactionJson != undefined) {
            let userRoleTransactions = JSON.parse(userRoleTransactionJson);

            let userPageRights = userRoleTransactions.find(ut => ut.RouteUrl === window.location.pathname);
            this.PageRights = new PageRightModel(userPageRights);
        }
    }

    getUploadedFileData(event: any, contentType: string) {
        let promise = new Promise((resolve, reject) => {

            if (event.target.files.length > 0) {
                const uploadedFile = event.target.files[0];

                let fileUploadData = new FileUploadModel();
                fileUploadData.ContentType = contentType;
                fileUploadData.FileName = uploadedFile.name;
                fileUploadData.FileType = uploadedFile.type;
                fileUploadData.FileSize = uploadedFile.size;

                this.readBase64FromFile(uploadedFile).subscribe(response => {
                    fileUploadData.Base64Data = response;
                    fileUploadData.UploadFile = uploadedFile;

                    resolve(fileUploadData);
                });
            }
            else {
                resolve(null);
            }
        });

        return promise;
    }
 
    setUploadedFile(fileUploaded: FileUploadModel) {
        if (fileUploaded == null)
            return null;

        return new FileUploadModel({
            UserId: this.UserModel.UserTypeId,
            ContentId: null,
            FilePath: null,
            ContentType: fileUploaded.ContentType,
            FileName: fileUploaded.FileName,
            FileType: fileUploaded.FileType,
            FileSize: fileUploaded.FileSize,
            Base64Data: fileUploaded.Base64Data
        });
    }
    
    checkIfMatchingPasswords(passwordKey: string, passwordConfirmationKey: string) {
        return (group: FormGroup) => {
            let passwordInput = group.controls[passwordKey],
                passwordConfirmationInput = group.controls[passwordConfirmationKey];
            if (passwordInput.value !== passwordConfirmationInput.value) {
                return passwordConfirmationInput.setErrors({ notEquivalent: true })
            }
            else {
                return passwordConfirmationInput.setErrors(null);
            }
        }
    }

    validateAllFormFields(formGroup: FormGroup | FormArray) {
        formGroup.markAsTouched()

        Object.keys(formGroup.controls).forEach(field => {
            const control = formGroup.get(field);
            if (control instanceof FormControl) {
                control.markAsTouched({ onlySelf: true });
            } else if (control instanceof FormGroup) {
                this.validateAllFormFields(control);
            }
        });
    }

    debugHTMLObjects(htmlObject: any) {
        console.log(htmlObject);
    }

    validateDynamicFormArrayFields(group: FormGroup | FormArray) {
        group.markAsTouched()
        for (let i in group.controls) {
            if (group.controls[i] instanceof FormControl) {
                group.controls[i].markAsTouched();
            } else {
                this.validateDynamicFormArrayFields(group.controls[i]);
            }
        }
    }

    setMandatoryFieldControl(formControl: FormGroup, controlName: string, chkControl: any) {
        if (chkControl.detail.checked) {
            formControl.controls[controlName].clearValidators();
        }
        else {
            formControl.controls[controlName].setValidators([Validators.required]);
        }
        formControl.controls[controlName].updateValueAndValidity();
    }

    setDefaultImage(formGroup: FormGroup, controlId: string) {
        formGroup.get(controlId).setValue(this.Constants.DefaultImageUrl);
    }

}
