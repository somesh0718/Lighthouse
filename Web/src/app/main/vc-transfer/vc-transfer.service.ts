import { Injectable } from "@angular/core";
import { retry, catchError, tap } from "rxjs/operators";
import { BaseService } from 'app/services/base.service';
import { Observable } from "rxjs";

@Injectable()
export class VCTransferService {
    constructor(private http: BaseService) { }


    getVCSchoolsByVTPAndVCId(vcSchoolParams: any): Observable<any> {
        return this.http
            .HttpPost(this.http.Services.VocationalCoordinator.GetVCSchoolsByVTPAndVCId, vcSchoolParams)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(function (response: any) {
                    return response.Results;
                })
            );
    }

    saveVCTransfers(formData: any) {
        return this.http
            .HttpPost(this.http.Services.VocationalCoordinator.SaveVCTransfers, formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }
}
