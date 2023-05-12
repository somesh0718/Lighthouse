import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { HmIssueReportingPage } from './hm-issue-reporting.page';

describe('HmIssueReportingPage', () => {
  let component: HmIssueReportingPage;
  let fixture: ComponentFixture<HmIssueReportingPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HmIssueReportingPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(HmIssueReportingPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
