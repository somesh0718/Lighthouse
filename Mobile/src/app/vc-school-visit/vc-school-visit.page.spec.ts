import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { VcSchoolVisitPage } from './vc-school-visit.page';

describe('VcSchoolVisitPage', () => {
  let component: VcSchoolVisitPage;
  let fixture: ComponentFixture<VcSchoolVisitPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VcSchoolVisitPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(VcSchoolVisitPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
