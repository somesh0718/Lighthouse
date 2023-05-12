import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { VtIssueReportingPage } from './vt-issue-reporting.page';

describe('VtIssueReportingPage', () => {
  let component: VtIssueReportingPage;
  let fixture: ComponentFixture<VtIssueReportingPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VtIssueReportingPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(VtIssueReportingPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
