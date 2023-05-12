import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';
import { VcIssueReportingPageRoutingModule } from './vc-issue-reporting-routing.module';
import { VcIssueReportingPage } from './vc-issue-reporting.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ReactiveFormsModule,
    VcIssueReportingPageRoutingModule
  ],
  declarations: [VcIssueReportingPage]
})
export class VcIssueReportingPageModule { }
