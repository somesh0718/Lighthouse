import { Injectable } from '@angular/core';
import { AbstractControl, FormGroup } from '@angular/forms';
import { UserModel } from '../models/user.model';
import { AppConstants } from '../app.constants';
import { Storage } from '@ionic/storage';
import { Platform, MenuController, ToastController, AlertController } from '@ionic/angular';
import { Geolocation } from '@ionic-native/geolocation/ngx';

function getWindow(): any {
    return window;
}
declare var navigator: any;

@Injectable()
export class HelperService {
    addContentTypeHeader: boolean | string = true;
    tickerInstance: any = null;
    env: string;
    menuAccesses: string;

    constructor(
        private localStorage: Storage,
        private toastCtrl: ToastController,
        private alertCtrl: AlertController,
        private geolocation: Geolocation,


    ) { }

    checkInternetConnection() {
        var isOn;
        // let states = {};
        // states[Connection.UNKNOWN] = 'Unknown connection';
        // states[Connection.ETHERNET] = 'Ethernet connection';
        // states[Connection.WIFI] = 'WiFi connection';
        // states[Connection.CELL_2G] = 'Cell 2G connection';
        // states[Connection.CELL_3G] = 'Cell 3G connection';
        // states[Connection.CELL_4G] = 'Cell 4G connection';
        // states[Connection.CELL] = 'Cell generic connection';
        // states[Connection.NONE] = 'No network connection';

        if (navigator.connection.type !== 'No network connection' && navigator.connection.type !== 'none' && navigator.connection.type !== 'unknown') {
            isOn = true;
        } else {
            isOn = false;
        }

        return isOn;
    }

    masterAccess(accessList: Array<any> = []) {
        let masterList = [
            { name: 'AcademicYears', access: 1, sync: true },
            { name: 'Divisions', access: 1, sync: true },
            { name: 'JobRoles', access: 1, sync: true },
            { name: 'Phases', access: 1, sync: true },
            { name: 'Roles', access: 1, sync: true },
            { name: 'SchoolCategories', access: 1, sync: true },
            { name: 'SchoolClasses', access: 1, sync: true },
            { name: 'Sections', access: 1, sync: true },
            { name: 'Sectors', access: 1, sync: true },
            { name: 'SiteHeaders', access: 1, sync: true },
            { name: 'Transactions', access: 1, sync: true },
            { name: 'States', access: 1, sync: true },
            { name: 'Districts', access: 1, sync: true },
            { name: 'DataTypes', access: 1, sync: true },
            { name: 'DataValues', access: 1, sync: true },
            { name: 'HeadMasters', access: 1, sync: true },
            { name: 'VocationalTrainingProviders', access: 1, sync: true },
            { name: 'VocationalCoordinators', access: 1, sync: true },
            { name: 'VocationalTrainers', access: 1, sync: true },
            { name: 'Schools', access: 1, sync: true },
            // { name: 'SectorJobRoles', access: 0, sync: true },
            { name: 'Students', access: 1, sync: true },
            // { name: 'VTClasses', access: 0, sync: true },
            // { name: 'VTPSectors', access: 0, sync: true },
            // { name: 'VTSchoolSectors', access: 0, sync: true },
            // { name: 'VCSchoolSectors', access: 0, sync: true },
        ];

        for (const iterator of accessList) {
            if (!iterator.access) {
                const index = masterList.findIndex((x) => x.name === iterator['name']);
                if (index !== -1) {
                    masterList.splice(index, 1);
                }
            }
        }
        return masterList;
    }

    getCurrentCoordinates() {
        return new Promise((resolve, reject) => {
            let options = { maximumAge: 3000, timeout: 5000, enableHighAccuracy: true };
            this.geolocation.getCurrentPosition(options).then((resp) => {
                // this.latitude = resp.coords.latitude;
                // this.longitude = resp.coords.longitude;
                console.log('geo: ', resp);
                resolve(resp);
            }, (error) => {

                if (error.code == 3) {
                    let geoLoc = {
                        coords: {
                            latitude: 2,
                            longitude: 2
                        },
                        exception: error
                    };
                    resolve(geoLoc);
                }
                else {
                    alert('Error getting location: ' + error.message);
                    reject(error);
                }
            });
        });
    }

    getHtmlMessage(errors: string[]): string {
        let errorMessages = '<ul style="list-style: none; margin: 0; padding: 0;">';

        for (const errorText of errors) {
            errorMessages += '<li>' + errorText + '</li>';
        }

        return errorMessages + '</ul>';
    }

    async showAlert(msg) {
        const alert = await this.alertCtrl.create({
            message: msg,
            buttons: [
                {
                    text: 'Okay',
                    handler: () => { }
                }
            ],
            backdropDismiss: false
        });

        await alert.present();
    }

    async presentToast(toastData) {
        const toast = await this.toastCtrl.create({
            message: toastData,
            duration: 2000
        });
        toast.present();
    }

    // return the global window object
    get nativeWindow(): any {
        return getWindow();
    }

    isProdEnv(): boolean {
        return this.env.toLocaleLowerCase() === 'prod' ||
            this.env.toLocaleLowerCase() === 'production'
            ? true
            : false;
    }
    isStageEnv(): boolean {
        return this.env.toLocaleLowerCase() === 'prod' ||
            this.env.toLocaleLowerCase() === 'production'
            ? true
            : false;
    }
    isDevEnv(): boolean {
        return this.env.toLocaleLowerCase() === 'dev' ||
            this.env.toLocaleLowerCase() === 'development'
            ? true
            : false;
    }

    /**
     * Use this method to create logs to the server
     * Pass info like error stack (if error), user info, user brower and other details
     */
    serverLogger(log: any) {
        // tslint:disable-next-line:no-console
    }

    secondsTicksCounter(): object {
        let seconds = 0;
        // tslint:disable-next-line:prefer-const
        let interval;
        return {
            start: () => {
                return setInterval(() => {
                    seconds++;
                }, 1000);
            },
            stop: (intervalInstance: any) => {
                clearInterval(intervalInstance);
                return seconds;
            },
            intervalInstance: null
        };
    }

    deleteAllCookies(): void {
        const cookies = document.cookie.split(';');
        for (const cookie of cookies) {
            const eqPos = cookie.indexOf('=');
            const name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
            document.cookie = `${name}=;expires=Thu, 01 Jan 1970 00:00:00 GMT`;
        }
    }

    // getUserFromLocalStorage(): UserModel {
    //     const user = sessionStorage.getItem(AppConstants.UserLocalStorage);
    //     return <UserModel>JSON.parse(user);
    // }

    // getUserRole(): string {
    //     const user: UserModel = this.getUserFromLocalStorage();
    //     return user ? user.AccountType : null;
    // }

    // isUserLoggedIn(): boolean {
    //     const user = sessionStorage.getItem(AppConstants.UserLocalStorage);
    //     return user && user.length > 0 ? true : false;
    // }

    isRegistrationPage(url: string) {
        return url === AppConstants.RegistrationPageUrl ? true : false;
    }

    redirectToLogin() { }

    // getUserDefaultPageUrl(): string {
    //     const role: string = this.getUserRole();
    //     const defaultPage = "/roles";
    //     return defaultPage;
    // }

    initFormControls(
        self: object,
        formGroup: FormGroup,
        controlNames: string[]
    ): void {
        for (const controlName of controlNames) {
            self[controlName] = formGroup.controls[controlName];
        }
    }

    getInputValidationClass(
        formGroup: FormGroup,
        formControlName: string
    ): string {
        if (formGroup) {
            const formControl: AbstractControl =
                formGroup.controls[formControlName];
            if (formControl && formControl.touched) {
                return formControl.valid
                    ? AppConstants.SuccessInputClass
                    : AppConstants.ErrorInputClass;
            }
        }

        return '';
    }

    getConfirmInputValidationClass(
        compareFromGroup: FormGroup,
        formControl: AbstractControl,
        confirmFormControl: AbstractControl,
        test: FormGroup
    ): string {
        if (
            (formControl && formControl.touched) ||
            (confirmFormControl && confirmFormControl.touched)
        ) {
            return compareFromGroup.valid
                ? AppConstants.SuccessInputClass
                : AppConstants.ErrorInputClass;
        }
        if (formControl && formControl.touched) {
            return formControl.valid
                ? AppConstants.SuccessInputClass
                : AppConstants.ErrorInputClass;
        } else {
            return '';
        }
    }

    showCompareCtrlsValidationMsg(
        frmGroup: FormGroup,
        ctrl1: AbstractControl,
        ctrl2: AbstractControl
    ): boolean {
        return frmGroup && !frmGroup.valid && (ctrl1.touched || ctrl1.touched)
            ? true
            : false;
    }

    showCtrlValidationMsg(formControl: AbstractControl): boolean {
        return formControl &&
            !formControl.valid &&
            formControl.touched &&
            formControl.errors
            ? true
            : false;
    }

    // isAccessToResourceAllowed(resourceName: string): boolean {
    //     const accessesRaw: any = <any>(
    //         JSON.parse(
    //             sessionStorage.getItem(AppConstants.ResourceAccessLocalStorage)
    //         )
    //     );
    //     if (!accessesRaw) {
    //         return false;
    //     }
    //     let actions: string[];
    //     if (!accessesRaw[resourceName]) {
    //         return false;
    //     }
    //     actions = accessesRaw[resourceName].split(",");
    //     if (actions.length === 1 && actions[0] === "") {
    //         actions = [];
    //     }
    //     return !actions || actions.length < 1 ? false : true;
    // }

    // isAccessToActionAllowed(resourceName: string, actionName: string): boolean {
    //     const accessesRaw: any = <any>(
    //         JSON.parse(
    //             sessionStorage.getItem(AppConstants.ResourceAccessLocalStorage)
    //         )
    //     );
    //     const actions: string = accessesRaw[resourceName];

    //     return actions && actions.indexOf(actionName) !== -1 ? true : false;
    // }

    getObjectKeys(object: {}): string[] {
        if (!object || typeof object !== 'object') {
            throw new Error(
                'Only objects can be passed to retrieve its own enumerable properties(keys).'
            );
        }
        return Object.keys(object);
    }

    signOutUser() {
        // sessionStorage.clear();
        window.location.href = AppConstants.LoginPageUrl;
    }

    searchInArray(inputArray, lookUpArray, caseSensitiveSearch): any[] {
        const result: any[] = [];
        // tslint:disable-next-line: prefer-for-of
        outer: for (let index = 0; index < inputArray.length; index++) {
            const item = inputArray[index];
            // tslint:disable-next-line: prefer-for-of
            for (let i = 0; i < lookUpArray.length; i++) {
                const lookUpItem = lookUpArray[i];
                if (item[lookUpItem.key] !== lookUpItem.value) {
                    continue outer;
                }
            }
            result.push(item);
        }
        return result;
    }

    searchInArrayTest(
        inputArray: any[],
        lookUpArray: any[],
        caseSensitiveSearch?: boolean,
        exactMatch?: boolean
    ): any[] {
        const result: any[] = [];
        outer: for (const item of inputArray) {
            for (const lookUpItem of lookUpArray) {
                const sourceString: string = caseSensitiveSearch
                    ? item[lookUpItem.key].toString()
                    : item[lookUpItem.key].toString().toLocaleLowerCase();
                const searchForString: string = caseSensitiveSearch
                    ? lookUpItem.value.toString()
                    : lookUpItem.value.toString().toLocaleLowerCase();
                if (exactMatch) {
                    if (sourceString === searchForString) {
                        continue outer;
                    }
                } else {
                    if (sourceString.indexOf(searchForString) === -1) {
                        continue outer;
                    }
                }
            }
            result.push(item);
        }
        return result;
    }
}
