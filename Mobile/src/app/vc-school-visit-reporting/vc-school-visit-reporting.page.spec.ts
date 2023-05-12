import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { VcSchoolVisitReportingPage } from './vc-school-visit-reporting.page';

describe('VcSchoolVisitReportingPage', () => {
  let component: VcSchoolVisitReportingPage;
  let fixture: ComponentFixture<VcSchoolVisitReportingPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VcSchoolVisitReportingPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(VcSchoolVisitReportingPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
