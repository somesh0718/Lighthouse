import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { VtDailyReportingPage } from './vt-daily-reporting.page';

const routes: Routes = [
  {
    path: '',
    component: VtDailyReportingPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VtDailyReportingPageRoutingModule {}
