import { Injectable } from '@angular/core';
import { BaseService } from '../services/base.service';

@Injectable()
export class BroadcastMessageService {
    constructor(
        private http: BaseService
    ) {
    }

    getAndLoadBroadcastMessages(obj) {
        // don't have the data yet
        return new Promise((resolve, reject) => {
            // We're using Angular HTTP provider to request the data,
            // then on the response, it'll map the JSON data to a parsed JS object.
            // Next, we process the data and resolve the promise with the new data.
            this.http.localStorage.get('currentUser').then((cU) => {
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
                    RequestFrom: 'Mobile',
                    Filter: {},
                    SortOrder: 'asc',
                    PageIndex: 1,
                    PageSize: 10000
                };

                this.http
                    .post(this.http.Services.BaseUrl + this.http.Services.BroadcastMessage.GetAllByCriteria, formData, options)
                    .subscribe(
                        (data: any) => {
                            if (data.Success) {
                                this.http.api.dropTableByName(obj.getTable).then(
                                    () => {
                                        this.http.api.createTableByQuery(obj.getTableCreateQuery).then(
                                            () => {
                                                // tslint:disable-next-line: no-string-literal
                                                this.http.api.insertGetTable(obj.getTable, data['Results']).then(
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
