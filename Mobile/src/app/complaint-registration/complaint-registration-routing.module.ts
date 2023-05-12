import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ComplaintRegistrationPage } from './complaint-registration.page';

const routes: Routes = [
  {
    path: '',
    component: ComplaintRegistrationPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ComplaintRegistrationPageRoutingModule {}
