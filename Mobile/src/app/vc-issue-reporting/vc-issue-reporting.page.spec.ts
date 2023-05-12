import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { VcIssueReportingPage } from './vc-issue-reporting.page';

describe('VcIssueReportingPage', () => {
  let component: VcIssueReportingPage;
  let fixture: ComponentFixture<VcIssueReportingPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [VcIssueReportingPage],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(VcIssueReportingPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
