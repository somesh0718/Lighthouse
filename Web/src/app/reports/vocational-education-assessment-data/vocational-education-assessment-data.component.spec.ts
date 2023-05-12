import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VocationalEducationAssessmentDataComponent } from './vocational-education-assessment-data.component';

describe('VocationalEducationAssessmentDataComponent', () => {
  let component: VocationalEducationAssessmentDataComponent;
  let fixture: ComponentFixture<VocationalEducationAssessmentDataComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VocationalEducationAssessmentDataComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VocationalEducationAssessmentDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
