import { Injectable } from '@angular/core';
import { retry, catchError, tap } from 'rxjs/operators';
import { HttpClient, HttpHandler } from '@angular/common/http';
import * as CryptoJS from 'crypto-js';
import { Storage } from '@ionic/storage';
import { BaseService } from './base.service';
import { ApiService } from './api.service';
import { HelperService } from './helper.service';

@Injectable({
    providedIn: 'root'
})
export class CommonService {
    private secretKey = 'igmite$123';

    constructor(
        public httpHandler: HttpHandler,
        private http: BaseService,
        public httpCli: HttpClient,
    ) {
    }

    getEncryptText(plainText): string {
        const encryptText = CryptoJS.AES.encrypt(plainText, this.secretKey).toString();
        return encryptText;
    }

    getDecryptText(encryptText): string {
        const decryptText = CryptoJS.AES.decrypt(encryptText, this.secretKey).toString(CryptoJS.enc.Utf8);
        return decryptText;
    }

    GetMasterDataByType(formData: any, isSelectOption?: boolean) {
        let selectTitle = (formData.SelectTitle == undefined ? "Select" : "Select " + formData.SelectTitle);

        return this.http
            .HttpPost(this.http.Services.MasterData.GetAll, formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap((response: any) => {
                    //response.Results.unshift({ Id: null, Name: selectTitle, Description: "", SequenceNo: 1 });
                    return response;
                })
            );
    }

    GetSchoolsByVCId(formData: any) {
        return this.http
            .HttpPost(this.http.Services.MasterData.GetSchoolsByVCId, formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap((response: any) => {
                    //response.Results.unshift({ Id: null, Name: "Select School", Description: "", SequenceNo: 1 });
                    return response;
                })
            );
    }

    GetSchoolsByAcademicYearId(academicYearId: string) {
        let requestParams = {
            DataId: academicYearId,
        };

        return this.http
            .HttpPost(this.http.Services.MasterData.GetSchoolsByVCId, requestParams)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap((response: any) => {
                    return response;
                })
            );
    }

    // var plainText = 'Rakesh Gautam';
    // var encryptText = this.commonService.getEncryptText(plainText);
    // var decryptText = this.commonService.getDecryptText(encryptText);
    // console.log(encryptText, decryptText);

    // var encodeText = btoa(plainText);
    // var decodeText = atob(encodeText);
    // console.log(encodeText, decodeText)
}
