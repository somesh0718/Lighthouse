import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';
import { NgCalendarModule } from 'ionic2-calendar';

import { DRPDailyReportingPageRoutingModule } from './drp-daily-reporting-routing.module';

import { DRPDailyReportingPage } from './drp-daily-reporting.page';
import { CalModalPageModule } from '../calendar/cal-modal/cal-modal.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    IonicModule,
    DRPDailyReportingPageRoutingModule,
    NgCalendarModule,
    CalModalPageModule,
  ],
  declarations: [DRPDailyReportingPage]
})
export class DRPDailyReportingPageModule { }
