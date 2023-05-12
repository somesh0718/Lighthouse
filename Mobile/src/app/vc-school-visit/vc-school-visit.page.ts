import { Component, OnInit, ViewChild } from '@angular/core';
import { DatePipe } from '@angular/common';
import { NgForm } from '@angular/forms';
import { VCSchoolVisitService } from './vc-school-visit.service';
import { HelperService } from '../services/helper.service';
import { Router } from '@angular/router';
import { VCSchoolVisitModel } from './vc-school-visit.model';
import { AlertController, LoadingController, ModalController } from '@ionic/angular';
import { CalModalPage } from '../calendar/cal-modal/cal-modal.page';
import { ApiService } from '../services/api.service';
import { Storage } from '@ionic/storage';
import { BasePage } from '../common/base.page';

@Component({
  selector: 'app-vc-school-visit',
  templateUrl: './vc-school-visit.page.html',
  styleUrls: ['./vc-school-visit.page.scss'],
})
export class VcSchoolVisitPage extends BasePage<VCSchoolVisitModel> implements OnInit {

  vcSchoolVisitModel: VCSchoolVisitModel;
  maxDate: any;
  minDate: any;

  @ViewChild('f') Form: NgForm;
  submittedRecords: any;

  constructor(
    public datepipe: DatePipe,
    private vcSchoolVisitService: VCSchoolVisitService,
    private helperService: HelperService,
    private router: Router,
    public loadingController: LoadingController,
    private api: ApiService,
    private localStorage: Storage,
    private alertCtrl: AlertController,
    private modalCtrl: ModalController
  ) {
    super();

    this.vcSchoolVisitModel = new VCSchoolVisitModel();
  }

  ngOnInit() {
  }

  ionViewDidEnter() {
    this.maxDate = this.datepipe.transform(new Date(), 'yyyy-MM-dd');
    this.loadSubmittedRecords();

    this.localStorage.get('currentUser').then((r) => {
      this.UserModel = JSON.parse(r);

      let dateOfAllocation = new Date(this.UserModel.DateOfAllocation);
      let maxDate = new Date(Date.now());

      let Time = maxDate.getTime() - dateOfAllocation.getTime();
      let Days = Math.floor(Time / (1000 * 3600 * 24));
      
      if (Days < this.Constants.BackDatedReportingDays) {
        this.minDate = this.DateFormatPipe.transform(this.UserModel.DateOfAllocation, this.Constants.DateValFormat);
      }
      else {
        let past7days = maxDate;
        past7days.setDate(past7days.getDate() - this.Constants.BackDatedReportingDays)
        this.minDate = past7days;
      }
    });
  }

  loadSubmittedRecords() {
    this.loadingController
      .create({
        message: 'Please wait...'
      })
      .then((loadEl) => {
        loadEl.present();
        // if (this.helperService.checkInternetConnection()) {

        //   this.vcSchoolVisitService.getVCSchoolVisits().subscribe((res) => {
        //     console.log(res);
        //     this.submittedRecords = res.Results;
        //     loadEl.dismiss();
        //   }, (err) => {
        //     console.log(err);
        //     loadEl.dismiss();
        //   });
        // } else {
        //   loadEl.dismiss();

        // }
        this.api.selectTableData('GetVCSchoolVisits').then((res) => {
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
      iterator.calendarDate = iterator.ReportDate;
    }
    const modal = await this.modalCtrl.create({
      component: CalModalPage,
      componentProps: {
        Records: this.submittedRecords,
        title: 'VC School Visits',
      },
      cssClass: 'cal-modal',
      backdropDismiss: false,
    });

    await modal.present();

    modal.onDidDismiss().then((result) => {

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

  onSubmit(form: NgForm) {
    this.loadingController
      .create({
        message: 'Please wait...'
      })
      .then((loadEl) => {
        loadEl.present();

        const input = form.value;
        input.ReportDate = new Date(input.ReportDate).toISOString();
        input.CMDate = new Date(input.CMDate).toISOString();
        input.TEDate = new Date(input.TEDate).toISOString();
        input.VCId = this.UserModel.UserTypeId;

        this.helperService.getCurrentCoordinates().then((res: any) => {
          input.GeoLocation = res.coords.latitude + '-' + res.coords.longitude;
          input.Latitude = res.coords.latitude;
          input.Longitude = res.coords.longitude;

          this.vcSchoolVisitModel = new VCSchoolVisitModel(input);

          if (this.helperService.checkInternetConnection()) {

            this.vcSchoolVisitService.createOrUpdateVCSchoolVisit(this.vcSchoolVisitModel)
              .subscribe((Resp: any) => {
                loadEl.dismiss();

                if (Resp.Success) {
                  const successMessages = this.helperService.getHtmlMessage(Resp.Messages);
                  this.helperService.presentToast(successMessages);
                  this.router.navigateByUrl('/folder/Home');
                }
                else {
                  const errorMessages = this.helperService.getHtmlMessage(Resp.Errors);
                  this.helperService.showAlert(errorMessages);
                }
              }, error => {
                this.savingOfflineVTIssueReporting();
                loadEl.dismiss();
                console.log('VCSchoolVisit errors =>', error);
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

  savingOfflineVTIssueReporting() {
    this.helperService.presentToast('Saving Offline...');

    this.api.insertUploadTable('UploadVCSchoolVisits', this.vcSchoolVisitModel).then(() => {
      this.router.navigateByUrl('/folder/Home');
      this.helperService.presentToast('VC School Visits Form Saved Offline Successfully.');
    }, (err) => {
      alert(err);
      console.log('VCSchoolVisits errors =>', err);
    });
  }

}
