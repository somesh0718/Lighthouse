import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { VtDailyReportingPage } from './vt-daily-reporting.page';

describe('VtDailyReportingPage', () => {
  let component: VtDailyReportingPage;
  let fixture: ComponentFixture<VtDailyReportingPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VtDailyReportingPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(VtDailyReportingPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
