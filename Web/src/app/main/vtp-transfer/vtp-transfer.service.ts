import { Injectable } from "@angular/core";
import { retry, catchError, tap } from "rxjs/operators";
import { BaseService } from 'app/services/base.service';
import { forkJoin, Observable } from "rxjs";
import { UserModel } from "app/models/user.model";
import { CommonService } from "app/services/common.service";

@Injectable()
export class VTPTransferService {
    constructor(private http: BaseService, private commonService: CommonService) { }


    GetSchoolByVTPIdSectorId(ayId: string,vtpId: string, sectorId: string): Observable<any> {
        var requestParams = {
            DataId: ayId,
            DataId1: vtpId,
            DataId2: sectorId
        };

        return this.http
            .HttpPost(this.http.Services.VocationalTrainingProvider.GetSchoolByVTPIdSectorId, requestParams)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(function (response: any) {
                    return response.Results;
                })
            );
    }

    saveVTPTransfers(formData: any) {
        return this.http
            .HttpPost(this.http.Services.VocationalTrainingProvider.VTPTransfer, formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    getDropdownforClass(userModel: UserModel): Observable<any[]> {
        let vtpRequest = this.commonService.GetVTPByAYId(userModel.RoleCode,userModel.UserTypeId,userModel.AcademicYearId)

        // Observable.forkJoin (RxJS 5) changes to just forkJoin() in RxJS 6
        return forkJoin([vtpRequest]);
    }
}
