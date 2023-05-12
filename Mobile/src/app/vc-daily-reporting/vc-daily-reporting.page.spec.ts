import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { VcDailyReportingPage } from './vc-daily-reporting.page';

describe('VcDailyReportingPage', () => {
  let component: VcDailyReportingPage;
  let fixture: ComponentFixture<VcDailyReportingPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VcDailyReportingPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(VcDailyReportingPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
