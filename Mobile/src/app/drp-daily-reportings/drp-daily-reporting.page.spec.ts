import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { DRPDailyReportingPage } from './drp-daily-reporting.page';

describe('DRPDailyReportingPage', () => {
  let component: DRPDailyReportingPage;
  let fixture: ComponentFixture<DRPDailyReportingPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DRPDailyReportingPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(DRPDailyReportingPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
