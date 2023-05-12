import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LabConditionComponent } from './lab-condition.component';

describe('LabConditionComponent', () => {
  let component: LabConditionComponent;
  let fixture: ComponentFixture<LabConditionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LabConditionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LabConditionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
