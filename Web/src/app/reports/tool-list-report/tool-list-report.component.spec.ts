import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ToolListReportComponent } from './tool-list-report.component';

describe('ToolListReportComponent', () => {
  let component: ToolListReportComponent;
  let fixture: ComponentFixture<ToolListReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ToolListReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ToolListReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
