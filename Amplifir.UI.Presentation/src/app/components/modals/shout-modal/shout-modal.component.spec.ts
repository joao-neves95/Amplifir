import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShoutModalComponent } from './shout-modal.component';

describe('ShoutModalComponent', () => {
  let component: ShoutModalComponent;
  let fixture: ComponentFixture<ShoutModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShoutModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShoutModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
