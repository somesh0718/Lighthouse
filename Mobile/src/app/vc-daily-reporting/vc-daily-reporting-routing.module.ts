import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { VcDailyReportingPage } from './vc-daily-reporting.page';

const routes: Routes = [
  {
    path: '',
    component: VcDailyReportingPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VcDailyReportingPageRoutingModule {}
