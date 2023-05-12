import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';
import { VtIssueReportingPageRoutingModule } from './vt-issue-reporting-routing.module';
import { VtIssueReportingPage } from './vt-issue-reporting.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    IonicModule,
    VtIssueReportingPageRoutingModule
  ],
  declarations: [VtIssueReportingPage]
})
export class VtIssueReportingPageModule { }
