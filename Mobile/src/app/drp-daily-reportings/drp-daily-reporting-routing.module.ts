import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DRPDailyReportingPage } from './drp-daily-reporting.page';

const routes: Routes = [
  {
    path: '',
    component: DRPDailyReportingPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DRPDailyReportingPageRoutingModule {}
