import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { forkJoin, Observable, of } from 'rxjs';
import { retry, catchError, tap } from 'rxjs/operators';
import { AppConstants } from '../app.constants';
import { UserModel } from '../models/user.model';
import { ApiService } from '../services/api.service';
import { BaseService } from '../services/base.service';
import { HelperService } from '../services/helper.service';
import { VTGuestLectureConductedModel } from './vt-guest-lecture-conducted.model';
import { Storage } from '@ionic/storage';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class VTGuestLectureConductedService {
    constructor(
        private http: BaseService,
        private api: ApiService,
        private helperService: HelperService,
        private localStorage: Storage,
        public httpCli: HttpClient,
    ) { }

    getVTGuestLectureConducteds(): Observable<any> {
        return this.http
            .HttpGet(this.http.Services.VTGuestLectureConducted.GetAll)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response.Results;
                })
            );
    }

    getVTGuestLectureConductedById(vtGuestLectureId: string) {
        return this.http
            .HttpGet(this.http.Services.VTGuestLectureConducted.GetById + '?vtGuestLectureId=' + vtGuestLectureId)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap((response: any) => {
                    return response.Results;
                })
            );
    }

    createOrUpdateVTGuestLectureConducted(formData: any) {
        return this.http
            .HttpPost(this.http.Services.VTGuestLectureConducted.CreateOrUpdate, formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    createOrUpdateVTGuestLectureConductedBulk(pageData) {
        return new Promise((resolve, reject) => {
            this.api.selectTableData(pageData.uploadTable).then((list: any) => {
                if (list.length > 0) {
                    const obsList = [];
                    const options = {
                        headers: {
                            Authorization: 'Bearer ' + AppConstants.AuthToken
                        }
                    };
                    for (const iterator of list) {
                        if (typeof iterator.MethodologyIds === 'string' && iterator.MethodologyIds !== null) { iterator.MethodologyIds = JSON.parse(iterator.MethodologyIds); }
                        iterator.UnitSessionsModels = JSON.parse(iterator.UnitSessionsModels);
                        iterator.StudentAttendances = JSON.parse(iterator.StudentAttendances);

                        if (iterator.GLPhotoFile != null) {
                            iterator.GLPhotoFile = JSON.parse(iterator.GLPhotoFile);
                        }

                        if (iterator.GLLecturerPhotoFile != null) {
                            iterator.GLLecturerPhotoFile = JSON.parse(iterator.GLLecturerPhotoFile);
                        }

                        obsList.push(this.http.post(this.http.Services.BaseUrl + this.http.Services.VTGuestLectureConducted.CreateOrUpdate,
                            iterator, options).pipe(
                                catchError(err => of({ isError: true, error: err }))
                            ));
                    }

                    forkJoin(obsList).subscribe((res: any) => {
                        const failedList: any = res.filter((x: any) => x.Success === false);
                        const erroredList: any = res.filter((x: any) => x.isError === true);
                        if (erroredList.length > 0 || failedList.length > 0) {
                            let errorMessage = '';
                            for (let index = 0; index < res.length; index++) {
                                if (res[index].hasOwnProperty('isError')) {
                                    errorMessage = errorMessage + pageData.title + ': ' + res[index].error.message + '<br><br>';
                                    if (res[index].error.status >= 400) {
                                        this.api.deleteSpecificData(pageData.uploadTable, list[index]);
                                    }
                                } else {
                                    if (res[index].Success === false) {
                                        if (res[index].Errors.length > 0)
                                            errorMessage = errorMessage + res[index].Errors.toString() + '<br><br>';

                                        if (res[index].Messages.length > 0)
                                            errorMessage = errorMessage + res[index].Messages.toString() + '<br><br>';
                                    }
                                    this.api.deleteSpecificData(pageData.uploadTable, list[index]);
                                }

                            }

                            if (errorMessage != '')
                                this.http.helperService.showAlert(errorMessage);

                            resolve(true);
                        } else {
                            this.api.deleteTableData(pageData.uploadTable);
                            resolve(true);
                        }

                    }, (err) => {
                        console.log(err);
                        reject(err);
                    });
                } else {
                    resolve(true);
                }
            }, (err) => {
                reject(err);
            });
        });
    }

    deleteVTGuestLectureConductedById(vtGuestLectureId: string) {
        const vtGuestLectureConductedParams = {
            VTGuestLectureId: vtGuestLectureId
        };

        return this.http
            .HttpPost(this.http.Services.VTGuestLectureConducted.Delete, vtGuestLectureConductedParams)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    getStudentList(): Observable<any> {
        return this.http
            .HttpGet(this.http.Services.StudentClass.GetAll)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response.Results;
                })
            );
    }

    getVTClasses(): Observable<any> {
        return this.http
            .HttpGet(this.http.Services.VTClass.GetAll)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response.Results;
                })
            );
    }

    getDropdownForVTGuestLectureConducted(currentUser: UserModel): Observable<any[]> {
        // let reportTypeRequest = this.http.GetMasterDataByType({ DataType: 'DataValues', ParentId: 'VTReportType', SelectTitle: 'Report Type' });
        const glMethodlogyRequest = this.http.GetMasterDataByType({ DataType: 'DataValues', ParentId: 'GLMethodology', SelectTitle: 'GL Methodology' });
        const glConductedByRequest = this.http.GetMasterDataByType({ DataType: 'DataValues', ParentId: 'GLConductedBy', SelectTitle: 'GL Conducted By' });
        const glWorkStatusRequest = this.http.GetMasterDataByType({ DataType: 'DataValues', ParentId: 'GLWorkStatus', SelectTitle: 'GL Work Status' });
        const glTypeRequest = this.http.GetMasterDataByType({ DataType: 'DataValues', ParentId: 'GLType', SelectTitle: 'GL Type' });
        const classRequest = this.http.GetClassesByVTId({ DataId: currentUser.LoginId, DataId1: currentUser.UserTypeId, SelectTitle: 'Class' });
        const moduleRequest = this.http.GetMasterDataByType({ DataType: 'CourseModules', SelectTitle: 'Modules Taught' });

        // Observable.forkJoin (RxJS 5) changes to just forkJoin() in RxJS 6
        return forkJoin([glMethodlogyRequest, glConductedByRequest, glWorkStatusRequest, glTypeRequest, classRequest, moduleRequest]);
    }

    getStudentsByClassId(formData: any): Observable<any> {
        return this.http
            .HttpPost(this.http.Services.MasterData.GetStudentsByClassIdForVT, formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response.Results;
                })
            );
    }


    getGuestLectureModelFromFormGroup(formGroup: FormGroup): VTGuestLectureConductedModel {
        const guestLectureModel = new VTGuestLectureConductedModel();

        guestLectureModel.ClassTaughtId = formGroup.get('ClassTaughtId').value;
        guestLectureModel.SectionIds = formGroup.get('SectionIds').value;
        guestLectureModel.ReportingDate = this.http.getDateTimeFromControl(formGroup.get("ReportingDate").value);
        guestLectureModel.GLType = formGroup.get('GLType').value;
        guestLectureModel.GLTopic = formGroup.get('GLTopic').value;
        guestLectureModel.ClassTime = formGroup.get('ClassTime').value;
        guestLectureModel.MethodologyIds = formGroup.get('MethodologyIds').value;
        guestLectureModel.GLMethodologyDetails = formGroup.get('GLMethodologyDetails').value;
        //guestLectureModel.GLPhotoInClass = formGroup.get('GLPhotoInClass').value;
        guestLectureModel.GLConductedBy = formGroup.get('GLConductedBy').value;
        guestLectureModel.GLPersonDetails = formGroup.get('GLPersonDetails').value;
        guestLectureModel.GLName = formGroup.get('GLName').value;
        guestLectureModel.GLMobile = formGroup.get('GLMobile').value;
        guestLectureModel.GLEmail = formGroup.get('GLEmail').value;
        guestLectureModel.GLQualification = formGroup.get('GLQualification').value;
        guestLectureModel.GLAddress = formGroup.get('GLAddress').value;
        guestLectureModel.GLWorkStatus = formGroup.get('GLWorkStatus').value;
        guestLectureModel.GLCompany = formGroup.get('GLCompany').value;
        guestLectureModel.GLDesignation = formGroup.get('GLDesignation').value;
        guestLectureModel.GLWorkExperience = formGroup.get('GLWorkExperience').value;
        //guestLectureModel.GLPhoto = formGroup.get('GLPhoto').value;

        guestLectureModel.StudentAttendances = formGroup.get('StudentAttendances').value;

        return guestLectureModel;
    }

    getAndLoadVTGuestLectureConducted(obj) {
        // don't have the data yet
        return new Promise((resolve, reject) => {
            // We're using Angular HTTP provider to request the data,
            // then on the response, it'll map the JSON data to a parsed JS object.
            // Next, we process the data and resolve the promise with the new data.
            this.localStorage.get('currentUser').then((cU) => {
                const user = JSON.parse(cU);
                const options = {
                    headers: {
                        Authorization: 'Bearer ' + user.AuthToken
                    }
                };
                const formData = {
                    UserId: user.UserId,
                    UserTypeId: user.UserTypeId,
                    Name: null,
                    CharBy: null,
                    Filter: {},
                    SortOrder: 'asc',
                    PageIndex: 1,
                    PageSize: 10000
                };
                this.httpCli
                    .post(this.http.Services.BaseUrl + this.http.Services.VTGuestLectureConducted.GetAllByCriteria, formData, options)
                    .subscribe(
                        (data: any) => {
                            if (data.Success) {
                                this.api.dropTableByName(obj.getTable).then(
                                    () => {
                                        this.api.createTableByQuery(obj.getTableCreateQuery).then(
                                            () => {
                                                // tslint:disable-next-line: no-string-literal
                                                this.api.insertGetTable(obj.getTable, data['Results']).then(
                                                    () => {
                                                        resolve(true);
                                                    },
                                                    (err) => {
                                                        reject(err);
                                                    }
                                                );
                                            },
                                            (err) => {
                                                reject(err);
                                            }
                                        );
                                    },
                                    (err) => {
                                        reject(err);
                                    }
                                );
                            } else {
                                reject(data.Errors[0]);
                            }
                        },
                        (err) => {
                            console.log(err);
                            reject(err);
                        }
                    );
            });
        });
    }
}
