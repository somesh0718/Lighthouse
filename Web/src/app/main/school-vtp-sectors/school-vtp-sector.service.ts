import { Injectable } from "@angular/core";
import { forkJoin, Observable } from "rxjs";
import { retry, catchError, tap } from "rxjs/operators";
import { BaseService } from 'app/services/base.service';

@Injectable()
export class SchoolVTPSectorService {
    constructor(private http: BaseService) { }

    getSchoolVTPSectors(): Observable<any> {
        return this.http
            .HttpGet(this.http.Services.SchoolVTPSector.GetAll)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response.Results;
                })
            );
    }

    GetAllByCriteria(filters: any): Observable<any> {
        return this.http
            .HttpPost(this.http.Services.SchoolVTPSector.GetAllByCriteria, filters)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response.Results;
                })
            );
    }

    getSchoolVTPSectorById(schoolVTPSectorId: string) {
        let requestParams = {
            DataId: schoolVTPSectorId
        };

        return this.http
            .HttpPost(this.http.Services.SchoolVTPSector.GetById, requestParams)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(function (response: any) {
                    return response.Results;
                })
            );
    }

    createOrUpdateSchoolVTPSector(formData: any) {
        return this.http
            .HttpPost(this.http.Services.SchoolVTPSector.CreateOrUpdate, formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    deleteSchoolVTPSectorById(schoolVTPSectorId: string) {
        var schoolVTPSectorParams = {
            DataId: schoolVTPSectorId
        };

        return this.http
            .HttpPost(this.http.Services.SchoolVTPSector.Delete, schoolVTPSectorParams)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    getDropdownForSchoolVTPSector(): Observable<any[]> {
        let academicYearRequest = this.http.GetMasterDataByType({ DataType: 'AcademicYears', SelectTitle: 'Academic Year' });
        let schoolRequest = this.http.GetMasterDataByType({ DataType: 'Schools', SelectTitle: 'School' }, false);
        let vtpRequest = this.http.GetMasterDataByType({ DataType: 'VocationalTrainingProviders', SelectTitle: 'VTP' }, false);
        let sectorRequest = this.http.GetMasterDataByType({ DataType: 'Sectors', SelectTitle: 'Sector' }, false);

        // Observable.forkJoin (RxJS 5) changes to just forkJoin() in RxJS 6
        return forkJoin([academicYearRequest, schoolRequest, vtpRequest, sectorRequest]);
    }
}
