import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { VcSchoolVisitPage } from './vc-school-visit.page';

const routes: Routes = [
  {
    path: '',
    component: VcSchoolVisitPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VcSchoolVisitPageRoutingModule {}
