import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { IonicModule } from '@ionic/angular';

import { VtFieldIndustryVisitConductedPageRoutingModule } from './vt-field-industry-visit-conducted-routing.module';

import { VtFieldIndustryVisitConductedPage } from './vt-field-industry-visit-conducted.page';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ReactiveFormsModule,
    VtFieldIndustryVisitConductedPageRoutingModule
  ],
  declarations: [VtFieldIndustryVisitConductedPage]
})
export class VtFieldIndustryVisitConductedPageModule {}
