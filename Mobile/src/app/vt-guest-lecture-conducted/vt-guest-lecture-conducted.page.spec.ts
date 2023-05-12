import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { VtGuestLectureConductedPage } from './vt-guest-lecture-conducted.page';

describe('VtGuestLectureConductedPage', () => {
  let component: VtGuestLectureConductedPage;
  let fixture: ComponentFixture<VtGuestLectureConductedPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VtGuestLectureConductedPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(VtGuestLectureConductedPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
