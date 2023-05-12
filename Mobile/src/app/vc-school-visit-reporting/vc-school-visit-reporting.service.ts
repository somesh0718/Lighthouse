import { Injectable } from '@angular/core';
import { forkJoin, Observable, of } from 'rxjs';
import { retry, catchError, tap } from 'rxjs/operators';
import { AppConstants } from '../app.constants';
import { ApiService } from '../services/api.service';
import { BaseService } from '../services/base.service';
import { HelperService } from '../services/helper.service';
import { Storage } from '@ionic/storage';
import { HttpClient } from '@angular/common/http';
import { VCSchoolVisitReportingModel } from './vc-school-visit-reporting.model';
import { FormGroup } from '@angular/forms';
import { DatePipe } from '@angular/common';

@Injectable()
export class VcSchoolVisitReportingService {
    constructor(
        private http: BaseService,
        private api: ApiService,
        private localStorage: Storage,
    ) {

    }

    getVcSchoolVisitReporting(): Observable<any> {
        return this.http
            .HttpGet(this.http.Services.VcSchoolVisitReporting.GetAll)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response.Results;
                })
            );
    }

    getVcSchoolVisitReportingById(vcSchoolVisitId: string) {
        return this.http
            .HttpGet(this.http.Services.VcSchoolVisitReporting.GetById + '?vcSchoolVisitId=' + vcSchoolVisitId)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap((response: any) => {
                    return response.Results;
                })
            );
    }

    createOrUpdateVcSchoolVisitReporting(formData: any) {
        return this.http
            .HttpPost(this.http.Services.VcSchoolVisitReporting.CreateOrUpdate, formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    createOrUpdateVcSchoolVisitReportingBulk(pageData) {
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
                        obsList.push(this.http.post(this.http.Services.BaseUrl + this.http.Services.VcSchoolVisitReporting.CreateOrUpdate,
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

    deleteVcSchoolVisitReportingById(vcSchoolVisitId: string) {
        const vcSchoolVisitParams = {
            VcSchoolVisitReportingId: vcSchoolVisitId
        };

        return this.http
            .HttpPost(this.http.Services.VcSchoolVisitReporting.Delete, vcSchoolVisitParams)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.http.HandleError),
                tap(response => {
                    return response;
                })
            );
    }

    getAndLoadVcSchoolVisitReporting(obj) {
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
                this.http
                    .post(this.http.Services.BaseUrl + this.http.Services.VcSchoolVisitReporting.GetAllByCriteria, formData, options)
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

    getVCSchoolVisitReportingModelFromFormGroup(formGroup: FormGroup): VCSchoolVisitReportingModel {
        const vcSchoolVisitReportingModel = new VCSchoolVisitReportingModel();
        //vcSchoolVisitReportingModel.VCId = formGroup.get('VCId').value;
        vcSchoolVisitReportingModel.CompanyName = formGroup.get('CompanyName').value;
        vcSchoolVisitReportingModel.VisitDate = formGroup.get('VisitDate').value;

        vcSchoolVisitReportingModel.Month = this.http.DateFormatPipe.transform(vcSchoolVisitReportingModel.VisitDate, 'MMMM');
        vcSchoolVisitReportingModel.SchoolId = formGroup.get('SchoolId').value;
        vcSchoolVisitReportingModel.DistrictCode = formGroup.get('DistrictCode').value;
        vcSchoolVisitReportingModel.SchoolEmailId = formGroup.get('SchoolEmailId').value;
        vcSchoolVisitReportingModel.PrincipalName = formGroup.get('PrincipalName').value;
        vcSchoolVisitReportingModel.PrincipalPhoneNo = formGroup.get('PrincipalPhoneNo').value;
        vcSchoolVisitReportingModel.SectorId = formGroup.get('SectorId').value;

        vcSchoolVisitReportingModel.JobRoleId = formGroup.get('JobRoleId').value;
        vcSchoolVisitReportingModel.VTId = formGroup.get('VTId').value;
        vcSchoolVisitReportingModel.VTPhoneNo = formGroup.get('VTPhoneNo').value;
        vcSchoolVisitReportingModel.Labs = formGroup.get('Labs').value;
        vcSchoolVisitReportingModel.Books = formGroup.get('Books').value;
        vcSchoolVisitReportingModel.NoOfGLConducted = formGroup.get('NoOfGLConducted').value;
        vcSchoolVisitReportingModel.NoOfIndustrialVisits = formGroup.get('NoOfIndustrialVisits').value;

        vcSchoolVisitReportingModel.Class9Boys = formGroup.get('Class9Boys').value;
        vcSchoolVisitReportingModel.Class9Girls = formGroup.get('Class9Girls').value;
        vcSchoolVisitReportingModel.Class10Boys = formGroup.get('Class10Boys').value;
        vcSchoolVisitReportingModel.Class10Girls = formGroup.get('Class10Girls').value;
        vcSchoolVisitReportingModel.Class11Boys = formGroup.get('Class11Boys').value;
        vcSchoolVisitReportingModel.Class11Girls = formGroup.get('Class11Girls').value;
        vcSchoolVisitReportingModel.Class12Boys = formGroup.get('Class12Boys').value;
        vcSchoolVisitReportingModel.Class12Girls = formGroup.get('Class12Girls').value;
        vcSchoolVisitReportingModel.TotalBoys = formGroup.get('TotalBoys').value;
        vcSchoolVisitReportingModel.TotalGirls = formGroup.get('TotalGirls').value;

        return vcSchoolVisitReportingModel;
    }
}
