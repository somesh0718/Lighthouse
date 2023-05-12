import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { VcIssueReportingPage } from './vc-issue-reporting.page';

const routes: Routes = [
  {
    path: '',
    component: VcIssueReportingPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VcIssueReportingPageRoutingModule {}
