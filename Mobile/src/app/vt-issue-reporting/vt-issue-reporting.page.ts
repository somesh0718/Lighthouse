import { Component, OnInit, ViewChild } from '@angular/core';
import { DatePipe } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { VTIssueReportingService } from './vt-issue-reporting.service';
import { HelperService } from '../services/helper.service';
import { Router } from '@angular/router';
import { VTIssueReportingModel } from './vt-issue-reporting.model';
import { AlertController, LoadingController, ModalController } from '@ionic/angular';
import { CalModalPage } from '../calendar/cal-modal/cal-modal.page';
import { ApiService } from '../services/api.service';
import { Storage } from '@ionic/storage';
import { forkJoin } from 'rxjs';
import { BasePage } from '../common/base.page';

@Component({
  selector: 'app-vt-issue-reporting',
  templateUrl: './vt-issue-reporting.page.html',
  styleUrls: ['./vt-issue-reporting.page.scss'],
})
export class VtIssueReportingPage extends BasePage<VTIssueReportingModel> implements OnInit {
  maxDate: any;
  minDate: any;
  submittedRecords: any;
  issueReportingForm: FormGroup;
  issueReportingModel: VTIssueReportingModel;

  commonMasterData: any = [];
  mainIssueList: [];
  subIssueListByUser: any = [];
  subIssueList: [];
  monthList: [];
  classList: [];
  studentTypeList: [];
  issueStatusList: [];

  constructor(
    public datepipe: DatePipe,
    private vtIssueReportingService: VTIssueReportingService,
    private helperService: HelperService,
    private router: Router,
    public loadingController: LoadingController,
    private api: ApiService,
    private localStorage: Storage,
    private formBuilder: FormBuilder,
    private alertCtrl: AlertController,
    private modalCtrl: ModalController
  ) {
    super();

    this.issueReportingModel = new VTIssueReportingModel();
    this.issueReportingForm = this.createVTIssueReportingForm();
  }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.issueReportingModel = new VTIssueReportingModel();
    this.issueReportingForm = this.createVTIssueReportingForm();

    this.loadMasters();
  }

  ionViewDidEnter() {
    this.maxDate = this.datepipe.transform(new Date(), 'yyyy-MM-dd');
    this.loadSubmittedRecords();

    this.localStorage.get('currentUser').then((r) => {
      this.UserModel = JSON.parse(r);
      this.minDate = this.DateFormatPipe.transform(this.UserModel.DateOfAllocation, this.Constants.DateValFormat);
    });

    this.issueReportingForm.reset();
  }

  loadMasters() {
    this.loadingController
      .create({
        message: 'Please wait...'
      })
      .then((loadEl) => {
        loadEl.present();
        const obsList = [this.api.loadCommonMasterData(), this.api.loadMainIssueByUser(), this.api.loadSubIssueByUser()];

        forkJoin(obsList).subscribe((res: any) => {
          this.commonMasterData = res[0];
          this.mainIssueList = res[1];
          this.subIssueListByUser = res[2];

          //this.mainIssueList = this.commonMasterData.filter((x) => x.DataTypeId === 'MainIssue');
          this.monthList = this.commonMasterData.filter((x) => x.DataTypeId === 'Months');
          this.classList = this.commonMasterData.filter((x) => x.DataTypeId === 'ClassesAffected');
          this.studentTypeList = this.commonMasterData.filter((x) => x.DataTypeId === 'StudentType');
          this.issueStatusList = this.commonMasterData.filter((x) => x.DataTypeId === 'IssueStatus');

          loadEl.dismiss();

        }, (err) => {
          this.showAlert('Please do Master Sync first.');
          loadEl.dismiss();
        });

      });
  }

  onChangeMainIssue(mainIssueId: any): void {
    this.subIssueList = this.subIssueListByUser.filter((x) => x.Description == mainIssueId);
  }

  loadSubmittedRecords() {
    this.loadingController
      .create({
        message: 'Please wait...'
      })
      .then((loadEl) => {
        loadEl.present();

        this.api.selectTableData('GetVTIssueReporting').then((res) => {
          this.submittedRecords = res;
          loadEl.dismiss();
        }, (err) => {
          this.showAlert('Please do Get My Sync first.');
          loadEl.dismiss();
        });
      });
  }

  async openCalModal() {
    for (const iterator of this.submittedRecords) {
      iterator.calendarDate = iterator.IssueReportDate;
    }
    const modal = await this.modalCtrl.create({
      component: CalModalPage,
      componentProps: {
        Records: this.submittedRecords,
        title: 'VT Issue Reporting',
      },
      cssClass: 'cal-modal',
      backdropDismiss: false,
    });

    await modal.present();

    modal.onDidDismiss().then((result) => {

    });
  }

  saveOrUpdateVTIssueReportingDetails() {

    // this.issueReportingForm.markAllAsTouched();
    if (!this.issueReportingForm.valid) {
      this.validateDynamicFormArrayFields(this.issueReportingForm);
      return;
    }
    if (this.issueReportingForm.valid) {
      this.loadingController
        .create({
          message: 'Please wait...'
        })
        .then((loadEl) => {
          loadEl.present();

          this.issueReportingModel = this.vtIssueReportingService.getVTIssueReportingFromFormGroup(this.issueReportingForm);
          this.issueReportingModel.IssueReportDate = this.DateFormatPipe.transform(this.issueReportingForm.get('IssueReportDate').value, this.Constants.ServerDateFormat);
          this.issueReportingModel.VTId = this.UserModel.UserTypeId;
          this.issueReportingModel.AcademicYearId = this.UserModel.AcademicYearId;

          this.helperService.getCurrentCoordinates().then((res: any) => {
            this.issueReportingModel.GeoLocation = res.coords.latitude + '-' + res.coords.longitude;
            this.issueReportingModel.Latitude = res.coords.latitude;
            this.issueReportingModel.Longitude = res.coords.longitude;

            if (this.helperService.checkInternetConnection()) {
              this.vtIssueReportingService.createOrUpdateVTIssueReporting(this.issueReportingModel)
                .subscribe((Resp: any) => {
                  loadEl.dismiss();
                  
                  if (Resp.Success) {
                    const successMessages = this.helperService.getHtmlMessage(Resp.Messages);
                    loadEl.dismiss();
                    this.helperService.presentToast(successMessages);

                    this.router.navigateByUrl('/folder/Home');
                  }
                  else {
                    const errorMessages = this.helperService.getHtmlMessage(Resp.Errors);
                    loadEl.dismiss();
                    this.helperService.showAlert(errorMessages);
                  }
                }, error => {
                  this.savingOfflineVTIssueReporting();
                  loadEl.dismiss();
                  console.log('VTIssueReporting errors =>', error);
                });
            } else {
              this.savingOfflineVTIssueReporting();
              loadEl.dismiss();
            }

          }, (err) => {
            loadEl.dismiss();
            return;
          });
        });
    }
    else {
      const invalidControls = [];
      const controls = this.issueReportingForm.controls;
      for (const name in controls) {
        if (controls[name].invalid) {
          invalidControls.push(name);
        }
      }

      alert('Invalid Inputs. ' + JSON.stringify(invalidControls));
    }
  }

  savingOfflineVTIssueReporting() {
    this.helperService.presentToast('Saving Offline...');
    
    this.api.insertUploadTable('UploadVTIssueReporting', this.issueReportingModel).then(() => {
      this.router.navigateByUrl('/folder/Home');
      this.helperService.presentToast('VT Issue Reporting Conducted Form Saved Offline Successfully.');
    }, (err) => {
      alert(err);
      console.log('VT Issue Reporting Conducted errors =>', err);
    });
  }

  async showAlert(msg) {
    const alert = await this.alertCtrl.create({
      message: msg,
      buttons: [
        {
          text: 'Okay',
          handler: () => {
            this.router.navigateByUrl('/folder/Home');
          }
        }
      ],
      backdropDismiss: false
    });

    await alert.present();
  }

  createVTIssueReportingForm(): FormGroup {
    return this.formBuilder.group({
      VTIssueReportingId: new FormControl(this.issueReportingModel.VTIssueReportingId),
      VCId: new FormControl(this.issueReportingModel.VTId),
      IssueReportDate: new FormControl('', Validators.required),
      MainIssue: new FormControl('', Validators.required),
      SubIssue: new FormControl('', Validators.required),
      StudentClass: new FormControl('', Validators.required),
      Month: new FormControl('', Validators.required),
      StudentType: new FormControl('', Validators.required),
      NoOfStudents: new FormControl('', [Validators.required, Validators.pattern(this.Constants.Regex.Number)]),
      IssueDetails: new FormControl('', Validators.maxLength(350)),
      IssueStatus: new FormControl(''),
    });
  }
}
