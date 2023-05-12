import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentClassAssesmentDetailsComponent } from './student-class-assesment-details.component';

describe('StudentClassAssesmentDetailsComponent', () => {
  let component: StudentClassAssesmentDetailsComponent;
  let fixture: ComponentFixture<StudentClassAssesmentDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StudentClassAssesmentDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentClassAssesmentDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
