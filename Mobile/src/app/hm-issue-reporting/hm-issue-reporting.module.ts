import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';
import { HmIssueReportingPageRoutingModule } from './hm-issue-reporting-routing.module';
import { HmIssueReportingPage } from './hm-issue-reporting.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ReactiveFormsModule,
    HmIssueReportingPageRoutingModule
  ],
  declarations: [HmIssueReportingPage]
})
export class HmIssueReportingPageModule { }
