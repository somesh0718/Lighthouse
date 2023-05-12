import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HmIssueReportingPage } from './hm-issue-reporting.page';

const routes: Routes = [
  {
    path: '',
    component: HmIssueReportingPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HmIssueReportingPageRoutingModule {}
