import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule,  } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { VcSchoolVisitReportingPageRoutingModule } from './vc-school-visit-reporting-routing.module';

import { VcSchoolVisitReportingPage } from './vc-school-visit-reporting.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    IonicModule,
    VcSchoolVisitReportingPageRoutingModule
  ],
  declarations: [VcSchoolVisitReportingPage]
})
export class VcSchoolVisitReportingPageModule {}
