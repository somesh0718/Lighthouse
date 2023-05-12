import { Injectable } from "@angular/core";
import { forkJoin, Observable } from "rxjs";
import { retry, catchError, tap } from "rxjs/operators";
import { CommonService } from 'app/services/common.service';
import { UserModel } from 'app/models/user.model';


@Injectable()
export class StudentClassDetailService {
    constructor(private http: CommonService) { }

    getStudentClassDetails(): Observable<any> {
        return this.http
            .HttpGet(this.http.Services.StudentClassDetail.GetAll)
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
            .HttpPost(this.http.Services.StudentClassDetail.GetAllByCriteria, filters)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response.Results;
                })
            );
    }

    getStudentClassDetailById(studentId: string) {
        let requestParams = {
            DataId: studentId
        };

        return this.http
            .HttpPost(this.http.Services.StudentClassDetail.GetById, requestParams)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(function (response: any) {
                    return response.Results;
                })
            );
    }

    createOrUpdateStudentClassDetail(formData: any) {
        return this.http
            .HttpPost(this.http.Services.StudentClassDetail.CreateOrUpdate, formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    deleteStudentClassDetailById(studentId: string) {
        var studentClassDetailParams = {
            DataId: studentId
        };

        return this.http
            .HttpPost(this.http.Services.StudentClassDetail.Delete, studentClassDetailParams)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    getDropdownforStudentClassDetails(userModel: UserModel): Observable<any[]> {
        let studentsRequest = this.http.GetStudentsByVTId({ DataId: userModel.LoginId, DataId1: userModel.UserTypeId });
        let socialCategoryRequest = this.http.GetMasterDataByType({ DataType: 'DataValues', ParentId: 'SocialCategory', SelectTitle: 'Social Category' });
        let religionRequest = this.http.GetMasterDataByType({ DataType: 'DataValues', ParentId: 'Religion', SelectTitle: 'Religion' });
        let academicYearRequest = this.http.GetMasterDataByType({ DataType: 'AcademicYearsByVT', RoleId: userModel.RoleCode, ParentId: userModel.UserTypeId, SelectTitle: 'Academic Year' });
        let academicYearAllRequest = this.http.GetMasterDataByType({ DataType: 'AcademicYears', ParentId: userModel.UserTypeId, SelectTitle: 'Academic Year' });
        let sectorRequest = this.http.GetMasterDataByType({ DataType: 'Sectors', SelectTitle: 'Sector' });
        let classRequest = this.http.GetMasterDataByType({ DataType: 'SchoolClasses', SelectTitle: 'Classes' });
        let vtpRequest = this.http.GetVTPByAYId(userModel.RoleCode, userModel.UserTypeId, userModel.AcademicYearId);
        let schoolRequest = this.http.GetMasterDataByType({ DataType: 'SchoolsByVT', ParentId: userModel.UserTypeId, SelectTitle: 'School' }, false);
        let streamRequest = this.http.GetMasterDataByType({ DataType: 'DataValues', ParentId: 'Streams', SelectTitle: 'Streams' });

        // Observable.forkJoin (RxJS 5) changes to just forkJoin() in RxJS 6
        return forkJoin([studentsRequest, socialCategoryRequest, religionRequest, academicYearRequest, academicYearAllRequest, sectorRequest, classRequest, vtpRequest, schoolRequest, streamRequest]);
    }

    getDropdownforStudentClass(userModel: UserModel): Observable<any[]> {
        let academicYearRequest = this.http.GetMasterDataByType({ DataType: 'AcademicYearsByVT', RoleId: userModel.RoleCode, ParentId: userModel.UserTypeId, SelectTitle: 'Academic Year' });
        let academicYearAllRequest = this.http.GetMasterDataByType({ DataType: 'AcademicYears', ParentId: userModel.UserTypeId, SelectTitle: 'Academic Year' });
        let sectorRequest = this.http.GetMasterDataByType({ DataType: 'Sectors', SelectTitle: 'Sector' });
        let genderRequest = this.http.GetMasterDataByType({ DataType: 'DataValues', ParentId: 'StudentGender', SelectTitle: 'Gender' });
        let classRequest = this.http.GetMasterDataByType({ DataType: 'SchoolClasses', SelectTitle: 'Classes' });
        let vtpRequest = this.http.GetVTPByAYId(userModel.RoleCode, userModel.UserTypeId, userModel.AcademicYearId);

        // Observable.forkJoin (RxJS 5) changes to just forkJoin() in RxJS 6
        return forkJoin([academicYearRequest, vtpRequest, sectorRequest, genderRequest, academicYearAllRequest, classRequest]);
    }
}
