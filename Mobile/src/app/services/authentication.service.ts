
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { retry, catchError, tap } from 'rxjs/operators';
import { BaseService } from '../services/base.service';
import { UserModel } from '../models/user.model';
import { Storage } from '@ionic/storage';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {
    private userSubject: BehaviorSubject<UserModel>;
    public currentUser: Observable<UserModel>;

    //private userSubject = new BehaviorSubject<UserModel[]>([]);
    //public currentUser = this.userSubject.asObservable();

    constructor(
        private router: Router,
        private localStorage: Storage,
        private http: BaseService
    ) {
        this.localStorage.get('currentUser').then((currentUserJson) => {
            this.userSubject = new BehaviorSubject<UserModel>(JSON.parse(currentUserJson));
            this.currentUser = this.userSubject.asObservable();
        });
    }

    public get authUser(): UserModel {
        return this.userSubject.value;
        //return this.userSubject.value[0];
    }

    loginUser(formData: any): Observable<any> {
        return this.http
            .HttpPost('Account/LoginByUserId', formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.handleError),
                tap(logResp => {
                    try {
                        //this.localStorage.set('currentUser', JSON.stringify(logResp.Result));
                        this.userSubject.next(logResp.Result);
                    } catch (httpError) {
                        console.log(httpError)
                    }
                    return logResp;
                })
            );
    }

    rightsUser(formData: any): Observable<any> {
        return this.http
            .HttpPost('Account/GetUserTransactionsById', formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.handleError),
                tap(logResp => {
                    try {
                        //this.localStorage.set('rightsUser', JSON.stringify(logResp.Result));
                        this.userSubject.next(logResp.Result);
                    } catch (httpError) {
                        console.log(httpError)
                    }
                    return logResp;
                })
            );
    }

    handleError(error: HttpErrorResponse) {
        let errorMessage = 'Unknown error!';
        if (error.error instanceof ErrorEvent) {
            // Client-side errors
            errorMessage = `Error: ${error.error.message}`;
        } else {
            // Server-side errors
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }
        alert(errorMessage);
        return throwError(errorMessage);
    }

    // getCurrentUser(): UserModel {
    //     var currentUserJson = this.localStorage.getItem('currentUser');
    //     return (currentUserJson != undefined) ? JSON.parse(currentUserJson) : null;
    // }

    resetLogin() {
        // remove user from local storage to log user out
        this.localStorage.remove('currentUser');
        this.userSubject.next(null);
    }

    logoutUser(formData) {
        return this.http
            .HttpPost('Account/LogoutByUserId', formData)
            .pipe(
                retry(this.http.Services.RetryServieNo),
                catchError(this.handleError),
                tap(logResp => {
                    return logResp;
                })
            );
    }
}

