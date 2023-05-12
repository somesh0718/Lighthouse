import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { VtIssueReportingPage } from './vt-issue-reporting.page';

const routes: Routes = [
  {
    path: '',
    component: VtIssueReportingPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VtIssueReportingPageRoutingModule {}
