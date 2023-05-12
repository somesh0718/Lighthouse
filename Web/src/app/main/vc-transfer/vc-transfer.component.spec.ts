import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VCTransferComponent } from './vc-transfer.component';

describe('VCTransferComponent', () => {
  let component: VCTransferComponent;
  let fixture: ComponentFixture<VCTransferComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VCTransferComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VCTransferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
