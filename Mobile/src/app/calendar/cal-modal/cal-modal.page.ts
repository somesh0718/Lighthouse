import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { AlertController, ModalController } from '@ionic/angular';
import { CalendarComponent } from 'ionic2-calendar';
import { NavParams } from '@ionic/angular';
import { DatePipe } from '@angular/common';
import { AppConstants } from 'src/app/app.constants';

@Component({
  selector: 'app-cal-modal',
  templateUrl: './cal-modal.page.html',
  styleUrls: ['./cal-modal.page.scss'],
})
export class CalModalPage implements AfterViewInit {
  public dateFormatPipe = new DatePipe('en-US');
  public constants: AppConstants;

  calendar = { mode: 'month', currentDate: new Date(), allDayLabel: ' ' };
  eventSource = [];
  viewTitle: string;
  @ViewChild(CalendarComponent) myCal: CalendarComponent;
  pageTitle = '';
  event = {
    title: '',
    desc: '',
    startTime: null,
    endTime: '',
    allDay: true
  };

  modalReady = false;

  constructor(
    private modalCtrl: ModalController,
    private alertCtrl: AlertController,
    public navParams: NavParams,
  ) {
    this.constants = new AppConstants();
    const records = navParams.get('Records');
    this.pageTitle = navParams.get('title');

    this.loadEvents(records);
  }

  next() {
    this.myCal.slideNext();
  }

  back() {
    this.myCal.slidePrev();
  }

  onViewTitleChanged(title) {
    this.viewTitle = title;
  }

  onTimeSelected(ev) {
    this.event.startTime = new Date(ev.selectedTime);
  }

  close() {
    this.modalCtrl.dismiss();
  }

  async onEventSelected(event) {

    let calWorkDetails = this.getCalendarEventDetails(event);

    const alert = await this.alertCtrl.create({
      header: event.title,
      subHeader: event.desc,
      message: calWorkDetails,
      buttons: ['OK'],
    });
    alert.present();
  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.modalReady = true;
    }, 0);
  }

  loadEvents(records) {
    const events = [];
    for (const event of records) {
      events.push({
        title: 'Date : ' + this.dateFormatPipe.transform(event.calendarDate, this.constants.DateFormat),
        startTime: new Date(event.calendarDate),
        endTime: new Date(event.calendarDate),
        allDay: true,
        calObj: event,
      });
    }
    this.eventSource = events;
  }

  getCalendarEventDetails(event): string {
    // Use Angular date pipe for conversion
    const startTime = this.dateFormatPipe.transform(event.startTime, this.constants.DateFormat);
    let calWorkDetails = '<ul style="list-style: none; margin: 0; padding: 0;">';

    if (this.pageTitle === 'VT Field Industry Visit Conducted') {
      calWorkDetails += '<li><b>Date:</b> ' + startTime + '</li>';
      calWorkDetails += '<li><b>Class Name:</b> ' + event.calObj.ClassName + '</li>';
      calWorkDetails += '<li><b>Field Visit Organisation:</b> ' + event.calObj.FVOrganisation + '</li>';
      calWorkDetails += '<li><b>FV Contact Person Name:</b> ' + event.calObj.FVContactPersonName + '</li>';

      if (event.ApprovalStatus != null)
        calWorkDetails += '<li><b>Approval Status:</b> ' + event.ApprovalStatus + '</li>';

    } else if (this.pageTitle === 'VC School Visits') {
      calWorkDetails += '<li><b>Date:</b> ' + startTime + '</li>';
      calWorkDetails += '<li><b>School:</b> ' + event.calObj.SchoolName + '</li>';
      calWorkDetails += '<li><b>Sector:</b> ' + event.calObj.SectorName + '</li>';
      calWorkDetails += '<li><b>VT Report Submitted:</b> ' + event.calObj.VTReportSubmitted + '</li>';
      calWorkDetails += '<li><b>VT Working Days:</b> ' + event.calObj.VTWorkingDays + '</li>';
      calWorkDetails += '<li><b>VR Leave Days:</b> ' + event.calObj.VRLeaveDays + '</li>';

    } else if (this.pageTitle === 'VC Daily Reporting') {
      calWorkDetails += '<li><b>Date:</b> ' + startTime + '</li>';
      calWorkDetails += '<li><b>VC Name:</b> ' + event.calObj.VCName + '</li>';
      calWorkDetails += '<li><b>Report Type:</b> ' + event.calObj.ReportType + '</li>';

      if (event.calObj.WorkTypes != null)
        calWorkDetails += '<li><b>Work Types:</b> ' + event.calObj.WorkTypes + '</li>';

    } else if (this.pageTitle === 'VT Daily Reporting') {
      calWorkDetails += '<li><b>Date:</b> ' + startTime + '</li>';
      calWorkDetails += '<li><b>School:</b> ' + event.calObj.SchoolName + '</li>';
      calWorkDetails += '<li><b>Sector:</b> ' + event.calObj.SectorName + '</li>';
      calWorkDetails += '<li><b>Report Type:</b> ' + event.calObj.ReportType + '</li>';

      if (event.calObj.WorkTypes != null)
        calWorkDetails += '<li><b>Work Types:</b> ' + event.calObj.WorkTypes + '</li>';

    } else if (this.pageTitle === 'VT Guest Lecture Conducted') {
      calWorkDetails += '<li><b>Date:</b> ' + startTime + '</li>';
      calWorkDetails += '<li><b>Class Name:</b> ' + event.calObj.ClassName + '</li>';
      calWorkDetails += '<li><b>Guest Lecture Type:</b> ' + event.calObj.GLType + '</li>';
      calWorkDetails += '<li><b>Guest Lecture Topic:</b> ' + event.calObj.GLTopic + '</li>';

      if (event.calObj.GLName != null && event.calObj.GLName != '')
        calWorkDetails += '<li><b>Guest Lecturer:</b> ' + event.calObj.GLName + '</li>';

      if (event.calObj.ApprovalStatus != null)
        calWorkDetails += '<li><b>Approval Status:</b> ' + event.calObj.ApprovalStatus + '</li>';

    } else if (this.pageTitle === 'VT Issue Reporting') {
      calWorkDetails += '<li><b>Date:</b> ' + startTime + '</li>';
      calWorkDetails += '<li><b>Main Issue:</b> ' + event.calObj.MainIssue + '</li>';
      calWorkDetails += '<li><b>Sub Issue:</b> ' + event.calObj.SubIssue + '</li>';
      calWorkDetails += '<li><b>Student Type:</b> ' + event.calObj.StudentType + '</li>';

    } else if (this.pageTitle === 'VC Issue Reporting') {
      calWorkDetails += '<li><b>Date:</b> ' + startTime + '</li>';
      calWorkDetails += '<li><b>Main Issue:</b> ' + event.calObj.MainIssue + '</li>';
      calWorkDetails += '<li><b>Sub Issue:</b> ' + event.calObj.SubIssue + '</li>';
      calWorkDetails += '<li><b>Student Type:</b> ' + event.calObj.StudentType + '</li>';

    } else if (this.pageTitle === 'HM Issue Reporting') {
      calWorkDetails += '<li><b>Date:</b> ' + startTime + '</li>';
      calWorkDetails += '<li><b>Main Issue:</b> ' + event.calObj.MainIssue + '</li>';
      calWorkDetails += '<li><b>Sub Issue:</b> ' + event.calObj.SubIssue + '</li>';
      calWorkDetails += '<li><b>Student Type:</b> ' + event.calObj.StudentType + '</li>';

    } else if (this.pageTitle === 'VC School Visit Reporting') {
      calWorkDetails += '<li><b>Date:</b> ' + startTime + '</li>';
      calWorkDetails += '<li><b>School Name:</b> ' + event.calObj.SchoolName + '</li>';
      calWorkDetails += '<li><b>VT Name:</b> ' + event.calObj.VTName + '</li>';
      calWorkDetails += '<li><b>District Name:</b> ' + event.calObj.DistrictName + '</li>';
    }

    calWorkDetails += '</ul>';

    return calWorkDetails;
  }
}
