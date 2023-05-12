import { Injectable } from '@angular/core';
import { forkJoin, Observable, of } from 'rxjs';
import { retry, catchError, tap } from 'rxjs/operators';
import { ApiService } from '../services/api.service';
import { BaseService } from '../services/base.service';
import { AppConstants } from '../app.constants';
import { HelperService } from '../services/helper.service';
import { HttpClient } from '@angular/common/http';
import { UserModel } from '../models/user.model';
import { FormGroup } from '@angular/forms';
import { VTFieldIndustryVisitConductedModel } from './vt-field-industry-visit-conducted.model';
import { Storage } from '@ionic/storage';

@Injectable()
export class VTFieldIndustryVisitConductedService {
    constructor(
        private http: BaseService,
        private api: ApiService,
        private helperService: HelperService,
        public httpCli: HttpClient,
        private localStorage: Storage,
    ) { }

    getFieldIndustryVisitModelFromFormGroup(formGroup: FormGroup): VTFieldIndustryVisitConductedModel {
        const fieldIndustryVisitModel = new VTFieldIndustryVisitConductedModel();

        fieldIndustryVisitModel.ClassTaughtId = formGroup.get('ClassTaughtId').value;
        fieldIndustryVisitModel.ReportingDate = this.http.getDateTimeFromControl(formGroup.get("ReportingDate").value);
        fieldIndustryVisitModel.SectionIds = formGroup.get('SectionIds').value;
        fieldIndustryVisitModel.FieldVisitTheme = formGroup.get('FieldVisitTheme').value;
        fieldIndustryVisitModel.FieldVisitActivities = formGroup.get('FieldVisitActivities').value;
        fieldIndustryVisitModel.FVOrganisation = formGroup.get('FVOrganisation').value;
        fieldIndustryVisitModel.FVOrganisationAddress = formGroup.get('FVOrganisationAddress').value;
        fieldIndustryVisitModel.FVDistance = formGroup.get('FVDistance').value;
        //fieldIndustryVisitModel.FVPhoto = formGroup.get('FVPhoto').value;
        fieldIndustryVisitModel.FVContactPersonName = formGroup.get('FVContactPersonName').value;
        fieldIndustryVisitModel.FVContactPersonMobile = formGroup.get('FVContactPersonMobile').value;
        fieldIndustryVisitModel.FVContactPersonEmail = formGroup.get('FVContactPersonEmail').value;
        fieldIndustryVisitModel.FVContactPersonDesignation = formGroup.get('FVContactPersonDesignation').value;
        fieldIndustryVisitModel.FVOrganisationInterestStatus = formGroup.get('FVOrganisationInterestStatus').value;
        fieldIndustryVisitModel.FVOrignisationOJTStatus = formGroup.get('FVOrignisationOJTStatus').value;
        fieldIndustryVisitModel.FeedbackFromOrgnisation = formGroup.get('FeedbackFromOrgnisation').value;
        fieldIndustryVisitModel.Remarks = formGroup.get('Remarks').value;
        fieldIndustryVisitModel.FVStudentSafety = formGroup.get('FVStudentSafety').value;

        fieldIndustryVisitModel.StudentAttendances = formGroup.get('StudentAttendances').value;

        return fieldIndustryVisitModel;
    }

    getDropdownForVTFieldIndustry(currentUser: UserModel): Observable<any[]> {
        const classRequest = this.http.GetClassesByVTId({ DataId: currentUser.LoginId, DataId1: currentUser.UserTypeId, SelectTitle: 'Class' });
        const moduleRequest = this.http.GetMasterDataByType({ DataType: 'CourseModules', SelectTitle: 'Modules Taught' });

        // Observable.forkJoin (RxJS 5) changes to just forkJoin() in RxJS 6
        return forkJoin([classRequest, moduleRequest]);
    }

    getVTFieldIndustryVisitConducteds(): Observable<any> {
        return this.http
            .HttpGet(this.http.Services.VTFieldIndustryVisitConducted.GetAll)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response.Results;
                })
            );
    }


    getAndLoadVTFieldIndustryVisitConducteds(obj) {
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
                    .post(this.http.Services.BaseUrl + this.http.Services.VTFieldIndustryVisitConducted.GetAllByCriteria, formData, options)
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

    getVTFieldIndustryVisitConductedById(vtFieldIndustryVisitConductedId: string) {
        return this.http
            .HttpGet(this.http.Services.VTFieldIndustryVisitConducted.GetById +
                '?vtFieldIndustryVisitConductedId=' + vtFieldIndustryVisitConductedId)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap((response: any) => {
                    return response.Results;
                })
            );
    }

    createOrUpdateVTFieldIndustryVisitConducted(formData: any) {
        return this.http
            .HttpPost(this.http.Services.VTFieldIndustryVisitConducted.CreateOrUpdate, formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }


    createOrUpdateVTFieldIndustryVisitConductedBulk(pageData) {
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
                        iterator.UnitSessionsModels = JSON.parse(iterator.UnitSessionsModels);
                        iterator.StudentAttendances = JSON.parse(iterator.StudentAttendances);

                        if (iterator.FVPictureFile != null && iterator.FVPictureFile != '') {
                            iterator.FVPictureFile = JSON.parse(iterator.FVPictureFile);
                        }

                        obsList.push(this.http.post(this.http.Services.BaseUrl + this.http.Services.VTFieldIndustryVisitConducted.CreateOrUpdate,
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

    deleteVTFieldIndustryVisitConductedById(vtFieldIndustryVisitConductedId: string) {
        const vtFieldIndustryVisitConductedParams = {
            VTFieldIndustryVisitConductedId: vtFieldIndustryVisitConductedId
        };

        return this.http
            .HttpPost(this.http.Services.VTFieldIndustryVisitConducted.Delete, vtFieldIndustryVisitConductedParams)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }
}
