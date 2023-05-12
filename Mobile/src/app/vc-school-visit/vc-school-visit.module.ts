import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { VcSchoolVisitPageRoutingModule } from './vc-school-visit-routing.module';

import { VcSchoolVisitPage } from './vc-school-visit.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    VcSchoolVisitPageRoutingModule
  ],
  declarations: [VcSchoolVisitPage]
})
export class VcSchoolVisitPageModule {}
