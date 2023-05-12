import { NgModule, Injector, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';
import { DatePipe } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Guid } from 'guid-typescript';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { IonicStorageModule } from '@ionic/storage';
import { Geolocation } from '@ionic-native/geolocation/ngx';
import { SQLite } from '@ionic-native/sqlite/ngx';
import { NgCalendarModule } from 'ionic2-calendar';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { CalModalPageModule } from './calendar/cal-modal/cal-modal.module';

import { BaseService } from './services/base.service';
import { HttpService } from './services/http.service';
import { AuthenticationService } from './services/authentication.service';
import { VTIssueReportingService } from './vt-issue-reporting/vt-issue-reporting.service';
import { VCIssueReportingService } from './vc-issue-reporting/vc-issue-reporting.service';
import { HMIssueReportingService } from './hm-issue-reporting/hm-issue-reporting.service';
import { VCDailyReportingService } from './vc-daily-reporting/vc-daily-reporting.service';
import { VTDailyReportingService } from './vt-daily-reporting/vt-daily-reporting.service';
import { VTFieldIndustryVisitConductedService } from './vt-field-industry-visit-conducted/vt-field-industry-visit-conducted.service';
import { VTGuestLectureConductedService } from './vt-guest-lecture-conducted/vt-guest-lecture-conducted.service';
import { VCSchoolVisitService } from './vc-school-visit/vc-school-visit.service';
import { HelperService } from './services/helper.service';
import { BroadcastMessageService } from './broadcast-message/broadcast-message.service';
import { ComplaintRegistrationService } from './complaint-registration/complaint-registration.service';
import { DRPDailyReportingService } from './drp-daily-reportings/drp-daily-reporting.service';
import { VcSchoolVisitReportingService } from './vc-school-visit-reporting/vc-school-visit-reporting.service';

@NgModule({
  declarations: [AppComponent],
  entryComponents: [],
  imports: [
    BrowserModule,
    IonicModule.forRoot(),
    AppRoutingModule,
    IonicStorageModule.forRoot(),    
    HttpClientModule,    
    NgCalendarModule,
    CalModalPageModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [
    AuthenticationService,
    HelperService,
    VTIssueReportingService,
    VCIssueReportingService,
    HMIssueReportingService,
    VCDailyReportingService,
    VTFieldIndustryVisitConductedService,
    VTDailyReportingService,
    VTGuestLectureConductedService,
    VCSchoolVisitService,
    BroadcastMessageService,
    ComplaintRegistrationService,
    DRPDailyReportingService,
    VcSchoolVisitReportingService,
    BaseService,
    HttpService,
    StatusBar,
    SplashScreen,
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    DatePipe,
    Geolocation,
    SQLite
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent],
})
export class AppModule {
  static injector: Injector;

  constructor(injector: Injector) {
    AppModule.injector = injector;
  }
}
