import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { VtFieldIndustryVisitConductedPage } from './vt-field-industry-visit-conducted.page';

describe('VtFieldIndustryVisitConductedPage', () => {
  let component: VtFieldIndustryVisitConductedPage;
  let fixture: ComponentFixture<VtFieldIndustryVisitConductedPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VtFieldIndustryVisitConductedPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(VtFieldIndustryVisitConductedPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
