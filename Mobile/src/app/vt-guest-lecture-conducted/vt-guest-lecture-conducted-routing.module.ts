import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { VtGuestLectureConductedPage } from './vt-guest-lecture-conducted.page';

const routes: Routes = [
  {
    path: '',
    component: VtGuestLectureConductedPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VtGuestLectureConductedPageRoutingModule {}
