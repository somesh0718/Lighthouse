import { Component, OnInit, ViewChild } from '@angular/core';
import { DatePipe } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { ComplaintRegistrationService } from './complaint-registration.service';
import { HelperService } from '../services/helper.service';
import { Router } from '@angular/router';
import { ComplaintRegistrationModel } from './complaint-registration.model';
import { AlertController, LoadingController, ModalController } from '@ionic/angular';
import { CalModalPage } from '../calendar/cal-modal/cal-modal.page';
import { ApiService } from '../services/api.service';
import { Storage } from '@ionic/storage';
import { forkJoin } from 'rxjs';
import { BasePage } from '../common/base.page';
import { FileUploadModel } from '../models/file.upload.model';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-complaint-registration',
  templateUrl: './complaint-registration.page.html',
  styleUrls: ['./complaint-registration.page.scss'],
})
export class ComplaintRegistrationPage extends BasePage<ComplaintRegistrationModel> implements OnInit {
  submittedRecords: any;
  complaintRegistrationForm: FormGroup;
  complaintRegistrationModel: ComplaintRegistrationModel;
  ScreenshotFile: FileUploadModel;
  commonMasterData: any = [];

  constructor(
    public datepipe: DatePipe,
    private ComplaintRegistrationService: ComplaintRegistrationService,
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
    this.ScreenshotFile = new FileUploadModel();
    this.complaintRegistrationModel = new ComplaintRegistrationModel();
    this.complaintRegistrationForm = this.createComplaintRegistrationForm();
  }

  ngOnInit() {
  }

  ionViewWillEnter() {
    this.complaintRegistrationModel = new ComplaintRegistrationModel();
    this.complaintRegistrationForm = this.createComplaintRegistrationForm();

    //this.loadMasters();
  }

  ionViewDidEnter() {

    //this.loadSubmittedRecords();

    // this.localStorage.get('currentUser').then((r) => {
    //   this.UserModel = JSON.parse(r);
    // });

    this.complaintRegistrationForm.reset();
  }

  // loadMasters() {
  //   this.loadingController
  //     .create({
  //       message: 'Please wait...'
  //     })
  //     .then((loadEl) => {
  //       loadEl.present();
  //       const obsList = [this.api.loadCommonMasterData()];

  //       forkJoin(obsList).subscribe((res: any) => {
  //         this.commonMasterData = res[0];

  //         // this.mainIssueList = this.commonMasterData.filter((x) => x.DataTypeId === 'MainIssue');
  //         // this.monthList = this.commonMasterData.filter((x) => x.DataTypeId === 'Months');
  //         // this.classList = this.commonMasterData.filter((x) => x.DataTypeId === 'ClassesAffected');
  //         // this.studentTypeList = this.commonMasterData.filter((x) => x.DataTypeId === 'StudentType');
  //         // this.issueStatusList = this.commonMasterData.filter((x) => x.DataTypeId === 'IssueStatus');

  //         loadEl.dismiss();

  //       }, (err) => {
  //         this.showAlert('Please do Master Sync first.');
  //         loadEl.dismiss();
  //       });

  //     });
  // }

//   loadSubmittedRecords() {
//     this.loadingController
//     .create({
//       message: 'Please wait...'
//     })
//     .then((loadEl) => {
//       loadEl.present();

//       this.api.selectTableData('GetComplaintRegistration').then((res) => {
//         this.submittedRecords = res;
//         loadEl.dismiss();
//       }, (err) => {
//         this.showAlert('Please do Get My Sync first.');
//         loadEl.dismiss();
//       });
//     });
// }

  // async openCalModal() {
  //   for (const iterator of this.submittedRecords) {
  //     iterator.calendarDate = iterator.IssueReportDate;
  //   }
  //   const modal = await this.modalCtrl.create({
  //     component: CalModalPage,
  //     componentProps: {
  //       Records: this.submittedRecords,
  //       title: 'Complaint Registration',
  //     },
  //     cssClass: 'cal-modal',
  //     backdropDismiss: false,
  //   });

  //   await modal.present();

  //   modal.onDidDismiss().then((result) => {

  //   });
  // }

  uploadedScreenshotFile(event) {
    if (event.target.files.length > 0) {
      var fileExtn = event.target.files[0].name.split('.').pop().toLowerCase();

      if (this.AllowedImageExtensions.indexOf(fileExtn) == -1) {
        this.complaintRegistrationForm.get('ScreenshotFile').setValue(null);
        this.helperService.showAlert("Please upload image file only.");
        return;
      }

      this.getUploadedFileData(event, this.Constants.DocumentType.ComplaintRegistration).then((response: FileUploadModel) => {
        this.ScreenshotFile = response;
        this.complaintRegistrationForm.get('ScreenshotFile').setValue(response.Base64Data);
      });
    }
  }

  setUploadedScreenshotFile(fileUploaded: FileUploadModel) {
    if (fileUploaded == null)
        return null;

    return new FileUploadModel({
        UserId: Guid.create()['value'],
        ContentId: null,
        FilePath: null,
        ContentType: fileUploaded.ContentType,
        FileName: fileUploaded.FileName,
        FileType: fileUploaded.FileType,
        FileSize: fileUploaded.FileSize,
        Base64Data: fileUploaded.Base64Data
    });
}

  saveOrUpdateComplaintRegistrationDetails() {

    // this.complaintRegistrationForm.markAllAsTouched();
    if (!this.complaintRegistrationForm.valid) {
      this.validateDynamicFormArrayFields(this.complaintRegistrationForm);
      return;
    }
    if (this.complaintRegistrationForm.valid) {
      this.loadingController
        .create({
          message: 'Please wait...'
        })
        .then((loadEl) => {
          loadEl.present();

          this.complaintRegistrationModel = this.ComplaintRegistrationService.getComplaintRegistrationFromFormGroup(this.complaintRegistrationForm);
          this.complaintRegistrationModel.ScreenshotFile = (this.ScreenshotFile.Base64Data != '' ? this.setUploadedScreenshotFile(this.ScreenshotFile) : null);
          
          // this.complaintRegistrationModel.IssueReportDate = this.DateFormatPipe.transform(this.complaintRegistrationForm.get('IssueReportDate').value, this.Constants.ServerDateFormat);
          // this.complaintRegistrationModel.VTId = this.UserModel.UserTypeId;

          this.helperService.getCurrentCoordinates().then((res: any) => {
            this.complaintRegistrationModel.GeoLocation = res.coords.latitude + '-' + res.coords.longitude;
            this.complaintRegistrationModel.Latitude = res.coords.latitude;
            this.complaintRegistrationModel.Longitude = res.coords.longitude;

            if (this.helperService.checkInternetConnection()) {
              this.ComplaintRegistrationService.createOrUpdateComplaintRegistration(this.complaintRegistrationModel)
                .subscribe((Resp: any) => {
                  loadEl.dismiss();

                  if (Resp.Success) {
                    const successMessages = this.helperService.getHtmlMessage(Resp.Messages);
                    loadEl.dismiss();
                    this.helperService.presentToast(successMessages);

                    this.router.navigateByUrl('/login');
                  }
                  else {
                    const errorMessages = this.helperService.getHtmlMessage(Resp.Errors);
                    loadEl.dismiss();
                    this.helperService.showAlert(errorMessages);
                  }
                }, error => {
                  loadEl.dismiss();
                  console.log('ComplaintRegistration errors =>', error);
                  this.helperService.showAlert(error);
                });
            } else {
              this.helperService.presentToast('Saving Offline...');
              this.api.insertUploadTable('UploadComplaintRegistration', this.complaintRegistrationModel).then(() => {
                loadEl.dismiss();

                this.router.navigateByUrl('/login');
                this.helperService.presentToast('Complaint Registration Form Saved Offline Successfully.');
              }, (err) => {
                loadEl.dismiss();
                alert(err);
                console.log('Complaint Registration Conducted errors =>', err);
              });
            }

          }, (err) => {
            loadEl.dismiss();
            return;
          });
        });
    }
    else {
      const invalidControls = [];
      const controls = this.complaintRegistrationForm.controls;
      for (const name in controls) {
        if (controls[name].invalid) {
          invalidControls.push(name);
        }
      }

      alert('Invalid Inputs. ' + JSON.stringify(invalidControls));
    }
  }

  async showAlert(msg) {
    const alert = await this.alertCtrl.create({
      message: msg,
      buttons: [
        {
          text: 'Okay',
          handler: () => {
            this.router.navigateByUrl('/login');
          }
        }
      ],
      backdropDismiss: false
    });

    await alert.present();
  }

  createComplaintRegistrationForm(): FormGroup {
    return this.formBuilder.group({
      ComplaintRegistrationId: new FormControl(this.complaintRegistrationModel.ComplaintRegistrationId),
      UserType: new FormControl('', Validators.required),
      UserName: new FormControl('', Validators.required),
      EmailId: new FormControl('', Validators.required),
      Subject: new FormControl('', Validators.required),
      IssueDetails: new FormControl('', Validators.required),
      ScreenshotFile: new FormControl(''),
    });
  }

}
