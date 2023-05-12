import { Injectable } from "@angular/core";
import { retry, catchError, tap } from "rxjs/operators";
import { BaseService } from 'app/services/base.service';
import { forkJoin, Observable } from "rxjs";
import { UserModel } from "app/models/user.model";
import { CommonService } from "app/services/common.service";

@Injectable()
export class VTTransferService {
    constructor(private http: BaseService, private commonService: CommonService) { }


    GetSchoolStudentsByVTId(ayId: string, vtId: string): Observable<any> {
        var requestParams = {
            DataId: ayId,
            DataId1: vtId
        };

        return this.http
            .HttpPost(this.http.Services.VocationalTrainer.GetSchoolStudentsByVTId, requestParams)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(function (response: any) {
                    return response.Results;
                })
            );
    }

    saveVTTransfers(formData: any) {
        return this.http
            .HttpPost(this.http.Services.VocationalTrainer.VTTransfer, formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    getDropdownforClass(userModel: UserModel): Observable<any[]> {

        let academicYearAllRequest = this.http.GetMasterDataByType({ DataType: 'AcademicYears', ParentId: userModel.UserTypeId, SelectTitle: 'Academic Year' });

        let vtpRequest = this.commonService.GetVTPByAYId(userModel.RoleCode, userModel.UserTypeId, userModel.AcademicYearId)

        // Observable.forkJoin (RxJS 5) changes to just forkJoin() in RxJS 6
        return forkJoin([academicYearAllRequest, vtpRequest]);
    }
}
