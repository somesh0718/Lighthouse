import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { VtFieldIndustryVisitConductedPage } from './vt-field-industry-visit-conducted.page';

const routes: Routes = [
  {
    path: '',
    component: VtFieldIndustryVisitConductedPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VtFieldIndustryVisitConductedPageRoutingModule {}
