import { Component, OnInit, ViewChild } from '@angular/core';
import { LoadingController, MenuController } from '@ionic/angular';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginModel } from '../models/login.model';
import { UserModel } from '../models/user.model';
import { AppConstants } from '../app.constants';
import { Storage } from '@ionic/storage';
import { AuthenticationService } from '../services/authentication.service';
import { ApiService } from '../services/api.service';
import { HelperService } from '../services/helper.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {
  @ViewChild('f') loginForm: NgForm;
  public loginModel = new LoginModel();
  public appInfo = environment;

  constructor(
    private menu: MenuController,
    private router: Router,
    private authService: AuthenticationService,
    public loadingController: LoadingController,
    private localStorage: Storage,
    private helperService: HelperService,
    private api: ApiService,

  ) { }

  ngOnInit() {

    // Samsung Tab - 5203497f5bbe9400   M10 - RZ8M91G7SNX

    //this.loginForm.controls['UserId'].setValue('vt.test1@email.com');
    //this.loginForm.controls['Password'].setValue('Pass$123');
  }

  ionViewDidEnter() {
    this.menu.enable(false);
    this.localStorage.get('currentUser').then((currentUserJson) => {
      if (currentUserJson !== null && currentUserJson !== undefined) {
        AppConstants.AuthToken = JSON.parse(currentUserJson).AuthToken;
        this.router.navigateByUrl('/folder/Home');
      }
    });
  }

  ionViewDidLeave() {
    this.menu.enable(true);
  }

  onSubmit(f: NgForm) {
    this.loadingController
      .create({
        message: 'Please wait...'
      })
      .then((loadEl) => {
        loadEl.present();

        this.loginModel.UserId = f.value.UserId;
        this.loginModel.Password = f.value.Password;
        this.loginModel.RememberMe = true;
        // this.loginModel.RememberMe = f.value.RememberMe;
        if (this.helperService.checkInternetConnection()) {
          this.authService.loginUser(this.loginModel)
            .subscribe((logResp: any) => {
              if (logResp.Success && logResp.Errors.length === 0) {

                if ('Roles : VC, VT, HM, DisRP'.indexOf(logResp.Result.RoleCode) == -1) {
                  loadEl.dismiss();
                  alert('Error: Mobile app is applicable for Only VC, VT, HM and DRP users');
                  return;
                }

                const currentUser: UserModel = {
                  LoginUniqueId: logResp.Result.LoginUniqueId,
                  AcademicYearId: logResp.Result.AcademicYearId,
                  UserTypeId: logResp.Result.UserTypeId,
                  LoginId: logResp.Result.LoginId,
                  Password: logResp.Result.Password,
                  UserId: logResp.Result.UserId,
                  UserName: logResp.Result.UserName,
                  FirstName: logResp.Result.FirstName,
                  LastName: logResp.Result.LastName,
                  EmailId: logResp.Result.EmailId,
                  Mobile: logResp.Result.Mobile,
                  IsAdmin: false,
                  Designation: logResp.Result.Designation,
                  DateOfJoining: logResp.Result.DateOfJoining,
                  DateOfAllocation: logResp.Result.DateOfAllocation,
                  DefaultStateId: logResp.Result.DefaultStateId,
                  StateId: logResp.Result.StateId,
                  DivisionId: logResp.Result.DivisionId,
                  DistrictId: logResp.Result.DistrictId,
                  BlockId: logResp.Result.BlockId,
                  RoleCode: logResp.Result.RoleCode,
                  AccountType: logResp.Result.AccountType,
                  SectorId: logResp.Result.SectorId,
                  LandingPageUrl: logResp.Result.LandingPageUrl,
                  InvalidAttempt: logResp.Result.InvalidAttempt,
                  IsLocked: logResp.Result.IsLocked,
                  IsPasswordReset: logResp.Result.IsPasswordReset,
                  LastLoginDate: logResp.Result.LastLoginDate,
                  PasswordExpiredOn: logResp.Result.PasswordExpiredOn,
                  PasswordResetToken: logResp.Result.PasswordResetToken,
                  PasswordUpdateDate: logResp.Result.PasswordUpdateDate,
                  TokenExpiredOn: logResp.Result.TokenExpiredOn,
                  AuthToken: logResp.Result.AuthToken,
                  RoleTransactions: []
                };

                // store user details and basic auth credentials in local storage to keep user logged in between page refreshes
                this.localStorage.set('currentUser', JSON.stringify(currentUser));
                this.localStorage.set('masterList', JSON.stringify(AppConstants.masterList));
                AppConstants.AuthToken = currentUser.AuthToken;

                this.authService.rightsUser(this.loginModel)
                  .subscribe((res: any) => {
                    if (res.Success) {
                      this.localStorage.set('rightsUser', JSON.stringify(res.Results));
                      const allPages = AppConstants.appPages;
                      for (const iterator of allPages) {
                        const ind = res.Results.findIndex((x) => x.PageTitle === iterator.title);
                        if (ind === -1) {
                          iterator.access = false;
                        } else {
                          iterator.access = true;
                        }
                        if (iterator.title === 'VC School Visits') { iterator.access = false; }
                      }
                      this.localStorage.set('allowedPages', JSON.stringify(allPages));

                      this.api.createUploadTables().then(() => { }, (err) => {
                        alert('Error Creating Upload Tables. Please use Upload Sync first to try again.');
                      });
                      // this.api.createGetTables();
                      loadEl.dismiss();
                      this.router.navigateByUrl('/folder/Home');
                    } else {
                      let errorMessage = '';
                      for (const msg of res.Messages) {
                        errorMessage = errorMessage + msg;
                      }
                      loadEl.dismiss();
                      alert('Error: \n' + errorMessage);
                    }
                  }, (err) => {
                    loadEl.dismiss();
                  });
                // login successful so redirect to return url

              } else {
                loadEl.dismiss();
                alert('Error: Wrong credentials entered.');
              }
            }, (err) => {
              loadEl.dismiss();
            });
        } else {
          loadEl.dismiss();
          alert('Internet Connection is required.');
        }
      });
  }

  getLogoTitle(): string {
    if (this.appInfo.gj)
      return "Gujarat";
    else if (this.appInfo.mh)
      return "Maharashtra";
    else if (this.appInfo.jh)
      return "Jharkhand";
    else if (this.appInfo.od)
      return "Odisha";
    else if (this.appInfo.lahi)
      return "LAHI";
    else if (this.appInfo.demo)
      return "DEMO";
    else
      return "UAT";
  }
}
