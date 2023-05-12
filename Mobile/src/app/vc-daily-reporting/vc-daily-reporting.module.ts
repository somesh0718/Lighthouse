import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';
import { NgCalendarModule } from 'ionic2-calendar';

import { VcDailyReportingPageRoutingModule } from './vc-daily-reporting-routing.module';

import { VcDailyReportingPage } from './vc-daily-reporting.page';
import { CalModalPageModule } from '../calendar/cal-modal/cal-modal.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    IonicModule,
    VcDailyReportingPageRoutingModule,
    NgCalendarModule,
    CalModalPageModule,
  ],
  declarations: [VcDailyReportingPage]
})
export class VcDailyReportingPageModule { }
