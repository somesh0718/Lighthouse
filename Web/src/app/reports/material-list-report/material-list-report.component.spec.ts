import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MaterialListReportComponent } from './material-list-report.component';

describe('MaterialListReportComponent', () => {
  let component: MaterialListReportComponent;
  let fixture: ComponentFixture<MaterialListReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MaterialListReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MaterialListReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
