import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VTPTransferComponent } from './vtp-transfer.component';

describe('VtpTransferComponent', () => {
  let component: VTPTransferComponent;
  let fixture: ComponentFixture<VTPTransferComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VTPTransferComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VTPTransferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
