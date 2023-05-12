import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { VtDailyReportingPageRoutingModule } from './vt-daily-reporting-routing.module';

import { VtDailyReportingPage } from './vt-daily-reporting.page';
import { FormGroup, FormControl, FormArray, FormBuilder } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    VtDailyReportingPageRoutingModule,
    ReactiveFormsModule
  ],
  declarations: [VtDailyReportingPage]
})
export class VtDailyReportingPageModule {}
